using SistemaGestion.Data;
using SistemaGestion.Forms;

namespace SistemaGestion;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        try
        {
            DatabaseHelper.InitializeDatabase();
        }
        catch
        {
            return;
        }

        Application.Run(new MainForm());
    }
}
