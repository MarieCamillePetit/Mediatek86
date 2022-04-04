using System;
using System.Windows.Forms;
using Mediatek86.controleur;
using Serilog;
using Serilog.Formatting.Json;

namespace Mediatek86
{
    /// <summary>
    /// Class program 
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(new JsonFormatter(), "logs/log.txt",
                rollingInterval: RollingInterval.Day)
                .CreateLogger();

            new Controle();
        }
    }
}
