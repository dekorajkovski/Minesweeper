using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    static class Program
    {
        public static MineField[][] fields=new MineField[20][];
        public static string timeplayed = "0";
        public static Dictionary<String, PlayerStatistics> stats;
        public static FileStream statisticsFile = null;
        public static int GRID_MAX=15;
        public static bool canPlay = true;
        public static bool firstClick = true;
        public static System.Windows.Forms.Timer timer1;
        public static Thread oThread;
        public static Semaphore canContinue = new Semaphore(1,1);
        public static Random random = new Random();
        public static int uncoveredFields = 0;
        public static int numOfTotalBombs = 0;
        public static int selectedFlags = 0;
        public static int selectedQuestionMarks = 0;
        public static int totalFields = 0;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        public static bool hasWon() {
            if (totalFields - uncoveredFields - numOfTotalBombs == 0)
            {
                updateStats(true);
                StreamWriter sw = new StreamWriter(statisticsFile);
                sw.AutoFlush = true;
                sw.WriteLine(Form2.name + " 1 " + Program.timeplayed);
                return true;
            }
            return false;
        }

        public static void writeStats()
        {
            
        }

        private static void clickEvent(object sender, EventArgs e)
        {
            if (!canPlay) return;
            try
            {
                MouseEventArgs me = (MouseEventArgs)e;
                MineField field = (MineField)sender;
                if (me.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    if (field.status == MineField.Status.uncovered) return;
                    if (field.status == MineField.Status.normal)
                    {
                        field.status = MineField.Status.flagged;
                        field.uncover("flag");
                    }
                    else if (field.status == MineField.Status.flagged)
                    {
                        field.status = MineField.Status.unknown;
                        field.uncover("ques");
                    }
                    else if (field.status == MineField.Status.unknown)
                    {
                        field.status = MineField.Status.normal;
                        field.uncover("normal");
                    }
                }
                else {
                    if (field.isBomb)
                    {
                        if (firstClick)
                        {
                            
                            field.isBomb = false;
                            Program.numOfTotalBombs--;
                            firstClick = false;
                        }
                        else
                        {
                            field.BackColor = Color.Red;
                            field.uncover("bomb");
                            Program.canPlay = false;
                            uncoverBombs();
                            Program.timer1.Stop();
                            StreamWriter sw = new StreamWriter(statisticsFile);
                            sw.AutoFlush = true;
                            sw.WriteLine(Form2.name+" 0 "+Program.timeplayed);
                            updateStats(false);
                            
                            MessageBox.Show("You lost, try again.","BOMB!",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                        }
                    }
                    //field.status = MineField.Status.uncovered;
                    if (field.status == MineField.Status.normal)
                        field.calculate();
                    firstClick = false;
                   // Debug.WriteLine("field clicked");
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex) {
#pragma warning restore CS0168 // Variable is declared but never used
            }
        }

       
        private class GenerateFields {
            public static void generate()
            {
                MineField.map = new Dictionary<string, Image>();
                for (int i = 1; i <= 8; i++)
                {
                    MineField.map.Add(i.ToString(), Image.FromFile("../../img/" + i.ToString() + ".png"));
                }
                MineField.map.Add("bomb", Image.FromFile("../../img/bomba.png"));
                MineField.map.Add("flag", Image.FromFile("../../img/flag.png"));
                MineField.map.Add("ques", Image.FromFile("../../img/ques.png"));
                canContinue.WaitOne();
                for (int i = 0; i < GRID_MAX; i++)
                {
                    fields[i] = new MineField[20];
                    for (int j = 0; j < GRID_MAX; j++)
                    {

                        fields[i][j] = new MineField();
                        //if (((i + j) % 5) == 0)
                        //{
                        //    fields[i][j].isBomb = true;
                        //    Debug.WriteLine(i.ToString() + " " + j.ToString());
                        //}
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
                fields[li-1][i].down = null;

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

        static void Shuffle(bool[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + (int)(random.NextDouble() * (n - i));
                bool t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }

        public static void randomNewGame(int rows) {
            bool[] ar = new bool[rows*rows];
            int bombs = (int)Math.Round(0.25 * rows * rows);
            Program.numOfTotalBombs = bombs;
            for (int i = 0; i < rows*rows; i++)
            {
                if (i < bombs) ar[i] = true;
                else ar[i] = false;
            }
            Shuffle(ar);
            Shuffle(ar);
            Shuffle(ar);
            int k = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    fields[i][j].isBomb = ar[k];
                    k++;
                }
            }

        }



       /* public static void boiKocka(object sender, PaintEventArgs e)
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
        } */

        public static void updateStats(bool win)
        {
            if (!stats.ContainsKey(Form2.name))
            {
                PlayerStatistics ps = new PlayerStatistics(Form2.name);
                ps.totalPlayed = 1;
                
                if (win)
                {
                    ps.totalWon++;
                }

                int f = 0;
                int.TryParse(Program.timeplayed, out f);
                ps.minsPlayed = f;

                stats.Add(Form2.name, ps);

            }
            else {
                PlayerStatistics tmp = null;
                stats.TryGetValue(Form2.name, out tmp);
                tmp.totalPlayed++;
                
                if (win)
                {
                    tmp.totalWon++;
                }

                int f = 0;
                int.TryParse(Program.timeplayed, out f);
                if (tmp.minsPlayed > f && win)
                    tmp.minsPlayed = f;

            }
        }

        static void Main()
        {
            if (!File.Exists("../../res/saveFile.txt"))
                statisticsFile= File.Create("../../res/saveFile.txt");
            else statisticsFile = File.Open("../../res/saveFile.txt", FileMode.Open);

            Debug.WriteLine(statisticsFile.CanRead);
            stats = new Dictionary<string, PlayerStatistics>();
            StreamReader stream=new StreamReader(statisticsFile);
            string line = "";
            while ((line = stream.ReadLine()) != null) {
                string[] seperated = line.Split(' ');
                if (!stats.ContainsKey(seperated[0]))
                {
                    PlayerStatistics ps = new PlayerStatistics(seperated[0]);
                    ps.totalPlayed = 1;
                    int g = 0;
                    int.TryParse(seperated[1], out g);
                    if (g == 1) {
                        ps.totalWon++;
                    }

                    int f = 0;
                    int.TryParse(seperated[2], out f);
                    ps.minsPlayed = f;

                    stats.Add(seperated[0], ps);

                }
                else {
                    PlayerStatistics tmp = null;
                    stats.TryGetValue(seperated[0], out tmp);
                    tmp.totalPlayed++;
                    int g = 0;
                    int.TryParse(seperated[1], out g);
                    if (g == 1)
                    {
                        tmp.totalWon++;
                    }

                    int f = 0;
                    int.TryParse(seperated[2], out f);
                    if(tmp.minsPlayed>f && g==1)
                    tmp.minsPlayed = f;

                }
            }
            ////stream.Close();
            
            Application.SetCompatibleTextRenderingDefault(false);
             oThread = new Thread(new ThreadStart(GenerateFields.generate));
            oThread.Start();
            //constructFields();
              
            
            Application.EnableVisualStyles();
            Application.Run(new Form1());

        }
    }
}
