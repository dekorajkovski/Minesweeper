using System;
using System.Collections.Generic;
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
        public MineField up { get; set; }
        public MineField down { get; set; }
        public MineField left { get; set; }
        public MineField right { get; set; }

        public Boolean isBomb { get; set; }

        public Status status { get; set; }

        public MineField():base() {
            
            up = down = left = right = null;
            isBomb = false;
            status = Status.normal;
        }
        
    }
}
