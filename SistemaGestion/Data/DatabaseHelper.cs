using Microsoft.Data.Sqlite;

namespace SistemaGestion.Data;

public static class DatabaseHelper
{
    private const string DataFolderName = "SistemaGestion";
    private const string DbFileName = "gestion.db";

    /// <summary>
    /// Base de datos por usuario en LocalAppData (no se toca al reinstalar el programa en Archivos de programa).
    /// Si existía copia en ProgramData (versiones anteriores), se copia una vez al nuevo lugar.
    /// </summary>
    private static readonly Lazy<string> _databasePath = new(ResolveDatabasePath);

    public static string DatabasePath => _databasePath.Value;

    private static string ResolveDatabasePath()
    {
        string localRoot = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string localFolder = Path.Combine(localRoot, DataFolderName);
        Directory.CreateDirectory(localFolder);
        string localDb = Path.Combine(localFolder, DbFileName);

        if (File.Exists(localDb))
            return localDb;

        string legacyFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            DataFolderName);
        string legacyDb = Path.Combine(legacyFolder, DbFileName);

        if (!File.Exists(legacyDb))
            return localDb;

        try
        {
            File.Copy(legacyDb, localDb, overwrite: false);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "Los datos anteriores están en la carpeta compartida del sistema y no se pudieron copiar al perfil de usuario. "
                + "Se seguirá usando esa ubicación para no perder información.\n\n"
                + ex.Message,
                "Base de datos",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return legacyDb;
        }

        return localDb;
    }

    public static SqliteConnection GetConnection()
    {
        return new SqliteConnection($"Data Source={DatabasePath};");
    }

    public static void InitializeDatabase()
    {
        try
        {
            using var conn = GetConnection();
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "PRAGMA foreign_keys = ON;";
                cmd.ExecuteNonQuery();
            }

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = """
                    CREATE TABLE IF NOT EXISTS Clientes (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nombre TEXT NOT NULL,
                        Email TEXT,
                        Telefono TEXT,
                        Pais TEXT DEFAULT 'Argentina',
                        FechaAlta TEXT DEFAULT (datetime('now'))
                    );
                    """;
                cmd.ExecuteNonQuery();
            }

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = """
                    CREATE TABLE IF NOT EXISTS Servicios (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nombre TEXT NOT NULL,
                        Categoria TEXT NOT NULL,
                        PrecioMin REAL NOT NULL,
                        PrecioMax REAL NOT NULL,
                        Descripcion TEXT
                    );
                    """;
                cmd.ExecuteNonQuery();
            }

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = """
                    CREATE TABLE IF NOT EXISTS Ventas (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        ClienteId INTEGER,
                        FechaVenta TEXT DEFAULT (datetime('now')),
                        Total REAL NOT NULL,
                        FormaPago TEXT NOT NULL,
                        Estado TEXT DEFAULT 'Pendiente',
                        Observaciones TEXT,
                        FOREIGN KEY (ClienteId) REFERENCES Clientes(Id)
                    );
                    """;
                cmd.ExecuteNonQuery();
            }

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = """
                    CREATE TABLE IF NOT EXISTS DetallesVenta (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        VentaId INTEGER NOT NULL,
                        Producto TEXT NOT NULL,
                        Cantidad INTEGER NOT NULL DEFAULT 1,
                        PrecioUnitario REAL NOT NULL,
                        Subtotal REAL NOT NULL,
                        FOREIGN KEY (VentaId) REFERENCES Ventas(Id)
                    );
                    """;
                cmd.ExecuteNonQuery();
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error al inicializar la base de datos: {ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            throw;
        }
    }
}
