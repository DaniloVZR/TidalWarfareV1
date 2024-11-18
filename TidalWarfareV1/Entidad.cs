using System;
using System.Drawing;

namespace TidalWarfareV1
{
    internal class Entidad : ObjetoGrafico
    {
        //int vida;
        protected int cantFrame;
        protected int tamFrame;
        protected int conFrame;
        //int direccion;
        protected Rectangle rectangle;
        protected Bitmap bmp;
        protected int posx = 0;
        protected int posy = 0;
        protected int direccionY = 0;  // Añadimos variable para controlar la fila del sprite (dirección)        

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

        public void Animacion()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error en animación: {ex.Message}");
                conFrame = 0;
            }
        }
        protected void CambiarDireccion(int nuevaDireccion)
        {
            direccionY = nuevaDireccion;
            conFrame = 0;  // Reiniciamos el frame al cambiar de dirección
        }
    }
}
