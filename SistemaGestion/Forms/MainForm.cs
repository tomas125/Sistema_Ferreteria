using System.Globalization;
using SistemaGestion.Helpers;
using SistemaGestion.Models;
using SistemaGestion.Services;

namespace SistemaGestion.Forms;

public partial class MainForm : Form
{
    private const string ClienteVentaPorDefecto = "Mostrador";

    private readonly VentaService _ventaService = new();

    public MainForm()
    {
        InitializeComponent();
        cmbDescuento.Items.AddRange(new object[] { "0%", "5%", "10%", "15%", "20%" });
        cmbDescuento.SelectedIndex = 0;
        cmbDescuento.SelectedIndexChanged += (_, _) => ActualizarTotal();
        txtProducto.KeyDown += Entrada_KeyDown;
        nudCantidad.KeyDown += Entrada_KeyDown;
        txtPrecio.KeyDown += Entrada_KeyDown;
        ConfigurarGrid();
        ActualizarTotal();
        RefrescarBarraEstado();
    }

    private void Entrada_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
            return;
        e.SuppressKeyPress = true;
        e.Handled = true;
        BtnAgregar_Click(btnAgregar, EventArgs.Empty);
    }

    private static bool TryParsePrecioTexto(string? texto, out decimal precio)
    {
        var t = texto?.Trim() ?? "";
        return decimal.TryParse(t, NumberStyles.Any, CurrencyFormat.Pesos, out precio)
            || decimal.TryParse(t, NumberStyles.Any, CultureInfo.InvariantCulture, out precio);
    }

    private void ConfigurarGrid()
    {
        dgvItems.Columns.Clear();
        dgvItems.AutoGenerateColumns = false;
        dgvItems.EnableHeadersVisualStyles = false;
        dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
        dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        dgvItems.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        dgvItems.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
        dgvItems.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
        dgvItems.ReadOnly = false;
        dgvItems.AllowUserToDeleteRows = true;

        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColNum", HeaderText = "#", Width = 40, ReadOnly = true });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColProducto", HeaderText = "Producto/Servicio", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = false });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "ColCantidad", HeaderText = "Cantidad", Width = 80, ReadOnly = false });
        var colPrecio = new DataGridViewTextBoxColumn { Name = "ColPrecio", HeaderText = "Precio Unit.", Width = 120, ReadOnly = false };
        colPrecio.DefaultCellStyle.Format = "C2";
        colPrecio.DefaultCellStyle.FormatProvider = CurrencyFormat.Pesos;
        dgvItems.Columns.Add(colPrecio);
        var colSub = new DataGridViewTextBoxColumn { Name = "ColSubtotal", HeaderText = "Subtotal", Width = 120, ReadOnly = true };
        colSub.DefaultCellStyle.Format = "C2";
        colSub.DefaultCellStyle.FormatProvider = CurrencyFormat.Pesos;
        dgvItems.Columns.Add(colSub);
        dgvItems.Columns.Add(new DataGridViewButtonColumn
        {
            Name = "ColEliminar",
            HeaderText = "",
            Text = "Eliminar",
            Width = 90,
            ReadOnly = true,
        });

        dgvItems.CellParsing += DgvItems_CellParsing;
        dgvItems.CellValidating += DgvItems_CellValidating;
        dgvItems.CellEndEdit += DgvItems_CellEndEdit;
        dgvItems.CellContentClick += DgvItems_CellContentClick;
        dgvItems.RowsRemoved += DgvItems_RowsRemoved;
        dgvItems.DataError += (_, e) => { e.ThrowException = false; };
    }

    private void DgvItems_CellParsing(object? sender, DataGridViewCellParsingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var name = dgvItems.Columns[e.ColumnIndex].Name;
        if (name == "ColCantidad")
        {
            if (int.TryParse(e.Value?.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int c)
                || int.TryParse(e.Value?.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out c))
            {
                e.Value = c;
                e.ParsingApplied = true;
            }
        }
        else if (name == "ColPrecio" && TryParsePrecioTexto(e.Value?.ToString(), out var p))
        {
            e.Value = p;
            e.ParsingApplied = true;
        }
    }

    private void DgvItems_CellValidating(object? sender, DataGridViewCellValidatingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var name = dgvItems.Columns[e.ColumnIndex].Name;

        if (name == "ColProducto")
        {
            var t = (e.FormattedValue?.ToString() ?? "").Trim();
            if (t.Length < 3)
            {
                MessageBox.Show("El producto o servicio debe tener al menos 3 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
        else if (name == "ColCantidad")
        {
            if (!int.TryParse(e.FormattedValue?.ToString(), NumberStyles.Integer, CultureInfo.CurrentCulture, out int c)
                && !int.TryParse(e.FormattedValue?.ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out c))
            {
                MessageBox.Show("Ingrese una cantidad numérica válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            if (c is < 1 or > 9999)
            {
                MessageBox.Show("La cantidad debe estar entre 1 y 9999.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
        else if (name == "ColPrecio")
        {
            if (!TryParsePrecioTexto(e.FormattedValue?.ToString(), out var p) || p <= 0)
            {
                MessageBox.Show("Ingrese un precio válido mayor a 0 (pesos $).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
    }

    private void DgvItems_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var name = dgvItems.Columns[e.ColumnIndex].Name;
        if (name is not ("ColProducto" or "ColCantidad" or "ColPrecio"))
            return;

        var row = dgvItems.Rows[e.RowIndex];
        if (row.IsNewRow) return;

        RecalcularSubtotalFila(row);
        RenumerarFilasItems();
        ActualizarTotal();
    }

    private void DgvItems_CellContentClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (dgvItems.Columns[e.ColumnIndex].Name != "ColEliminar") return;
        if (dgvItems.Rows[e.RowIndex].IsNewRow) return;

        dgvItems.EndEdit();
        dgvItems.Rows.RemoveAt(e.RowIndex);
    }

    private void DgvItems_RowsRemoved(object? sender, DataGridViewRowsRemovedEventArgs e)
    {
        RenumerarFilasItems();
        ActualizarTotal();
    }

    private static void RecalcularSubtotalFila(DataGridViewRow row)
    {
        if (row.IsNewRow) return;
        try
        {
            int cant = Convert.ToInt32(row.Cells["ColCantidad"].Value);
            decimal precio = Convert.ToDecimal(row.Cells["ColPrecio"].Value);
            row.Cells["ColSubtotal"].Value = cant * precio;
        }
        catch
        {
            // fila incompleta durante edición
        }
    }

    private void RenumerarFilasItems()
    {
        int i = 1;
        foreach (DataGridViewRow row in dgvItems.Rows)
        {
            if (row.IsNewRow) continue;
            row.Cells["ColNum"].Value = i++;
        }
    }

    private void BtnAgregar_Click(object? sender, EventArgs e)
    {
        var producto = txtProducto.Text.Trim();
        if (producto.Length < 3)
        {
            MessageBox.Show("Ingrese un producto o servicio válido (mínimo 3 caracteres).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtProducto.Focus();
            return;
        }

        if (nudCantidad.Value is < 1 or > 9999)
        {
            MessageBox.Show("La cantidad debe estar entre 1 y 9999.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            nudCantidad.Focus();
            return;
        }

        var precioTexto = txtPrecio.Text.Trim();
        if (!TryParsePrecioTexto(precioTexto, out var precio))
        {
            MessageBox.Show("Ingrese un precio válido mayor a 0 (pesos $).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPrecio.Focus();
            return;
        }

        if (precio <= 0)
        {
            MessageBox.Show("Ingrese un precio válido mayor a 0 (pesos $).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtPrecio.Focus();
            return;
        }

        int cantidad = (int)nudCantidad.Value;
        decimal subtotal = cantidad * precio;
        int n = dgvItems.Rows.Count + 1;

        dgvItems.Rows.Add(n, producto, cantidad, precio, subtotal, "Eliminar");
        RenumerarFilasItems();
        ActualizarTotal();

        txtProducto.Clear();
        nudCantidad.Value = 1;
        txtPrecio.Clear();
        txtProducto.Focus();
    }

    private static decimal ObtenerPorcentajeDescuentoDesdeCombo(int selectedIndex) => selectedIndex switch
    {
        1 => 5m,
        2 => 10m,
        3 => 15m,
        4 => 20m,
        _ => 0m,
    };

    public void ActualizarTotal()
    {
        decimal suma = 0;
        foreach (DataGridViewRow row in dgvItems.Rows)
        {
            if (row.IsNewRow) continue;
            if (row.Cells["ColSubtotal"].Value is decimal d)
                suma += d;
        }

        decimal pct = ObtenerPorcentajeDescuentoDesdeCombo(cmbDescuento.SelectedIndex);
        decimal montoDesc = Math.Round(suma * (pct / 100m), 2);
        decimal totalFinal = Math.Round(suma - montoDesc, 2);

        if (pct > 0 && suma > 0)
        {
            lblTotal.Text =
                "Subtotal: " + suma.ToString("C2", CurrencyFormat.Pesos)
                + Environment.NewLine
                + $"Descuento ({pct}%): -" + montoDesc.ToString("C2", CurrencyFormat.Pesos)
                + Environment.NewLine
                + "TOTAL: " + totalFinal.ToString("C2", CurrencyFormat.Pesos);
        }
        else
        {
            lblTotal.Text = "TOTAL: " + suma.ToString("C2", CurrencyFormat.Pesos);
        }
    }

    private string? ObtenerFormaPago()
    {
        if (rbEfectivo.Checked) return "Efectivo";
        if (rbTransferencia.Checked) return "Transferencia";
        if (rbTarjeta.Checked) return "Tarjeta";
        return null;
    }

    private void BtnGuardar_Click(object? sender, EventArgs e)
    {
        if (dgvItems.Rows.Count == 0)
        {
            MessageBox.Show("Agregue al menos un ítem a la venta.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var forma = ObtenerFormaPago();
        if (forma is null)
        {
            MessageBox.Show("Seleccione una forma de pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        decimal subtotal = 0;
        foreach (DataGridViewRow row in dgvItems.Rows)
        {
            if (row.IsNewRow) continue;
            if (row.Cells["ColSubtotal"].Value is decimal d)
                subtotal += d;
        }

        decimal pctDesc = ObtenerPorcentajeDescuentoDesdeCombo(cmbDescuento.SelectedIndex);
        decimal montoDesc = Math.Round(subtotal * (pctDesc / 100m), 2);
        decimal total = Math.Round(subtotal - montoDesc, 2);

        if (total <= 0)
        {
            MessageBox.Show("El total debe ser mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var items = new List<DetalleVenta>();
        foreach (DataGridViewRow row in dgvItems.Rows)
        {
            if (row.IsNewRow) continue;
            items.Add(new DetalleVenta
            {
                Producto = row.Cells["ColProducto"].Value?.ToString() ?? "",
                Cantidad = Convert.ToInt32(row.Cells["ColCantidad"].Value),
                PrecioUnitario = Convert.ToDecimal(row.Cells["ColPrecio"].Value),
                Subtotal = Convert.ToDecimal(row.Cells["ColSubtotal"].Value),
            });
        }

        try
        {
            _ventaService.GuardarVenta(ClienteVentaPorDefecto, forma, items, total);
            MessageBox.Show("SE REGISTRÓ CORRECTAMENTE LA VENTA.", "Venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarFormulario();
            RefrescarBarraEstado();
        }
        catch
        {
            // Mensaje ya mostrado en servicio
        }
    }

    private void LimpiarFormulario()
    {
        dgvItems.Rows.Clear();
        txtProducto.Clear();
        nudCantidad.Value = 0;
        txtPrecio.Clear();
        rbEfectivo.Checked = false;
        rbTransferencia.Checked = false;
        rbTarjeta.Checked = false;
        cmbDescuento.SelectedIndex = 0;
        ActualizarTotal();
    }

    private void BtnLimpiar_Click(object? sender, EventArgs e) => LimpiarFormulario();

    private void BtnVerHistorial_Click(object? sender, EventArgs e)
    {
        using var f = new HistorialForm();
        f.ShowDialog(this);
    }

    private void BtnEstadisticas_Click(object? sender, EventArgs e)
    {
        using var f = new EstadisticasForm();
        f.ShowDialog(this);
    }

    private void BtnGestionVentas_Click(object? sender, EventArgs e)
    {
        using var f = new GestionVentasForm();
        f.ShowDialog(this);
    }

    private void RefrescarBarraEstado()
    {
        statusFecha.Text = "Fecha: " + DateTime.Now.ToString("dddd dd/MM/yyyy HH:mm", new CultureInfo("es-AR"));
        try
        {
            statusVentasHoy.Text = "Ventas hoy: " + _ventaService.ContarVentasDelDia();
        }
        catch
        {
            statusVentasHoy.Text = "Ventas hoy: —";
        }
    }

    private void TimerReloj_Tick(object? sender, EventArgs e)
    {
        RefrescarBarraEstado();
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        CargarLogo();
        RefrescarBarraEstado();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        //picLogo.Image?.Dispose();
        //picLogo.Image = null;
        //base.OnFormClosed(e);
    }

    private void CargarLogo()
    {
        try
        {
            var path = Path.Combine(AppContext.BaseDirectory, "IMAGENES", "logo.png");
            if (!File.Exists(path))
                return;

            //picLogo.Image?.Dispose();
            //picLogo.Image = Image.FromFile(path);

            // Icono de la barra de título (junto al título de la ventana)
            using (var bmp = new Bitmap(path))
            {
                Icon = Icon.FromHandle(bmp.GetHicon());
            }

            ShowIcon = true;
        }
        catch
        {
            // Si el archivo no está o es inválido, se deja el PictureBox vacío
        }
    }

    //HOla
}
