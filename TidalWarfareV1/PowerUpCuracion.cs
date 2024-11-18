using System;
using System.Collections.Generic;
using System.Drawing;

namespace TidalWarfareV1
{
    internal class PowerUpCuracion:ObjetoGrafico
    {
        private int cantidadCuracion = 10;
        private bool activo = true;
        private static Random random = new Random();
        public bool Activo => activo;

        public PowerUpCuracion(int x, int y) : base(x, y, 40, 40, "Botiquin")
        {
        }

        public void Recoger(Navio navio)
        {
            if (!activo) return;
            activo = false;
            Imagen.Visible = false;
            navio.Curar(cantidadCuracion);
        }

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
