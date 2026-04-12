using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using SistemaGestion.Forms;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.Json;

namespace SistemaGestion.Services;

/// <summary>
/// Actualizaciones por internet: lee update-config.json (manifestUrl), obtiene un JSON de versión y descarga el instalador.
/// </summary>
public static class AppUpdateService
{
    private const string ConfigFileName = "update-config.json";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        ReadCommentHandling = JsonCommentHandling.Skip,
        AllowTrailingCommas = true,
    };

    private static readonly HttpClient Http = CreateHttpClient();

    private static HttpClient CreateHttpClient()
    {
        var c = new HttpClient { Timeout = TimeSpan.FromMinutes(15) };
        c.DefaultRequestHeaders.UserAgent.ParseAdd("SistemaGestion-Update/1.0");
        c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return c;
    }

    public static Version GetCurrentAssemblyVersion()
    {
        var v = Assembly.GetExecutingAssembly().GetName().Version;
        return v ?? new Version(1, 0, 0, 0);
    }

    private static string ConfigPath =>
        Path.Combine(AppContext.BaseDirectory, ConfigFileName);

    /// <summary>
    /// Comprueba el manifiesto, descarga si hay versión nueva y ofrece ejecutar el instalador.
    /// </summary>
    public static async Task RunUpdateCheckAsync(Form owner)
    {
        if (!File.Exists(ConfigPath))
        {
            MessageBox.Show(
                owner,
                $"No se encontró el archivo de configuración de actualizaciones:\n{ConfigPath}\n\n"
                + "El proveedor del sistema debe incluir update-config.json junto al programa, "
                + "con la clave \"manifestUrl\" apuntando al archivo de versiones publicado en internet.",
                "Actualizar sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        UpdateConfigFile? cfg;
        try
        {
            await using var fs = File.OpenRead(ConfigPath);
            cfg = await JsonSerializer.DeserializeAsync<UpdateConfigFile>(fs, JsonOptions).ConfigureAwait(true);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                owner,
                "No se pudo leer update-config.json.\n\n" + ex.Message,
                "Actualizar sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (cfg is null || string.IsNullOrWhiteSpace(cfg.ManifestUrl))
        {
            MessageBox.Show(
                owner,
                "La URL del manifiesto de actualizaciones está vacía (manifestUrl en update-config.json).\n\n"
                + "El proveedor debe configurar esa dirección antes de habilitar las descargas automáticas.",
                "Actualizar sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        if (!Uri.TryCreate(cfg.ManifestUrl.Trim(), UriKind.Absolute, out var manifestUri))
        {
            MessageBox.Show(owner, "La URL del manifiesto no es válida.", "Actualizar sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            ValidateHttpsOrLocalhost(manifestUri, "manifiesto");
        }
        catch (Exception ex)
        {
            MessageBox.Show(owner, ex.Message, "Actualizar sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        UpdateManifestFile? manifest;
        try
        {
            manifest = await FetchManifestAsync(manifestUri).ConfigureAwait(true);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            MessageBox.Show(
                owner,
                "No se encontró el archivo de versiones en internet (error 404).\n\n"
                + "Eso suele indicar que el manifiesto todavía no está en GitHub, o que la URL no coincide con la rama/archivo.\n\n"
                + "Solución típica:\n"
                + "• Hacé commit y push del archivo updates/manifest.json en el repo, rama main.\n"
                + "• Esperá un minuto y probá de nuevo (GitHub puede tardar un poco).\n\n"
                + "URL consultada:\n" + manifestUri,
                "Actualizar sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                owner,
                "No se pudo obtener la información de la última versión. Compruebe su conexión a internet.\n\n" + ex.Message,
                "Actualizar sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (manifest is null || string.IsNullOrWhiteSpace(manifest.Version) || string.IsNullOrWhiteSpace(manifest.DownloadUrl))
        {
            MessageBox.Show(
                owner,
                "El manifiesto en el servidor no tiene el formato esperado (faltan version o downloadUrl).",
                "Actualizar sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (!TryParseVersionString(manifest.Version, out var remoteVersion))
        {
            MessageBox.Show(
                owner,
                $"La versión indicada en el servidor no es válida: \"{manifest.Version}\".",
                "Actualizar sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        if (!Uri.TryCreate(manifest.DownloadUrl.Trim(), UriKind.Absolute, out var downloadUri))
        {
            MessageBox.Show(owner, "La URL de descarga del instalador no es válida.", "Actualizar sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            ValidateHttpsOrLocalhost(downloadUri, "descarga");
        }
        catch (Exception ex)
        {
            MessageBox.Show(owner, ex.Message, "Actualizar sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var current = GetCurrentAssemblyVersion();
        if (remoteVersion <= current)
        {
            MessageBox.Show(
                owner,
                $"Su sistema está al día.\n\nVersión instalada: {FormatVersion(current)}\nÚltima versión publicada: {FormatVersion(remoteVersion)}",
                "Actualizar sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        var notas = string.IsNullOrWhiteSpace(manifest.ReleaseNotes)
            ? ""
            : "\n\nNotas de la versión:\n" + manifest.ReleaseNotes.Trim();

        var descargar = MessageBox.Show(
            owner,
            $"Hay una nueva versión disponible.\n\n"
            + $"Versión instalada: {FormatVersion(current)}\n"
            + $"Nueva versión: {FormatVersion(remoteVersion)}"
            + notas
            + "\n\n¿Descargar el instalador ahora?",
            "Actualizar sistema",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (descargar != DialogResult.Yes)
            return;

        var tempPath = Path.Combine(
            Path.GetTempPath(),
            $"SistemaGestion_Instalador_{remoteVersion.Major}_{remoteVersion.Minor}_{remoteVersion.Build}.exe");

        try
        {
            if (File.Exists(tempPath))
                File.Delete(tempPath);
        }
        catch
        {
            // ignorar si no se puede borrar un archivo previo
        }

        using var downloadForm = new UpdateDownloadForm(manifest.DownloadUrl.Trim(), tempPath);

        if (downloadForm.ShowDialog(owner) != DialogResult.OK)
            return;

        if (!string.IsNullOrWhiteSpace(manifest.Sha256))
        {
            try
            {
                await VerifySha256Async(tempPath, manifest.Sha256).ConfigureAwait(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    owner,
                    "La descarga no coincide con la firma de seguridad esperada. No instale este archivo.\n\n" + ex.Message,
                    "Actualizar sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                try { File.Delete(tempPath); } catch { /* ignore */ }
                return;
            }
        }

        var instalar = MessageBox.Show(
            owner,
            "La descarga finalizó correctamente.\n\n"
            + "Se cerrará este programa y se aplicará la actualización en segundo plano "
            + "(sin ventana del instalador). Guarde cualquier trabajo pendiente.\n\n"
            + "Al terminar, abra de nuevo el sistema desde el acceso directo o el menú Inicio.\n\n"
            + "¿Continuar?",
            "Actualizar sistema",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (instalar != DialogResult.Yes)
        {
            MessageBox.Show(
                owner,
                $"Instalador guardado en:\n{tempPath}\n\nEjecute ese archivo cuando quiera actualizar.",
                "Actualizar sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        // Inno Setup: instalación silenciosa (sin asistente). Ver https://jrsoftware.org/ishelp/topic_setupcmdline.htm
        const string silentInnoArgs = "/VERYSILENT /NORESTART /SUPPRESSMSGBOXES /CLOSEAPPLICATIONS /SP-";

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = tempPath,
                Arguments = silentInnoArgs,
                UseShellExecute = true,
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                owner,
                "No se pudo iniciar el instalador.\n\n" + ex.Message + $"\n\nArchivo: {tempPath}",
                "Actualizar sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        Application.Exit();
    }

    public static async Task DownloadInstallerAsync(
        string downloadUrl,
        string destinationPath,
        IProgress<(long Read, long? Total)>? progress,
        CancellationToken cancellationToken)
    {
        using var response = await Http.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
            .ConfigureAwait(false);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new HttpRequestException(
                "El instalador no está en el servidor (error 404). "
                + "El proveedor debe publicar un Release en GitHub con el archivo adjunto y el mismo nombre que indica el manifiesto.\n\n"
                + downloadUrl,
                null,
                HttpStatusCode.NotFound);
        }

        response.EnsureSuccessStatusCode();
        var total = response.Content.Headers.ContentLength;
        await using var source = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
        await using var dest = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 81920, FileOptions.Asynchronous);

        progress?.Report((0, total));

        var buffer = new byte[81920];
        long read = 0;
        while (true)
        {
            var n = await source.ReadAsync(buffer.AsMemory(0, buffer.Length), cancellationToken).ConfigureAwait(false);
            if (n == 0)
                break;
            await dest.WriteAsync(buffer.AsMemory(0, n), cancellationToken).ConfigureAwait(false);
            read += n;
            progress?.Report((read, total > 0 ? total : null));
        }

        progress?.Report((read, total > 0 ? total : read));
    }

    private static async Task<UpdateManifestFile?> FetchManifestAsync(Uri manifestUri)
    {
        using var response = await Http.GetAsync(manifestUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw new HttpRequestException(
                response.ReasonPhrase,
                null,
                HttpStatusCode.NotFound);
        }

        response.EnsureSuccessStatusCode();
        await using var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        return await JsonSerializer.DeserializeAsync<UpdateManifestFile>(stream, JsonOptions).ConfigureAwait(false);
    }

    private static async Task VerifySha256Async(string filePath, string expectedHex)
    {
        var expected = NormalizeHex(expectedHex);
        if (expected.Length == 0)
            return;

        await using var fs = File.OpenRead(filePath);
        var hash = await SHA256.HashDataAsync(fs).ConfigureAwait(false);
        var actual = Convert.ToHexString(hash);
        if (!string.Equals(actual, expected, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("El archivo descargado no coincide con el hash SHA256 publicado.");
    }

    private static string NormalizeHex(string hex)
    {
        var s = hex.Trim().Replace(" ", "", StringComparison.Ordinal);
        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            s = s[2..];
        return s;
    }

    private static void ValidateHttpsOrLocalhost(Uri uri, string descripcion)
    {
        if (uri.Scheme == Uri.UriSchemeHttps)
            return;
        if (uri.Scheme == Uri.UriSchemeHttp &&
            (uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase) || uri.Host == "127.0.0.1"))
            return;
        throw new InvalidOperationException(
            $"Por seguridad, la URL de {descripcion} debe usar HTTPS (se permite HTTP solo en localhost para pruebas).");
    }

    private static bool TryParseVersionString(string text, out Version version)
    {
        var s = text.Trim();
        if (s.Length > 0 && (s[0] == 'v' || s[0] == 'V'))
            s = s[1..];
        return Version.TryParse(s, out version!);
    }

    private static string FormatVersion(Version v) =>
        v.Revision > 0 ? v.ToString() : $"{v.Major}.{v.Minor}.{v.Build}";

    private sealed class UpdateConfigFile
    {
        public string? ManifestUrl { get; set; }
    }

    private sealed class UpdateManifestFile
    {
        public string? Version { get; set; }
        public string? DownloadUrl { get; set; }
        public string? Sha256 { get; set; }
        public string? ReleaseNotes { get; set; }
    }
}
