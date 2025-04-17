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
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        public static int nehezseg=1;
        public static int gravitacioertek=1;

        private void Form3_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form menu = new Menu();
            menu.Show();
            this.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked) { nehezseg = 1; }
            else if(radioButton2.Checked) { nehezseg = 2;}
            else { nehezseg = 3;}
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton6.Checked) { gravitacioertek = 1; }
            else { gravitacioertek = 2;}
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible=false;
            label3.Visible=false;
        }
    }
}
