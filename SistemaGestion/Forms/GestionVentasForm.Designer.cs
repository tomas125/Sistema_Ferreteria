#nullable disable
namespace SistemaGestion.Forms;

partial class GestionVentasForm
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
        lblInfo = new Label();
        dgvVentas = new DataGridView();
        panelBottom = new Panel();
        btnFinalizar = new Button();
        btnCerrar = new Button();
        panelTop.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvVentas).BeginInit();
        panelBottom.SuspendLayout();
        SuspendLayout();
        //
        // panelTop
        //
        panelTop.Controls.Add(lblTitulo);
        panelTop.Controls.Add(lblInfo);
        panelTop.Dock = DockStyle.Top;
        panelTop.Height = 72;
        panelTop.Padding = new Padding(10, 8, 10, 4);
        //
        // lblTitulo
        //
        lblTitulo.AutoSize = true;
        lblTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblTitulo.Location = new Point(10, 8);
        lblTitulo.Text = "Gestión de ventas (estado)";
        //
        // lblInfo
        //
        lblInfo.AutoSize = true;
        lblInfo.Location = new Point(10, 36);
        lblInfo.MaximumSize = new Size(720, 0);
        lblInfo.Text = "Ventas nuevas quedan en Pendiente hasta marcarlas FINALIZADO aquí. El historial lista todas.";
        //
        // dgvVentas
        //
        dgvVentas.AllowUserToAddRows = false;
        dgvVentas.AllowUserToDeleteRows = false;
        dgvVentas.Dock = DockStyle.Fill;
        dgvVentas.MultiSelect = false;
        dgvVentas.ReadOnly = true;
        dgvVentas.RowHeadersVisible = false;
        dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvVentas.CellDoubleClick += DgvVentas_CellDoubleClick;
        //
        // panelBottom
        //
        panelBottom.Controls.Add(btnFinalizar);
        panelBottom.Controls.Add(btnCerrar);
        panelBottom.Dock = DockStyle.Bottom;
        panelBottom.Height = 44;
        //
        // btnFinalizar
        //
        btnFinalizar.Location = new Point(10, 8);
        btnFinalizar.Size = new Size(200, 28);
        btnFinalizar.Text = "Marcar como FINALIZADO";
        btnFinalizar.BackColor = Color.SeaGreen;
        btnFinalizar.FlatStyle = FlatStyle.Flat;
        btnFinalizar.ForeColor = Color.White;
        btnFinalizar.UseVisualStyleBackColor = false;
        btnFinalizar.Click += BtnFinalizar_Click;
        //
        // btnCerrar
        //
        btnCerrar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCerrar.Location = new Point(620, 8);
        btnCerrar.Text = "Cerrar";
        btnCerrar.DialogResult = DialogResult.Cancel;
        btnCerrar.Click += BtnCerrar_Click;
        //
        // GestionVentasForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 420);
        Controls.Add(dgvVentas);
        Controls.Add(panelBottom);
        Controls.Add(panelTop);
        Font = new Font("Segoe UI", 9F);
        BackColor = Color.WhiteSmoke;
        Text = "Gestión de ventas";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(600, 320);
        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)dgvVentas).EndInit();
        panelBottom.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Panel panelTop;
    private Label lblTitulo;
    private Label lblInfo;
    private DataGridView dgvVentas;
    private Panel panelBottom;
    private Button btnFinalizar;
    private Button btnCerrar;
}
