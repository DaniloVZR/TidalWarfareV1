using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidalWarfareV1
{
    public class Tablero
    {
        private List<Point> coordMuros = new List<Point>();
        private List<Point> coordMinas = new List<Point>();

        public Tablero()
        {
            InicializarTablero();
        }
        public List<Point> CoordMuros { get => coordMuros; set => coordMuros = value; }
        public List<Point> CoordMinas { get => coordMinas; set => coordMinas = value; }

        private void InicializarTablero()
        {
            CrearLimites(0, 800, 0);
            CrearLimites(650, 800, 0);
            CrearLimites(0,650,1);
            CrearLimites(800, 700, 1);
            CrearRocasAleatorias();
            CrearBombasAleatorias();
        }

        private void CrearLimites(int inicio, int fin, int direccion)
        {
            for (int i = 0; i < fin; i += 50)
            {
                Point coordenada;
                if(direccion == 1)
                {
                    coordenada = new Point(inicio, i);  
                } else
                {
                    coordenada = new Point(i, inicio);
                }
                coordMuros.Add(coordenada);
            }
        }

        void CrearRocasAleatorias()
        {
            Random random = new Random();
            for (int fila = 50; fila < 800; fila += 50)
            {
                for (int columna = 50; columna < 750; columna += 50)
                {
                    if (fila == 50 && columna == 100 || fila == 50 && columna == 50 || fila == 100 && columna == 50 || fila == 100 && columna == 100)
                        continue;

                    if (fila == 700 && columna == 600 || fila == 700 && columna == 550 || fila == 750 && columna == 600 || fila == 750 && columna == 550)
                        continue;

                    if (fila >= 300 && fila <= 500 && columna >= 200 && columna <= 400)
                        continue;

                    bool existeMuro = false;
                    if (coordMuros.Contains(new Point(fila, columna)))
                        existeMuro = true;
                    if (!existeMuro && random.Next(0, 7) == 0)
                    {
                        Point point = new Point(fila, columna);
                        coordMuros.Add(point);
                    }
                }
            }
        }

        void CrearBombasAleatorias()
        {
            Random random = new Random();
            for (int fila = 50; fila < 800; fila += 50)
            {
                for (int columna = 50; columna < 750; columna += 50)
                {
                    if (fila == 50 && columna == 100 || fila == 50 && columna == 50 || fila == 100 && columna == 50 || fila == 100 && columna == 100)
                        continue;

                    if (fila == 700 && columna == 600 || fila == 700 && columna == 550 || fila == 750 && columna == 600 || fila == 750 && columna == 550)
                        continue;

                    if (fila >= 300 && fila <= 500 && columna >= 200 && columna <= 400)
                        continue;

                    bool existeMuro = false;
                    if (coordMinas.Contains(new Point(fila, columna)))
                        existeMuro = true;
                    if (!existeMuro && random.Next(0, 15) == 0)
                    {
                        Point point = new Point(fila, columna);
                        coordMinas.Add(point);
                    }
                }
            }
        }
    }
}
