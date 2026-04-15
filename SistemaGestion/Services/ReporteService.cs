using SistemaGestion.Data;

namespace SistemaGestion.Services;

public class ReporteResumen
{
    public decimal TotalVendidoMes { get; set; }
    public string FormaPagoMasUsada { get; set; } = "-";
    public int CantidadFormaPagoMasUsada { get; set; }
}

public class ReporteService
{
    public ReporteResumen ObtenerResumenMesActual()
    {
        var res = new ReporteResumen();
        var now = DateTime.Now;
        var inicio = new DateTime(now.Year, now.Month, 1);
        var fin = inicio.AddMonths(1).AddDays(-1);
        var inicioStr = inicio.ToString("yyyy-MM-dd");
        var finStr = fin.ToString("yyyy-MM-dd");

        try
        {
            using var conn = DatabaseHelper.GetConnection();
            conn.Open();

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = """
                    SELECT IFNULL(SUM(Total), 0) FROM Ventas
                    WHERE date(FechaVenta, 'localtime') >= date($ini) AND date(FechaVenta, 'localtime') <= date($fin)
                    AND Estado IN ('FINALIZADO', 'Pendiente', 'En Curso', 'Entregado');
                    """;
                cmd.Parameters.AddWithValue("$ini", inicioStr);
                cmd.Parameters.AddWithValue("$fin", finStr);
                res.TotalVendidoMes = Convert.ToDecimal(cmd.ExecuteScalar());
            }

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = """
                    SELECT FormaPago, COUNT(*) AS c FROM Ventas
                    WHERE date(FechaVenta, 'localtime') >= date($ini) AND date(FechaVenta, 'localtime') <= date($fin)
                    AND Estado IN ('FINALIZADO', 'Pendiente', 'En Curso', 'Entregado')
                    GROUP BY FormaPago ORDER BY c DESC LIMIT 1;
                    """;
                cmd.Parameters.AddWithValue("$ini", inicioStr);
                cmd.Parameters.AddWithValue("$fin", finStr);
                using var r = cmd.ExecuteReader();
                if (r.Read())
                {
                    res.FormaPagoMasUsada = r.GetString(0);
                    res.CantidadFormaPagoMasUsada = r.GetInt32(1);
                }
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al calcular estadísticas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return res;
    }
}
