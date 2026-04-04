using SistemaGestion.Data;

namespace SistemaGestion.Services;

public class ReporteResumen
{
    public decimal TotalVendidoMes { get; set; }
    public int ProyectosCerradosMes { get; set; }
    public string FormaPagoMasUsada { get; set; } = "-";
    public int CantidadFormaPagoMasUsada { get; set; }
    public decimal ProyeccionMes { get; set; }
    public IReadOnlyDictionary<string, decimal> GananciaPorSocio { get; set; } =
        new Dictionary<string, decimal>();
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
                    WHERE date(FechaVenta) >= date($ini) AND date(FechaVenta) <= date($fin)
                    AND Estado IN ('Pendiente', 'En Curso', 'Entregado');
                    """;
                cmd.Parameters.AddWithValue("$ini", inicioStr);
                cmd.Parameters.AddWithValue("$fin", finStr);
                res.TotalVendidoMes = Convert.ToDecimal(cmd.ExecuteScalar());
            }

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = """
                    SELECT COUNT(*) FROM Ventas
                    WHERE date(FechaVenta) >= date($ini) AND date(FechaVenta) <= date($fin)
                    AND Estado = 'Entregado';
                    """;
                cmd.Parameters.AddWithValue("$ini", inicioStr);
                cmd.Parameters.AddWithValue("$fin", finStr);
                res.ProyectosCerradosMes = Convert.ToInt32(cmd.ExecuteScalar());
            }

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = """
                    SELECT FormaPago, COUNT(*) AS c FROM Ventas
                    WHERE date(FechaVenta) >= date($ini) AND date(FechaVenta) <= date($fin)
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

            int dayOfMonth = now.Day;
            int daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            if (dayOfMonth > 0 && res.TotalVendidoMes > 0)
            {
                decimal promedioDiario = res.TotalVendidoMes / dayOfMonth;
                res.ProyeccionMes = promedioDiario * daysInMonth;
            }
            else
            {
                res.ProyeccionMes = 0;
            }

            var socios = new List<(string Nombre, decimal Pct)>();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT Nombre, Porcentaje FROM Socios ORDER BY Id;";
                using var r = cmd.ExecuteReader();
                while (r.Read())
                    socios.Add((r.GetString(0), r.GetDecimal(1)));
            }

            var dict = new Dictionary<string, decimal>();
            foreach (var (nombre, pct) in socios)
                dict[nombre] = Math.Round(res.TotalVendidoMes * (pct / 100m), 2);
            res.GananciaPorSocio = dict;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al calcular estadísticas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return res;
    }
}
