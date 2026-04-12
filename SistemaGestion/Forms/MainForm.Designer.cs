#nullable disable
namespace SistemaGestion.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        panelSuperior = new Panel();
        panelEntrada = new Panel();
        lblProducto = new Label();
        txtProducto = new TextBox();
        lblCantidad = new Label();
        nudCantidad = new NumericUpDown();
        lblPrecio = new Label();
        txtPrecio = new TextBox();
        lblEnterHint = new Label();
        btnAgregar = new Button();
        toolStripPrincipal = new ToolStrip();
        tsbVerHistorial = new ToolStripButton();
        tsbGestionVentas = new ToolStripButton();
        tsbEstadisticas = new ToolStripButton();
        panelCentral = new Panel();
        dgvItems = new DataGridView();
        panelFilaInferiorItems = new Panel();
        btnLimpiar = new Button();
        lblTotal = new Label();
        panelCabeceraGrid = new Panel();
        lblPreview = new Label();
        panelInferior = new Panel();
        flowBotones = new FlowLayoutPanel();
        btnGuardar = new Button();
        gbDescuento = new GroupBox();
        lblDescuentoPct = new Label();
        cmbDescuento = new ComboBox();
        gbFormaPago = new GroupBox();
        rbTarjeta = new RadioButton();
        rbTransferencia = new RadioButton();
        rbEfectivo = new RadioButton();
        statusStrip = new StatusStrip();
        statusFecha = new ToolStripStatusLabel();
        statusVentasHoy = new ToolStripStatusLabel();
        statusSeparador = new ToolStripStatusLabel();
        statusVersion = new ToolStripStatusLabel();
        timerReloj = new System.Windows.Forms.Timer(components);
        panelSuperior.SuspendLayout();
        panelEntrada.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
        toolStripPrincipal.SuspendLayout();
        panelCentral.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
        panelFilaInferiorItems.SuspendLayout();
        panelCabeceraGrid.SuspendLayout();
        panelInferior.SuspendLayout();
        flowBotones.SuspendLayout();
        gbDescuento.SuspendLayout();
        gbFormaPago.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // panelSuperior
        // 
        panelSuperior.BackColor = Color.WhiteSmoke;
        panelSuperior.Controls.Add(panelEntrada);
        panelSuperior.Controls.Add(toolStripPrincipal);
        panelSuperior.Dock = DockStyle.Top;
        panelSuperior.Location = new Point(0, 0);
        panelSuperior.Name = "panelSuperior";
        panelSuperior.Size = new Size(917, 228);
        panelSuperior.TabIndex = 0;
        // 
        // panelEntrada
        // 
        panelEntrada.BackColor = Color.WhiteSmoke;
        panelEntrada.Controls.Add(lblProducto);
        panelEntrada.Controls.Add(txtProducto);
        panelEntrada.Controls.Add(lblCantidad);
        panelEntrada.Controls.Add(nudCantidad);
        panelEntrada.Controls.Add(lblPrecio);
        panelEntrada.Controls.Add(txtPrecio);
        panelEntrada.Controls.Add(lblEnterHint);
        panelEntrada.Controls.Add(btnAgregar);
        panelEntrada.Dock = DockStyle.Fill;
        panelEntrada.Location = new Point(0, 30);
        panelEntrada.Name = "panelEntrada";
        panelEntrada.Padding = new Padding(12, 8, 12, 8);
        panelEntrada.Size = new Size(917, 198);
        panelEntrada.TabIndex = 1;
        // 
        // lblProducto
        // 
        lblProducto.AutoSize = true;
        lblProducto.Location = new Point(15, 10);
        lblProducto.Name = "lblProducto";
        lblProducto.Size = new Size(105, 15);
        lblProducto.TabIndex = 0;
        lblProducto.Text = "Producto/Servicio:";
        // 
        // txtProducto
        // 
        txtProducto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtProducto.Location = new Point(140, 7);
        txtProducto.Name = "txtProducto";
        txtProducto.Size = new Size(650, 23);
        txtProducto.TabIndex = 1;
        // 
        // lblCantidad
        // 
        lblCantidad.AutoSize = true;
        lblCantidad.Location = new Point(15, 40);
        lblCantidad.Name = "lblCantidad";
        lblCantidad.Size = new Size(58, 15);
        lblCantidad.TabIndex = 2;
        lblCantidad.Text = "Cantidad:";
        // 
        // nudCantidad
        // 
        nudCantidad.Location = new Point(140, 37);
        nudCantidad.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
        nudCantidad.Name = "nudCantidad";
        nudCantidad.Size = new Size(100, 23);
        nudCantidad.TabIndex = 3;
        // 
        // lblPrecio
        // 
        lblPrecio.AutoSize = true;
        lblPrecio.Location = new Point(15, 70);
        lblPrecio.Name = "lblPrecio";
        lblPrecio.Size = new Size(60, 15);
        lblPrecio.TabIndex = 4;
        lblPrecio.Text = "Precio ($):";
        // 
        // txtPrecio
        // 
        txtPrecio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtPrecio.Location = new Point(140, 67);
        txtPrecio.Name = "txtPrecio";
        txtPrecio.Size = new Size(650, 23);
        txtPrecio.TabIndex = 5;
        // 
        // lblEnterHint
        // 
        lblEnterHint.AutoSize = true;
        lblEnterHint.ForeColor = SystemColors.GrayText;
        lblEnterHint.Location = new Point(140, 96);
        lblEnterHint.MaximumSize = new Size(560, 0);
        lblEnterHint.Name = "lblEnterHint";
        lblEnterHint.Size = new Size(327, 15);
        lblEnterHint.TabIndex = 6;
        lblEnterHint.Text = "Tip: también puede presionar Enter para agregar el producto.";
        // 
        // btnAgregar
        // 
        btnAgregar.Anchor = AnchorStyles.Top;
        btnAgregar.BackColor = Color.DodgerBlue;
        btnAgregar.FlatStyle = FlatStyle.Flat;
        btnAgregar.ForeColor = Color.White;
        btnAgregar.Location = new Point(748, 92);
        btnAgregar.Name = "btnAgregar";
        btnAgregar.Size = new Size(135, 28);
        btnAgregar.TabIndex = 7;
        btnAgregar.Text = "AGREGAR";
        btnAgregar.UseVisualStyleBackColor = false;
        btnAgregar.Click += BtnAgregar_Click;
        // 
        // toolStripPrincipal
        // 
        toolStripPrincipal.BackColor = Color.WhiteSmoke;
        toolStripPrincipal.GripStyle = ToolStripGripStyle.Hidden;
        toolStripPrincipal.ImageScalingSize = new Size(20, 20);
        toolStripPrincipal.Items.AddRange(new ToolStripItem[] { tsbVerHistorial, tsbGestionVentas, tsbEstadisticas });
        toolStripPrincipal.Location = new Point(0, 0);
        toolStripPrincipal.Name = "toolStripPrincipal";
        toolStripPrincipal.Padding = new Padding(8, 2, 8, 2);
        toolStripPrincipal.RenderMode = ToolStripRenderMode.System;
        toolStripPrincipal.Size = new Size(917, 30);
        toolStripPrincipal.Stretch = true;
        toolStripPrincipal.TabIndex = 0;
        toolStripPrincipal.Text = "toolStripPrincipal";
        // 
        // tsbVerHistorial
        // 
        tsbVerHistorial.BackColor = Color.FromArgb(173, 214, 255);
        tsbVerHistorial.DisplayStyle = ToolStripItemDisplayStyle.Text;
        tsbVerHistorial.ForeColor = Color.FromArgb(0, 51, 102);
        tsbVerHistorial.Margin = new Padding(0, 0, 8, 0);
        tsbVerHistorial.Name = "tsbVerHistorial";
        tsbVerHistorial.Padding = new Padding(10, 2, 10, 2);
        tsbVerHistorial.Size = new Size(94, 26);
        tsbVerHistorial.Text = "Ver Historial";
        tsbVerHistorial.Click += BtnVerHistorial_Click;
        // 
        // tsbGestionVentas
        // 
        tsbGestionVentas.BackColor = Color.White;
        tsbGestionVentas.DisplayStyle = ToolStripItemDisplayStyle.Text;
        tsbGestionVentas.ForeColor = SystemColors.ControlText;
        tsbGestionVentas.Margin = new Padding(0, 0, 8, 0);
        tsbGestionVentas.Name = "tsbGestionVentas";
        tsbGestionVentas.Padding = new Padding(10, 2, 10, 2);
        tsbGestionVentas.Size = new Size(108, 26);
        tsbGestionVentas.Text = "Gestión ventas";
        tsbGestionVentas.Click += BtnGestionVentas_Click;
        // 
        // tsbEstadisticas
        // 
        tsbEstadisticas.BackColor = Color.White;
        tsbEstadisticas.DisplayStyle = ToolStripItemDisplayStyle.Text;
        tsbEstadisticas.ForeColor = SystemColors.ControlText;
        tsbEstadisticas.Name = "tsbEstadisticas";
        tsbEstadisticas.Padding = new Padding(10, 2, 10, 2);
        tsbEstadisticas.Size = new Size(91, 23);
        tsbEstadisticas.Text = "Estadísticas";
        tsbEstadisticas.Click += BtnEstadisticas_Click;
        // 
        // panelCentral
        // 
        panelCentral.BackColor = Color.WhiteSmoke;
        panelCentral.Controls.Add(dgvItems);
        panelCentral.Controls.Add(panelFilaInferiorItems);
        panelCentral.Controls.Add(panelCabeceraGrid);
        panelCentral.Dock = DockStyle.Fill;
        panelCentral.Location = new Point(0, 228);
        panelCentral.Name = "panelCentral";
        panelCentral.Padding = new Padding(12, 8, 12, 8);
        panelCentral.Size = new Size(917, 201);
        panelCentral.TabIndex = 1;
        // 
        // dgvItems
        // 
        dgvItems.AllowUserToAddRows = false;
        dgvItems.BackgroundColor = Color.White;
        dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvItems.Dock = DockStyle.Fill;
        dgvItems.Location = new Point(12, 34);
        dgvItems.MultiSelect = false;
        dgvItems.Name = "dgvItems";
        dgvItems.RowHeadersVisible = false;
        dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvItems.Size = new Size(893, 81);
        dgvItems.TabIndex = 1;
        // 
        // panelFilaInferiorItems
        // 
        panelFilaInferiorItems.Controls.Add(btnLimpiar);
        panelFilaInferiorItems.Controls.Add(lblTotal);
        panelFilaInferiorItems.Dock = DockStyle.Bottom;
        panelFilaInferiorItems.Location = new Point(12, 115);
        panelFilaInferiorItems.Name = "panelFilaInferiorItems";
        panelFilaInferiorItems.MinimumSize = new Size(0, 76);
        panelFilaInferiorItems.Size = new Size(893, 76);
        panelFilaInferiorItems.TabIndex = 2;
        // 
        // btnLimpiar
        // 
        btnLimpiar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnLimpiar.BackColor = Color.Tomato;
        btnLimpiar.FlatStyle = FlatStyle.Flat;
        btnLimpiar.ForeColor = Color.White;
        btnLimpiar.Location = new Point(3, 22);
        btnLimpiar.Name = "btnLimpiar";
        btnLimpiar.Size = new Size(120, 30);
        btnLimpiar.TabIndex = 0;
        btnLimpiar.Text = "Limpiar";
        btnLimpiar.UseVisualStyleBackColor = false;
        btnLimpiar.Click += BtnLimpiar_Click;
        // 
        // lblTotal
        // 
        lblTotal.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblTotal.AutoSize = false;
        lblTotal.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblTotal.ForeColor = Color.DarkGreen;
        lblTotal.Location = new Point(132, 6);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(749, 64);
        lblTotal.TabIndex = 1;
        lblTotal.Text = "TOTAL: $0.00";
        lblTotal.TextAlign = ContentAlignment.TopRight;
        // 
        // panelCabeceraGrid
        // 
        panelCabeceraGrid.Controls.Add(lblPreview);
        panelCabeceraGrid.Dock = DockStyle.Top;
        panelCabeceraGrid.Location = new Point(12, 8);
        panelCabeceraGrid.Name = "panelCabeceraGrid";
        panelCabeceraGrid.Size = new Size(893, 26);
        panelCabeceraGrid.TabIndex = 0;
        // 
        // lblPreview
        // 
        lblPreview.AutoSize = true;
        lblPreview.Dock = DockStyle.Fill;
        lblPreview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPreview.Location = new Point(0, 0);
        lblPreview.Name = "lblPreview";
        lblPreview.Padding = new Padding(3, 4, 0, 0);
        lblPreview.Size = new Size(124, 19);
        lblPreview.TabIndex = 0;
        lblPreview.Text = "PREVISUALIZACIÓN:";
        // 
        // panelInferior
        // 
        panelInferior.BackColor = Color.WhiteSmoke;
        panelInferior.Controls.Add(flowBotones);
        panelInferior.Controls.Add(gbDescuento);
        panelInferior.Controls.Add(gbFormaPago);
        panelInferior.Dock = DockStyle.Bottom;
        panelInferior.Location = new Point(0, 449);
        panelInferior.Name = "panelInferior";
        panelInferior.Padding = new Padding(12, 8, 12, 8);
        panelInferior.Size = new Size(917, 168);
        panelInferior.TabIndex = 2;
        // 
        // flowBotones
        // 
        flowBotones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        flowBotones.AutoSize = true;
        flowBotones.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flowBotones.Controls.Add(btnGuardar);
        flowBotones.Location = new Point(12, 124);
        flowBotones.Name = "flowBotones";
        flowBotones.Size = new Size(135, 36);
        flowBotones.TabIndex = 2;
        flowBotones.WrapContents = false;
        // 
        // btnGuardar
        // 
        btnGuardar.BackColor = Color.SeaGreen;
        btnGuardar.FlatStyle = FlatStyle.Flat;
        btnGuardar.ForeColor = Color.White;
        btnGuardar.Location = new Point(3, 3);
        btnGuardar.Margin = new Padding(3, 3, 12, 3);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(120, 30);
        btnGuardar.TabIndex = 0;
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = false;
        btnGuardar.Click += BtnGuardar_Click;
        // 
        // gbDescuento
        // 
        gbDescuento.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbDescuento.Controls.Add(lblDescuentoPct);
        gbDescuento.Controls.Add(cmbDescuento);
        gbDescuento.Location = new Point(15, 68);
        gbDescuento.Name = "gbDescuento";
        gbDescuento.Size = new Size(887, 50);
        gbDescuento.TabIndex = 1;
        gbDescuento.TabStop = false;
        gbDescuento.Text = "Descuento (sobre el total, con todos los ítems cargados)";
        // 
        // lblDescuentoPct
        // 
        lblDescuentoPct.AutoSize = true;
        lblDescuentoPct.Location = new Point(16, 22);
        lblDescuentoPct.Name = "lblDescuentoPct";
        lblDescuentoPct.Size = new Size(66, 15);
        lblDescuentoPct.TabIndex = 0;
        lblDescuentoPct.Text = "Porcentaje:";
        // 
        // cmbDescuento
        // 
        cmbDescuento.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbDescuento.Location = new Point(140, 18);
        cmbDescuento.Name = "cmbDescuento";
        cmbDescuento.Size = new Size(120, 23);
        cmbDescuento.TabIndex = 0;
        // 
        // gbFormaPago
        // 
        gbFormaPago.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbFormaPago.Controls.Add(rbTarjeta);
        gbFormaPago.Controls.Add(rbTransferencia);
        gbFormaPago.Controls.Add(rbEfectivo);
        gbFormaPago.Location = new Point(15, 8);
        gbFormaPago.Name = "gbFormaPago";
        gbFormaPago.Size = new Size(887, 55);
        gbFormaPago.TabIndex = 0;
        gbFormaPago.TabStop = false;
        gbFormaPago.Text = "Forma de Pago";
        // 
        // rbTarjeta
        // 
        rbTarjeta.AutoSize = true;
        rbTarjeta.Location = new Point(280, 22);
        rbTarjeta.Name = "rbTarjeta";
        rbTarjeta.Size = new Size(59, 19);
        rbTarjeta.TabIndex = 2;
        rbTarjeta.TabStop = true;
        rbTarjeta.Text = "Tarjeta";
        rbTarjeta.UseVisualStyleBackColor = true;
        // 
        // rbTransferencia
        // 
        rbTransferencia.AutoSize = true;
        rbTransferencia.Location = new Point(140, 22);
        rbTransferencia.Name = "rbTransferencia";
        rbTransferencia.Size = new Size(94, 19);
        rbTransferencia.TabIndex = 1;
        rbTransferencia.TabStop = true;
        rbTransferencia.Text = "Transferencia";
        rbTransferencia.UseVisualStyleBackColor = true;
        // 
        // rbEfectivo
        // 
        rbEfectivo.AutoSize = true;
        rbEfectivo.Location = new Point(20, 22);
        rbEfectivo.Name = "rbEfectivo";
        rbEfectivo.Size = new Size(67, 19);
        rbEfectivo.TabIndex = 0;
        rbEfectivo.TabStop = true;
        rbEfectivo.Text = "Efectivo";
        rbEfectivo.UseVisualStyleBackColor = true;
        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] { statusFecha, statusVentasHoy, statusSeparador, statusVersion });
        statusStrip.Location = new Point(0, 617);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(917, 22);
        statusStrip.TabIndex = 3;
        statusStrip.Text = "statusStrip1";
        // 
        // statusFecha
        // 
        statusFecha.Name = "statusFecha";
        statusFecha.Size = new Size(44, 17);
        statusFecha.Text = "Fecha: ";
        // 
        // statusVentasHoy
        // 
        statusVentasHoy.Name = "statusVentasHoy";
        statusVentasHoy.Size = new Size(76, 17);
        statusVentasHoy.Text = "Ventas hoy: 0";
        // 
        // statusSeparador
        // 
        statusSeparador.Name = "statusSeparador";
        statusSeparador.Size = new Size(664, 17);
        statusSeparador.Spring = true;
        // 
        // statusVersion
        // 
        statusVersion.Name = "statusVersion";
        statusVersion.Size = new Size(73, 17);
        statusVersion.Text = "Versión: 1.0";
        // 
        // timerReloj
        // 
        timerReloj.Enabled = true;
        timerReloj.Interval = 60000;
        timerReloj.Tick += TimerReloj_Tick;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.WhiteSmoke;
        ClientSize = new Size(917, 639);
        Controls.Add(panelCentral);
        Controls.Add(panelInferior);
        Controls.Add(statusStrip);
        Controls.Add(panelSuperior);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(640, 520);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Sistema de Gestión de Ventas";
        panelSuperior.ResumeLayout(false);
        panelSuperior.PerformLayout();
        panelEntrada.ResumeLayout(false);
        panelEntrada.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
        toolStripPrincipal.ResumeLayout(false);
        toolStripPrincipal.PerformLayout();
        panelCentral.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
        panelFilaInferiorItems.ResumeLayout(false);
        panelFilaInferiorItems.PerformLayout();
        panelCabeceraGrid.ResumeLayout(false);
        panelCabeceraGrid.PerformLayout();
        panelInferior.ResumeLayout(false);
        panelInferior.PerformLayout();
        flowBotones.ResumeLayout(false);
        gbDescuento.ResumeLayout(false);
        gbDescuento.PerformLayout();
        gbFormaPago.ResumeLayout(false);
        gbFormaPago.PerformLayout();
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private Panel panelSuperior;
    private Panel panelEntrada;
    private Label lblProducto;
    private TextBox txtProducto;
    private Label lblCantidad;
    private NumericUpDown nudCantidad;
    private Label lblPrecio;
    private TextBox txtPrecio;
    private Label lblEnterHint;
    private Button btnAgregar;
    private Panel panelCentral;
    private Panel panelCabeceraGrid;
    private Label lblPreview;
    private DataGridView dgvItems;
    private Panel panelFilaInferiorItems;
    private Button btnLimpiar;
    private Label lblTotal;
    private Panel panelInferior;
    private GroupBox gbFormaPago;
    private RadioButton rbEfectivo;
    private RadioButton rbTransferencia;
    private RadioButton rbTarjeta;
    private GroupBox gbDescuento;
    private Label lblDescuentoPct;
    private ComboBox cmbDescuento;
    private FlowLayoutPanel flowBotones;
    private Button btnGuardar;
    private ToolStrip toolStripPrincipal;
    private ToolStripButton tsbVerHistorial;
    private ToolStripButton tsbGestionVentas;
    private ToolStripButton tsbEstadisticas;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel statusFecha;
    private ToolStripStatusLabel statusVentasHoy;
    private ToolStripStatusLabel statusSeparador;
    private ToolStripStatusLabel statusVersion;
    private System.Windows.Forms.Timer timerReloj;
}
