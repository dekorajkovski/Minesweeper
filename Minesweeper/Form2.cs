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
    public partial class Form2 : Form
    {
        public String name { get; set; }
        public String choice { get; set; }
        
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            name = textBox1.Text.Trim();
            if (radioButton1.Checked) choice = radioButton1.Text;
            else if (radioButton2.Checked) choice = radioButton2.Text;
            else choice = radioButton3.Text;
            this.Close();
        }
    }
}
