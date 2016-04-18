using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Dictionary<String, Image> map;
        public MineField up { get; set; }
        public MineField down { get; set; }
        public MineField left { get; set; }
        public MineField right { get; set; }

        public Boolean isBomb { get; set; }

        public Status status { get; set; }

        public MineField():base() {

            map = new Dictionary<string, Image>();
            for (int i=1; i<=8; i++)
            {
                map.Add(i.ToString(), Image.FromFile("../../img/" + i.ToString() + ".png"));
            }
            map.Add("bomb", Image.FromFile("../../img/bomba.png"));
            //map.Add("0", Image.FromFile("../../img/blank.png"));

            this.BackColor = Color.Black;
            //this.Location = new System.Drawing.Point(3, 3);
            //this.Name = "pictureBox1";
            this.Size = new System.Drawing.Size(25, 25);
            this.TabIndex = 0;
            this.TabStop = false;
           
            up = down = left = right = null;
            isBomb = false;
            status = Status.normal;
        }

        public void calculate() {
            if (this.isBomb)
            {
                this.uncover("bomb");
                
            }
            else if (this.status.Equals(Status.uncovered))
            {
                return;
            }
            else {
                int num = 0;
                if (this.up != null && this.up.isBomb) num++;
                
                else if (this.up != null) {
                    this.up.calculate();
                }
                if (this.up != null&&this.up.right!=null&&this.up.right.isBomb) num++;
                else if (this.up != null && this.up.right != null)
                {
                    this.up.right.calculate();
                }
                if (this.left != null && this.left.isBomb) num++;
                else if (this.left != null) {
                    this.left.calculate();
                }
                if (this.left != null && this.left.up != null && this.left.up.isBomb) num++;
                else if (this.left != null && this.left.up != null) {
                    this.left.up.calculate();
                }
                if (this.down != null && this.down.isBomb) num++;
                else if (this.down != null) {
                    this.down.calculate();
                }
                if (this.down != null && this.down.left != null && this.down.left.isBomb) num++;
                else if (this.down != null && this.down.left != null) {
                    this.down.left.calculate();
                }
                if (this.right != null && this.right.isBomb) num++;
                else if (this.right != null) {
                    this.right.calculate();
                }
                if (this.right != null && this.right.down != null && this.right.down.isBomb) num++;
                else if (this.right != null && this.right.down != null) {
                    this.right.down.calculate();
                }
                
                this.uncover(num.ToString());
                return;
            }
        }
        public void uncover(String a) {
            Image img;

            map.TryGetValue(a, out img);
            this.Image = img;

            if (a.Equals("bomb"))
            {
                //TODO: FINISH GAME
            }
                
        }
        
    }
}
