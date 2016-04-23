using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        String name;
        DateTime vremeStart;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Minesweeper";
            textBox1.Hide();
            mineField.Hide();
            this.BackgroundImage = Image.FromFile("../../img/bground.jpg");
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = (DateTime.Now - vremeStart).ToString(@"mm\:ss");
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if (f2.name != "" && f2.choice != null)
            {
                mineField.Controls.Clear();
                name = f2.name;
                if (f2.choice.Equals("Easy"))
                {
                    this.Height = 260;
                    this.Width = 195;
                    mineField.Width = 170;
                    mineField.Height = 200;
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            mineField.Controls.Add(Program.fields[i][j]);
                        }
                    }
                }
                else if (f2.choice.Equals("Medium"))
                {
                    this.Height = 420;
                    this.Width = 345;
                    mineField.Width = 320;
                    mineField.Height = 370;
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            mineField.Controls.Add(Program.fields[i][j]);
                        }
                    }
                }
                else
                {
                    this.Height = 580;
                    this.Width = 495;
                    mineField.Width = 470;
                    mineField.Height = 540;
                    for (int i = 0; i < 15; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            mineField.Controls.Add(Program.fields[i][j]);
                        }
                    }
                }
                vremeStart = DateTime.Now;
                this.BackgroundImage = null;
                this.BackColor = Color.SeaGreen;
                timer1.Start();
                mineField.Show();
                textBox1.Show();
                label1.Hide();
        }
    }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This game is a FINKI project.\nMade by:\nBorijan Georgievski\nDejan Rajkovski\nBodan Gjozinski","About");
        }
    }
}
