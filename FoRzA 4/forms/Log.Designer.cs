namespace Forza4.forms
{
    partial class Log
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Log));
            this.lbl_Server = new System.Windows.Forms.Label();
            this.txt_IP = new System.Windows.Forms.TextBox();
            this.txt_Username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_Join = new System.Windows.Forms.RadioButton();
            this.RB_Host = new System.Windows.Forms.RadioButton();
            this.txt_Porta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Server
            // 
            this.lbl_Server.AutoSize = true;
            this.lbl_Server.Enabled = false;
            this.lbl_Server.Location = new System.Drawing.Point(192, 85);
            this.lbl_Server.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Server.Name = "lbl_Server";
            this.lbl_Server.Size = new System.Drawing.Size(66, 17);
            this.lbl_Server.TabIndex = 1;
            this.lbl_Server.Text = "Server IP";
            // 
            // txt_IP
            // 
            this.txt_IP.Enabled = false;
            this.txt_IP.Location = new System.Drawing.Point(51, 81);
            this.txt_IP.Margin = new System.Windows.Forms.Padding(4);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(132, 22);
            this.txt_IP.TabIndex = 2;
            this.txt_IP.Text = "127.0.0.1";
            // 
            // txt_Username
            // 
            this.txt_Username.Location = new System.Drawing.Point(28, 32);
            this.txt_Username.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Username.Name = "txt_Username";
            this.txt_Username.Size = new System.Drawing.Size(132, 22);
            this.txt_Username.TabIndex = 3;
            this.txt_Username.Text = "asdasd";
            this.txt_Username.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Username";
            this.label2.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(61, 247);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 43);
            this.button1.TabIndex = 5;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Start_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_Join);
            this.groupBox1.Controls.Add(this.RB_Host);
            this.groupBox1.Controls.Add(this.txt_IP);
            this.groupBox1.Controls.Add(this.lbl_Server);
            this.groupBox1.Location = new System.Drawing.Point(28, 98);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(305, 123);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // RB_Join
            // 
            this.RB_Join.AutoSize = true;
            this.RB_Join.Location = new System.Drawing.Point(29, 53);
            this.RB_Join.Margin = new System.Windows.Forms.Padding(4);
            this.RB_Join.Name = "RB_Join";
            this.RB_Join.Size = new System.Drawing.Size(55, 21);
            this.RB_Join.TabIndex = 1;
            this.RB_Join.Text = "Join";
            this.RB_Join.UseVisualStyleBackColor = true;
            this.RB_Join.CheckedChanged += new System.EventHandler(this.RB_Join_CheckedChanged);
            // 
            // RB_Host
            // 
            this.RB_Host.AutoSize = true;
            this.RB_Host.Checked = true;
            this.RB_Host.Location = new System.Drawing.Point(9, 25);
            this.RB_Host.Margin = new System.Windows.Forms.Padding(4);
            this.RB_Host.Name = "RB_Host";
            this.RB_Host.Size = new System.Drawing.Size(112, 21);
            this.RB_Host.TabIndex = 0;
            this.RB_Host.TabStop = true;
            this.RB_Host.Text = "Host a match";
            this.RB_Host.UseVisualStyleBackColor = true;
            this.RB_Host.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // txt_Porta
            // 
            this.txt_Porta.Location = new System.Drawing.Point(28, 36);
            this.txt_Porta.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Porta.Name = "txt_Porta";
            this.txt_Porta.Size = new System.Drawing.Size(132, 22);
            this.txt_Porta.TabIndex = 3;
            this.txt_Porta.Text = "42069";
            this.txt_Porta.TextChanged += new System.EventHandler(this.txt_Porta_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(177, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Port (42000 to 48000)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 306);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Porta);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Username);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Log";
            this.Text = "Login";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Server;
        private System.Windows.Forms.TextBox txt_IP;
        private System.Windows.Forms.TextBox txt_Username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_Join;
        private System.Windows.Forms.RadioButton RB_Host;
        private System.Windows.Forms.TextBox txt_Porta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
    }
}

