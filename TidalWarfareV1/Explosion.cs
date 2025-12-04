using System;
using System.Drawing;
using System.Windows.Forms;
namespace TidalWarfareV1
{
    /// <summary>
    /// Clase que maneja la animación de una explosión en el juego.    
    /// </summary>
    internal class Explosion : Entidad
    {
        // Timer que controla la velocidad de la animación de la explosión
        private Timer timerExplosion;        
        // Evento que se dispara cuando la animación de la explosión ha terminado.                
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
            // Siguiente animación
            Animacion();
            // Cuando llegamos al último frame detiene la animación
            if (conFrame == 0)
            {
                timerExplosion.Stop();
                AnimacionCompletada.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Método para liberar los recursos utilizados por la explosión        
        /// </summary>
        public void Dispose()
        {
            timerExplosion.Stop();
            timerExplosion.Dispose();
            Imagen?.Dispose();
        }
    }
}