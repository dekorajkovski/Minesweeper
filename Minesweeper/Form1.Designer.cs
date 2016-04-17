namespace Minesweeper
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mineField = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // mineField
            // 
            for (int i = 0; i < Program.GRID_MAX; i++)
            {
                for (int j = 0; j < Program.GRID_MAX; j++)
                {
                    mineField.Controls.Add(Program.fields[i][j]);
                }
            }
            this.mineField.AccessibleName = "";
            this.mineField.Location = new System.Drawing.Point(12, 12);
            this.mineField.Name = "mineField";
            this.mineField.Size = new System.Drawing.Size(260, 237);
            this.mineField.TabIndex = 0;
            this.mineField.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.mineField);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel mineField;
    }
}

