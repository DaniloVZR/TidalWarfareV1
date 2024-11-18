using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TidalWarfareV1
{
    internal class Mina : ObjetoGrafico
    {
        private bool activa = true;
        private const int DANIO_EXPLOSION = 50;
        public event EventHandler<ExplosionEventArgs> Explosion;
        public bool Activa => activa;

        public Mina(int x, int y)
            : base(x, y, 50, 50, "Mina")
        {
        }

        public void Explotar(Form formulario)
        {
            if (!activa) return;
            activa = false;

            // Calcular posición centrada para la explosión
            Point posicionExplosion = new Point(
                Imagen.Location.X - (128 - 50) / 2,
                Imagen.Location.Y - (128 - 50) / 2
            );

            // Crear y configurar la explosión
            var explosion = new Explosion(posicionExplosion);
            explosion.AnimacionCompletada += (s, e) => {
                formulario.Controls.Remove(explosion.Imagen);
                explosion.Dispose();
            };

            // Añadir la explosión al formulario
            formulario.Controls.Add(explosion.Imagen);
            explosion.Imagen.BringToFront();

            // Ocultar la mina
            Imagen.Visible = false;

            // Notificar el daño
            OnExplosion(new ExplosionEventArgs(GetBounds(), DANIO_EXPLOSION));
        }

        protected virtual void OnExplosion(ExplosionEventArgs e)
        {
            Explosion?.Invoke(this, e);
        }

        public bool CheckCollision(Rectangle bounds)
        {
            return activa && GetBounds().IntersectsWith(bounds);
        }
    }

    public class ExplosionEventArgs : EventArgs
    {
        public Rectangle Area { get; }
        public int Danio { get; }

        public ExplosionEventArgs(Rectangle area, int danio)
        {
            Area = area;
            Danio = danio;
        }
    }
}