#nullable disable
namespace SistemaGestion.Forms;

partial class EstadisticasForm
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
        lblMes = new Label();
        lblTotalMes = new Label();
        lblFormaPago = new Label();
        btnExportarMes = new Button();
        btnCerrar = new Button();
        SuspendLayout();
        //
        // lblMes
        //
        lblMes.AutoSize = true;
        lblMes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblMes.Location = new Point(16, 16);
        lblMes.Text = "Mes en curso";
        //
        // lblTotalMes
        //
        lblTotalMes.AutoSize = true;
        lblTotalMes.Location = new Point(16, 48);
        lblTotalMes.MaximumSize = new Size(640, 0);
        lblTotalMes.Text = "Total vendido ($): —";
        //
        // lblFormaPago
        //
        lblFormaPago.AutoSize = true;
        lblFormaPago.Location = new Point(16, 76);
        lblFormaPago.MaximumSize = new Size(640, 0);
        lblFormaPago.Text = "Forma de pago más utilizada: —";
        //
        // btnExportarMes
        //
        btnExportarMes.Location = new Point(16, 118);
        btnExportarMes.Size = new Size(220, 30);
        btnExportarMes.Text = "Exportar historial del mes (CSV)";
        btnExportarMes.Click += BtnExportarMes_Click;
        //
        // btnCerrar
        //
        btnCerrar.DialogResult = DialogResult.Cancel;
        btnCerrar.Text = "Cerrar";
        btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCerrar.Location = new Point(560, 200);
        //
        // EstadisticasForm
        //
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(680, 250);
        Controls.Add(btnCerrar);
        Controls.Add(btnExportarMes);
        Controls.Add(lblFormaPago);
        Controls.Add(lblTotalMes);
        Controls.Add(lblMes);
        Font = new Font("Segoe UI", 9F);
        BackColor = Color.WhiteSmoke;
        Text = "Estadísticas";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(500, 260);
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblMes;
    private Label lblTotalMes;
    private Label lblFormaPago;
    private Button btnExportarMes;
    private Button btnCerrar;
}
