using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TidalWarfareV1.Program;

namespace TidalWarfareV1
{
    /// <summary>
    /// Formulario para configurar una partida de juego.
    /// Permite a los jugadores seleccionar colores para sus navíos y comenzar el juego.
    /// </summary>
    public partial class Jugar : Form
    {
        public Jugar()
        {
            InitializeComponent();
        }
        
        // Evento que se activa al hacer clic en el botón "Volver".
        // Oculta el formulario actual y muestra el formulario de inicio.        
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inicio inicio = new Inicio();
            inicio.Show();
        }

        /// <summary>
        /// Verifica que ambos jugadores estén registrados y, si es válido, inicia la partida.
        /// </summary>
        private void btnEmpezar_Click(object sender, EventArgs e)
        {
            if (txt_jugador1.Text == "" || txt_jugador2.Text == "")
            {
                MessageBox.Show("Ingrese nombre de usuario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (GestionDB.BuscarJugadores(txt_jugador1.Text) == 0)
            {
                MessageBox.Show($"{txt_jugador1.Text} no está registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (GestionDB.BuscarJugadores(txt_jugador2.Text) == 0)
            {
                MessageBox.Show($"{txt_jugador2.Text} no está registrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            InformacionJugadores.jugador1 = txt_jugador1.Text;
            InformacionJugadores.jugador2 = txt_jugador2.Text;
            this.Hide();
            Mapa mapa = new Mapa();
            mapa.Show();
        }

        private void Restaurarcolornavio2()
        {
            // Habilita todos los botones del navío 2
            btn_naranja_2.Enabled = true;
            btn_cafe_2.Enabled = true;
            btn_morado_2.Enabled = true;
            btn_rojo_2.Enabled = true;
            btn_gris_2.Enabled = true;
            btn_verde_2.Enabled = true;
        }

        private void Restaurarcolornavio1()
        {
            // Habilita todos los botones del navío 1
            btn_naranja_1.Enabled = true;
            btn_cafe_1.Enabled = true;
            btn_morado_1.Enabled = true;
            btn_rojo_1.Enabled = true;
            btn_gris_1.Enabled = true;
            btn_verde_1.Enabled = true;
        }

        // Seleccionar colores del navio 1
        public void btn_naranja_1_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio2(); // Habilita todos los colores del navío 2
            pb_navio1.Image = Properties.Resources.NavioSoloNaranja; // Cambia la imagen del navío 1
            btn_naranja_2.Enabled = false; // Deshabilita el color seleccionado
            InformacionJugadores.colorNavio1 = "NavioNaranja"; // Guarda el color seleccionado
        }

        private void btn_cafe_1_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio2();
            pb_navio1.Image = Properties.Resources.NavioSoloCafe;
            btn_cafe_2.Enabled = false;
            InformacionJugadores.colorNavio1 = "NavioCafe";
        }

        private void btn_morado_1_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio2();
            pb_navio1.Image = Properties.Resources.NavioSoloMorado;
            btn_morado_2.Enabled = false;
            InformacionJugadores.colorNavio1 = "NavioMorado";
        }

        private void btn_rojo_1_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio2();
            pb_navio1.Image = Properties.Resources.NavioSoloRojo;
            btn_rojo_2.Enabled = false;
            InformacionJugadores.colorNavio1 = "NavioRojo";
        }

        private void btn_gris_1_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio2();
            pb_navio1.Image = Properties.Resources.NavioSoloGris;
            btn_gris_2.Enabled = false;
            InformacionJugadores.colorNavio1 = "NavioGris";
        }

        private void btn_verde_1_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio2();
            pb_navio1.Image = Properties.Resources.NavioSoloVerde;
            btn_verde_2.Enabled = false;
            InformacionJugadores.colorNavio1 = "NavioVerde";
        }

        // Seleccionar colores del navio 2
        private void btn_naranja_2_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio1(); // Habilita todos los colores del Navío 1
            pb_navio2.Image = Properties.Resources.NavioSoloNaranja; // Cambia la imagen del Navío 2
            btn_naranja_1.Enabled = false; // Deshabilita el color seleccionado en el Navío 1
            InformacionJugadores.colorNavio1 = "NavioNaranja"; // Guarda el color seleccionado
        }

        private void btn_cafe_2_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio1();
            pb_navio2.Image = Properties.Resources.NavioSoloCafe;
            btn_cafe_1.Enabled = false;
            InformacionJugadores.colorNavio2 = "NavioCafe";
        }

        private void btn_morado_2_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio1();
            pb_navio2.Image = Properties.Resources.NavioSoloMorado;
            btn_morado_1.Enabled = false;
            InformacionJugadores.colorNavio2 = "NavioMorado";
        }

        private void btn_rojo_2_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio1();
            pb_navio2.Image = Properties.Resources.NavioSoloRojo;
            btn_rojo_1.Enabled = false;
            InformacionJugadores.colorNavio2 = "NavioRojo";
        }

        private void btn_gris_2_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio1();
            pb_navio2.Image = Properties.Resources.NavioSoloGris;
            btn_gris_1.Enabled = false;
            InformacionJugadores.colorNavio2 = "NavioGris";
        }

        private void btn_verde_2_Click(object sender, EventArgs e)
        {
            Restaurarcolornavio1();
            pb_navio2.Image = Properties.Resources.NavioSoloVerde;
            btn_verde_1.Enabled = false;
            InformacionJugadores.colorNavio2 = "NavioVerde";
        }

        // En caso de que el usuario no esté registrado, podrá hacerlo dandole click al botón, de lo contrario no podrá iniciar partida
        private void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Registro registro = new Registro();
            registro.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InformacionJugadores.jugador1 = "Jugador 1";
            InformacionJugadores.jugador2 = "Jugador 2";
            this.Hide();
            Mapa mapa = new Mapa();
            mapa.Show();
        }
    }
}
