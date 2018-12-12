﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forza4
{
    public class Forza4Logic

    {
        #region Dichiarazione di campo e _mosseRimanenti, inizializzazione di _righe e _colonne

        //Numero di righe del campo di gioco
        protected int _righe = 6;
        public int Righe
        {
            get { return _righe; }
            set { _righe = value; }
        }

        //Numero di colonne del campo di gioco
        protected int _colonne = 7;
        public int Colonne
        {
            get { return _colonne; }
            set { _colonne = value; }
        }

        protected int[,] _campo;

        public int[,] Campo
        {
            get { return _campo; }
            set { _campo = value; }
        }

        protected int _mosseRimanenti;
        public int MosseRimanenti
        {
            get { return _mosseRimanenti; }
        }
        #endregion

        #region Campi membro e proprietà
        #region Colonna dell'ultima mossa effettuata da uno dei giocatori
        protected int _ultimaColonnaMossa;
        public int UltimaColonnaMossa
        {
            get { return _ultimaColonnaMossa; }
        }
        #endregion

        #region Riga dell'ultima mossa effettuata da uno dei giocatori
        protected int _ultimaRigaMossa;
        public int UltimaRigaMossa
        {
            get { return _ultimaRigaMossa; }
        }
        #endregion

        #region Identifica i turni (1 per il giocatore 1, -1 per il giocatore 2)
        protected int _turno = 1;
        public int Turno
        {
            get { return _turno; }
            set { _turno = value; }
        }

        protected int _proprioTurno;
        public int ProprioTurno
        {
            get { return _proprioTurno; }
            set { _proprioTurno = value; }
        }


        #endregion

        #region Indentifica il vincitore della partita (0 --> pareggio, 1 --> giocatore1, 2 --> giocatore2)
        protected int _vincitore = 0;
        public int Vincitore
        {
            get { return _vincitore; }
        }
        #endregion
        #endregion

        #region Costruttori
        public Forza4Logic()
        {
            _campo = new int[_righe, _colonne]; //Matrice che identifica l'intero tabellone di gioco
            _mosseRimanenti = _righe * _colonne; //Contatore delle mosse rimanenti al riempimento del campo
            reset(); //Si imposta tutto il campo a 0
        }
        public Forza4Logic(int righe, int colonne)
        {
            _righe = righe;
            _colonne = colonne;

            _campo = new int[_righe, _colonne]; //Matrice che identifica l'intero tabellone di gioco
            _mosseRimanenti = _righe * _colonne; //Contatore delle mosse rimanenti al riempimento del campo
            reset(); //Si imposta tutto il campo a 0
        }
        #endregion
        protected void reset() //Svuota tutte le celle della matrice (le porta a 0)
        {
            for (int i = 0; i < _righe; i++)
            {
                for (int j = 0; j < _colonne; j++)
                {
                    _campo[i, j] = 0;
                }
            }
        }
        protected bool mossa(int colonna) //Metodo per effettuare una mossa in una colonna non piena e calcolare la riga in cui si fermetà il gettone
        {
            bool esitoMossa = false;
            int riga = _righe - 1; //Identifica la riga in cui andrà a posizionarsi il gettone

            while (!esitoMossa && riga >= 0) //Continua a scorrere le righe (dal basso verso l'alto) finché non si trova una posizione libera
            {
                if (_campo[riga, colonna] == 0) //Se la posizione è libera
                {
                    esitoMossa = true; //Indica che la mossa è stata eseguita

                    _ultimaRigaMossa = riga;
                    _ultimaColonnaMossa = colonna;

                    _campo[_ultimaRigaMossa, _ultimaColonnaMossa] = _turno; //Posiziona il gettone del giocatore che sta giocando in questo momento
                    _mosseRimanenti--; //Decrementa le mosse rimanenti

                    if (_mosseRimanenti % 2 == 0)
                    {
                        _turno = 1;
                    }
                    else
                    {
                        _turno = -1;
                    }

                    //stampaCampo();
                }
                else
                {
                    riga--; //Altrimenti alzati di una riga e riprova
                }
            }
            return esitoMossa;
        }
        private bool annullaMossa()
        {
            bool esito = false;
            if (_campo[_ultimaRigaMossa, _ultimaColonnaMossa] != 0)
            {
                _campo[_ultimaRigaMossa, _ultimaColonnaMossa] = 0;
                _mosseRimanenti++;
                esito = true;
            }
            return esito;
        }

        #region Verifica Possibili situazioni di vittoria
        protected void verificaVittoria()
        {
            int focalRow = _ultimaRigaMossa, focalCol = _ultimaColonnaMossa; //Ultimo gettobe aggiunto, se esiste un segmento vincente, passa sicuramente per questo punto

            int maxRow = Math.Min(_righe - 1, focalRow + 3); //Riga più in basso in cui può esserci una sequenza vincente
            int minCol = Math.Max(0, focalCol - 3); //Colonna più a sinistra in cui può esserci una sequenza vincente

            int minRow = Math.Max(0, focalRow - 3); //Riga di limite più alto in cui può una sequenza vincente
            int maxCol = Math.Min(_colonne - 1, focalCol + 3);//Colonna più a destra in cui può esserci una sequenza vincente


            //Verifica delle vittorie possibili 
            //La sequenza che porta alla vittoria comprende sicuramente la posizione dell'ultima mossa
            //Pertanto si calcola l'intervallo delle celle in cui può esserci una sequenza vincente, che comprende l'ultima mossa effettuata

            //Se si trova una sequenza vincente, allora non si continua a cercare il vincitore
            vittoriaOrizzontale(focalRow, minCol, maxCol);

            if (_vincitore == 0)
                vittoriaVerticale(focalCol, minRow, maxRow);

            if (_vincitore == 0)
                vittoriaObliquaSX(focalRow, focalCol, minRow, minCol);

            if (_vincitore == 0)
                vittoriaObliquaDX(focalRow, focalCol, maxCol, minRow);
        }
        protected void vittoriaOrizzontale(int focalRow, int minCol, int maxCol)
        {
            int col = minCol, sommaSegmento = 0;

            while (col <= maxCol - 3 && _vincitore == 0)
            {
                sommaSegmento = _campo[focalRow, col] + _campo[focalRow, col + 1] + _campo[focalRow, col + 2] + _campo[focalRow, col + 3];

                if (sommaSegmento >= 4)
                {
                    _vincitore = 1;
                }
                else if (sommaSegmento <= -4)
                {
                    _vincitore = -1;
                }
                else
                {
                    col++;
                }
            }
        }
        protected void vittoriaVerticale(int focalCol, int minRow, int maxRow)
        {
            int row = maxRow, sommaSegmento = 0;

            while (row >= minRow + 3 && _vincitore == 0)
            {
                sommaSegmento = _campo[row, focalCol] + _campo[row - 1, focalCol] + _campo[row - 2, focalCol] + _campo[row - 3, focalCol];
                if (sommaSegmento >= 4)
                {
                    _vincitore = 1;
                }
                else if (sommaSegmento <= -4)
                {
                    _vincitore = -1;
                }
                else
                {
                    row--;
                }
            }
        }
        protected void vittoriaObliquaSX(int focalRow, int focalCol, int minRow, int minCol)
        {
            int maxRow = focalRow;
            int maxCol = focalCol;

            if (focalRow < _righe - 1 && focalCol < _colonne - 1)
            {
                do
                {
                    maxCol++;
                    maxRow++;
                }
                while ((maxRow < _righe - 1 && maxRow < focalRow + 3) && (maxCol < _colonne - 1 && maxCol < focalCol + 3));
            }

            int row = maxRow, col = maxCol, sommaSegmento = 0;

            while (row >= minRow + 3 && col >= minCol + 3 && _vincitore == 0)
            {
                sommaSegmento = _campo[row, col] + _campo[row - 1, col - 1] + _campo[row - 2, col - 2] + _campo[row - 3, col - 3];

                if (sommaSegmento >= 4)
                {
                    _vincitore = 1;
                }
                else if (sommaSegmento <= -4)
                {
                    _vincitore = -1;
                }
                else
                {
                    row--;
                    col--;
                }

            }

        }
        protected void vittoriaObliquaDX(int focalRow, int focalCol, int maxCol, int minRow)
        {
            int maxRow = focalRow;
            int minCol = focalCol;

            if (focalRow < _righe - 1 && focalCol > 0)
            {
                do
                {
                    minCol--;
                    maxRow++;
                }
                while ((maxRow < _righe - 1 && maxRow < focalRow + 3) && (minCol > 0 && minCol > focalCol - 3));
            }

            int row = maxRow, col = minCol, sommaSegmento = 0;

            while (row >= minRow + 3 && col <= maxCol - 3 && _vincitore == 0)
            {
                sommaSegmento = _campo[row, col] + _campo[row - 1, col + 1] + _campo[row - 2, col + 2] + _campo[row - 3, col + 3];

                if (sommaSegmento >= 4)
                {
                    _vincitore = 1;
                }
                else if (sommaSegmento <= -4)
                {
                    _vincitore = -1;
                }
                else
                {
                    row--;
                    col++;
                }

            }
        }
        #endregion
        public int eseguiMossa(int colonna, int proprioTurno)
        {
            //-4 partita appena iniziata
            //-3 mossa non eseguita per turno sbagliato
            //-2 mossa non eseguita per colonna piena
            //-1 a partita in corso

            //0 pareggio
            //1 vittoria
            //2 sconfitta
            int stato = -3;

            //if(true)
            if (_turno == proprioTurno)
            {
                if (!mossa(colonna))
                {
                    stato = -2;
                }
                else
                {
                    stato = -1;

                    verificaVittoria();

                    if (_mosseRimanenti <= 0 && _vincitore == 0)
                    {
                        stato = 0;
                    }
                    else if (_vincitore == _proprioTurno)
                    {
                        stato = 1;
                    }
                    else if(_vincitore == -_proprioTurno)
                    {
                        stato = 2;
                    }
                }
            }
            return stato;
        }
    }
}