using SistemaGestion.Helpers;
using SistemaGestion.Services;

namespace SistemaGestion.Forms;

public partial class EstadisticasForm : Form
{
    private readonly ReporteService _reporteService = new();

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
        lblProyectosCerrados.Text = $"Proyectos cerrados (Entregado): {r.ProyectosCerradosMes}";
        lblFormaPago.Text = r.CantidadFormaPagoMasUsada > 0
            ? $"Forma de pago más utilizada: {r.FormaPagoMasUsada} ({r.CantidadFormaPagoMasUsada} ventas)"
            : "Forma de pago más utilizada: —";
        lblProyeccion.Text = "Proyección del mes ($): " + r.ProyeccionMes.ToString("C2", CurrencyFormat.Pesos);

        flowSocios.Controls.Clear();
        foreach (var kv in r.GananciaPorSocio)
        {
            var lbl = new Label
            {
                AutoSize = true,
                Margin = new Padding(0, 2, 0, 2),
                Text = $"{kv.Key}: {kv.Value.ToString("C2", CurrencyFormat.Pesos)}",
            };
            flowSocios.Controls.Add(lbl);
        }
    }
}
