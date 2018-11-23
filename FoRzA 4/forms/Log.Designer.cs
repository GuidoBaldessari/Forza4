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
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Server
            // 
            this.lbl_Server.AutoSize = true;
            this.lbl_Server.Enabled = false;
            this.lbl_Server.Location = new System.Drawing.Point(144, 69);
            this.lbl_Server.Name = "lbl_Server";
            this.lbl_Server.Size = new System.Drawing.Size(51, 13);
            this.lbl_Server.TabIndex = 1;
            this.lbl_Server.Text = "Server IP";
            // 
            // txt_IP
            // 
            this.txt_IP.Enabled = false;
            this.txt_IP.Location = new System.Drawing.Point(38, 66);
            this.txt_IP.Name = "txt_IP";
            this.txt_IP.Size = new System.Drawing.Size(100, 20);
            this.txt_IP.TabIndex = 2;
            // 
            // txt_Username
            // 
            this.txt_Username.Location = new System.Drawing.Point(21, 26);
            this.txt_Username.Name = "txt_Username";
            this.txt_Username.Size = new System.Drawing.Size(100, 20);
            this.txt_Username.TabIndex = 3;
            this.txt_Username.Text = "asdasd";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Username";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(46, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(170, 35);
            this.button1.TabIndex = 5;
            this.button1.Text = "Join";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Start_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_Join);
            this.groupBox1.Controls.Add(this.RB_Host);
            this.groupBox1.Controls.Add(this.txt_IP);
            this.groupBox1.Controls.Add(this.lbl_Server);
            this.groupBox1.Location = new System.Drawing.Point(21, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 100);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // RB_Join
            // 
            this.RB_Join.AutoSize = true;
            this.RB_Join.Location = new System.Drawing.Point(22, 43);
            this.RB_Join.Name = "RB_Join";
            this.RB_Join.Size = new System.Drawing.Size(44, 17);
            this.RB_Join.TabIndex = 1;
            this.RB_Join.Text = "Join";
            this.RB_Join.UseVisualStyleBackColor = true;
            // 
            // RB_Host
            // 
            this.RB_Host.AutoSize = true;
            this.RB_Host.Checked = true;
            this.RB_Host.Location = new System.Drawing.Point(7, 20);
            this.RB_Host.Name = "RB_Host";
            this.RB_Host.Size = new System.Drawing.Size(88, 17);
            this.RB_Host.TabIndex = 0;
            this.RB_Host.TabStop = true;
            this.RB_Host.Text = "Host a match";
            this.RB_Host.UseVisualStyleBackColor = true;
            this.RB_Host.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // txt_Porta
            // 
            this.txt_Porta.Location = new System.Drawing.Point(21, 67);
            this.txt_Porta.Name = "txt_Porta";
            this.txt_Porta.Size = new System.Drawing.Size(100, 20);
            this.txt_Porta.TabIndex = 3;
            this.txt_Porta.Text = "50000";
            this.txt_Porta.TextChanged += new System.EventHandler(this.txt_Porta_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Port";
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 303);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Porta);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Username);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Log";
            this.Text = "Loggin";
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
    }
}

