using Microsoft.Data.Sqlite;
using SistemaGestion.Data;
using SistemaGestion.Models;

namespace SistemaGestion.Services;

public class VentaService
{
    private static int ObtenerOCrearClienteId(SqliteConnection conn, SqliteTransaction tx, string nombreCliente)
    {
        using (var cmd = conn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = "SELECT Id FROM Clientes WHERE Nombre = $n LIMIT 1;";
            cmd.Parameters.AddWithValue("$n", nombreCliente);
            var existe = cmd.ExecuteScalar();
            if (existe != null)
                return Convert.ToInt32(existe);
        }

        using (var cmd = conn.CreateCommand())
        {
            cmd.Transaction = tx;
            cmd.CommandText = """
                INSERT INTO Clientes (Nombre, Pais) VALUES ($n, 'Argentina') RETURNING Id;
                """;
            cmd.Parameters.AddWithValue("$n", nombreCliente);
            return Convert.ToInt32(cmd.ExecuteScalar()!);
        }
    }

    public int GuardarVenta(string nombreCliente, string formaPago, IReadOnlyList<DetalleVenta> items)
    {
        if (items.Count == 0)
            throw new InvalidOperationException("No hay ítems para guardar.");

        try
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var tx = conn.BeginTransaction();

            int clienteId = ObtenerOCrearClienteId(conn, tx, nombreCliente);

            decimal total = items.Sum(i => i.Subtotal);

            int ventaId;
            using (var cmd = conn.CreateCommand())
            {
                cmd.Transaction = tx;
                cmd.CommandText = """
                    INSERT INTO Ventas (ClienteId, Total, FormaPago, Estado)
                    VALUES ($cid, $tot, $fp, 'Pendiente') RETURNING Id;
                    """;
                cmd.Parameters.AddWithValue("$cid", clienteId);
                cmd.Parameters.AddWithValue("$tot", total);
                cmd.Parameters.AddWithValue("$fp", formaPago);
                ventaId = Convert.ToInt32(cmd.ExecuteScalar()!);
            }

            foreach (var d in items)
            {
                using var cmd = conn.CreateCommand();
                cmd.Transaction = tx;
                cmd.CommandText = """
                    INSERT INTO DetallesVenta (VentaId, Producto, Cantidad, PrecioUnitario, Subtotal)
                    VALUES ($vid, $prod, $cant, $pu, $sub);
                    """;
                cmd.Parameters.AddWithValue("$vid", ventaId);
                cmd.Parameters.AddWithValue("$prod", d.Producto);
                cmd.Parameters.AddWithValue("$cant", d.Cantidad);
                cmd.Parameters.AddWithValue("$pu", d.PrecioUnitario);
                cmd.Parameters.AddWithValue("$sub", d.Subtotal);
                cmd.ExecuteNonQuery();
            }

            tx.Commit();
            return ventaId;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al guardar la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            throw;
        }
    }

    public int ContarVentasDelDia()
    {
        try
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                SELECT COUNT(*) FROM Ventas
                WHERE date(FechaVenta) = date('now', 'localtime');
                """;
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al consultar ventas del día: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return 0;
        }
    }

    public IReadOnlyList<Venta> ListarVentas(
        DateTime? desde,
        DateTime? hasta,
        string? formaPago,
        string? estado)
    {
        var list = new List<Venta>();
        try
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            var sql = """
                SELECT v.Id, v.FechaVenta, c.Nombre, v.Total, v.FormaPago, v.Estado
                FROM Ventas v
                LEFT JOIN Clientes c ON c.Id = v.ClienteId
                WHERE 1=1
                """;

            if (desde.HasValue)
            {
                sql += " AND date(v.FechaVenta) >= date($desde)";
                cmd.Parameters.AddWithValue("$desde", desde.Value.ToString("yyyy-MM-dd"));
            }

            if (hasta.HasValue)
            {
                sql += " AND date(v.FechaVenta) <= date($hasta)";
                cmd.Parameters.AddWithValue("$hasta", hasta.Value.ToString("yyyy-MM-dd"));
            }

            if (!string.IsNullOrWhiteSpace(formaPago) && formaPago != "(Todas)")
            {
                sql += " AND v.FormaPago = $fp";
                cmd.Parameters.AddWithValue("$fp", formaPago);
            }

            if (!string.IsNullOrWhiteSpace(estado) && estado != "(Todos)")
            {
                sql += " AND v.Estado = $est";
                cmd.Parameters.AddWithValue("$est", estado);
            }

            sql += " ORDER BY v.FechaVenta DESC, v.Id DESC;";
            cmd.CommandText = sql;

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Venta
                {
                    Id = r.GetInt32(0),
                    FechaVenta = r.IsDBNull(1) ? null : r.GetString(1),
                    ClienteNombre = r.IsDBNull(2) ? null : r.GetString(2),
                    Total = r.GetDecimal(3),
                    FormaPago = r.GetString(4),
                    Estado = r.GetString(5),
                });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al listar ventas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return list;
    }

    public Venta? ObtenerVenta(int ventaId)
    {
        try
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                SELECT v.Id, v.ClienteId, v.FechaVenta, v.Total, v.FormaPago, v.Estado, v.Observaciones, c.Nombre
                FROM Ventas v
                LEFT JOIN Clientes c ON c.Id = v.ClienteId
                WHERE v.Id = $id;
                """;
            cmd.Parameters.AddWithValue("$id", ventaId);
            using var r = cmd.ExecuteReader();
            if (!r.Read())
                return null;

            return new Venta
            {
                Id = r.GetInt32(0),
                ClienteId = r.IsDBNull(1) ? null : r.GetInt32(1),
                FechaVenta = r.IsDBNull(2) ? null : r.GetString(2),
                Total = r.GetDecimal(3),
                FormaPago = r.GetString(4),
                Estado = r.GetString(5),
                Observaciones = r.IsDBNull(6) ? null : r.GetString(6),
                ClienteNombre = r.IsDBNull(7) ? null : r.GetString(7),
            };
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al obtener la venta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    public IReadOnlyList<DetalleVenta> ObtenerDetalles(int ventaId)
    {
        var list = new List<DetalleVenta>();
        try
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = """
                SELECT Id, VentaId, Producto, Cantidad, PrecioUnitario, Subtotal
                FROM DetallesVenta WHERE VentaId = $id ORDER BY Id;
                """;
            cmd.Parameters.AddWithValue("$id", ventaId);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new DetalleVenta
                {
                    Id = r.GetInt32(0),
                    VentaId = r.GetInt32(1),
                    Producto = r.GetString(2),
                    Cantidad = r.GetInt32(3),
                    PrecioUnitario = r.GetDecimal(4),
                    Subtotal = r.GetDecimal(5),
                });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al obtener detalles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return list;
    }

    public bool ActualizarEstado(int ventaId, string nuevoEstado)
    {
        try
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Ventas SET Estado = $e WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$e", nuevoEstado);
            cmd.Parameters.AddWithValue("$id", ventaId);
            return cmd.ExecuteNonQuery() > 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al actualizar estado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    public IReadOnlyList<Socio> ListarSocios()
    {
        var list = new List<Socio>();
        try
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Nombre, Rol, Porcentaje FROM Socios ORDER BY Id;";
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new Socio
                {
                    Id = r.GetInt32(0),
                    Nombre = r.GetString(1),
                    Rol = r.GetString(2),
                    Porcentaje = r.GetDecimal(3),
                });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al listar socios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return list;
    }
}
