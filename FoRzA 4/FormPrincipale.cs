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
using System.Net.Sockets;
using System.Threading;
using System.Net;
//client.send(cose) mandas i dati al server e lui li rimanda a tutti lol

namespace Forza4
{
    public partial class FormPrincipale : Form
    {
        #region variabili e proprietà
        IPAddress ip;
        public int porta;
        Socket socket;
        string msg;
        System.Windows.Forms.Timer timerAttesaAvversario;

        int stato;
        public Forza4Logic logica;

        private int righe;
        public int Righe
        {
            get { return righe; }
            set { righe = value; }
        }
        private int colonne;
        public int Colonne
        {
            get { return colonne; }
            set { colonne = value; }
        }
        private int altezzaPedina;
        public int AltezzaPedina
        {
            get { return altezzaPedina; }
            set { altezzaPedina = value; }
        }
        private int larghezzaPedina;
        public int LarghezzaPedina
        {
            get { return larghezzaPedina; }
            set { larghezzaPedina = value; }
        }

        Image PedinaPlayer = FoRzA_4.Properties.Resources.Pedina_1;
        Image PedinaAvversario = FoRzA_4.Properties.Resources.Pedina__1;
        Image PedinaVuota = FoRzA_4.Properties.Resources.Pedina_vuota;

        private int countPlayer;
        public int CountPlayer1
        {
            get { return countPlayer; }
            set { countPlayer = value; }
        }
        private int countAvv;
        public int CountPlayer2
        {
            get { return countAvv; }
            set { countAvv = value; }
        }

        public string turnoPlayer1 = "Me", turnoPlayer2 = "Avversario";
        #endregion

        #region Costruttore
        public FormPrincipale()
        {
            righe = 6;
            colonne = 7;
            larghezzaPedina = 100;
            altezzaPedina = 100;

            countPlayer = 0;
            countAvv = 0;

            logica = new Forza4Logic(righe, colonne);

            InitializeComponent();

            dgv.Size = new Size(larghezzaPedina * colonne, altezzaPedina * righe);
            dgv.RowTemplate.Height = altezzaPedina;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //this.label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dgv.RowCount = righe;
            dgv.ColumnCount = colonne;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            timerAttesaAvversario = new System.Windows.Forms.Timer();
            timerAttesaAvversario.Interval = 200;
            timerAttesaAvversario.Tick += new EventHandler(TimerTick);
            
            
            login();

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
        public void login()
        {
            Log FormLogin = new Log();

            if (FormLogin.ShowDialog() == DialogResult.OK)
            {
                ip = FormLogin.ip;
                porta = FormLogin.port;
                Thread t;
                if (FormLogin.mode == "host")
                {
                    t = new Thread(HostThread);
                }
                else
                {
                    t = new Thread(JoinerThread);
                }
                t.Start();
                timerAttesaAvversario.Start();
            }
            else //se viene chiuso il form del login si chiude tutto e bom
            {
                this.Close();
                timerAttesaAvversario.Stop();
            }
        }
        #endregion

        #region Inizio Connessione
        //MAI USATO
        /*public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }*/
        public void HostThread()
        {
            //AddToConsoleBox("Host thread started");
            
            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, porta);
            socket.Bind(localEndPoint);
            socket.Listen(1);
            socket = socket.Accept();
            //AddToConsoleBox("Checkpoint 0");
            logica.ProprioTurno = 1;
            stato = -4;

            GetChanges();
        }
        public void JoinerThread()
        {
            //ConsoleBox.Items.Add("Joiner thread started");
            IPEndPoint remoteEP = new IPEndPoint(ip, porta);
            socket.Connect(remoteEP);

            //AddToConsoleBox("Checkpoint 0");
            logica.ProprioTurno = -1;
            stato = -4;
            //Console.WriteLine(GetLocalIPAddress());
            //byte[] msg = Encoding.ASCII.GetBytes(GetLocalIPAddress());
            //int bytesSent = socket.Send(msg);
            GetChanges();
        }
        public void TimerTick(object sender, EventArgs e)
        {
            //Entrambi gli host sono connessi e la partita inizia
            if (stato == -4)
            {
                label4.Visible = false;
                dgv.Visible = true;
                timerAttesaAvversario.Stop();
                //aggiorna();                
            }
        }

        #endregion

        #region Partita


        private void dgv_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            stato = logica.eseguiMossa(e.ColumnIndex, logica.ProprioTurno);
            //La mossa viene inviata all'avversario solo se è il proprio turno e se la colonna non era piena

            if (stato >= -1)
            {
                msg = e.ColumnIndex.ToString() + "|";
                socket.Send(Encoding.ASCII.GetBytes(msg));
            }
            aggiorna();

