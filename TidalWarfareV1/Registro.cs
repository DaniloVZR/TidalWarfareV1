using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TidalWarfareV1
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            string registroJugador = txtNickname.Text;

            if (registroJugador.Length > 10)
            {
                MessageBox.Show("Excedio el limite de caracteres (10)", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (GestionDB.BuscarJugadores(registroJugador) != 0)
            {
                MessageBox.Show("Jugador ya registrado", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            GestionDB.RegistrarJugador(txtNickname.Text);
            MessageBox.Show("Jugado registrado!", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
