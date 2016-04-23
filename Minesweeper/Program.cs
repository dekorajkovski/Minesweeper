using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    static class Program
    {
        public static MineField[][] fields=new MineField[20][];

        public static int GRID_MAX=20;
        public static bool canPlay = true;
        public static System.Windows.Forms.Timer timer1;
        public static Thread oThread;
        public static Semaphore canContinue = new Semaphore(1,1);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        private static void clickEvent(object sender, EventArgs e)
        {
            if (!canPlay) return;
            try
            {
                MineField field = (MineField)sender;
                if (field.isBomb) {
                    field.uncover("bomb");
                    Program.canPlay = false;
                    uncoverBombs();
                    Program.timer1.Stop();
                }
                //field.status = MineField.Status.uncovered;
                if(field.status!=MineField.Status.uncovered)
                field.calculate();
                Debug.WriteLine("field clicked");
            }
            catch (Exception ex) {
            }
        }
        private class GenerateFields {
            public static void generate()
            {
                canContinue.WaitOne();
                for (int i = 0; i < GRID_MAX; i++)
                {
                    fields[i] = new MineField[20];
                    for (int j = 0; j < GRID_MAX; j++)
                    {

                        fields[i][j] = new MineField();
                        if (((i + j) % 5) == 0)
                        {
                            fields[i][j].isBomb = true;
                            Debug.WriteLine(i.ToString() + " " + j.ToString());
                        }
                        fields[i][j].Click += clickEvent;
                    }
                }
                canContinue.Release();
                
                oThread.Abort();
                connect();
            }

        }

        public static void uncoverBombs() {
            for (int i = 0; i < GRID_MAX; i++)
            {
                for (int j = 0; j < GRID_MAX; j++)
                {
                    if (fields[i][j].isBomb) fields[i][j].uncover("bomb");
                }
            }
        }

        public static void setLimits(int li) {
            connect();
            for (int i = 0; i < li; i++) {
                fields[i][li-1].right = null;
                fields[li-1][i].right = null;

            }

        }
        
        
        public static void connect() {
            for (int i = 0; i < GRID_MAX; i++)
            {
                for (int j = 0; j < GRID_MAX; j++)
                {
                    if (i != 0)
                        fields[i][j].up = fields[i - 1][j];
                    else fields[i][j].up = null;
                    if (j != GRID_MAX - 1)
                        fields[i][j].right = fields[i][j + 1];
                    else fields[i][j].right = null;
                    if (j != 0)
                        fields[i][j].left = fields[i][j - 1];
                    else fields[i][j].left = null;
                    if (i != GRID_MAX - 1)
                        fields[i][j].down = fields[i + 1][j];
                    else fields[i][j].down = null;
                }
            }
        }
        public static void resetFields() {
            for (int i = 0; i < GRID_MAX; i++)
            {
                for (int j = 0; j < GRID_MAX; j++)
                {
                    fields[i][j].BackColor = Color.Beige;
                    fields[i][j].Image= null;
                    fields[i][j].status = MineField.Status.normal;

                   

                }
            }
        }

        public static void boiKocka(object sender, PaintEventArgs e)
        {
            try
            {
                MineField mf = (MineField)sender;
                LinearGradientBrush linGrBrush = new LinearGradientBrush(new Point(mf.Height, 0), new Point(0, mf.Width), Color.FromArgb(255, 255, 225, 225), Color.FromArgb(50, 50, 50, 50));

                Pen pen = new Pen(linGrBrush);

                e.Graphics.FillRectangle(linGrBrush, 0, 0, mf.Height, mf.Width);

            }
            catch (Exception ex) {
            }
        }


        static void Main()
        {
            

            Application.SetCompatibleTextRenderingDefault(false);
             oThread = new Thread(new ThreadStart(GenerateFields.generate));
            oThread.Start();
            //constructFields();
              
            
            Application.EnableVisualStyles();
            Application.Run(new Form1());

        }
    }
}
