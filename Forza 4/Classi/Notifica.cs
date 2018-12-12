using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Forza4
{
    class Notifica : Form
    {
        
        //private PictureBox pictureBox1;
        private Label label1;
        private int x;
        private int y;
        private double duration;
        private Timer timer;
        #region stonda bordi
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
     (
         int nLeftRect,     // x-coordinate of upper-left corner
         int nTopRect,      // y-coordinate of upper-left corner
         int nRightRect,    // x-coordinate of lower-right corner
         int nBottomRect,   // y-coordinate of lower-right corner
         int nWidthEllipse, // height of ellipse
         int nHeightEllipse // width of ellipse
     );
        #endregion       
        public Notifica(string text, double durata, Point position)
        {
            InitializeComponent(text);
            this.label1.Text = text;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            duration = durata;

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;

            timer.Start();

            x = position.X - this.Width/2;
            y = position.Y - this.Height/2;
            Load += new EventHandler(Caricamento);

            this.Show();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0.01)
            {
                //this.Opacity -= 0.0125;
                this.Opacity -= duration/100;   
            }
            else
            {
                timer.Stop();
                this.Close();
                this.Dispose();
            }

        }
        private void Caricamento(object sender, EventArgs e)
        {
            this.SetDesktopLocation(x, y);

            

        }
        private void InitializeComponent(string text)
        {
            ShowInTaskbar = false;
            TopMost = true;
            this.label1 = new Label();
            //this.pictureBox1 = new System.Windows.Forms.PictureBox();
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            //this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            //| System.Windows.Forms.AnchorStyles.Left)
            //| System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 14);
            this.label1.Text = text;
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            //this.label1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            //this.pictureBox1.Image = global::DemoChatForm.Properties.Resources.info;
            //this.pictureBox1.Location = new System.Drawing.Point(12, 14);
            //this.pictureBox1.Name = "pictureBox1";
            //this.pictureBox1.Size = new System.Drawing.Size(26, 26);
            //this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            //this.pictureBox1.TabIndex = 1;
            //this.pictureBox1.TabStop = false;
            // 
            // Notifica
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(282, 52);
            //this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Notifica";
            this.TopMost = true;
            //((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
