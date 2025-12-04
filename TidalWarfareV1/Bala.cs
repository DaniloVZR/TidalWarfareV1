using System;
using System.Collections.Generic;
using System.Drawing;

namespace TidalWarfareV1
{
    /// <summary>
    /// Representa un proyectil en el juego que puede ser disparado por los navíos.
    /// Esta clase maneja el movimiento, colisiones y daño de las balas.
    /// </summary>
    internal class Bala : ObjetoGrafico
    {

        private int velocidad = 7; // velocidad del movimiento de la bala
        private int direccion; // Dirección del movimiento de la bala.
        private bool activa = true; // Indica si la bala está activa en el juego.
        private int damage = 10; // Daño que causa la bala al navio        
        public bool Activa { get => activa; set => activa = value; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase Bala.
        /// </summary>
        /// <param name="x">Posición inicial en el eje X.</param>
        /// <param name="y">Posición inicial en el eje Y.</param>
        /// <param name="direccion">Dirección inicial del movimiento (0: Izquierda, 1: Derecha, 2: Abajo, 3: Arriba).</param>     
        public Bala(int x, int y, int direccion) : base(x, y, 15, 15, "Bala")
        {
            this.direccion = direccion;

            // Ajustar la posición inicial según el tamaño y la dirección donde apunta el navío
            switch (direccion)
            {
                case 0: // Izquierda
                    setPos(x - 20, y); // Mover la bala más a la izquierda                    
                    break;
                case 1: // Derecha
                    setPos(x + 20, y); // Mover la bala más a la derecha
                    break;
                case 2: // Abajo
                    setPos(x, y + 20); // Mover la bala más abajo                    
                    break;
                case 3: // Arriba
                    setPos(x, y - 20); // Mover la bala más arriba                    
                    break;
            }
        }


        /// <summary>
        /// Actualiza la posición de la bala y maneja las colisiones con otros objetos.
        /// </summary>
        /// <param name="objetos">Lista de objetos gráficos en el juego para verificar colisiones.</param>
        /// <remarks>
        /// El método realiza las siguientes acciones:
        /// 1. Verifica si la bala está activa
        /// 2. Calcula la nueva posición según la dirección
        /// 3. Detecta colisiones con otros objetos
        /// 4. Si colisiona con un navío, aplica daño y reproduce un sonido
        /// </remarks>

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

            // Verificar colisiones con otros objetos
            Rectangle nuevaPosicion = new Rectangle(nuevaX, nuevaY, w, h);
            foreach (ObjetoGrafico obj in objetos)
            {
                
                if (obj != this && !(obj is Bala) && nuevaPosicion.IntersectsWith(obj.GetBounds()))
                {
                    if (obj is Navio navio)
                    {
                        Audio audio = new Audio(5);
                        audio.ReproducirAudio();
                        navio.RecibirDanio(damage);
                    }
                    activa = false;
                    return;
                }
            }

            setPos(nuevaX, nuevaY);
        }
    }
}
