using SistemaGestion.Data;
using SistemaGestion.Helpers;
using SistemaGestion.Services;
using System.Text;

namespace SistemaGestion.Forms;

public partial class EstadisticasForm : Form
{
    private readonly ReporteService _reporteService = new();
    private readonly VentaService _ventaService = new();

    public EstadisticasForm()
    {
        InitializeComponent();
        Cargar();
    }

    private void Cargar()
    {
        var now = DateTime.Now;
        lblMes.Text = $"Mes en curso: {now:MMMM yyyy}";

        var r = _reporteService.ObtenerResumenMesActual();

        lblTotalMes.Text = "Total vendido ($): " + r.TotalVendidoMes.ToString("C2", CurrencyFormat.Pesos);
        lblFormaPago.Text = r.CantidadFormaPagoMasUsada > 0
            ? $"Forma de pago más utilizada: {r.FormaPagoMasUsada} ({r.CantidadFormaPagoMasUsada} ventas)"
            : "Forma de pago más utilizada: —";
    }

    private void BtnExportarMes_Click(object? sender, EventArgs e)
    {
        var now = DateTime.Now;
        var inicio = new DateTime(now.Year, now.Month, 1);
        var fin = inicio.AddMonths(1).AddDays(-1);

        using var sfd = new SaveFileDialog
        {
            Filter = "CSV (*.csv)|*.csv",
            FileName = $"historial_mes_{now:yyyyMM}.csv",
        };
        if (sfd.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var lista = _ventaService.ListarVentas(inicio, fin, "(Todas)", "(Todos)");
            var sb = new StringBuilder();
            sb.AppendLine("ID;Fecha;Cliente;Total ($);Forma de Pago;Estado");
            foreach (var v in lista)
            {
                sb.AppendLine(string.Join(";",
                    EscapeCsv(v.Id),
                    EscapeCsv(v.FechaVenta),
                    EscapeCsv(v.ClienteNombre),
                    v.Total.ToString("F2", System.Globalization.CultureInfo.InvariantCulture),
                    EscapeCsv(v.FormaPago),
                    EscapeCsv(v.Estado)));
            }

            var utf8WithBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
            File.WriteAllText(sfd.FileName, sb.ToString(), utf8WithBom);
            MessageBox.Show("Archivo exportado correctamente.", "Exportar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al exportar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static string EscapeCsv(object? value)
    {
        var s = value?.ToString() ?? "";
        if (s.Contains(';') || s.Contains('"') || s.Contains('\n'))
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        return s;
    }

    private void btnBackup_Click(object sender, EventArgs e)
    {
        try
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Base de datos (*.db)|*.db";
                dialog.FileName = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.db";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(DatabaseHelper.DatabasePath, dialog.FileName, true);
                    MessageBox.Show("Backup exportado correctamente");
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al exportar backup: " + ex.Message);
        }
    }
}
