using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class End : Form
    {
        public End()
        {
            InitializeComponent();
        }

        public static int pontszam;

        private void Form4_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            pontszam = Game.pontszam;
            label1.Text = pontszam.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form jatek= new Game();
            Game.pontszam = 0;
            jatek.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
