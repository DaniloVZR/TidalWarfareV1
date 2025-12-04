using System;
using System.Windows.Forms;

namespace TidalWarfareV1
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Inicio());
        }

        public static class InformacionJugadores
        {
            public static string colorNavio1 = "NavioVerde";
            public static string colorNavio2 = "NavioGris";
            public static string jugador1 = "";
            public static string jugador2 = "";
        }
    }
}
