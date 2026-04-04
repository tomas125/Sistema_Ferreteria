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
        btnAgregar = new Button();
        panelCentral = new Panel();
        lblPreview = new Label();
        dgvItems = new DataGridView();
        lblTotal = new Label();
        panelInferior = new Panel();
        gbFormaPago = new GroupBox();
        rbTarjeta = new RadioButton();
        rbTransferencia = new RadioButton();
        rbEfectivo = new RadioButton();
        flowBotones = new FlowLayoutPanel();
        btnGuardar = new Button();
        btnLimpiar = new Button();
        toolStripPrincipal = new ToolStrip();
        tsbVerHistorial = new ToolStripButton();
        tsbEstadisticas = new ToolStripButton();
        statusStrip = new StatusStrip();
        statusFecha = new ToolStripStatusLabel();
        statusVentasHoy = new ToolStripStatusLabel();
        timerReloj = new System.Windows.Forms.Timer(components);
        panelSuperior.SuspendLayout();
        panelEntrada.SuspendLayout();
        panelCentral.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
        panelInferior.SuspendLayout();
        gbFormaPago.SuspendLayout();
        flowBotones.SuspendLayout();
        toolStripPrincipal.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();
        //
        // panelSuperior
        //
        panelSuperior.BackColor = Color.WhiteSmoke;
        panelSuperior.Dock = DockStyle.Top;
        panelSuperior.Location = new Point(0, 0);
        panelSuperior.Name = "panelSuperior";
        panelSuperior.Padding = new Padding(0);
        panelSuperior.Size = new Size(884, 158);
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
        panelEntrada.Controls.Add(btnAgregar);
        panelEntrada.Dock = DockStyle.Fill;
        panelEntrada.Name = "panelEntrada";
        panelEntrada.Padding = new Padding(12, 8, 12, 8);
        panelEntrada.TabIndex = 1;
        //
        // lblProducto
        //
        lblProducto.AutoSize = true;
        lblProducto.Location = new Point(15, 15);
        lblProducto.Name = "lblProducto";
        lblProducto.Size = new Size(108, 15);
        lblProducto.TabIndex = 0;
        lblProducto.Text = "Producto/Servicio:";
        //
        // txtProducto
        //
        txtProducto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtProducto.Location = new Point(140, 12);
        txtProducto.Name = "txtProducto";
        txtProducto.Size = new Size(715, 23);
        txtProducto.TabIndex = 1;
        //
        // lblCantidad
        //
        lblCantidad.AutoSize = true;
        lblCantidad.Location = new Point(15, 48);
        lblCantidad.Name = "lblCantidad";
        lblCantidad.Size = new Size(58, 15);
        lblCantidad.TabIndex = 2;
        lblCantidad.Text = "Cantidad:";
        //
        // nudCantidad
        //
        nudCantidad.Location = new Point(140, 45);
        nudCantidad.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
        nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudCantidad.Name = "nudCantidad";
        nudCantidad.Size = new Size(100, 23);
        nudCantidad.TabIndex = 3;
        nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
        //
        // lblPrecio
        //
        lblPrecio.AutoSize = true;
        lblPrecio.Location = new Point(15, 81);
        lblPrecio.Name = "lblPrecio";
        lblPrecio.Size = new Size(78, 15);
        lblPrecio.TabIndex = 4;
        lblPrecio.Text = "Precio ($):";
        //
        // txtPrecio
        //
        txtPrecio.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtPrecio.Location = new Point(140, 78);
        txtPrecio.Name = "txtPrecio";
        txtPrecio.Size = new Size(715, 23);
        txtPrecio.TabIndex = 5;
        //
        // btnAgregar
        //
        btnAgregar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnAgregar.BackColor = Color.DodgerBlue;
        btnAgregar.FlatStyle = FlatStyle.Flat;
        btnAgregar.ForeColor = Color.White;
        btnAgregar.Location = new Point(720, 104);
        btnAgregar.Name = "btnAgregar";
        btnAgregar.Size = new Size(135, 28);
        btnAgregar.TabIndex = 6;
        btnAgregar.Text = "Agregar";
        btnAgregar.UseVisualStyleBackColor = false;
        btnAgregar.Click += BtnAgregar_Click;
        //
        // panelCentral
        //
        panelCentral.BackColor = Color.WhiteSmoke;
        panelCentral.Controls.Add(lblPreview);
        panelCentral.Controls.Add(dgvItems);
        panelCentral.Controls.Add(lblTotal);
        panelCentral.Dock = DockStyle.Fill;
        panelCentral.Location = new Point(0, 130);
        panelCentral.Name = "panelCentral";
        panelCentral.Padding = new Padding(12, 8, 12, 8);
        panelCentral.Size = new Size(884, 318);
        panelCentral.TabIndex = 1;
        //
        // lblPreview
        //
        lblPreview.AutoSize = true;
        lblPreview.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblPreview.Location = new Point(15, 8);
        lblPreview.Name = "lblPreview";
        lblPreview.Size = new Size(55, 15);
        lblPreview.TabIndex = 0;
        lblPreview.Text = "PREVISUALIZACIÓN:";
        //
        // dgvItems
        //
        dgvItems.AllowUserToAddRows = false;
        dgvItems.AllowUserToDeleteRows = true;
        dgvItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        dgvItems.BackgroundColor = Color.White;
        dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvItems.Location = new Point(15, 28);
        dgvItems.MultiSelect = false;
        dgvItems.Name = "dgvItems";
        dgvItems.ReadOnly = false;
        dgvItems.RowHeadersVisible = false;
        dgvItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvItems.Size = new Size(854, 240);
        dgvItems.TabIndex = 1;
        //
        // lblTotal
        //
        lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        lblTotal.AutoSize = true;
        lblTotal.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTotal.ForeColor = Color.DarkGreen;
        lblTotal.Location = new Point(650, 278);
        lblTotal.Name = "lblTotal";
        lblTotal.Size = new Size(96, 21);
        lblTotal.TabIndex = 2;
        lblTotal.Text = "TOTAL: $0.00";
        //
        // panelInferior
        //
        panelInferior.BackColor = Color.WhiteSmoke;
        panelInferior.Controls.Add(gbFormaPago);
        panelInferior.Controls.Add(flowBotones);
        panelInferior.Dock = DockStyle.Bottom;
        panelInferior.Location = new Point(0, 448);
        panelInferior.Name = "panelInferior";
        panelInferior.Padding = new Padding(12, 8, 12, 8);
        panelInferior.Size = new Size(884, 110);
        panelInferior.TabIndex = 2;
        //
        // gbFormaPago
        //
        gbFormaPago.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        gbFormaPago.Controls.Add(rbTarjeta);
        gbFormaPago.Controls.Add(rbTransferencia);
        gbFormaPago.Controls.Add(rbEfectivo);
        gbFormaPago.Location = new Point(15, 8);
        gbFormaPago.Name = "gbFormaPago";
        gbFormaPago.Size = new Size(854, 55);
        gbFormaPago.TabIndex = 0;
        gbFormaPago.TabStop = false;
        gbFormaPago.Text = "Forma de Pago";
        //
        // rbTarjeta
        //
        rbTarjeta.AutoSize = true;
        rbTarjeta.Location = new Point(280, 22);
        rbTarjeta.Name = "rbTarjeta";
        rbTarjeta.Size = new Size(60, 19);
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
        rbTransferencia.Size = new Size(99, 19);
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
        // flowBotones
        //
        flowBotones.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        flowBotones.AutoSize = true;
        flowBotones.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        flowBotones.Controls.Add(btnGuardar);
        flowBotones.Controls.Add(btnLimpiar);
        flowBotones.FlowDirection = FlowDirection.LeftToRight;
        flowBotones.Location = new Point(12, 68);
        flowBotones.Name = "flowBotones";
        flowBotones.Size = new Size(280, 36);
        flowBotones.TabIndex = 1;
        flowBotones.WrapContents = false;
        //
        // toolStripPrincipal
        //
        toolStripPrincipal.BackColor = Color.WhiteSmoke;
        toolStripPrincipal.GripStyle = ToolStripGripStyle.Hidden;
        toolStripPrincipal.ImageScalingSize = new Size(20, 20);
        toolStripPrincipal.Items.AddRange(new ToolStripItem[] { tsbVerHistorial, tsbEstadisticas });
        toolStripPrincipal.Location = new Point(0, 0);
        toolStripPrincipal.Name = "toolStripPrincipal";
        toolStripPrincipal.Padding = new Padding(8, 2, 8, 2);
        toolStripPrincipal.Size = new Size(884, 28);
        toolStripPrincipal.TabIndex = 4;
        toolStripPrincipal.Text = "toolStripPrincipal";
        toolStripPrincipal.Dock = DockStyle.Top;
        toolStripPrincipal.RenderMode = ToolStripRenderMode.System;
        toolStripPrincipal.Stretch = true;
        toolStripPrincipal.TabIndex = 0;
        //
        // tsbVerHistorial
        //
        tsbVerHistorial.DisplayStyle = ToolStripItemDisplayStyle.Text;
        tsbVerHistorial.Name = "tsbVerHistorial";
        tsbVerHistorial.Size = new Size(85, 21);
        tsbVerHistorial.Text = "Ver Historial";
        tsbVerHistorial.BackColor = Color.FromArgb(173, 214, 255);
        tsbVerHistorial.ForeColor = Color.FromArgb(0, 51, 102);
        tsbVerHistorial.Margin = new Padding(0, 0, 8, 0);
        tsbVerHistorial.Padding = new Padding(10, 2, 10, 2);
        tsbVerHistorial.Click += BtnVerHistorial_Click;
        //
        // tsbEstadisticas
        //
        tsbEstadisticas.DisplayStyle = ToolStripItemDisplayStyle.Text;
        tsbEstadisticas.Name = "tsbEstadisticas";
        tsbEstadisticas.Size = new Size(78, 21);
        tsbEstadisticas.Text = "Estadísticas";
        tsbEstadisticas.BackColor = Color.White;
        tsbEstadisticas.ForeColor = SystemColors.ControlText;
        tsbEstadisticas.Padding = new Padding(10, 2, 10, 2);
        tsbEstadisticas.Click += BtnEstadisticas_Click;
        panelSuperior.Controls.Add(panelEntrada);
        panelSuperior.Controls.Add(toolStripPrincipal);
        //
        // btnGuardar
        //
        btnGuardar.BackColor = Color.SeaGreen;
        btnGuardar.FlatStyle = FlatStyle.Flat;
        btnGuardar.ForeColor = Color.White;
        btnGuardar.Margin = new Padding(3, 3, 12, 3);
        btnGuardar.Name = "btnGuardar";
        btnGuardar.Size = new Size(120, 30);
        btnGuardar.TabIndex = 0;
        btnGuardar.Text = "Guardar";
        btnGuardar.UseVisualStyleBackColor = false;
        btnGuardar.Click += BtnGuardar_Click;
        //
        // btnLimpiar
        //
        btnLimpiar.BackColor = Color.Tomato;
        btnLimpiar.FlatStyle = FlatStyle.Flat;
        btnLimpiar.ForeColor = Color.White;
        btnLimpiar.Margin = new Padding(3, 3, 12, 3);
        btnLimpiar.Name = "btnLimpiar";
        btnLimpiar.Size = new Size(120, 30);
        btnLimpiar.TabIndex = 1;
        btnLimpiar.Text = "Limpiar";
        btnLimpiar.UseVisualStyleBackColor = false;
        btnLimpiar.Click += BtnLimpiar_Click;
        //
        // statusStrip
        //
        statusStrip.Dock = DockStyle.Bottom;
        statusStrip.Items.AddRange(new ToolStripItem[] { statusFecha, statusVentasHoy });
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(884, 22);
        statusStrip.TabIndex = 3;
        statusStrip.Text = "statusStrip1";
        //
        // statusFecha
        //
        statusFecha.Name = "statusFecha";
        statusFecha.Size = new Size(120, 17);
        statusFecha.Text = "Fecha: ";
        //
        // statusVentasHoy
        //
        statusVentasHoy.Name = "statusVentasHoy";
        statusVentasHoy.Size = new Size(150, 17);
        statusVentasHoy.Text = "Ventas hoy: 0";
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
        ClientSize = new Size(884, 580);
        Controls.Add(panelCentral);
        Controls.Add(panelInferior);
        Controls.Add(statusStrip);
        Controls.Add(panelSuperior);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(700, 500);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Sistema de Gestión de Ventas";
        panelEntrada.ResumeLayout(false);
        panelEntrada.PerformLayout();
        panelSuperior.ResumeLayout(false);
        panelSuperior.PerformLayout();
        panelCentral.ResumeLayout(false);
        panelCentral.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
        panelInferior.ResumeLayout(false);
        panelInferior.PerformLayout();
        gbFormaPago.ResumeLayout(false);
        gbFormaPago.PerformLayout();
        flowBotones.ResumeLayout(false);
        toolStripPrincipal.ResumeLayout(false);
        toolStripPrincipal.PerformLayout();
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
    private Button btnAgregar;
    private Panel panelCentral;
    private Label lblPreview;
    private DataGridView dgvItems;
    private Label lblTotal;
    private Panel panelInferior;
    private GroupBox gbFormaPago;
    private RadioButton rbEfectivo;
    private RadioButton rbTransferencia;
    private RadioButton rbTarjeta;
    private FlowLayoutPanel flowBotones;
    private Button btnGuardar;
    private Button btnLimpiar;
    private ToolStrip toolStripPrincipal;
    private ToolStripButton tsbVerHistorial;
    private ToolStripButton tsbEstadisticas;
    private StatusStrip statusStrip;
    private ToolStripStatusLabel statusFecha;
    private ToolStripStatusLabel statusVentasHoy;
    private System.Windows.Forms.Timer timerReloj;
}