            //!!!Disabilitare il click nella datagridview per il turno dell'avversario!!!
        }
        public void GetChanges()
        {
            while (true)
            {
                string data = null;
                string[] mossa;
                byte[] bytes = new Byte[1024];

                while (true)
                {
                    int bytesRec = socket.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    int separatore = data.IndexOf('|');
                    if (separatore > -1)
                    {
                        mossa = data.Split('|');
                        break;
                    }
                }
                try
                {
                    stato = logica.eseguiMossa(Convert.ToInt32(mossa[0]), -logica.ProprioTurno);
                    aggiorna();
                    Console.WriteLine(stato);
                }
                catch (Exception)
                {
                    //throw new Exception("Errore nella comunicazione");
                }
            }
        }//legge finchè non trova |
        public void aggiorna()
        {
            //LEGENDA DI STATO
                //-4 partita appena iniziata
                //-3 mossa non eseguita per turno sbagliato
                //-2 mossa non eseguita per colonna piena
                //-1 a partita in corso

                //0 pareggio
                //1 vittoria giocatore 1
                //2 vittoria giocatore 2
            aggiornaGrafica();

            Point position = new Point(this.Left + this.Width / 2, this.Top + this.Height / 2);

            Notifica notifica;

            switch (stato)
            {
                case -4:
                   /*
                    //MessageBox.Show("Tocca all'avversario!");

                    //SI BUGGA PER QUALCHE MOTIVO
                    if(logica.ProprioTurno == logica.Turno)
                    {
                        notifica = new Notifica("E' il tuo turno!", 3, position);
                        //notifica.Show();
                    }
                    else
                    {
                        notifica = new Notifica("Tocca all'avversario", 3, position);
                        //notifica.Show();
                    }*/
                    break;
                case -3:
                    //MessageBox.Show("Tocca all'avversario!");
                    notifica = new Notifica("Tocca all'avversario", 3, position);
                    break;
                case -2:
                    //MessageBox.Show("Colonna Piena");
                    notifica = new Notifica("Colonna Piena!", 3, position);
                    break;
                case -1:
                    CambiaTurnolbl();
                    break;
                case 0:
                    //MessageBox.Show("Pareggio!");
                    notifica = new Notifica("Pareggio!", 3, position);

                    //CountPlayer1++;
                    //CountPlayer2++;
                    break;
                case 1:
                    
                    if (logica.ProprioTurno == 1)
                    {
                        //MessageBox.Show("Hai vinto!");
                        notifica = new Notifica("Hai vinto :)", 3, position);
                    }
                    else
                    {
                        //SI BUGGA PER QUALCHE MOTIVO
                        //notifica = new Notifica("Hai perso", 3, position);
                        
                    }
                    CountPlayer1++;
                    break;
                case 2:
                    
                    if (logica.ProprioTurno == -1)
                    {
                        //MessageBox.Show("Hai vinto!");
                        notifica = new Notifica("Hai vinto :)", 3, position);
                    }
                    else
                    {
                        //SI BUGGA PER QUALCHE MOTIVO
                        //notifica = new Notifica("Hai perso ", 3, position);
                        
                    }
                    CountPlayer2++;
                    break;
            }

            if (stato >= 0)
            {
                //btnRestart.Visible = true;
                Reset();
            }
        }

        private void Reset()
        {
            int tmp = logica.ProprioTurno;
            logica = new Forza4Logic(righe, colonne);
            logica.ProprioTurno = -tmp;
            stato = -4;
            aggiorna();
            CambiaTurnolbl();
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void CambiaTurnolbl()
        {
            if (logica.Turno == logica.ProprioTurno)
                label3.Text = "Turno: " + turnoPlayer1;
            else
                label3.Text = "Turno: " + turnoPlayer2;
        }
        public void aggiornaGrafica()
        {
            for (int i = 0; i < righe; i++)
            {
                for (int j = 0; j < colonne; j++)
                {
                    dgv[j, i].Value = logica.Campo[i, j];
                }
            }
            //ColoraCelle();
        }
        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            Rectangle cella = e.CellBounds;

            if (Convert.ToInt32(dgv[e.ColumnIndex, e.RowIndex].Value) == 0)
            {
                e.Graphics.DrawImage(PedinaVuota, cella);
            }
            else if (Convert.ToInt32(dgv[e.ColumnIndex, e.RowIndex].Value) == -1)
            {
                e.Graphics.DrawImage(PedinaAvversario, cella);
            }
            else if (Convert.ToInt32(dgv[e.ColumnIndex, e.RowIndex].Value) == 1)
            {
                e.Graphics.DrawImage(PedinaPlayer, cella);
            }
            //inserisco i bordi delle celle
            e.Graphics.DrawRectangle(Pens.Black, cella);
            e.Handled = true;
        }
        #endregion
    }
}

