using Microsoft.Data.Sqlite;

namespace SistemaGestion.Data;

public static class DatabaseHelper
{
    public static string DatabasePath =>
        Path.Combine(AppContext.BaseDirectory, "gestion.db");

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

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = """
                    CREATE TABLE IF NOT EXISTS Socios (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nombre TEXT NOT NULL,
                        Rol TEXT NOT NULL,
                        Porcentaje REAL DEFAULT 33.33
                    );
                    """;
                cmd.ExecuteNonQuery();
            }

            int sociosCount;
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM Socios;";
                sociosCount = Convert.ToInt32(cmd.ExecuteScalar());
            }

            if (sociosCount == 0)
            {
                using var insert = conn.CreateCommand();
                insert.CommandText = """
                    INSERT INTO Socios (Nombre, Rol, Porcentaje) VALUES
                    ('Socio 1', 'Especialista en IA', 33.33),
                    ('Socio 2', 'Desarrollador Full Stack', 33.33),
                    ('Socio 3', 'Marketing y Comercial', 33.33);
                    """;
                insert.ExecuteNonQuery();
            }

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = """
                    UPDATE Ventas SET Estado = 'FINALIZADO'
                    WHERE Estado = 'Pendiente';
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
