using PiattaformaNoleggioVeicoli.Business.Managers;
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
        public event EventHandler<CodiceFiscaleUpdatedArgs> EsistenzaCodiceFiscale;     // dichiara l'evento da generare
        public class CodiceFiscaleUpdatedArgs : EventArgs       // classe che estende eventArgs che viene passata nell'evento esistenza codice fiscale
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
        public void SetCliente(ClientiModel cliente)        // va a riempire i vari componenti del control in base ai dati del cliente passato in imput
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
            txtProvincia.Text = cliente.Provincia;
            txtNazione.Text = cliente.Nazione;
            txtNote.Text = cliente.Note;            
        }
        public ClientiModel GetDatiCliente(ClientiModel cliente)        // restituisce i dati del cliente attuali al chiamante 
        {
            var clienteModificato = cliente;        // usa una variabile di appoggio per non sovrascrivere il clienteModel passato in input
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
            clienteModificato.Provincia = txtProvincia.Text;
            clienteModificato.Nazione = txtNazione.Text;
            clienteModificato.Note = txtNote.Text;
            return clienteModificato;
        }
        protected void txtCodiceFiscale_TextChanged(object sender, EventArgs e)         // verifica l'esistenza del codice fiscale e nel caso esista, genera l'evento passando l'id del cliente con quel codice fiscale
        {
            var clientiManager = new ClientiManager();
            var esistenzaCf = clientiManager.EsistenzaCodiceFiscale(txtCodiceFiscale.Text);
            if (esistenzaCf.HasValue)
            {                
                var codiceFiscaleUpdatedArgs = new CodiceFiscaleUpdatedArgs()
                {
                    IdCliente = esistenzaCf.Value
                };
                EsistenzaCodiceFiscale(this, codiceFiscaleUpdatedArgs);         // scatena l'evento mandando come object this (ovvero txtCodiceFiscale) e come argomento eventArgs l'id del cliente relativo a quel codice fiscale
            }
        }
        protected void txtDataNascita_TextChanged(object sender, EventArgs e)        // controlla la data di nascita, se non riesce a convertirla viene inserita nel campo della data di nascita una stringa vuota, se si inserisce una data di nascita maggiore della data di oggi, mette la data di oggi nel formato giorno mese anno, altrimenti scrive la data sempre nel formato giorno mese anno
        {
            if (!DateTime.TryParse(txtDataNascita.Text,out DateTime dataNascita))
            {
                txtDataNascita.Text = String.Empty;
                return;
            }
            if (dataNascita>DateTime.Now)
            {
                txtDataNascita.Text = DateTime.Now.ToString("dd/MM/yyyy");
                return;
            }    
            txtDataNascita.Text = dataNascita.ToString("dd/MM/yyyy");
        }
    }
}