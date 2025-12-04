using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidalWarfareV1
{
    // Clase Jugador que tiene las propiedades y datos de un jugador
    public class Jugador
    {
        // Campos que almacenan los datos del jugador
        int id_jugador;
        string nickname;
        int victorias;
        int derrotas;
        int partidas;

        public Jugador()
        {
        }

        // Propiedades públicas para acceder y modificar los datos del jugador
        public int Id_jugador { get => id_jugador; set => id_jugador = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public int Victorias { get => victorias; set => victorias = value; }
        public int Derrotas { get => derrotas; set => derrotas = value; }
        public int Partidas { get => partidas; set => partidas = value; }
    }
}
