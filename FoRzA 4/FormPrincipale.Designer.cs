using System.Windows.Forms;

namespace Forza4
{
    partial class FormPrincipale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipale));
            this.lblMe = new System.Windows.Forms.Label();
            this.lblAvv = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.lblTurno = new System.Windows.Forms.Label();
            this.lblAttesa = new System.Windows.Forms.Label();
            this.btnRestart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMe
            // 
            this.lblMe.AutoSize = true;
            this.lblMe.Font = new System.Drawing.Font("Arial", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMe.Location = new System.Drawing.Point(11, 12);
            this.lblMe.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMe.Name = "lblMe";
            this.lblMe.Size = new System.Drawing.Size(65, 24);
            this.lblMe.TabIndex = 0;
            this.lblMe.Text = "Me: 0";
            // 
            // lblAvv
            // 
            this.lblAvv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAvv.AutoSize = true;
            this.lblAvv.Font = new System.Drawing.Font("Arial", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblAvv.Location = new System.Drawing.Point(828, 12);
            this.lblAvv.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAvv.Name = "lblAvv";
            this.lblAvv.Size = new System.Drawing.Size(145, 24);
            this.lblAvv.TabIndex = 1;
            this.lblAvv.Text = "Avversario: 0";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv.ColumnHeadersHeight = 4;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv.ColumnHeadersVisible = false;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidth = 4;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgv.Size = new System.Drawing.Size(0, 0);
            this.dgv.TabIndex = 2;
            this.dgv.Visible = false;
            this.dgv.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_CellContentClick);
            this.dgv.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgv_CellPainting);
            // 
            // lblTurno
            // 
            this.lblTurno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTurno.AutoSize = true;
            this.lblTurno.Font = new System.Drawing.Font("Arial", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblTurno.Location = new System.Drawing.Point(0, 12);
            this.lblTurno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTurno.Name = "lblTurno";
            this.lblTurno.Size = new System.Drawing.Size(113, 24);
            this.lblTurno.TabIndex = 3;
            this.lblTurno.Text = "Turno: Me";
            // 
            // lblAttesa
            // 
            this.lblAttesa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAttesa.AutoSize = true;
            this.lblAttesa.Font = new System.Drawing.Font("Arial", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblAttesa.Location = new System.Drawing.Point(269, 257);
            this.lblAttesa.Name = "lblAttesa";
            this.lblAttesa.Size = new System.Drawing.Size(462, 24);
            this.lblAttesa.TabIndex = 4;
            this.lblAttesa.Text = "In attesa della connessione dell\'avversario...";
            // 
            // btnRestart
            // 
            this.btnRestart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRestart.Font = new System.Drawing.Font("Arial", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnRestart.Location = new System.Drawing.Point(850, 70);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(122, 38);
            this.btnRestart.TabIndex = 5;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Visible = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // FormPrincipale
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.lblAttesa);
            this.Controls.Add(this.lblTurno);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.lblAvv);
            this.Controls.Add(this.lblMe);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "FormPrincipale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Forza 4";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMe;
        private System.Windows.Forms.Label lblAvv;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label lblTurno;
        private Label lblAttesa;
        private Button btnRestart;
    }
}