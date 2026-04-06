using SistemaGestion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestion.Helpers
{
    public static class BackupHelper
    {
        public static void CrearBackup()
        {
            string dbPath = DatabaseHelper.DatabasePath;

            string backupFolder = Path.Combine(
                Path.GetDirectoryName(dbPath),
                "backups"
            );

            if (!Directory.Exists(backupFolder))
                Directory.CreateDirectory(backupFolder);

            string backupFile = Path.Combine(
                backupFolder,
                $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.db"
            );

            File.Copy(dbPath, backupFile, true);
        }
    }
}
