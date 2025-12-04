using System;
using System.Drawing;

namespace TidalWarfareV1
{
    /// <summary>
    /// Representa una entidad animada en el juego que utiliza sprites.
    /// Esta clase hereda de ObjetoGrafico y maneja la animación de sprites
    /// mediante frames y direcciones.
    /// </summary>
    public class Entidad : ObjetoGrafico
    {

        protected int cantFrame; // Cantidad de los frames del sprite
        protected int tamFrame; // Tamaño del frame
        protected int conFrame; // Contador de frames actuales
        protected Rectangle rectangle;
        protected Bitmap bmp;
        protected int posy = 0; // Posición actual en el eje Y dentro del sprite sheet.
        protected int direccionY = 0;  // Añadimos variable para controlar la fila del sprite (dirección)        

        /// <summary>
        /// Inicializa una nueva instancia de la clase Entidad.
        /// </summary>
        public Entidad(Point coor, int w, int h, string nombreRecurso, int frames, int Tamframe) : base(coor.X, coor.Y, w, h, nombreRecurso)
        {
            cantFrame = frames;
            tamFrame = Tamframe;
            conFrame = 0;
            bmp = (Bitmap)Properties.Resources.ResourceManager.GetObject(nombreRecurso);

            // Iniciamos con el primer frame
            rectangle = new Rectangle(0, 0, tamFrame, tamFrame);
            Imagen.Image = bmp.Clone(rectangle, bmp.PixelFormat);
        }

        /// <summary>
        /// Actualiza la animación de la entidad, cambiando al siguiente frame.
        /// </summary>
        public void Animacion()
        {
            // Calculamos la posición X basada en el frame actual
            int frameX = (conFrame * tamFrame);

            // Usamos direccionY para seleccionar la fila correcta del sprite
            rectangle = new Rectangle(frameX, direccionY * tamFrame, tamFrame, tamFrame);

            // Verificar que el rectángulo está dentro de los límites del bitmap
            if (rectangle.Right <= bmp.Width && rectangle.Bottom <= bmp.Height)
            {
                Imagen.Image = bmp.Clone(rectangle, bmp.PixelFormat);
            }

            // Incrementamos el contador de frame
            conFrame = (conFrame + 1) % cantFrame;
        }

        /// <summary>
        /// Cambia la dirección de la animación, seleccionando una fila diferente del sprite sheet.
        /// </summary>
        protected void CambiarDireccion(int nuevaDireccion)
        {
            direccionY = nuevaDireccion;
            conFrame = 0;  // Reiniciamos el frame al cambiar de dirección
        }
    }
}
