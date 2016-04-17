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
        DateTime vremeStart;
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < Program.GRID_MAX; i++)
            {
                for (int j = 0; j < Program.GRID_MAX; j++)
                {
                    mineField.Controls.Add(Program.fields[i][j]);
                }
            }
        }


        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            vremeStart = DateTime.Now;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = (DateTime.Now - vremeStart).ToString(@"mm\:ss");
        }
    }
}
