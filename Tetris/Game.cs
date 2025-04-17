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
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.Location = new Point(this.ClientSize.Width / 2 - panel1.Size.Width / 2,
    this.ClientSize.Height / 2 - panel1.Size.Height / 2);
            panel1.Anchor = AnchorStyles.None;
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            foreach (Control control in this.Controls)
            {
                control.PreviewKeyDown += new PreviewKeyDownEventHandler(control_PreviewKeyDown);
            }
            this.KeyPreview = true;//hogy a form kapja el először a key-eventet

        }

        void control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            /*Some key presses, such as the TAB, RETURN, ESC, and arrow keys, are typically ignored by some controls
             * because they are not considered input key presses. For example, by default, a Button control ignores 
             * the arrow keys. Pressing the arrow keys typically causes the focus to move to the previous or next 
             * control. The arrow keys are considered navigation keys and pressing these keys typically do not raise
             * the KeyDown event for aButton. However, pressing the arrow keys for a Button does raise the PreviewKeyDown event. 
             * By handling the PreviewKeyDown event for a Button and setting the IsInputKey property to true, 
             * you can raise the KeyDown event when the arrow keys are pressed. 
             * However, if you handle the arrow keys, the focus will no longer move to the previous or next control.*/
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.IsInputKey = true;//Gets or sets a value indicating whether a key is a regular input key.
                //a fokust ne allitsa at
            }
        }

        Label[,] kockak = new Label[22, 22];
        Label[,] block = new Label[5, 5];
        Label[,] block1 = new Label[5, 5];
        Label[,] kovetkezo = new Label[6, 6];
        Random r = new Random();

        int szam = 1;
        int kovszam = 1;

        int hossz, szel, hossz1, szel1;

        bool egyfel = true;
        bool kettofel = true, kettobal = false, kettojobb = false, kettole = false;
        bool haromfel = true, harombal = false, haromjobb = false, haromle = false;
        bool otfel = true;
        bool hatfel = true;
        bool hetfel = true, hetbal = false, hetjobb = false, hetle = false;

        public static int pontszam = 0;



        bool jatekvege = false;


        public static int nehezseg;
        public static int gravitacioertek;

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            timer1.Stop();
            timer3.Stop();
            pontszam = 0;
            label2.Text = pontszam.ToString();
            button3.Visible = false;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    block[i, j].BackColor = Color.Transparent;
                    block[i, j].BorderStyle = BorderStyle.None;
                    block1[i, j].BorderStyle = BorderStyle.None;
                    kovetkezo[i, j].BorderStyle = BorderStyle.None;
                    block1[i, j].BackColor = Color.Transparent;
                    kovetkezo[i, j].BackColor = Color.Transparent;
                }
            }
            for (int i = 0; i <= 11; i++)
            {
                for (int j = 0; j <= 21; j++)
                {
                    kockak[i, j].BackColor = Color.Transparent;
                    kockak[i, j].BorderStyle = BorderStyle.None;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            button1.Enabled= false;

            nehezseg = Settings.nehezseg;
            if (nehezseg == 1) { timer1.Interval = 700; }
            if (nehezseg == 2) { timer1.Interval = 300; }
            if (nehezseg == 3) { timer1.Interval = 100; }

            gravitacioertek = Settings.gravitacioertek;

            button3.Visible = true;
            szam = r.Next(1, 8);
            kovszam = r.Next(1, 8);
            hossz = panel1.Height / 20;
            szel = panel1.Width / 10;
            hossz1 = panel2.Height / 5;
            szel1 = panel2.Width / 5;
            for (int i = 0; i <= 11; i++)
            {
                for (int j = 0; j <= 21; j++)
                {
                    kockak[i, j] = new Label();
                    kockak[i, j].Left = (i - 1) * szel;
                    kockak[i, j].Top = (j - 1) * hossz;
                    kockak[i, j].Width = szel;
                    kockak[i, j].Height = hossz;
                    kockak[i, j].BackColor = Color.Transparent;
                    kockak[i, j].TabIndex = 0;
                    if (j == 21 || j == 0 || i == 0 || i == 11)
                        kockak[i, j].TabIndex = 1;
                    panel1.Controls.Add(kockak[i, j]);
                    kockak[i, j].BringToFront();
                    kockak[i, j].Text = kockak[i, j].TabIndex.ToString();
                    //kockak[i, j].Text = i+","+j;
                }
            }
            for (int i = 0; i <= 4; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    kovetkezo[i, j] = new Label();
                    kovetkezo[i, j].Left = i * szel1;
                    kovetkezo[i, j].Top = j * hossz1;
                    kovetkezo[i, j].Width = szel1;
                    kovetkezo[i, j].Height = hossz1;
                    kovetkezo[i, j].BackColor = Color.Transparent;
                    kovetkezo[i, j].Visible = false;
                    kovetkezo[i, j].BorderStyle = BorderStyle.None;
                    panel2.Controls.Add(kovetkezo[i, j]);
                    kovetkezo[i, j].BringToFront();
                }
            }
            spawn();

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
        }

        private void spawn()
        {
            szam = kovszam;
            kovszam = r.Next(1, 8);
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    block1[i, j] = new Label();
                    block1[i, j].Left = (i + 2) * szel;
                    block1[i, j].Top = (j - 1) * hossz;
                    block1[i, j].Width = szel;
                    block1[i, j].Height = hossz;
                    block1[i, j].BackColor = Color.Transparent;
                    block1[i, j].BorderStyle = BorderStyle.None;
                    panel1.Controls.Add(block1[i, j]);
                }
            }
            for (int i = 0; i <= 4; i++)
            {
                for (int j = 0; j <= 4; j++)
                {

                    kovetkezo[i, j].BackColor = Color.Transparent;
                    kovetkezo[i, j].Visible = false;
                    kovetkezo[i, j].BorderStyle = BorderStyle.None;
                    panel2.Controls.Add(kovetkezo[i, j]);
                    kovetkezo[i, j].BringToFront();
                }
            }


            if (kovszam == 1)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 2; j <= 4; j++)
                    {
                        kovetkezo[i, j].Visible = true;
                        kovetkezo[i, j].BorderStyle = BorderStyle.FixedSingle;
                        kovetkezo[i, j].BackColor = Color.Aqua;
                        if (j > 2)
                        {
                            kovetkezo[i, j].BackColor = Color.Transparent;
                            kovetkezo[i, j].Visible = false;
                        }
                        //block[i, j].Text = block[i, j].TabIndex.ToString();
                    }
                }
            }
            if (kovszam == 2)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        kovetkezo[i, j].Visible = true;
                        kovetkezo[i, j].BorderStyle = BorderStyle.FixedSingle;
                        kovetkezo[i, j].BackColor = Color.Blue;
                        if (j > 2 || i > 3 || (j == 1 && (i == 2 || i == 3)))
                        {
                            kovetkezo[i, j].BackColor = Color.Transparent;
                            kovetkezo[i, j].Visible = false;
                        }
                    }
                }
            }
            if (kovszam == 3)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        kovetkezo[i, j].Visible = true;
                        kovetkezo[i, j].BorderStyle = BorderStyle.FixedSingle;
                        kovetkezo[i, j].BackColor = Color.Orange;
                        if ((j == 1 && i < 3) || j > 2 || i > 3)
                        {
                            kovetkezo[i, j].BackColor = Color.Transparent;
                            kovetkezo[i, j].Visible = false;
                        }
                    }
                }
            }
            if (kovszam == 4)
            {
                for (int i = 1; i <= 4; i++)
                {

                    for (int j = 1; j <= 4; j++)
                    {
                        kovetkezo[i, j].Visible = true;
                        kovetkezo[i, j].BorderStyle = BorderStyle.FixedSingle;
                        kovetkezo[i, j].BackColor = Color.Yellow;
                        if (i > 2 || j > 2)
                        {
                            kovetkezo[i, j].BackColor = Color.Transparent;
                            kovetkezo[i, j].Visible = false;
                        }
                    }
                }
            }
            if (kovszam == 5)
            {

                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        kovetkezo[i, j].Visible = true;
                        kovetkezo[i, j].BorderStyle = BorderStyle.FixedSingle;
                        kovetkezo[i, j].BackColor = Color.Green;
                        if ((i == 1 && j == 1) || (i > 2 && j > 1) || j > 2 || i == 4)
                        {
                            kovetkezo[i, j].BackColor = Color.Transparent;
                            kovetkezo[i, j].Visible = false;
                        }
                    }
                }
            }
            if (kovszam == 6)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        kovetkezo[i, j].Visible = true;
                        kovetkezo[i, j].BorderStyle = BorderStyle.FixedSingle;
                        kovetkezo[i, j].BackColor = Color.Red;
                        if ((i > 2 && j == 1) || i == 4 || j > 2 || (i == 1 && j == 2))
                        {
                            kovetkezo[i, j].BackColor = Color.Transparent;
                            kovetkezo[i, j].Visible = false;
                        }
                    }
                }
            }
            if (kovszam == 7)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        kovetkezo[i, j].Visible = true;
                        kovetkezo[i, j].BorderStyle = BorderStyle.FixedSingle;
                        kovetkezo[i, j].BackColor = Color.Purple;
                        if (j > 2 || i > 3 || (j == 1 && (i == 1 || i > 2)))
                        {
                            kovetkezo[i, j].BackColor = Color.Transparent;
                            kovetkezo[i, j].Visible = false;
                        }
                    }
                }
            }

            if (szam == 1)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        block[i, j] = new Label();
                        block[i, j].Left = (i + 2) * szel;
                        block[i, j].Top = (j - 1) * hossz;
                        block[i, j].Width = szel;
                        block[i, j].Height = hossz;
                        block[i, j].BackColor = Color.Aqua;
                        block[i, j].BorderStyle = BorderStyle.FixedSingle;
                        panel1.Controls.Add(block[i, j]);
                        block[i, j].BringToFront();
                        block[i, j].TabIndex = 1;
                        if (j >= 2)
                        {
                            block[i, j].Visible = false;
                        }
                        //block[i, j].Text = block[i, j].TabIndex.ToString();
                    }
                }
            }
            if (szam == 2)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        block[i, j] = new Label();
                        block[i, j].Left = (i + 2) * szel;
                        block[i, j].Top = (j - 1) * hossz;
                        block[i, j].Width = szel;
                        block[i, j].Height = hossz;
                        block[i, j].BackColor = Color.Blue;
                        block[i, j].BorderStyle = BorderStyle.FixedSingle;
                        panel1.Controls.Add(block[i, j]);
                        block[i, j].BringToFront();
                        block[i, j].TabIndex = 1;
                        if (j > 2 || i > 3 || (j == 1 && (i == 2 || i == 3)))
                        {
                            block[i, j].Visible = false;
                        }
                    }
                }
            }
            if (szam == 3)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        block[i, j] = new Label();
                        block[i, j].Left = (i + 2) * szel;
                        block[i, j].Top = (j - 1) * hossz;
                        block[i, j].Width = szel;
                        block[i, j].Height = hossz;
                        block[i, j].BackColor = Color.Orange;
                        block[i, j].BorderStyle = BorderStyle.FixedSingle;
                        panel1.Controls.Add(block[i, j]);
                        block[i, j].BringToFront();
                        block[i, j].TabIndex = 1;
                        if ((j == 1 && i < 3) || j > 2 || i > 3)
                        {
                            block[i, j].Visible = false;
                        }
                    }
                }
            }
            if (szam == 4)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        block[i, j] = new Label();
                        block[i, j].Left = (i + 2) * szel;
                        block[i, j].Top = (j - 1) * hossz;
                        block[i, j].Width = szel;
                        block[i, j].Height = hossz;
                        block[i, j].BackColor = Color.Yellow;
                        block[i, j].BorderStyle = BorderStyle.FixedSingle;
                        panel1.Controls.Add(block[i, j]);
                        block[i, j].BringToFront();
                        block[i, j].TabIndex = 1;
                        if (i > 2 || j > 2)
                        {
                            block[i, j].Visible = false;
                        }
                    }
                }
            }
            if (szam == 5)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        block[i, j] = new Label();
                        block[i, j].Left = (i + 2) * szel;
                        block[i, j].Top = (j - 1) * hossz;
                        block[i, j].Width = szel;
                        block[i, j].Height = hossz;
                        block[i, j].BackColor = Color.Green;
                        block[i, j].BorderStyle = BorderStyle.FixedSingle;
                        panel1.Controls.Add(block[i, j]);
                        block[i, j].BringToFront();
                        block[i, j].TabIndex = 1;
                        if ((i == 1 && j == 1) || (i > 2 && j > 1) || j > 2 || i == 4)
                        {
                            block[i, j].Visible = false;
                        }
                    }
                }
            }
            if (szam == 6)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        block[i, j] = new Label();
                        block[i, j].Left = (i + 2) * szel;
                        block[i, j].Top = (j - 1) * hossz;
                        block[i, j].Width = szel;
                        block[i, j].Height = hossz;
                        block[i, j].BackColor = Color.Red;
                        block[i, j].BorderStyle = BorderStyle.FixedSingle;
                        panel1.Controls.Add(block[i, j]);
                        block[i, j].BringToFront();
                        block[i, j].TabIndex = 1;
                        if ((i > 2 && j == 1) || i == 4 || j > 2 || (i == 1 && j == 2))
                        {
                            block[i, j].Visible = false;
                        }
                    }
                }
            }
            if (szam == 7)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        block[i, j] = new Label();
                        block[i, j].Left = (i + 2) * szel;
                        block[i, j].Top = (j - 1) * hossz;
                        block[i, j].Width = szel;
                        block[i, j].Height = hossz;
                        block[i, j].BackColor = Color.Purple;
                        block[i, j].BorderStyle = BorderStyle.FixedSingle;
                        panel1.Controls.Add(block[i, j]);
                        block[i, j].BringToFront();
                        block[i, j].TabIndex = 1;
                        if (j > 2 || i > 3 || (j == 1 && (i == 1 || i > 2)))
                        {
                            block[i, j].Visible = false;
                        }
                    }
                }
            }

            for (int i = 1; i <= 4 && !jatekvege; i++)
            {
                for (int j = 1; j <= 4 && !jatekvege; j++)
                {
                    if (block[i, j].Visible == true)
                    {
                        int x = block[i, j].Left / szel + 1;
                        int y = block[i, j].Top / hossz + 1;
                        if (kockak[x, y].TabIndex == 1)
                        {
                            timer1.Stop();
                            timer3.Stop();
                            jatekvege = true;
                            Form vege = new End();
                            vege.Show();
                            this.Close();
                        }
                    }
                }
            }
            if (!jatekvege)
            {
                timer1.Enabled = true;
                timer3.Enabled = true;
            }
            this.Focus();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (nehezseg == 1) { timer1.Interval = 700; }
                if (nehezseg == 2) { timer1.Interval = 300; }
                if (nehezseg == 3) { timer1.Interval = 100; }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {


            for (int i = 1; i <= 10; i++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    if (kockak[i, j].TabIndex == 0)
                    {
                        kockak[i, j].BackColor = Color.Transparent;
                        kockak[i, j].BorderStyle = BorderStyle.None;
                    }
                }
            }



            if (e.KeyCode == Keys.Escape) { Application.Exit(); }
            else if (e.KeyCode != Keys.Up)
            {
                if (Control.ModifierKeys == Keys.Shift)
                {
                    lemegy();
                }
                if (e.KeyCode == Keys.Left)
                {
                    balra();
                }

                if (e.KeyCode == Keys.Right)
                {
                    jobbra();
                }

                if (e.KeyCode == Keys.Down)
                {
                    //lemegy();
                    timer1.Interval = 50;
                }
            }
            else
            {
                if (szam == 1)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        if (egyfel == true)
                        {
                            bool fordul = true;
                            for (int i = 2; i <= 4; i++)
                            {
                                int x = block[1, i].Left / szel + 1;
                                int y = block[1, i].Top / hossz + 1;
                                if (kockak[x, y].TabIndex == 1)
                                {
                                    fordul = false;
                                }
                            }
                            if (fordul)
                            {
                                for (int i = 2; i <= 4; i++)
                                {
                                    //block[1, i].TabIndex = 1;
                                    block[1, i].Visible = true;
                                    //block[i, 1].TabIndex = 0;
                                    block[i, 1].Visible = false;
                                }
                                egyfel = false;
                            }
                        }
                        else
                        {
                            bool fordul = true;
                            for (int i = 2; i <= 4; i++)
                            {
                                int x = block[i, 1].Left / szel + 1;
                                int y = block[i, 1].Top / hossz + 1;
                                if (x < 11 && kockak[x, y].TabIndex == 1)
                                {
                                    fordul = false;
                                }
                            }
                            if (fordul)
                            {
                                if (block[1, 1].Left > (10 - 4) * szel)
                                {
                                    int allando = block[1, 1].Left / szel;
                                    for (int i = 1; i <= 4; i++)
                                    {
                                        for (int j = 1; j <= 4; j++)
                                        {
                                            block[i, j].Left -= (4 - (10 - allando)) * szel;

                                        }
                                    }

                                }
                                for (int i = 2; i <= 4; i++)
                                {
                                    block[1, i].Visible = false;
                                    //block[1, i].TabIndex = 0;
                                    block[i, 1].Visible = true;
                                    //block[i, 1].TabIndex = 1;
                                }
                                egyfel = true;
                            }
                        }
                    }

                }
                if (szam == 2)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        if (kettofel == true)
                        {
                            int x = block[1, 2].Left / szel + 1;
                            int x1 = block[2, 2].Left / szel + 1;
                            int y = block[1, 2].Top / hossz + 1;
                            int y1 = block[1, 2].Top / hossz + 1;
                            if (kockak[x, y + 1].TabIndex == 0 && kockak[x1, y1 + 1].TabIndex == 0
                                && kockak[x, y].TabIndex == 0 && kockak[x1 - 1, y1].TabIndex == 0)
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {

                                        block[i, j].Visible = false;
                                        if ((i == 2 && j < 4) || (i == 1 && j == 3))
                                        {
                                            block[i, j].Visible = true;
                                        }
                                    }
                                }
                                kettofel = false;
                                kettojobb = true;
                            }
                        }
                        else if (kettojobb == true)
                        {
                            int x = block[2, 1].Left / szel + 1;
                            int x1 = block[2, 2].Left / szel + 1;
                            int y = block[2, 1].Top / hossz + 1;
                            int y1 = block[2, 2].Top / hossz + 1;
                            if (x < 11 && kockak[x - 1, y].TabIndex == 0 && ((kockak[x + 1, y].TabIndex == 0 &&
                                kockak[x + 1, y + 1].TabIndex == 0) || x == 10))
                            { for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        if (!(kockak[x + 1, y].TabIndex == 0 && kockak[x1 + 1, y1].TabIndex == 0))
                                            block[i, j].Left -= szel;
                                        block[i, j].Visible = false;
                                        if ((j == 1 && i < 4) || (i == 3 && j == 2))
                                        {
                                            block[i, j].Visible = true;
                                        }
                                    }
                                }
                                kettojobb = false;
                                kettole = true;
                            }

                        }
                        else if (kettole == true)
                        {
                            int x = block[1, 1].Left / szel + 1;
                            int y = block[1, 1].Top / hossz + 1;
                            if (kockak[x, y + 1].TabIndex == 0 && kockak[x, y + 2].TabIndex == 0)
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        block[i, j].Visible = false;
                                        if ((i == 1 && j < 4) || (i == 2 && j == 1))
                                        {
                                            block[i, j].Visible = true;
                                        }
                                    }
                                }
                            kettole = false;
                        }
                        else
                        {
                            int x = block[2, 1].Left / szel + 1;
                            int y = block[2, 1].Top / hossz + 1;
                            if (x < 11 && kockak[x, y + 1].TabIndex == 0 && (kockak[x + 1, y + 1].TabIndex == 0 || x == 10))
                            { for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        if (kockak[x + 1, y + 1].TabIndex == 1)
                                            block[i, j].Left -= szel;
                                        block[i, j].Visible = false;
                                        if (!(j > 2 || i > 3 || (j == 1 && (i == 2 || i == 3))))
                                        {
                                            block[i, j].Visible = true;
                                        }
                                    }
                                }
                                kettofel = true;
                            }
                        }
                    }

                }
                if (szam == 3)
                {

                    if (e.KeyCode == Keys.Up)
                    {
                        if (haromfel == true)
                        {
                            int x = block[2, 2].Left / szel + 1;
                            int y = block[2, 2].Top / hossz + 1;
                            if (kockak[x, y + 1].TabIndex == 0 && kockak[x, y - 1].TabIndex == 0 && kockak[x - 1, y - 1].TabIndex == 0)
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        block[i, j].Visible = false;
                                        if ((i == 1 && j == 1) || (i == 2 && j < 4))
                                            block[i, j].Visible = true;
                                    }
                                }
                                haromfel = false;
                                haromjobb = true;
                            }
                        }
                        else if (haromjobb == true)
                        {

                            int x = block[2, 1].Left / szel + 1;
                            int y = block[2, 1].Top / hossz + 1;
                            if (x < 11 && kockak[x - 1, y + 1].TabIndex == 0 && (kockak[x + 1, y].TabIndex == 0 || x == 10))
                            { for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        if (kockak[x + 1, y].TabIndex == 1)
                                            block[i, j].Left -= szel;
                                        block[i, j].Visible = false;
                                        if ((j == 1 && i < 4) || (i == 1 && j < 3))
                                            block[i, j].Visible = true;
                                    }
                                }
                                haromjobb = false;
                                haromle = true;
                            }

                        }
                        else if (haromle == true)
                        {
                            int x = block[1, 2].Left / szel + 1;
                            int y = block[1, 2].Top / hossz + 1;
                            if (kockak[x, y + 1].TabIndex == 0 && kockak[x + 1, y + 1].TabIndex == 0)
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        block[i, j].Visible = false;
                                        if ((i == 1 && j < 4) || (i == 2 && j == 3))
                                            block[i, j].Visible = true;
                                    }
                                }
                                haromle = false;
                                harombal = true;
                            }
                        }
                        else
                        {

                            int x = block[1, 2].Left / szel + 1;
                            int y = block[1, 2].Top / hossz + 1;
                            if (x < 11 && kockak[x + 1, y].TabIndex == 0 && ((kockak[x + 2, y].TabIndex == 0 &&
                                kockak[x + 2, y - 1].TabIndex == 0) || x == 9))
                            { for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        if (kockak[x + 2, y].TabIndex == 1 || kockak[x + 2, y - 1].TabIndex == 1)
                                            block[i, j].Left -= szel;
                                        block[i, j].Visible = false;
                                        if ((i == 3 && j < 3) || (j == 2 && i < 4))
                                            block[i, j].Visible = true;
                                    }
                                }
                                harombal = false;
                                haromfel = true;
                            }
                        }
                    }

                }
                if (szam == 5)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        if (otfel == true)
                        {
                            int x = block[1, 1].Left / szel + 1;
                            int y = block[1, 1].Top / hossz + 1;
                            if (kockak[x, y].TabIndex == 0 && kockak[x + 1, y + 2].TabIndex == 0)
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        block[i, j].Visible = false;
                                        if ((i == 1 && j < 3) || (i == 2 && j > 1 && j < 4))
                                            block[i, j].Visible = true;
                                    }
                                }
                                otfel = false;
                            }
                        }
                        else
                        {
                            int x = block[1, 1].Left / szel + 1;
                            int y = block[1, 1].Top / hossz + 1;
                            if (x < 10 && kockak[x + 1, y].TabIndex == 0 && (kockak[x + 2, y].TabIndex == 0 || x == 9))
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        if (x == 9)
                                            block[i, j].Left -= szel;
                                        block[i, j].Visible = false;
                                        if ((i > 1 && i < 4 && j == 1) || (j == 2 && i < 3))
                                            block[i, j].Visible = true;
                                    }
                                }
                                otfel = true;
                            }
                        }
                    }

                }
                if (szam == 6)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        if (hatfel == true)
                        {
                            int x = block[1, 1].Left / szel + 1;
                            int y = block[1, 1].Top / hossz + 1;
                            if (kockak[x, y + 1].TabIndex == 0 && kockak[x, y + 2].TabIndex == 0)
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        block[i, j].Visible = false;
                                        if ((i == 2 && j < 3) || (i == 1 && j > 1 && j < 4))
                                            block[i, j].Visible = true;
                                    }
                                }
                                hatfel = false;
                            }
                        }
                        else
                        {
                            int x = block[1, 1].Left / szel + 1;
                            int y = block[1, 1].Top / hossz + 1;
                            if (x < 10 && kockak[x, y].TabIndex == 0 && (kockak[x + 2, y + 1].TabIndex == 0 || x == 9))
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        if (x == 9)
                                            block[i, j].Left -= szel;
                                        block[i, j].Visible = false;
                                        if ((i < 3 && j == 1) || (i > 1 && i < 4 && j == 2))
                                            block[i, j].Visible = true;
                                    }
                                }
                                hatfel = true;
                            }
                        }
                    }

                }
                if (szam == 7)
                {
                    if (e.KeyCode == Keys.Up)
                    {
                        if (hetfel == true)
                        {
                            int x = block[1, 1].Left / szel + 1;
                            int y = block[1, 1].Top / hossz + 1;
                            if (kockak[x + 1, y + 2].TabIndex == 0)
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        block[i, j].Visible = false;
                                        if ((i == 2 && j < 4) || (i == 1 && j == 2))
                                            block[i, j].Visible = true;
                                    }
                                }
                                hetfel = false;
                                hetjobb = true;
                            }
                        }
                        else if (hetjobb == true)
                        {
                            int x = block[1, 1].Left / szel + 1;
                            int y = block[1, 1].Top / hossz + 1;
                            if (kockak[x, y].TabIndex == 0 && (kockak[x + 2, y].TabIndex == 0 || x == 9))
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        if (x == 9)
                                            block[i, j].Left -= szel;
                                        block[i, j].Visible = false;
                                        if ((j == 1 && i < 4) || (i == 2 && j == 2))
                                            block[i, j].Visible = true;
                                    }
                                }
                                hetjobb = false;
                                hetle = true;
                            }
                        }
                        else if (hetle == true)
                        {
                            int x = block[1, 1].Left / szel + 1;
                            int y = block[1, 1].Top / hossz + 1;
                            if (kockak[x, y + 1].TabIndex == 0 && kockak[x, y + 2].TabIndex == 0)
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        block[i, j].Visible = false;
                                        if ((i == 1 && j < 4) || (i == 2 && j == 2))
                                            block[i, j].Visible = true;
                                    }
                                }
                                hetle = false;
                                hetbal = true;
                            }
                        }
                        else
                        {
                            int x = block[1, 1].Left / szel + 1;
                            int y = block[1, 1].Top / hossz + 1;
                            if (kockak[x + 1, y].TabIndex == 0 && (kockak[x + 2, y + 1].TabIndex == 0 || x == 9))
                            {
                                for (int i = 1; i <= 4; i++)
                                {
                                    for (int j = 1; j <= 4; j++)
                                    {
                                        if (x == 9)
                                            block[i, j].Left -= szel;
                                        block[i, j].Visible = false;
                                        if ((j == 2 && i < 4) || (i == 2 && j == 1))
                                            block[i, j].Visible = true;
                                    }
                                }
                                hetbal = false;
                                hetfel = true;
                            }
                        }
                    }
                }
            }

        }

        private void balra()
        {
            bool mehet = true;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    if (block[i, j].Visible == true)
                    {
                        int x = block[i, j].Left / szel + 1;
                        int y = block[i, j].Top / hossz + 1;
                        if (kockak[x - 1, y].TabIndex == 1)
                        {
                            mehet = false;
                        }
                    }
                }
            }
            if (mehet)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        block[i, j].Left -= szel;
                    }
                }
            }
        }

        private void jobbra()
        {
            bool mehet = true;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    if (block[i, j].Visible == true)
                    {
                        int x = block[i, j].Left / szel + 1;
                        int y = block[i, j].Top / hossz + 1;
                        if (kockak[x + 1, y].TabIndex == 1)
                        {
                            mehet = false;
                        }
                    }
                }
            }
            if (mehet)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        block[i, j].Left += szel;
                    }
                }
            }
        }

        private void lemegy()
        {
            if (timer2.Enabled == false)
            {
                timer2.Enabled = true;

                bool okes = true;
                while (okes)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        for (int j = 1; j <= 4; j++)
                        {
                            if (block[i, j].Visible == true)
                            {
                                int x = block[i, j].Left / szel + 1;
                                int y = block[i, j].Top / hossz + 1;
                                /*label1.Text= x.ToString();
                                label2.Text= y.ToString();*/
                                if (kockak[x, y + 1].TabIndex == 1)
                                {
                                    okes = false;
                                    for (int k = 1; k <= 4; k++)
                                    {
                                        for (int l = 1; l <= 4; l++)
                                        {
                                            if (block[k, l].Visible == true)
                                            {
                                                x = block[k, l].Left / szel + 1;
                                                y = block[k, l].Top / hossz + 1;
                                                /*label1.Text = x.ToString();
                                                label2.Text = y.ToString();*/
                                                kockak[x, y].TabIndex = 1;
                                                kockak[x, y].Text = "";
                                                kockak[x, y].BackColor = block[i, j].BackColor;
                                                kockak[x, y].BorderStyle = BorderStyle.FixedSingle;
                                                kockak[x, y].BringToFront();
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                    if (okes)
                        for (int i = 1; i <= 4; i++)
                        {
                            for (int j = 1; j <= 4; j++)
                            {
                                block[i, j].Top += hossz;
                            }
                        }
                }
                ellenoriz();
                spawn();
            }
        }


        private void ellenoriz()
        {
            bool cserelt = false;
            bool oksor = true;
            for (int j = 20; j > 0; j--)
            {
                oksor = true;
                for (int i = 1; i <= 10 && oksor; i++)
                {
                    if (kockak[i, j].TabIndex == 0)
                        oksor = false;
                }
                for (int i = 1; i <= 10 && oksor; i++)
                {
                    kockak[i, j].TabIndex = 0;
                    kockak[i, j].BackColor = Color.Transparent;
                    kockak[i, j].BorderStyle = BorderStyle.None;
                    for (int k = j; k > 0; k--)
                    {
                        if (kockak[i, k].TabIndex == 1)
                        {
                            cserelt = true;
                            if (gravitacioertek == 1)
                                gravitacio(i, k);
                            else
                                lees(i, k);
                        }

                    }
                }
                if (oksor)
                {
                    pontszam++;
                    label2.Text = pontszam.ToString();
                }
            }
            if (cserelt)
            {
                ellenoriz();
            }

        }

        private void lees(int x, int y)
        {
            kockak[x, y + 1].Text = "";
            kockak[x, y + 1].BackColor = kockak[x, y].BackColor;
            kockak[x, y + 1].TabIndex = 1;
            kockak[x, y + 1].BorderStyle = BorderStyle.FixedSingle;
            kockak[x, y].BackColor = Color.Transparent;
            kockak[x, y].TabIndex = 0;
            kockak[x, y].BorderStyle = BorderStyle.None;
        }
        private void gravitacio(int x,int y)
        {
            for (int i=y;i<20;i++)
            {
                if (kockak[x, i+1].TabIndex == 0)
                {
                    kockak[x, i + 1].Text = "";
                    kockak[x, i+1].BackColor = kockak[x,y].BackColor;
                    kockak[x, i + 1].TabIndex = 1;
                    kockak[x, i + 1].BorderStyle = BorderStyle.FixedSingle;
                    kockak[x, y].BackColor = Color.Transparent;
                    kockak[x, y].TabIndex = 0;
                    kockak[x,y].BorderStyle= BorderStyle.None;
                    y = i+1;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            bool jo = true;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    if (block[i, j].Visible == true)
                    {
                        int x = block[i, j].Left / szel + 1;
                        int y = block[i, j].Top / hossz + 1;
                        if (kockak[x, y + 1].TabIndex == 1)
                        {

                            for (int k = 1; k <= 4; k++)
                            {
                                for (int l = 1; l <= 4; l++)
                                {
                                    if (block[k, l].Visible == true)
                                    {
                                        x = block[k, l].Left / szel + 1;
                                        y = block[k, l].Top / hossz + 1;
                                        /*label1.Text = x.ToString();
                                        label2.Text = y.ToString();*/
                                        kockak[x, y].TabIndex = 1;
                                        kockak[x, y].Text = "";
                                        kockak[x, y].BackColor = block[i,j].BackColor;
                                        kockak[x, y].BorderStyle = BorderStyle.FixedSingle;
                                        kockak[x, y].BringToFront();
                                    }
                                }
                            }
                            jo = false;
                            break;
                        }
                    }

                }
            }
            if (jo)
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        block[i, j].Top += hossz;
                    }
                }
            else
            {
                ellenoriz();
                spawn();
            }

            
        }


        private void timer3_Tick(object sender, EventArgs e)
        {
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    block1[i, j].Left = block[i, j].Left;
                    block1[i, j].Top = block[i, j].Top;
                    block1[i, j].Visible = block[i, j].Visible;
                }
            }
            bool okes = true;
            while (okes)
            {
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        if (block1[i, j].Visible == true)
                        {
                            int x = block1[i, j].Left / szel + 1;
                            int y = block1[i, j].Top / hossz + 1;
                            if (kockak[x, y + 1].TabIndex == 1)
                            {
                                okes = false;
                                for (int k = 1; k <= 4; k++)
                                {
                                    for (int l = 1; l <= 4; l++)
                                    {
                                        if (block[k, l].Visible == true)
                                        {
                                            x = block1[k, l].Left / szel + 1;
                                            y = block1[k, l].Top / hossz + 1;
                                            kockak[x, y].BackColor = Color.Transparent;
                                            kockak[x, y].BorderStyle = BorderStyle.FixedSingle;
                                            //kockak[x, y].BringToFront();
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
                if (okes)
                    for (int i = 1; i <= 4; i++)
                    {
                        for (int j = 1; j <= 4; j++)
                        {
                            block1[i, j].Top += hossz;
                        }
                    }
            }
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form menu = new Menu();
            menu.Show();
            this.Close();
        }
    }
}
