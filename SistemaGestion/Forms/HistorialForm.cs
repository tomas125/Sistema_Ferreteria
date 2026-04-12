using System.Text;
using SistemaGestion.Helpers;
using SistemaGestion.Models;
using SistemaGestion.Services;

namespace SistemaGestion.Forms;

public partial class HistorialForm : Form
{
    private readonly VentaService _ventaService = new();

    // Constructor del historial: inicializa filtros, grilla y primera carga.
    public HistorialForm()
    {
        InitializeComponent();
        ConfigurarFiltros();
        ConfigurarGrid();
        CargarDatos();
    }

    // Define valores por defecto de filtros (rango mensual actual, forma de pago y estado).
    // Cuidado: cualquier cambio en textos de combos impacta el filtrado esperado por el servicio.
    private void ConfigurarFiltros()
    {
        var hoy = DateTime.Today;
        dtpDesde.Value = new DateTime(hoy.Year, hoy.Month, 1);
        dtpHasta.Value = hoy;

        cmbFormaPago.Items.AddRange(new object[] { "(Todas)", "Efectivo", "Transferencia", "Tarjeta" });
        cmbFormaPago.SelectedIndex = 0;

        cmbEstado.Items.AddRange(new object[] { "(Todos)", "FINALIZADO", "En Curso", "Entregado", "Cancelado", "Pendiente" });
        cmbEstado.SelectedIndex = 0;
    }

    // Configura estructura y estilos de la grilla del historial.
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

    // Aplica filtros seleccionados y carga ventas resultantes en pantalla.
    private void CargarDatos()
    {
        dgvVentas.Rows.Clear();
        string? fp = cmbFormaPago.SelectedItem?.ToString();
        string? est = cmbEstado.SelectedItem?.ToString();

        IReadOnlyList<Venta> lista = _ventaService.ListarVentas(
            dtpDesde.Value.Date,
            dtpHasta.Value.Date,
            fp,
            est);

        foreach (var v in lista)
        {
            dgvVentas.Rows.Add(
                v.Id,
                v.FechaVenta ?? "",
                v.ClienteNombre ?? "",
                v.Total,
                v.FormaPago,
                v.Estado);
        }
    }

    // Evento de botón para ejecutar el filtrado actual.
    private void BtnFiltrar_Click(object? sender, EventArgs e) => CargarDatos();

    private void BtnMarcarFinalizado_Click(object? sender, EventArgs e)
    {
        var ids = dgvVentas.SelectedRows.Cast<DataGridViewRow>()
            .Where(r => !r.IsNewRow)
            .Select(TryGetRowVentaId)
            .Where(id => id.HasValue)
            .Select(id => id!.Value)
            .Distinct()
            .ToList();

        if (ids.Count == 0)
        {
            MessageBox.Show(
                this,
                "Seleccione al menos una fila (clic en la fila, Ctrl+clic para varias, Mayús+clic para un rango).",
                "Historial de ventas",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        var pregunta = ids.Count == 1
            ? $"¿Marcar la venta #{ids[0]} como FINALIZADO?"
            : $"¿Marcar las {ids.Count} ventas seleccionadas como FINALIZADO?";

        if (MessageBox.Show(this, pregunta, "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        int n = _ventaService.ActualizarEstadoLote(ids, "FINALIZADO");
        if (n <= 0)
            return;

        MessageBox.Show(
            this,
            n == 1 ? "Estado actualizado a FINALIZADO." : $"{n} ventas actualizadas a FINALIZADO.",
            "Historial de ventas",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        CargarDatos();
    }

    private static int? TryGetRowVentaId(DataGridViewRow row)
    {
        var idObj = row.Cells["Id"].Value;
        if (idObj is int id)
            return id;
        return int.TryParse(idObj?.ToString(), out int x) ? x : null;
    }

    // Permite abrir detalle de una venta con doble clic.
    private void DgvVentas_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var idObj = dgvVentas.Rows[e.RowIndex].Cells["Id"].Value;
        if (idObj is int id)
            AbrirDetalle(id);
        else if (int.TryParse(idObj?.ToString(), out int id2))
            AbrirDetalle(id2);
    }

    // Abre la vista de detalle y refresca historial al volver.
    private void AbrirDetalle(int ventaId)
    {
        using var f = new DetalleVentaForm(ventaId);
        f.ShowDialog(this);
        CargarDatos();
    }

    // Exporta el contenido visible en la grilla a CSV (UTF-8 con BOM para mejor compatibilidad con Excel).
    // Cuidado: exporta lo que está en UI; si hay filtros aplicados, el archivo también queda filtrado.
    private void BtnExportar_Click(object? sender, EventArgs e)
    {
        using var sfd = new SaveFileDialog
        {
            Filter = "CSV (*.csv)|*.csv",
            FileName = $"historial_ventas_{DateTime.Now:yyyyMMdd_HHmm}.csv",
        };
        if (sfd.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            var sb = new StringBuilder();
            sb.AppendLine("ID;Fecha;Cliente;Total ($);Forma de Pago;Estado");
            foreach (DataGridViewRow row in dgvVentas.Rows)
            {
                if (row.IsNewRow) continue;
                string line = string.Join(";",
                    EscapeCsv(row.Cells["Id"].Value),
                    EscapeCsv(row.Cells["Fecha"].Value),
                    EscapeCsv(row.Cells["Cliente"].Value),
                    row.Cells["Total"].Value?.ToString() ?? "",
                    EscapeCsv(row.Cells["FormaPago"].Value),
                    EscapeCsv(row.Cells["Estado"].Value));
                sb.AppendLine(line);
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

    // Escapa campos con separadores/comillas/saltos de línea para mantener integridad del CSV.
    private static string EscapeCsv(object? value)
    {
        var s = value?.ToString() ?? "";
        if (s.Contains(';') || s.Contains('"') || s.Contains('\n'))
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        return s;
    }
}
