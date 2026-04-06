using SistemaGestion.Helpers;
using SistemaGestion.Models;
using SistemaGestion.Services;

namespace SistemaGestion.Forms;

public partial class GestionVentasForm : Form
{
    private readonly VentaService _ventaService = new();

    public GestionVentasForm()
    {
        InitializeComponent();
        ConfigurarGrid();
        CargarDatos();
    }

    private void ConfigurarGrid()
    {
        dgvVentas.Columns.Clear();
        dgvVentas.AutoGenerateColumns = false;
        dgvVentas.EnableHeadersVisualStyles = false;
        dgvVentas.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
        dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvVentas.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;

        dgvVentas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", Width = 50 });
        dgvVentas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Fecha", HeaderText = "Fecha", Width = 150 });
        dgvVentas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cliente", HeaderText = "Cliente", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
        var colTotal = new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Total ($)", Width = 110 };
        colTotal.DefaultCellStyle.Format = "C2";
        colTotal.DefaultCellStyle.FormatProvider = CurrencyFormat.Pesos;
        dgvVentas.Columns.Add(colTotal);
        dgvVentas.Columns.Add(new DataGridViewTextBoxColumn { Name = "FormaPago", HeaderText = "Forma de Pago", Width = 120 });
        dgvVentas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Estado", HeaderText = "Estado", Width = 100 });
    }

    private void CargarDatos()
    {
        dgvVentas.Rows.Clear();
        foreach (var v in _ventaService.ListarVentasPendientesGestion())
        {
            dgvVentas.Rows.Add(
                v.Id,
                v.FechaVenta ?? "",
                v.ClienteNombre ?? "",
                v.Total,
                v.FormaPago,
                v.Estado);
        }

        lblInfo.Text = dgvVentas.Rows.Count == 0
            ? "No hay ventas pendientes de gestión."
            : "Seleccione una venta y pulse «Marcar como FINALIZADO» o haga doble clic para ver el detalle.";
    }

    private void BtnFinalizar_Click(object? sender, EventArgs e)
    {
        if (dgvVentas.CurrentRow is null || dgvVentas.CurrentRow.IsNewRow)
        {
            MessageBox.Show("Seleccione una venta en la grilla.", "Gestión", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var idObj = dgvVentas.CurrentRow.Cells["Id"].Value;
        if (idObj is not int id && !int.TryParse(idObj?.ToString(), out id))
            return;

        if (MessageBox.Show(
                $"¿Marcar la venta #{id} como FINALIZADO?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        if (_ventaService.ActualizarEstado(id, "FINALIZADO"))
        {
            MessageBox.Show("Estado actualizado a FINALIZADO.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarDatos();
        }
    }

    private void DgvVentas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var idObj = dgvVentas.Rows[e.RowIndex].Cells["Id"].Value;
        if (idObj is int id)
            AbrirDetalle(id);
        else if (int.TryParse(idObj?.ToString(), out int id2))
            AbrirDetalle(id2);
    }

    private void AbrirDetalle(int ventaId)
    {
        using var f = new DetalleVentaForm(ventaId);
        f.ShowDialog(this);
        CargarDatos();
    }

    private void BtnCerrar_Click(object? sender, EventArgs e) => Close();
}
