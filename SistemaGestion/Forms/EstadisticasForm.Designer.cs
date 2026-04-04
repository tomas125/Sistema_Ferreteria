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
        lblProyectosCerrados = new Label();
        lblFormaPago = new Label();
        lblProyeccion = new Label();
        lblSociosTitulo = new Label();
        flowSocios = new FlowLayoutPanel();
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
        // lblProyectosCerrados
        //
        lblProyectosCerrados.AutoSize = true;
        lblProyectosCerrados.Location = new Point(16, 76);
        lblProyectosCerrados.Text = "Proyectos cerrados (Entregado): —";
        //
        // lblFormaPago
        //
        lblFormaPago.AutoSize = true;
        lblFormaPago.Location = new Point(16, 104);
        lblFormaPago.MaximumSize = new Size(640, 0);
        lblFormaPago.Text = "Forma de pago más utilizada: —";
        //
        // lblProyeccion
        //
        lblProyeccion.AutoSize = true;
        lblProyeccion.Location = new Point(16, 132);
        lblProyeccion.MaximumSize = new Size(640, 0);
        lblProyeccion.Text = "Proyección del mes ($): —";
        //
        // lblSociosTitulo
        //
        lblSociosTitulo.AutoSize = true;
        lblSociosTitulo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblSociosTitulo.Location = new Point(16, 170);
        lblSociosTitulo.Text = "Ganancia por socio (mes)";
        //
        // flowSocios
        //
        flowSocios.FlowDirection = FlowDirection.TopDown;
        flowSocios.Location = new Point(16, 195);
        flowSocios.Size = new Size(640, 120);
        flowSocios.AutoScroll = true;
        //
        // btnCerrar
        //
        btnCerrar.DialogResult = DialogResult.Cancel;
        btnCerrar.Text = "Cerrar";
        btnCerrar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnCerrar.Location = new Point(560, 340);
        //
        // EstadisticasForm
        //
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(680, 390);
        Controls.Add(btnCerrar);
        Controls.Add(flowSocios);
        Controls.Add(lblSociosTitulo);
        Controls.Add(lblProyeccion);
        Controls.Add(lblFormaPago);
        Controls.Add(lblProyectosCerrados);
        Controls.Add(lblTotalMes);
        Controls.Add(lblMes);
        Font = new Font("Segoe UI", 9F);
        BackColor = Color.WhiteSmoke;
        Text = "Estadísticas";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(500, 320);
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblMes;
    private Label lblTotalMes;
    private Label lblProyectosCerrados;
    private Label lblFormaPago;
    private Label lblProyeccion;
    private Label lblSociosTitulo;
    private FlowLayoutPanel flowSocios;
    private Button btnCerrar;
}
