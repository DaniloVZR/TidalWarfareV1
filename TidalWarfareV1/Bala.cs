using System;
using System.Collections.Generic;
using System.Drawing;

namespace TidalWarfareV1
{
    internal class Bala : ObjetoGrafico
    {
        private int velocidad = 7;
        private int direccion;
        private bool activa = true;
        private Rectangle limitesPantalla;


        private int danio = 10; // Daño base de la bala
        public int Danio => danio;

        public bool Activa { get => activa; set => activa = value; }

        public Bala(int x, int y, int direccion) : base(x, y, 15, 15, "Bala")
        {
            this.direccion = direccion;

            // Ajustar la posición inicial según el tamaño
            switch (direccion)
            {
                case 0: // Izquierda
                    setPos(x - 20, y); // Mover la bala más a la izquierda
                    Imagen.Image.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;
                case 1: // Derecha
                    setPos(x + 20, y); // Mover la bala más a la derecha
                    break;
                case 2: // Abajo
                    setPos(x, y + 20); // Mover la bala más abajo
                    Imagen.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 3: // Arriba
                    setPos(x, y - 20); // Mover la bala más arriba
                    Imagen.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }
        }

        public void Mover(List<ObjetoGrafico> objetos)
        {
            if (!activa) return;

            int dx = 0, dy = 0;
            switch (direccion)
            {
                case 0: // Izquierda
                    dx = -velocidad;
                    break;
                case 1: // Derecha
                    dx = velocidad;
                    break;
                case 2: // Abajo
                    dy = velocidad;
                    break;
                case 3: // Arriba
                    dy = -velocidad;
                    break;
            }

            int nuevaX = X + dx;
            int nuevaY = Y + dy;

            // Verificar si la bala está fuera de la pantalla para mirar esta parte
            if (nuevaX < 0 || nuevaX > 800 || nuevaY < 0 || nuevaY > 600)
            {
                activa = false;
                return;
            }

            // Verificar colisiones con otros objetos
            Rectangle nuevaPosicion = new Rectangle(nuevaX, nuevaY, w, h);
            foreach (ObjetoGrafico obj in objetos)
            {
                if (obj != this && !(obj is Bala) && nuevaPosicion.IntersectsWith(obj.GetBounds()))
                {
                    if (obj is Navio navio)
                    {
                        navio.RecibirDanio(danio);
                    }
                    activa = false;
                    return;
                }
            }

            setPos(nuevaX, nuevaY);
        }
    }
}
