using System;
using System.Media;

namespace TidalWarfareV1
{
    /// <summary>
    /// Clase que gestiona la reproducción de sonidos en el juego.
    /// </summary>
    internal class Audio
    {
        private SoundPlayer player;
        private int tipo;

        /// <summary>
        /// Constructor de la clase Audio.
        /// Inicializa un nuevo objeto con el tipo de sonido especificado.
        /// </summary>
        public Audio(int tipo)
        {
            this.tipo = tipo;
        }

        /// <summary>
        /// Reproduce un archivo de sonido en función del tipo especificado.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Se lanza si el tipo de sonido no es válido.</exception>
        public void ReproducirAudio()
        {
            // Selección del archivo de sonido basado en el tipo especificado
            switch (tipo)
            {
                // Tema principal
                case 1:
                    player = new SoundPlayer(Properties.Resources.Maintheme);
                    break;
                // Al disparar
                case 2:
                    player = new SoundPlayer(Properties.Resources.put_bom);
                    break;
                // Al explotar una mina
                case 3:
                    player = new SoundPlayer(Properties.Resources.Explosion1);
                    break;
                case 4:
                // Al navio obtener item de repacacion
                    player = new SoundPlayer(Properties.Resources.Fix);
                    break;
                case 5:
                // Impacto de la bala con el navio
                    player = new SoundPlayer(Properties.Resources.impacto);
                    break;                
                default:
                    throw new ArgumentOutOfRangeException(nameof(tipo), "Tipo de sonido no válido.");
            }
            // Reproduce el sonido
            player.Play();            
        }
    }
}