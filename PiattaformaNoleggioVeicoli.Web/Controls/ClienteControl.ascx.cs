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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
        }
        public ClientiModel Cliente { get => _cliente; set => _cliente = value; }       // serve per ottenere e settare il valore della variabile statica dichiarata sotto in modo che abbia lo stesso valore in tutta la pagina si poteva fare anche utilizzando una viewstate
        private static ClientiModel _cliente;

        public void SetCliente()        // va a riempire i vari componenti del control in base ai dati della proprietà Cliente
        {
            if (Cliente == null)
            {
                Cliente = new ClientiModel();
            }
            txtCognome.Text = Cliente.Cognome;
            txtNome.Text = Cliente.Nome;
            if (Cliente.DataNascita.HasValue)
            {
                clDataNascita.SelectedDate = Cliente.DataNascita.Value;
            }
            else
            {
                clDataNascita.SelectedDate = DateTime.MinValue;
            }
            txtCodiceFiscale.Text = Cliente.CodiceFiscale;
            txtPatente.Text = Cliente.Patente;
            txtTelefono.Text = Cliente.Telefono;
            txtEmail.Text = Cliente.Email;
            txtIndirizzo.Text = Cliente.Indirizzo;
            txtNumeroCivico.Text = Cliente.NumeroCivico;
            txtCap.Text = Cliente.Cap;
            txtCitta.Text = Cliente.Citta;
            txtComune.Text = Cliente.Comune;
            txtNazione.Text = Cliente.Nazione;
            txtNote.Text = Cliente.Note;            
        }

        public ClientiModel GetDatiCliente()        // restituisce i dati del cliente attuali al chiamante 
        {
            Cliente.Cognome = txtCognome.Text;
            Cliente.Nome = txtNome.Text;
            if (clDataNascita.SelectedDate != DateTime.MinValue)
            {
                Cliente.DataNascita = clDataNascita.SelectedDate;
            }
            Cliente.CodiceFiscale = txtCodiceFiscale.Text;
            Cliente.Patente = txtPatente.Text;
            Cliente.Telefono = txtTelefono.Text;
            Cliente.Email = txtEmail.Text;
            Cliente.Indirizzo = txtIndirizzo.Text;
            Cliente.NumeroCivico = txtNumeroCivico.Text;
            Cliente.Cap = txtCap.Text;
            Cliente.Citta = txtCitta.Text;
            Cliente.Comune = txtComune.Text;
            Cliente.Nazione = txtNazione.Text;
            Cliente.Note = txtNote.Text;
            return Cliente;
        }
    }
}