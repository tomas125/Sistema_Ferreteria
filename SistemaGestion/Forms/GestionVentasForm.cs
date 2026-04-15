using SistemaGestion.Helpers;
using SistemaGestion.Models;
using SistemaGestion.Services;

namespace SistemaGestion.Forms;

public partial class GestionVentasForm : Form
{
    private readonly VentaService _ventaService = new();

    // Constructor de la vista de gestión: prepara grilla y carga ventas pendientes.
    public GestionVentasForm()
    {
        InitializeComponent();
        ConfigurarGrid();
        CargarDatos();
    }

    // Define columnas y estilos visuales de la grilla de ventas.
    // Cuidado: si se cambian nombres de columna, también debe ajustarse la lectura por clave en otros métodos.
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

    // Consulta ventas pendientes en servicio y las muestra en la grilla.
    // Cuidado: asume que ListarVentasPendientesGestion devuelve datos consistentes con las columnas configuradas.
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
            ? "No hay ventas pendientes de gestión. Las ventas ya cerradas aparecen en «Ver Historial»."
            : "Seleccione una fila: «Marcar como FINALIZADO», «Cancelar venta» (no suma en estadísticas; queda en historial) o doble clic para el detalle.";
    }

    // Marca la venta seleccionada como FINALIZADO con confirmación del usuario.
    // Punto sensible: si el ID no parsea, el método termina silenciosamente.
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

    // Marca la venta como cancelada: no cuenta como venta concretada en estadísticas; permanece en historial.
    private void BtnCancelar_Click(object? sender, EventArgs e)
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
                $"¿Cancelar la venta #{id}?\n\n"
                + "No se contará como venta concretada en estadísticas ni en «Ventas hoy», "
                + "pero seguirá visible en «Ver Historial» con estado Cancelado.",
                "Confirmar cancelación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            return;

        if (_ventaService.ActualizarEstado(id, "Cancelado"))
        {
            MessageBox.Show("Venta cancelada. Figura en el historial con estado Cancelado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            CargarDatos();
        }
    }

    // Atajo de navegación: doble clic abre detalle de la venta elegida.
    private void DgvVentas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var idObj = dgvVentas.Rows[e.RowIndex].Cells["Id"].Value;
        if (idObj is int id)
            AbrirDetalle(id);
        else if (int.TryParse(idObj?.ToString(), out int id2))
            AbrirDetalle(id2);
    }

    // Abre el formulario de detalle y, al cerrar, refresca la lista por si hubo cambios de estado.
    private void AbrirDetalle(int ventaId)
    {
        using var f = new DetalleVentaForm(ventaId);
        f.ShowDialog(this);
        CargarDatos();
    }

    // Cierra la vista de gestión.
    private void BtnCerrar_Click(object? sender, EventArgs e) => Close();
}
