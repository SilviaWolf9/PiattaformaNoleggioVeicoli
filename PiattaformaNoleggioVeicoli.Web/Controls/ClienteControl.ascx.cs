﻿using PiattaformaNoleggioVeicoli.Business.Managers;
using PiattaformaNoleggioVeicoli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PiattaformaNoleggioVeicoli.Web.Controls
{
    public partial class ClienteControl : System.Web.UI.UserControl
    {
        public event EventHandler<CodiceFiscaleUpdatedArgs> EsistenzaCodiceFiscale;     
        public class CodiceFiscaleUpdatedArgs : EventArgs
        {
            public int IdCliente { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
        }
        //public ClientiModel Cliente { get => _cliente; set => _cliente = value; }       // serve per ottenere e settare il valore della variabile statica dichiarata sotto in modo che abbia lo stesso valore in tutta la pagina si poteva fare anche utilizzando una viewstate
        //private static ClientiModel _cliente;

        public void SetCliente(ClientiModel cliente)        // va a riempire i vari componenti del control in base ai dati della proprietà Cliente
        {
            if (cliente == null)
            {
                cliente = new ClientiModel();
            }
            txtCognome.Text = cliente.Cognome;
            txtNome.Text = cliente.Nome;
            if (cliente.DataNascita.HasValue)
            {
                txtDataNascita.Text = cliente.DataNascita.Value.ToString("dd/MM/yyyy");
            }            
            txtCodiceFiscale.Text = cliente.CodiceFiscale;
            txtPatente.Text = cliente.Patente;
            txtTelefono.Text = cliente.Telefono;
            txtEmail.Text = cliente.Email;
            txtIndirizzo.Text = cliente.Indirizzo;
            txtNumeroCivico.Text = cliente.NumeroCivico;
            txtCap.Text = cliente.Cap;
            txtCitta.Text = cliente.Citta;
            txtComune.Text = cliente.Comune;
            txtNazione.Text = cliente.Nazione;
            txtNote.Text = cliente.Note;            
        }

        public ClientiModel GetDatiCliente(ClientiModel cliente)        // restituisce i dati del cliente attuali al chiamante 
        {
            var clienteModificato = cliente;
            clienteModificato.Cognome = txtCognome.Text;
            clienteModificato.Nome = txtNome.Text;
            if (!string.IsNullOrWhiteSpace(txtDataNascita.Text))
            {
                clienteModificato.DataNascita = DateTime.Parse(txtDataNascita.Text);
            }
            clienteModificato.CodiceFiscale = txtCodiceFiscale.Text;
            clienteModificato.Patente = txtPatente.Text;
            clienteModificato.Telefono = txtTelefono.Text;
            clienteModificato.Email = txtEmail.Text;
            clienteModificato.Indirizzo = txtIndirizzo.Text;
            clienteModificato.NumeroCivico = txtNumeroCivico.Text;
            clienteModificato.Cap = txtCap.Text;
            clienteModificato.Citta = txtCitta.Text;
            clienteModificato.Comune = txtComune.Text;
            clienteModificato.Nazione = txtNazione.Text;
            clienteModificato.Note = txtNote.Text;
            return clienteModificato;
        }

        protected void txtCodiceFiscale_TextChanged(object sender, EventArgs e)
        {
            var clientiManager = new ClientiManager();
            var esistenzaCf = clientiManager.EsistenzaCodiceFiscale(txtCodiceFiscale.Text);
            if (esistenzaCf.HasValue)
            {                
                var codiceFiscaleUpdatedArgs = new CodiceFiscaleUpdatedArgs()
                {
                    IdCliente = esistenzaCf.Value
                };
                EsistenzaCodiceFiscale(this, codiceFiscaleUpdatedArgs); ;
            }
        }

        protected void txtDataNascita_TextChanged(object sender, EventArgs e)
        {
            if (!DateTime.TryParse(txtDataNascita.Text,out DateTime dataNascita))
            {
                //mettere infocontrol errore
                txtDataNascita.Text = String.Empty;
                return;
            }
            if (dataNascita>DateTime.Now)
            {
                //stampa messaggio errore
                txtDataNascita.Text = DateTime.Now.ToString("dd/MM/yyyy");
                return;
            }    
            txtDataNascita.Text = dataNascita.ToString("dd/MM/yyyy");
        }
    }
}