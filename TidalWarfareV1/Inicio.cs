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
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void btnJugar_Click(object sender, EventArgs e)
        {
            Jugar Jugar = new Jugar();
            Jugar.Show();
            this.Hide();          
        }

        private void btnRanking_Click(object sender, EventArgs e)
        {
            Ranking ranking = new Ranking();
            ranking.Inicio = this;
            ranking.Show();
            this.Hide();
        }
    }
}
