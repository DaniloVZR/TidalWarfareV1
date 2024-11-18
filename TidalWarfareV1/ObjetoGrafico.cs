using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TidalWarfareV1
{
    internal class ObjetoGrafico
    {
        private int x;
        private int y;
        protected int h;
        protected int w;
        PictureBox imagen;
        protected string nombreRecurso;

        protected int X { get => x; }
        protected int Y { get => y; }
        public PictureBox Imagen { get => imagen; set => imagen = value; }

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

        public virtual Rectangle GetBounds()
        {
            return Imagen.Bounds;
        }

        public void setPos(int x, int y)
        {
            this.x = x;
            this.y = y;
            imagen.Location = new Point(x, y);
        }
    }
}
