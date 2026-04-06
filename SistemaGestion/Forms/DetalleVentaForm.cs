using SistemaGestion.Helpers;
using SistemaGestion.Models;
using SistemaGestion.Services;

namespace SistemaGestion.Forms;

public partial class DetalleVentaForm : Form
{
    private readonly int _ventaId;
    private readonly VentaService _ventaService = new();

    public DetalleVentaForm(int ventaId)
    {
        _ventaId = ventaId;
        InitializeComponent();
        ConfigurarGrid();
        cmbEstado.Items.AddRange(new object[] { "FINALIZADO", "En Curso", "Entregado", "Cancelado", "Pendiente" });
        CargarDatos();
    }

    private void ConfigurarGrid()
    {
        dgvDetalle.Columns.Clear();
        dgvDetalle.AutoGenerateColumns = false;
        dgvDetalle.EnableHeadersVisualStyles = false;
        dgvDetalle.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
        dgvDetalle.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvDetalle.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Producto", HeaderText = "Producto/Servicio", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cantidad", HeaderText = "Cantidad", Width = 80 });
        var colPu = new DataGridViewTextBoxColumn { Name = "PrecioUnit", HeaderText = "Precio Unit.", Width = 110 };
        colPu.DefaultCellStyle.Format = "C2";
        colPu.DefaultCellStyle.FormatProvider = CurrencyFormat.Pesos;
        dgvDetalle.Columns.Add(colPu);
        var colSub = new DataGridViewTextBoxColumn { Name = "Subtotal", HeaderText = "Subtotal", Width = 110 };
        colSub.DefaultCellStyle.Format = "C2";
        colSub.DefaultCellStyle.FormatProvider = CurrencyFormat.Pesos;
        dgvDetalle.Columns.Add(colSub);
    }

    private void CargarDatos()
    {
        var venta = _ventaService.ObtenerVenta(_ventaId);
        if (venta is null)
        {
            MessageBox.Show("No se encontró la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Close();
            return;
        }

        lblFecha.Text = "Fecha: " + (venta.FechaVenta ?? "—");
        lblCliente.Text = "Cliente: " + (venta.ClienteNombre ?? "—");
        lblFormaPagoHdr.Text = "Forma de pago: " + venta.FormaPago;
        lblEstadoHdr.Text = "Estado: " + venta.Estado;

        lblTotalValor.Text = venta.Total.ToString("C2", CurrencyFormat.Pesos);
        cmbEstado.SelectedItem = venta.Estado;
        if (cmbEstado.SelectedIndex < 0)
            cmbEstado.SelectedIndex = 0;

        dgvDetalle.Rows.Clear();
        foreach (var d in _ventaService.ObtenerDetalles(_ventaId))
        {
            dgvDetalle.Rows.Add(d.Producto, d.Cantidad, d.PrecioUnitario, d.Subtotal);
        }

        flowSocios.Controls.Clear();
        foreach (var s in _ventaService.ListarSocios())
        {
            decimal monto = Math.Round(venta.Total * (s.Porcentaje / 100m), 2);
            var lbl = new Label
            {
                AutoSize = true,
                Margin = new Padding(0, 2, 0, 2),
                Text = $"{s.Nombre} ({s.Rol}): {monto.ToString("C2", CurrencyFormat.Pesos)} ({s.Porcentaje}%)",
            };
            flowSocios.Controls.Add(lbl);
        }
    }

    private void BtnActualizarEstado_Click(object? sender, EventArgs e)
    {
        if (cmbEstado.SelectedItem is not string nuevo || string.IsNullOrEmpty(nuevo))
            return;

        try
        {
            if (_ventaService.ActualizarEstado(_ventaId, nuevo))
            {
                MessageBox.Show("Estado actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
            }
        }
        catch
        {
            // Ya notificado
        }
    }
}
