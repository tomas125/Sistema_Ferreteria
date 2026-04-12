#nullable disable
namespace SistemaGestion.Forms;

partial class HistorialForm
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
        panelFiltros = new Panel();
        lblDesde = new Label();
        dtpDesde = new DateTimePicker();
        lblHasta = new Label();
        dtpHasta = new DateTimePicker();
        lblForma = new Label();
        cmbFormaPago = new ComboBox();
        lblEstado = new Label();
        cmbEstado = new ComboBox();
        btnFiltrar = new Button();
        btnExportar = new Button();
        dgvVentas = new DataGridView();
        panelBottom = new Panel();
        btnMarcarFinalizado = new Button();
        btnCerrar = new Button();
        panelFiltros.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
        panelBottom.SuspendLayout();
        SuspendLayout();
        //
        // panelFiltros
        //
        panelFiltros.Controls.Add(lblDesde);
        panelFiltros.Controls.Add(dtpDesde);
        panelFiltros.Controls.Add(lblHasta);
        panelFiltros.Controls.Add(dtpHasta);
        panelFiltros.Controls.Add(lblForma);
        panelFiltros.Controls.Add(cmbFormaPago);
        panelFiltros.Controls.Add(lblEstado);
        panelFiltros.Controls.Add(cmbEstado);
        panelFiltros.Controls.Add(btnFiltrar);
        panelFiltros.Controls.Add(btnExportar);
        panelFiltros.Dock = DockStyle.Top;
        panelFiltros.Height = 100;
        panelFiltros.Padding = new Padding(8);
        //
        // lblDesde
        //
        lblDesde.AutoSize = true;
        lblDesde.Location = new Point(8, 12);
        lblDesde.Text = "Desde:";
        //
        // dtpDesde
        //
        dtpDesde.Format = DateTimePickerFormat.Short;
        dtpDesde.Location = new Point(60, 8);
        dtpDesde.Width = 110;
        //
        // lblHasta
        //
        lblHasta.AutoSize = true;
        lblHasta.Location = new Point(190, 12);
        lblHasta.Text = "Hasta:";
        //
        // dtpHasta
        //
        dtpHasta.Format = DateTimePickerFormat.Short;
        dtpHasta.Location = new Point(240, 8);
        dtpHasta.Width = 110;
        //
        // lblForma
        //
        lblForma.AutoSize = true;
        lblForma.Location = new Point(8, 48);
        lblForma.Text = "Forma de pago:";
        //
        // cmbFormaPago
        //
        cmbFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbFormaPago.Location = new Point(110, 44);
        cmbFormaPago.Width = 140;
        //
        // lblEstado
        //
        lblEstado.AutoSize = true;
        lblEstado.Location = new Point(270, 48);
        lblEstado.Text = "Estado:";
        //
        // cmbEstado
        //
        cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbEstado.Location = new Point(330, 44);
        cmbEstado.Width = 140;
        //
        // btnFiltrar
        //
        btnFiltrar.Location = new Point(490, 40);
        btnFiltrar.Text = "Filtrar";
        btnFiltrar.Width = 90;
        btnFiltrar.Click += BtnFiltrar_Click;
        //
        // btnExportar
        //
        btnExportar.Location = new Point(590, 40);
        btnExportar.Text = "Exportar CSV";
        btnExportar.Width = 110;
        btnExportar.Click += BtnExportar_Click;
        //
        // dgvVentas
        //
        dgvVentas.AllowUserToAddRows = false;
        dgvVentas.AllowUserToDeleteRows = false;
        dgvVentas.Dock = DockStyle.Fill;
        dgvVentas.MultiSelect = true;
        dgvVentas.ReadOnly = true;
        dgvVentas.RowHeadersVisible = false;
        dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvVentas.CellDoubleClick += DgvVentas_CellDoubleClick;
        //
        // panelBottom
        //
        panelBottom.Controls.Add(btnMarcarFinalizado);
        panelBottom.Controls.Add(btnCerrar);
        panelBottom.Dock = DockStyle.Bottom;
        panelBottom.Height = 44;
        //
        // btnMarcarFinalizado
        //
        btnMarcarFinalizado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnMarcarFinalizado.Location = new Point(360, 8);
        btnMarcarFinalizado.Size = new Size(260, 28);
        btnMarcarFinalizado.Text = "Marcar selección como FINALIZADO";
        btnMarcarFinalizado.UseVisualStyleBackColor = true;
        btnMarcarFinalizado.Click += BtnMarcarFinalizado_Click;
        //
        // btnCerrar
        //
        btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCerrar.Location = new Point(630, 8);
        btnCerrar.Size = new Size(100, 28);
        btnCerrar.Text = "Cerrar";
        btnCerrar.DialogResult = DialogResult.Cancel;
        //
        // HistorialForm
        //
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(dgvVentas);
        Controls.Add(panelBottom);
        Controls.Add(panelFiltros);
        Font = new Font("Segoe UI", 9F);
        BackColor = Color.WhiteSmoke;
        Text = "Historial de ventas";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(600, 350);
        panelFiltros.ResumeLayout(false);
        panelFiltros.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
        panelBottom.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Panel panelFiltros;
    private Label lblDesde;
    private DateTimePicker dtpDesde;
    private Label lblHasta;
    private DateTimePicker dtpHasta;
    private Label lblForma;
    private ComboBox cmbFormaPago;
    private Label lblEstado;
    private ComboBox cmbEstado;
    private Button btnFiltrar;
    private Button btnExportar;
    private DataGridView dgvVentas;
    private Panel panelBottom;
    private Button btnMarcarFinalizado;
    private Button btnCerrar;
}
