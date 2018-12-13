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

        System.Windows.Forms.Timer timerAttesaAvversario;
        BackgroundWorker worker = new BackgroundWorker();
        private delegate void DELEGATE();


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

        Image PedinaPlayer;//= FoRzA_4.Properties.Resources.Pedina_1;
        Image PedinaAvversario;// = FoRzA_4.Properties.Resources.Pedina__1;
        Image PedinaVuota = FoRzA_4.Properties.Resources.Pedina_vuota;

        Color proprioSfondo, sfondoDefault = Color.FromArgb(0, 111, 244);
        Color sfondoVerde = Color.FromArgb(76, 255, 0), sfondoRosso = Color.FromArgb(255, 4, 0);
        private int countPlayer;
        public int CountPlayer
        {
            get { return countPlayer; }
            set { countPlayer = value; }
        }
        private int countAvv;
        public int CountAvv
        {
            get { return countAvv; }
            set { countAvv = value; }
        }

        public string userName = "Me", avvName = "Avversario";
        #endregion

        #region Costruttore
        public FormPrincipale()
        {
            InitializeComponent();

            righe = 6;
            colonne = 7;
            larghezzaPedina = 100;
            altezzaPedina = 100;

            countPlayer = 0;
            countAvv = 0;

            logica = new Forza4Logic(righe, colonne);

            this.BackColor = sfondoDefault;

            dgv.Size = new Size(larghezzaPedina * colonne, altezzaPedina * righe);
            dgv.Location = new Point(this.Width / 2 - dgv.Width / 2, this.Height / 2 - dgv.Height / 2);
            dgv.RowTemplate.Height = altezzaPedina;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowCount = righe;
            dgv.ColumnCount = colonne;

            lblTurno.Location = new Point(dgv.Location.X + dgv.Width / 2 - lblTurno.Width / 2, lblMe.Location.Y);



            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            setStartTimer();

            login();
        }

        private void setStartTimer()
        {
            timerAttesaAvversario = new System.Windows.Forms.Timer();
            timerAttesaAvversario.Interval = 200;
            timerAttesaAvversario.Tick += new EventHandler(TimerTick);
        }

        /*private void FormPrincipale_KeyPress(object sender, KeyEventArgs e) // da togliere nella release, nel designer ho aggiunto this.KeyDown = true; per il controllo tasti
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.R)       // Ctrl-S Save
            {
                logica.Turno = 1;
                restart();
                e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
            }
        }*/
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
        public void HostThread()
        {
            //AddToConsoleBox("Host thread started");
            PedinaPlayer = FoRzA_4.Properties.Resources.Pedina_1;
            PedinaAvversario = FoRzA_4.Properties.Resources.Pedina__1;
            //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            //IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, porta);
            socket.Bind(localEndPoint);
            socket.Listen(1);
            socket = socket.Accept();
            stato = -4;
            logica.ProprioTurno = 1;
            proprioSfondo = sfondoVerde;
            cambioSfondo();
            //this.BackColor = proprioSfondo;
            GetChanges();
        }
        public void JoinerThread()
        {
            PedinaPlayer = FoRzA_4.Properties.Resources.Pedina__1;
            PedinaAvversario = FoRzA_4.Properties.Resources.Pedina_1;           
            IPEndPoint remoteEP = new IPEndPoint(ip, porta);
            socket.Connect(remoteEP);        
            stato = -4;
            logica.ProprioTurno = -1;
            proprioSfondo = sfondoRosso;
            GetChanges();
        }
        public void TimerTick(object sender, EventArgs e)
        {
            //Entrambi gli host sono connessi e la partita inizia
            if (stato == -4)
            {
                lblAttesa.Visible = false;
                dgv.Visible = true;
                //btnRestart.Visible = false;
                timerAttesaAvversario.Stop();
                cambioSfondo();
                //this.BackColor = proprioSfondo;
            }
        }

        #endregion

        #region Partita

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Delegate del = new DELEGATE(aggiorna);
            this.Invoke(del);
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            stato = logica.eseguiMossa(e.ColumnIndex, logica.ProprioTurno);

            //La mossa viene inviata all'avversario solo se è il proprio turno e se la colonna non era piena
            if (stato >= -1)
            {
                socket.Send(Encoding.ASCII.GetBytes(e.ColumnIndex.ToString() + "|"));
            }
            aggiorna();

            //!!!Disabilitare il click nella datagridview per il turno dell'avversario!!!
        }
        public void GetChanges()
        {
            worker.DoWork += worker_DoWork;
            while (true)
            {
                int bytesRec;
                string data = null;
                int separatore = -1;
                string[] campi = new string[2];
                byte[] bytes = new Byte[1024];

                while (separatore <= -1)
                {
                    bytesRec = socket.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    separatore = data.IndexOf('|');
                    if (separatore > -1)
                    {
                        campi = data.Split('|');
                    }
                }

                if(campi[0]!= "#!new")
                {
                    stato = logica.eseguiMossa(Convert.ToInt32(campi[0]), -logica.ProprioTurno);
                     worker.RunWorkerAsync();
                }
                else
                {
                    stato = Convert.ToInt32(campi[1]);
                }

            }
        }//legge finchè non trova |
        public void aggiorna()
        {
            //LEGENDA DI STATO
            //-5 giocatore in attesa dell'avversario
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
                    if (logica.ProprioTurno == logica.Turno)
                    {
                        notifica = new Notifica("E' il tuo turno!", 3, position);
                    }
                    else
                    {
                        notifica = new Notifica("Tocca all'avversario", 3, position);
                    }
                    break;
                case -3:
                    notifica = new Notifica("Tocca all'avversario", 3, position);
                    break;
                case -2:
                    notifica = new Notifica("Colonna Piena!", 3, position);
                    break;
                case -1:
                    CambiaTurnolbl();
                    break;
                case 0:
                    notifica = new Notifica("Pareggio!", 3, position);
                    btnRestart.Visible = true;
                    dgv.Enabled = false;
                    break;
                case 1:
                    notifica = new Notifica("Hai vinto :)", 3, position);
                    countPlayer++;
                    btnRestart.Visible = true;
                    aggionalblPunti();
                    dgv.Enabled = false;
                    break;
                case 2:
                    notifica = new Notifica("Hai perso :(", 3, position);
                    countAvv++;
                    aggionalblPunti();
                    btnRestart.Visible = true;
                    dgv.Enabled = false;
                    break;
            }
        }
        private void aggionalblPunti()
        {
            lblMe.Text = userName +": " + countPlayer;
            lblAvv.Text = avvName + ": " + countAvv;
        }
        private void restart()
        {
            int tmp = logica.ProprioTurno;
            logica = new Forza4Logic(righe, colonne);
            aggiornaGrafica();
            logica.ProprioTurno = -tmp;
            btnRestart.Visible = false;
            dgv.Visible = false;
            dgv.Enabled = true;
            lblAttesa.Visible = true;
            socket.Send(Encoding.ASCII.GetBytes("#!new|" + -4));
            //aggiorna();
            //CambiaTurnolbl();
            Image imgtmp;
            imgtmp = PedinaPlayer;
            PedinaPlayer = PedinaAvversario;
            PedinaAvversario = imgtmp;
            timerAttesaAvversario.Start();
        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            cambioSfondo();
            this.BackColor = sfondoDefault;
            restart();
        }
        private void cambioSfondo()
        {
            if (logica.Turno == logica.ProprioTurno)
            {
                this.BackColor = sfondoVerde;
                lblTurno.Text = "Turno: " + userName;
            }
            else
            {
                lblTurno.Text = "Turno: " + avvName;
                this.BackColor = sfondoDefault;
            }
        }
        private void CambiaTurnolbl()
        {
            if (logica.Turno == logica.ProprioTurno)
            {
                lblTurno.Text = "Turno: " + userName;
                //this.BackColor = proprioSfondo;
            }
            else
            {
                lblTurno.Text = "Turno: " + avvName;
                //this.BackColor = sfondoDefault;
            }
            cambioSfondo();
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

