using SistemaGestion.Services;

namespace SistemaGestion.Forms;

public partial class UpdateDownloadForm : Form
{
    private readonly string _downloadUrl;
    private readonly string _destinationPath;
    private CancellationTokenSource? _cts;

    public UpdateDownloadForm(string downloadUrl, string destinationPath)
    {
        _downloadUrl = downloadUrl;
        _destinationPath = destinationPath;
        InitializeComponent();
    }

    private async void UpdateDownloadForm_Shown(object? sender, EventArgs e)
    {
        _cts = new CancellationTokenSource();
        var token = _cts.Token;

        var progress = new Progress<(long Read, long? Total)>(report =>
        {
            if (report.Total is long total && total > 0)
            {
                progressBar.Style = ProgressBarStyle.Continuous;
                progressBar.Maximum = 100;
                var pct = (int)Math.Min(100, report.Read * 100 / total);
                progressBar.Value = pct;
                lblEstado.Text = $"Descargando… {pct}% ({Megas(report.Read)} / {Megas(total)} MB)";
            }
            else
            {
                progressBar.Style = ProgressBarStyle.Marquee;
                lblEstado.Text = $"Descargando… {Megas(report.Read)} MB";
            }
        });

        try
        {
            await AppUpdateService.DownloadInstallerAsync(_downloadUrl, _destinationPath, progress, token).ConfigureAwait(true);
            DialogResult = DialogResult.OK;
        }
        catch (OperationCanceledException)
        {
            DialogResult = DialogResult.Cancel;
            try { if (File.Exists(_destinationPath)) File.Delete(_destinationPath); } catch { /* ignore */ }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Error al descargar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.Abort;
            try { if (File.Exists(_destinationPath)) File.Delete(_destinationPath); } catch { /* ignore */ }
        }
        finally
        {
            Close();
        }
    }

    private static string Megas(long bytes) =>
        (bytes / (1024.0 * 1024.0)).ToString("0.0", System.Globalization.CultureInfo.CurrentCulture);

    private void BtnCancelar_Click(object? sender, EventArgs e) => _cts?.Cancel();
}
