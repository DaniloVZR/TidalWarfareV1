using System;
using System.Drawing;
using System.Windows.Forms;

namespace TidalWarfareV1
{
    /// <summary>
    /// Representa una mina en el juego que puede explotar y causar daño.    
    /// </summary>
    internal class Mina : ObjetoGrafico
    {
        // Indicar si está activa la mina
        private bool activa = true;
        // Daño de la explosión
        private const int Damage_explosion = 50; 
        // Evento que se dispara cuando la mina explota        
        public event EventHandler<ExplosionEventArgs> Explosion; 
        public bool Activa => activa;

        // Constructor de mina
        public Mina(int x, int y): base(x, y, 50, 50, "Mina")
        {
        }

        /// <summary>
        /// Activa la explosión de la mina, crea una animación y notificando el daño
        /// </summary>
        public void Explotar(Form formulario)
        {
            // Verificar si la mina está activa
            if (!activa) return;
            activa = false;

            // Calcular posición centrada para la explosión
            Point posicionExplosion = new Point(
                Imagen.Location.X - (128 - 50) / 2,
                Imagen.Location.Y - (128 - 50) / 2
            );

            // Crear y configurar la explosión
            var explosion = new Explosion(posicionExplosion);
            explosion.AnimacionCompletada += (s, e) =>
            {
                formulario.Controls.Remove(explosion.Imagen);
                explosion.Dispose();
            };

            // Añadir la explosión al formulario
            formulario.Controls.Add(explosion.Imagen);
            explosion.Imagen.BringToFront();

            // Ocultar la mina
            Imagen.Visible = false;

            // Notificar el daño
            AlExplotar(new ExplosionEventArgs(GetBounds(), Damage_explosion));
        }

        /// <summary>
        /// Método protegido que dispara el evento de explosión
        /// </summary>
        protected virtual void AlExplotar(ExplosionEventArgs e)
        {
            Explosion.Invoke(this, e);
        }
    }

    // Información del evento de la explosión
    public class ExplosionEventArgs : EventArgs
    {
        // Área afectada de la explosión (hitbox)
        public Rectangle Area { get; }
        // Daño causado
        public int Damage { get; }

        // Constructor con eventos de la explosión
        public ExplosionEventArgs(Rectangle area, int damage)
        {
            Area = area;
            Damage = damage;
        }
    }
}