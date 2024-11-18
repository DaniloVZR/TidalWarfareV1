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
    public partial class Jugar : Form
    {
        public Jugar()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            Inicio inicio = new Inicio();
            inicio.Show();
        }

        private void btnEmpezar_Click(object sender, EventArgs e)
        {

            if (txt_jugador1.Text == "" || txt_jugador2.Text == "")
            {
                MessageBox.Show("Ingrese nombre de usuario","Fallo",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            
            

            InformacionJugadores.jugador1 = txt_jugador1.Text;
            InformacionJugadores.jugador2 = txt_jugador2.Text;
            this.Hide();
            Mapa mapa = new Mapa();
            mapa.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

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
            // Habilita todos los botones del navío 2
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
    }
}
