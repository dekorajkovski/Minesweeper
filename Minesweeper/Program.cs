using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    static class Program
    {
        public static MineField[][] fields=new MineField[20][];

        public static void recursiveSearch() {

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            for (int i = 0; i < 20; i++)
            {
                fields[i] = new MineField[20];
                for (int j = 0; j < 20; j++)
                {
                    fields[i][j] = new MineField();
                }
            }

            for (int i = 0; i < 20; i++)
            {
                fields[i]
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
