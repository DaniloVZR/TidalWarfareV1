using System;
using System.Windows.Forms;
using System.Drawing;

namespace TidalWarfareV1
{
    /// <summary>
    /// Representa un objeto gráfico base en el juego que puede ser dibujado en pantalla.
    /// Esta clase proporciona la funcionalidad básica para manejar posición, tamaño e imagen
    /// de los elementos visuales del juego.
    /// </summary>
    public class ObjetoGrafico
    {

        // Se crean los atributos del objeto grafico
        private int x; 
        private int y;
        protected int h;
        protected int w;
        PictureBox imagen;
        protected string nombreRecurso;

        protected int X { get => x; }
        protected int Y { get => y; }
        public PictureBox Imagen { get => imagen; set => imagen = value; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase ObjetoGrafico.
        /// </summary>
        public ObjetoGrafico(int x, int y, int h, int w, string nombreRecurso)
        {
            this.x = x;
            this.y = y;
            this.h = h;
            this.w = w;
            this.nombreRecurso = nombreRecurso;
            imagen = new PictureBox();
            imagen.Location = new Point(x, y);
            imagen.Size = new Size(w, h);
            imagen.Image = (Image)Properties.Resources.ResourceManager.GetObject(nombreRecurso);
            imagen.SizeMode = PictureBoxSizeMode.StretchImage;  
            imagen.BackColor = Color.Transparent;
        }

        // Función para obtener el recuadro para hacer la caja de colisiones
        public virtual Rectangle GetBounds()
        {
            return Imagen.Bounds;
        }

        // Actualiza la posición del objeto en la pantalla.
        public void setPos(int x, int y)
        {
            this.x = x;
            this.y = y;
            imagen.Location = new Point(x, y);
        }
    }
}
