using System;
using System.Drawing;
using System.Windows.Forms;

namespace TidalWarfareV1
{
    internal class Explosion : Entidad
    {
        private Timer timerExplosion;
        public event EventHandler AnimacionCompletada;

        public Explosion(Point posicion)
            : base(posicion, 128, 128, "Explosion", 10, 128)
        {
            ConfigurarTimer();
        }

        private void ConfigurarTimer()
        {
            timerExplosion = new Timer();
            timerExplosion.Interval = 50;  // Ajusta este valor para controlar la velocidad de la animación
            timerExplosion.Tick += TimerExplosion_Tick;
            timerExplosion.Start();
        }

        private void TimerExplosion_Tick(object sender, EventArgs e)
        {
            Animacion();

            // Cuando llegamos al último frame
            if (conFrame == 0)
            {
                timerExplosion.Stop();
                AnimacionCompletada?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {
            timerExplosion?.Stop();
            timerExplosion?.Dispose();
            Imagen?.Dispose();
        }
    }
}
