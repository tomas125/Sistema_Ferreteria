#nullable disable
namespace SistemaGestion.Forms;

partial class DetalleVentaForm
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
        panelTop = new Panel();
        lblTitulo = new Label();
        lblFecha = new Label();
        lblCliente = new Label();
        lblFormaPagoHdr = new Label();
        lblEstadoHdr = new Label();
        panelFill = new Panel();
        dgvDetalle = new DataGridView();
        panelTotal = new Panel();
        lblTotalTitulo = new Label();
        lblTotalValor = new Label();
        gbDistribucion = new GroupBox();
        flowSocios = new FlowLayoutPanel();
        panelEstado = new Panel();
        lblCambiarEstado = new Label();
        cmbEstado = new ComboBox();
        btnActualizarEstado = new Button();
        btnCerrar = new Button();
        panelTop.SuspendLayout();
        panelFill.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
        panelTotal.SuspendLayout();
        gbDistribucion.SuspendLayout();
        panelEstado.SuspendLayout();
        SuspendLayout();
        //
        // panelTop
        //
        panelTop.Controls.Add(lblTitulo);
        panelTop.Controls.Add(lblFecha);
        panelTop.Controls.Add(lblCliente);
        panelTop.Controls.Add(lblFormaPagoHdr);
        panelTop.Controls.Add(lblEstadoHdr);
        panelTop.Dock = DockStyle.Top;
        panelTop.Height = 118;
        panelTop.Padding = new Padding(10, 8, 10, 8);
        //
        // lblTitulo
        //
        lblTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTitulo.AutoSize = true;
        lblTitulo.Location = new Point(10, 8);
        lblTitulo.Text = "Detalle de venta";
        //
        // lblFecha
        //
        lblFecha.AutoSize = true;
        lblFecha.Location = new Point(10, 36);
        lblFecha.Name = "lblFecha";
        lblFecha.Size = new Size(200, 15);
        lblFecha.Text = "Fecha:";
        //
        // lblCliente
        //
        lblCliente.AutoSize = true;
        lblCliente.Location = new Point(10, 56);
        lblCliente.MaximumSize = new Size(520, 0);
        lblCliente.Text = "Cliente:";
        //
        // lblFormaPagoHdr
        //
        lblFormaPagoHdr.AutoSize = true;
        lblFormaPagoHdr.Location = new Point(10, 76);
        lblFormaPagoHdr.Text = "Forma de pago:";
        //
        // lblEstadoHdr
        //
        lblEstadoHdr.AutoSize = true;
        lblEstadoHdr.Location = new Point(420, 36);
        lblEstadoHdr.Text = "Estado:";
        //
        // panelFill
        //
        panelFill.Controls.Add(dgvDetalle);
        panelFill.Dock = DockStyle.Fill;
        //
        // dgvDetalle
        //
        dgvDetalle.AllowUserToAddRows = false;
        dgvDetalle.AllowUserToDeleteRows = false;
        dgvDetalle.Dock = DockStyle.Fill;
        dgvDetalle.ReadOnly = true;
        dgvDetalle.RowHeadersVisible = false;
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        //
        // panelTotal
        //
        panelTotal.Controls.Add(lblTotalTitulo);
        panelTotal.Controls.Add(lblTotalValor);
        panelTotal.Dock = DockStyle.Bottom;
        panelTotal.Height = 40;
        //
        // lblTotalTitulo
        //
        lblTotalTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblTotalTitulo.AutoSize = true;
        lblTotalTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTotalTitulo.ForeColor = Color.DarkGreen;
        lblTotalTitulo.Location = new Point(480, 10);
        lblTotalTitulo.Text = "TOTAL:";
        //
        // lblTotalValor
        //
        lblTotalValor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblTotalValor.AutoSize = true;
        lblTotalValor.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblTotalValor.ForeColor = Color.DarkGreen;
        lblTotalValor.Location = new Point(560, 10);
        lblTotalValor.Text = "$0.00";
        //
        // gbDistribucion
        //
        gbDistribucion.Controls.Add(flowSocios);
        gbDistribucion.Dock = DockStyle.Bottom;
        gbDistribucion.Height = 130;
        gbDistribucion.Text = "Distribución de ganancias (socios)";
        //
        // flowSocios
        //
        flowSocios.Dock = DockStyle.Fill;
        flowSocios.FlowDirection = FlowDirection.TopDown;
        flowSocios.AutoScroll = true;
        flowSocios.Padding = new Padding(8);
        //
        // panelEstado
        //
        panelEstado.Controls.Add(lblCambiarEstado);
        panelEstado.Controls.Add(cmbEstado);
        panelEstado.Controls.Add(btnActualizarEstado);
        panelEstado.Dock = DockStyle.Bottom;
        panelEstado.Height = 44;
        //
        // lblCambiarEstado
        //
        lblCambiarEstado.AutoSize = true;
        lblCambiarEstado.Location = new Point(10, 12);
        lblCambiarEstado.Text = "Cambiar estado:";
        //
        // cmbEstado
        //
        cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbEstado.Location = new Point(130, 8);
        cmbEstado.Size = new Size(180, 23);
        //
        // btnActualizarEstado
        //
        btnActualizarEstado.Location = new Point(320, 7);
        btnActualizarEstado.Text = "Actualizar estado";
        btnActualizarEstado.AutoSize = true;
        btnActualizarEstado.Click += BtnActualizarEstado_Click;
        //
        // btnCerrar
        //
        btnCerrar.DialogResult = DialogResult.Cancel;
        btnCerrar.Dock = DockStyle.Bottom;
        btnCerrar.Height = 36;
        btnCerrar.Text = "Cerrar";
        //
        // DetalleVentaForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.WhiteSmoke;
        ClientSize = new Size(720, 560);
        Controls.Add(panelFill);
        Controls.Add(panelTotal);
        Controls.Add(gbDistribucion);
        Controls.Add(panelEstado);
        Controls.Add(btnCerrar);
        Controls.Add(panelTop);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(600, 480);
        Name = "DetalleVentaForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Detalle de venta";
        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        panelFill.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
        panelTotal.ResumeLayout(false);
        panelTotal.PerformLayout();
        gbDistribucion.ResumeLayout(false);
        panelEstado.ResumeLayout(false);
        panelEstado.PerformLayout();
        ResumeLayout(false);
    }

    private Panel panelTop;
    private Label lblTitulo;
    private Label lblFecha;
    private Label lblCliente;
    private Label lblFormaPagoHdr;
    private Label lblEstadoHdr;
    private Panel panelFill;
    private DataGridView dgvDetalle;
    private Panel panelTotal;
    private Label lblTotalTitulo;
    private Label lblTotalValor;
    private GroupBox gbDistribucion;
    private FlowLayoutPanel flowSocios;
    private Panel panelEstado;
    private Label lblCambiarEstado;
    private ComboBox cmbEstado;
    private Button btnActualizarEstado;
    private Button btnCerrar;
}
