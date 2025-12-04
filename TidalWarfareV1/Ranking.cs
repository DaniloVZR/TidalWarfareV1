using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TidalWarfareV1
{
    public partial class Ranking : Form
    {
        // Inicialización del componente
        Inicio inicio;

        public Ranking()
        {
            InitializeComponent();
        }

        public Inicio Inicio { get => inicio; set => inicio = value; }

        // Función para volver al inicio
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Inicio inicio = new Inicio();
            inicio.Show();
        }

        /// <summary>
        ///  Al cargar el form de Ranking se cargará un gridview con los jugadores registrados en orden descendente de victorias conseguidas por cada jugador
        /// </summary>        
        private void Ranking_Load(object sender, EventArgs e)
        {
            List<Jugador> jugadores = new List<Jugador>();
            jugadores = GestionDB.BuscarJugadores();            

            if (jugadores.Count > 0)
            {
                gridRanking.DataSource = jugadores;
                gridRanking.Visible = true;
            }
        }
    }
}