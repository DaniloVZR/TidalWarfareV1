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
        // Listas para almacenar las coordenadas de los muros y minas en el tablero

        private List<Point> coordMuros = new List<Point>();
        private List<Point> coordMinas = new List<Point>();

        public Tablero()
        {
            InicializarTablero();
        }
        public List<Point> CoordMuros { get => coordMuros; set => coordMuros = value; }
        public List<Point> CoordMinas { get => coordMinas; set => coordMinas = value; }


        // Método para inicializar el tablero: coloca límites, rocas y minas
        private void InicializarTablero()
        {
            // Crear los límites del tablero, llamando a CrearLimites con las coordenadas y dirección correspondientes
            CrearLimites(0, 800, 0);      // Limite superior (horizontal)
            CrearLimites(650, 800, 0);    // Limite inferior (horizontal)
            CrearLimites(0, 650, 1);      // Limite izquierdo (vertical)
            CrearLimites(800, 700, 1);    // Limite derecho (vertical)
            CrearRocasAleatorias();
            CrearBombasAleatorias();
        }

        // Método para crear los límites del tablero (muros)
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

        // Método para crear rocas aleatorias en el tablero, con restriccion en los spawns

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

                    // Verificar si ya existe un muro en la coordenada evita sobreponer rocas sobre muros
                    bool existeMuro = false;
                    if (coordMuros.Contains(new Point(fila, columna)))
                        existeMuro = true;

                    // Si no existe muro y se cumple una condición aleatoria, colocar una roca
                    if (!existeMuro && random.Next(0, 7) == 0)
                    {
                        Point point = new Point(fila, columna);
                        coordMuros.Add(point);
                    }
                }
            }
        }


        // Método para crear minas aleatorias en el tablero, con restricciones similares a las rocas
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
