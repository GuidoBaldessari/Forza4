using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forza4.forms;
using Forza4.Classi;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace Forza4.forms
{
    public partial class Log : Form
    {
#region variabili
        public string username;
       public IPAddress ip;
        public int port;
        public string mode = "host";
        public bool PortaOkay = true;
        private bool errore = false;
#endregion
        public Log()
        {
            InitializeComponent();
        }        
        private void Start_Click(object sender, EventArgs e)
        {
            if (Controllo_Start())
            {
               
                username = txt_Username.Text;
                port = Convert.ToInt32(txt_Porta.Text);
                if (RB_Host.Checked)
                {
                  ip = IPAddress.Any;
                }
                else
                {
                    ip =IPAddress.Parse( txt_IP.Text);
                    mode = "client";
                }
                DialogResult = DialogResult.OK;                
            }
            else
            {
                if (!errore)
                {
                    MessageBox.Show("Completare tutti i campi!");
                    if (errore)
                    {
                        errore = false;
                    }
                }
               
            }
        }
        private bool Controllo_Start()
        {
            if (PortaOkay && txt_Username.Text != "" )
            {               
                if (RB_Host.Checked)
                {
                    return true;                    
                }
                else
                {
                    if (controlloIP(txt_IP.Text))
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Inserie un IP valido.");
                        errore = true;
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

    
        private bool controlloIP(string IPdaControllare)
        {
            IPAddress coso;
            bool thisGood = IPAddress.TryParse(IPdaControllare, out coso);
            try
            {
                string[] s = IPdaControllare.Split('.');
                if (s.Length != 4)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }            
        }             
        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txt_IP.Enabled = !txt_IP.Enabled;
            lbl_Server.Enabled = !lbl_Server.Enabled;
        }

        private void txt_Porta_TextChanged(object sender, EventArgs e)
        {
            if (txt_Porta.Text.Trim().Length > 0 && Int32.TryParse(txt_Porta.Text, out port)/*&& port >= 49152 && port <= 65535*/ )
            {
                txt_Porta.ForeColor = Color.Black;
                PortaOkay = true;
            }
            else
            {
                PortaOkay = false;
                txt_Porta.ForeColor = Color.Red;               
            }
        }


    }
}
