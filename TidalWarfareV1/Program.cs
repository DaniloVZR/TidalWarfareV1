using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Application.Run(new Jugar());
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
