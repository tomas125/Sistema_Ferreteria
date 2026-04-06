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
        btnBackup = new Button();
        SuspendLayout();
        // 
        // lblMes
        // 
        lblMes.AutoSize = true;
        lblMes.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblMes.Location = new Point(16, 16);
        lblMes.Name = "lblMes";
        lblMes.Size = new Size(96, 19);
        lblMes.TabIndex = 4;
        lblMes.Text = "Mes en curso";
        // 
        // lblTotalMes
        // 
        lblTotalMes.AutoSize = true;
        lblTotalMes.Location = new Point(16, 48);
        lblTotalMes.MaximumSize = new Size(640, 0);
        lblTotalMes.Name = "lblTotalMes";
        lblTotalMes.Size = new Size(113, 15);
        lblTotalMes.TabIndex = 3;
        lblTotalMes.Text = "Total vendido ($): —";
        // 
        // lblFormaPago
        // 
        lblFormaPago.AutoSize = true;
        lblFormaPago.Location = new Point(16, 76);
        lblFormaPago.MaximumSize = new Size(640, 0);
        lblFormaPago.Name = "lblFormaPago";
        lblFormaPago.Size = new Size(177, 15);
        lblFormaPago.TabIndex = 2;
        lblFormaPago.Text = "Forma de pago más utilizada: —";
        // 
        // btnExportarMes
        // 
        btnExportarMes.Location = new Point(16, 118);
        btnExportarMes.Name = "btnExportarMes";
        btnExportarMes.Size = new Size(220, 30);
        btnExportarMes.TabIndex = 1;
        btnExportarMes.Text = "Exportar historial del mes (CSV)";
        btnExportarMes.Click += BtnExportarMes_Click;
        // 
        // btnCerrar
        // 
        btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCerrar.DialogResult = DialogResult.Cancel;
        btnCerrar.Location = new Point(560, 200);
        btnCerrar.Name = "btnCerrar";
        btnCerrar.Size = new Size(75, 23);
        btnCerrar.TabIndex = 0;
        btnCerrar.Text = "Cerrar";
        // 
        // btnBackup
        // 
        btnBackup.BackColor = Color.White;
        btnBackup.Location = new Point(16, 169);
        btnBackup.Name = "btnBackup";
        btnBackup.Size = new Size(96, 23);
        btnBackup.TabIndex = 5;
        btnBackup.Text = "Backup";
        btnBackup.UseVisualStyleBackColor = false;
        btnBackup.Click += btnBackup_Click;
        // 
        // EstadisticasForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.WhiteSmoke;
        ClientSize = new Size(680, 250);
        Controls.Add(btnBackup);
        Controls.Add(btnCerrar);
        Controls.Add(btnExportarMes);
        Controls.Add(lblFormaPago);
        Controls.Add(lblTotalMes);
        Controls.Add(lblMes);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(500, 260);
        Name = "EstadisticasForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Estadísticas";
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblMes;
    private Label lblTotalMes;
    private Label lblFormaPago;
    private Button btnExportarMes;
    private Button btnCerrar;
    private Button btnBackup;
}
