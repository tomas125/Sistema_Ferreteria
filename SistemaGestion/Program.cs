using SistemaGestion.Data;
using SistemaGestion.Forms;

namespace SistemaGestion;

static class Program
{
    // WinForms requiere Single-Threaded Apartment para componentes COM/UI (por ejemplo: portapapeles, dialogs).
    [STAThread]
    static void Main()
    {
        // v1.0.9
        // Configura el entorno base de WinForms (DPI, estilos visuales, fuente por defecto, etc.).
        ApplicationConfiguration.Initialize();
        try
        {
            // Verifica/crea la base de datos y sus objetos necesarios antes de abrir la ventana principal.
            DatabaseHelper.InitializeDatabase();
        }
        catch
        {
            // CUIDADO: este catch silencioso oculta el error real y cierra la app sin informar al usuario.
            // Recomendado: registrar/loggear la excepción y mostrar un mensaje amigable antes de salir.
            return;
        }

        // Inicia el loop de mensajes de la UI y abre el formulario principal de la aplicación.
        Application.Run(new MainForm());
    }
}
