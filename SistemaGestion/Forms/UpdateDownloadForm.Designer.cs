#nullable disable
namespace SistemaGestion.Forms;

partial class UpdateDownloadForm
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
        lblEstado = new Label();
        progressBar = new ProgressBar();
        btnCancelar = new Button();
        SuspendLayout();
        //
        // lblEstado
        //
        lblEstado.AutoSize = false;
        lblEstado.Location = new Point(16, 16);
        lblEstado.Size = new Size(448, 40);
        lblEstado.Text = "Preparando descarga…";
        //
        // progressBar
        //
        progressBar.Location = new Point(16, 64);
        progressBar.MarqueeAnimationSpeed = 30;
        progressBar.Name = "progressBar";
        progressBar.Size = new Size(448, 24);
        progressBar.Style = ProgressBarStyle.Marquee;
        progressBar.TabIndex = 1;
        //
        // btnCancelar
        //
        btnCancelar.Location = new Point(369, 104);
        btnCancelar.Name = "btnCancelar";
        btnCancelar.Size = new Size(95, 28);
        btnCancelar.TabIndex = 2;
        btnCancelar.Text = "Cancelar";
        btnCancelar.UseVisualStyleBackColor = true;
        btnCancelar.Click += BtnCancelar_Click;
        //
        // UpdateDownloadForm
        //
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = btnCancelar;
        ClientSize = new Size(480, 148);
        Controls.Add(btnCancelar);
        Controls.Add(progressBar);
        Controls.Add(lblEstado);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "UpdateDownloadForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Descargando actualización";
        Shown += UpdateDownloadForm_Shown;
        ResumeLayout(false);
    }

    private Label lblEstado;
    private ProgressBar progressBar;
    private Button btnCancelar;
}
