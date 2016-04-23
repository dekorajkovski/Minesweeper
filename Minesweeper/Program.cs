﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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
                //field.status = MineField.Status.uncovered;
                if(field.status!=MineField.Status.uncovered)
                field.calculate();
                Debug.WriteLine("field clicked");
            }
            catch (Exception ex) {
            }
        }
        public static void setLimits(int li) {


        }
        
        public static void constructFields() {
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
                    fields[i][j].BackColor = Color.White;
                    fields[i][j].Image= null;
                    fields[i][j].status = MineField.Status.normal;
                }
            }
        }
       

        static void Main()
        {
            

            Application.SetCompatibleTextRenderingDefault(false);

            constructFields();
              
            
            Application.EnableVisualStyles();
            Application.Run(new Form1());

        }
    }
}
