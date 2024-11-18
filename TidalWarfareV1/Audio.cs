using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace TidalWarfareV1
{
    internal class Audio
    {
        private SoundPlayer player;
        private int tipo;

        public Audio(int tipo)
        {
            this.tipo = tipo;
        }


        public void ReproducirAudio()
        {
            // Selección del archivo de sonido basado en el tipo especificado
            switch (tipo)
            {
                case 1:
                    player = new SoundPlayer(Properties.Resources.Maintheme);
                    break;
                case 2:
                    player = new SoundPlayer(Properties.Resources.put_bom);
                    break;
                case 3:
                    player = new SoundPlayer(Properties.Resources.Explosion1);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(tipo), "Tipo de sonido no válido.");
            }

            player.Play();
        }
    }
}