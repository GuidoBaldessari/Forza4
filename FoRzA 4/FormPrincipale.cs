using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forza4.Classi;
using Forza4.forms;
using Forza4.Properties;
//client.send(cose) mandas i dati al server e lui li rimanda a tutti lol

namespace Forza4
{
    public partial class FormPrincipale : Form
    {
        #region variabili e proprietà
        public Client client;
        static public int righe = 6, colonne = 7, CountPlayer = 0, CountAvv = 0;
        public Forza4Logic logica = new Forza4Logic();     
        
        Image PedinaPlayer = FoRzA_4.Properties.Resources.Pedina_1 , PedinaAvversario = FoRzA_4.Properties.Resources.Pedina__1, PedinaVuota = FoRzA_4.Properties.Resources.Pedina_vuota;      
        static protected int larghezzaPedina = 150, altezzaPedina = 150;

        public string turnoPlayer = "Me", turnoAvversario = "Avversario";


        public int AltezzaPedina { get => altezzaPedina; set => altezzaPedina = value; }
        public int LarghezzaPedina { get => larghezzaPedina; set => larghezzaPedina = value; }

        #endregion
        
        public FormPrincipale()
        {
            InitializeComponent();
            dgv.Size = new System.Drawing.Size(larghezzaPedina * colonne, altezzaPedina * righe);           
            this.dgv.RowTemplate.Height = altezzaPedina; //
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            dgv.RowCount = righe;           
            dgv.ColumnCount = colonne;
            Aggiorna();
            login();

        }
        private void StartGame(string nome, string CosoACuiConnettersiHoTroppiNomiIp)
        {
            StartServer();
            StartClient(CosoACuiConnettersiHoTroppiNomiIp);
        }
        private void FormPrincipale_KeyPress(object sender, KeyEventArgs e) // da togliere nella release, nel designer ho aggiunto this.KeyDown = true; per il controllo tasti
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.R)       // Ctrl-S Save
            {
                logica.Turno = 1;
                Reset();
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
        }
        private void StartServer()
        {
            var server = new Server();
            //start listening for messages and copy the messages back to the client
            Task.Factory.StartNew(async () => {
                while (true)
                {
                    var received = await server.Receive();
                    server.Reply("" + received.Message, received.Sender);                    
                }
            });
        }
        private void StartClient( string IP)
        {
            client = Client.ConnectTo(IP, 32123);
            //wait for reply messages from server and send them to console 
            Task.Factory.StartNew(async () => {
                while (true)
                {
                    try
                    {
                        var received = await client.Receive();
                        MessageBox.Show("Server dice>"+received.Message);
                        if (received.Message.Contains("quit"))
                            break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("BOH");
                    }
                }
            });
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //controlloVittoria();
            int stato = logica.eseguiMossa(e.ColumnIndex);
            client.Send("dato");
            Aggiorna();
            switch (stato)
            {
                case -2:
                    MessageBox.Show("Colonna Piena:)");
                    break;
                case -1:
                    CambiaTurnolbl();
                    break;
                case 0:
                    MessageBox.Show("Pareggio :|");
                    CountAvv++;
                    CountPlayer++;
                    break;
                case 1:
                    MessageBox.Show("Giocatore 1 vince :)");
                    CountPlayer++;
                    break;
                case 2:
                    MessageBox.Show("Giocatore 2 vince :)");
                    CountAvv++;
                    break;
            }

            if (stato >= 0)
            {
                Reset();
            }

        }      
        private void CambiaTurnolbl()
        {
            if (logica.Turno == 1)
                label3.Text = "Turno: " + turnoPlayer;
            else
                label3.Text = "Turno: " + turnoAvversario;
            //Partita in corso
        }
        private void Reset()
        {
            logica = new Forza4Logic(righe, colonne);
            Aggiorna();
            CambiaTurnolbl();
        }
        void ColoraCelle()
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                for (int j = 0; j < dgv.ColumnCount; j++)
                {

                    int value = Convert.ToInt32(dgv[j, i].Value);
                    if (value == 0)
                        dgv[j, i].Style.BackColor = System.Drawing.Color.Blue;
                    if (value == 1)
                        dgv[j, i].Style.BackColor = System.Drawing.Color.Green;
                    if (value == -1)
                        dgv[j, i].Style.BackColor = System.Drawing.Color.Red;
                }
            }
        }
        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Rectangle cella = e.CellBounds;
                       
            if (dgv[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.Blue)
            {
                e.Graphics.DrawImage(PedinaVuota, cella);
            }
            if (dgv[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.Red)
            {
                e.Graphics.DrawImage(PedinaAvversario, cella);
            }
            if (dgv[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.Green)
            {
                e.Graphics.DrawImage(PedinaPlayer, cella);
            }
            //inserisco i bordi delle celle
            e.Graphics.DrawRectangle(Pens.Black, cella);
            e.Handled = true;
        }
        public void Aggiorna()
        {
            MatriceSuDgv(logica.Campo);
            ColoraCelle();
        }       
        void MatriceSuDgv(int[,] matrice)
        {
            
            for (int i = 0; i < righe; i++)
            {
                for (int j = 0; j < colonne; j++)
                {
                    dgv[j, i].Value = matrice[i, j];
                }
            }
        }
        public void login()
        {
            Log FormLogin = new Log();
            if (FormLogin.ShowDialog() == DialogResult.OK)
            {
                StartGame(FormLogin.Username, FormLogin.Ip);
            }
            else //se viene chiuso il form del login si chiude tutto e bom
            {
                this.Close();
            }
        }

    }
}

