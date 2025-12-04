using System;
using System.Collections.Generic;
using System.Drawing;

namespace TidalWarfareV1
{
    /// <summary>
    /// Clase que representa un Power-Up de curación en el juego.
    /// Este objeto gráfico puede ser recogido por un navío para restaurar su salud.
    /// </summary>
    internal class PowerUpCuracion:ObjetoGrafico
    {
        private int cantidadCuracion = 10; // Cantidad de curación
        private bool activo = true; // Estado del power-up
        private static Random random = new Random(); // Instanciar el random para las posiciones        
        public bool Activo => activo; // Get de Activo

        // Constructor por defecto
        public PowerUpCuracion(int x, int y) : base(x, y, 40, 40, "Botiquin"){}

        /// <summary>
        /// Método para que un navío recoja el Power-Up y reciba curación.
        /// </summary>
        public void Recoger(Navio navio)
        {
            if (!activo) return;
            activo = false;
            Imagen.Visible = false;
            navio.Curar(cantidadCuracion);
        }

        /// <summary>
        ///  Spawnear en posición aleatoria el power-up evitando las colisiones
        /// </summary>
        /// <param name="formSize"></param>
        /// <param name="objetos"></param>
        /// <returns></returns>
        public static Point ObtenerPosicionAleatoria(Size formSize, List<ObjetoGrafico> objetos)
        {
            int maxIntentos = 50;
            int intento = 0;
            while (intento < maxIntentos)
            {

                int x = random.Next(50, formSize.Width - 90);
                int y = random.Next(50, formSize.Height - 90);
                Rectangle nuevaPos = new Rectangle(x, y, 40, 40);

                bool hayColision = false;
                foreach (var obj in objetos)
                {
                    if (nuevaPos.IntersectsWith(obj.GetBounds()))
                    {
                        hayColision = true;
                        break;
                    }
                }

                if (!hayColision)
                {
                    return new Point(x, y);
                }
                intento++;
            }
            return new Point(formSize.Width / 2, formSize.Height / 2);
        }
    }
}
