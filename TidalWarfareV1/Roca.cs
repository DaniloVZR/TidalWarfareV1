using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidalWarfareV1
{
    /// <summary>
    /// Representa las rocas en el juego que el jugador no puede traspasar.    
    /// </summary>
    internal class Roca : ObjetoGrafico
    {
        public Roca(int x, int y) : base(x, y, 50, 50, "Roca") { }
    }
}
