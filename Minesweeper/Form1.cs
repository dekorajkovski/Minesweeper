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
            
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {    
            Form2 f2 = new Form2();
            f2.ShowDialog();
            if (f2.name != "" && f2.choice != null)
            {
                name = f2.name;
                if (f2.choice.Equals("Лесно"))
                {
                    mineField.Controls.Clear();
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            mineField.Controls.Add(Program.fields[i][j]);
                        }
                    }
                }
                else if (f2.choice.Equals("Средно"))
                {
                    mineField.Controls.Clear();
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
                    mineField.Controls.Clear();
                    for (int i = 0; i < 15; i++)
                    {
                        for (int j = 0; j < 15; j++)
                        {
                            mineField.Controls.Add(Program.fields[i][j]);
                        }
                    }
                }
                vremeStart = DateTime.Now;
                timer1.Start();
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = (DateTime.Now - vremeStart).ToString(@"mm\:ss");
        }
    }
}
