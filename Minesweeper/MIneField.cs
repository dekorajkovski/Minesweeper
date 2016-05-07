using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public class MineField : PictureBox
    {
        public enum Status {
            uncovered,flagged,unknown,normal
        }
        public static Dictionary<String, Image> map;
        public MineField up { get; set; }
        public MineField down { get; set; }
        public MineField left { get; set; }
        public MineField right { get; set; }

        public Boolean isBomb { get; set; }

        public Status status { get; set; }

        public MineField():base() {
            this.Margin = new Padding(1);
            
            //map.Add("0", Image.FromFile("../../img/blank.png"));

            //this.Paint += new PaintEventHandler(Program.boiKocka);
            this.Refresh();
            //this.BackColor = Color.White;
            //this.Location = new System.Drawing.Point(3, 3);
            //this.Name = "pictureBox1";
            this.Size = new System.Drawing.Size(25, 25);
            this.TabIndex = 0;
            this.TabStop = false;
           
            up = down = left = right = null;
            isBomb = false;
            status = Status.normal;
        }

        private void recalculate()
        {
            if (this.up != null && this.up.status != Status.uncovered && !this.up.isBomb)
            {
                this.up.calculate();
            }

             if (this.up != null && this.up.right != null && this.up.right.status != Status.uncovered
                  && !this.up.right.isBomb)
            {
                this.up.right.calculate();
            }

             if (this.left != null && this.left.status != Status.uncovered && !this.left.isBomb)
            {
                this.left.calculate();
            }

             if (this.left != null && this.left.up != null && this.left.up.status != Status.uncovered
                    && !this.left.up.isBomb)
            {
                this.left.up.calculate();
            }

             if (this.down != null && this.down.status != Status.uncovered
                    && !this.down.isBomb)
            {
                this.down.calculate();
            }

             if (this.down != null && this.down.left != null && this.down.left.status != Status.uncovered
                   && !this.down.left.isBomb)
            {
                this.down.left.calculate();
            }

             if (this.right != null && this.right.status != Status.uncovered
                    && !this.right.isBomb)
            {
                this.right.calculate();
            }

             if (this.right != null && this.right.down != null && this.right.down.status != Status.uncovered
                    && !this.right.down.isBomb)
            {
                this.right.down.calculate();
            }
        }
        public void calculate() { 

            if (this.isBomb)
            {
                this.uncover("bomb");
                this.status = Status.uncovered;
            }

            else
            {
                if (!this.isBomb)
                {
                    this.status = Status.uncovered;
                    this.uncover("empty");
                    
                }
                int num = 0;
                if (this.up != null && this.up.isBomb) num++;


                if (this.up != null && this.up.right != null && this.up.right.isBomb) num++;

                if (this.left != null && this.left.isBomb) num++;

                if (this.left != null && this.left.up != null && this.left.up.isBomb) num++;

                if (this.down != null && this.down.isBomb) num++;

                if (this.down != null && this.down.left != null && this.down.left.isBomb) num++;

                if (this.right != null && this.right.isBomb) num++;

                if (this.right != null && this.right.down != null && this.right.down.isBomb) num++;


                this.uncover(num.ToString());

                if (num == 0) this.recalculate();
                Debug.WriteLine(num.ToString());
                return;
            }
        }
        public void uncover(String a)
        {

            Image img;
            if (a.Equals("empty"))
            {
                //this.Paint += new PaintEventHandler(boiOtkrienaKocka);
                this.BackColor = Color.DarkGray;
                Program.uncoveredFields++;
                Debug.WriteLine("uncovered:" + Program.uncoveredFields);
                Debug.WriteLine("left fields:" + (Program.totalFields - Program.uncoveredFields).ToString());
                if (Program.hasWon())
                {
                    Program.canPlay = false;
                    Program.uncoverBombs();
                    Program.timer1.Stop();
                    MessageBox.Show("VICTORY", "YOU WON!!!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                return;
            }
            map.TryGetValue(a, out img);
            this.Image = img;

            
        }
        
        
        

    }
}
