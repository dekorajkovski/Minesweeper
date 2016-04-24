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
            this.components = new System.ComponentModel.Container();
            InitializeComponent();
            Program.timer1 = new System.Windows.Forms.Timer(this.components);
            aboutToolStripMenuItem.Click += new EventHandler(aboutToolStripMenuItem_Click);
            Program.timer1.Tick += new System.EventHandler(timer1_Tick);
            MaximizeBox = false;
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
                Program.canContinue.WaitOne();
                mineField.Controls.Clear();
                name = f2.name;
                Program.canPlay = true;
                Program.resetFields();
                if (f2.choice.Equals("Easy"))
                {
                    this.Height = 240;
                    this.Width = 175;
                    mineField.Width = 150;
                    mineField.Height = 180;
                    Program.setLimits(5);
                    Program.randomNewGame(5);
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
                    this.Height = 380;
                    this.Width = 315;
                    mineField.Width = 280;
                    mineField.Height = 330;
                    Program.setLimits(10);
                    Program.randomNewGame(10);
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
                    this.Height = 520;
                    this.Width = 455;
                    mineField.Width = 410;
                    mineField.Height = 500;
                    Program.setLimits(15);
                    Program.randomNewGame(15);
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
                Program.timer1.Start();
                mineField.Show();
                textBox1.Show();
                label1.Hide();
                Program.canContinue.Release();
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
