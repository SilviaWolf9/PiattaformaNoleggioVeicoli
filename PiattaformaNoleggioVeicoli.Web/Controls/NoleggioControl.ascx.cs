using PiattaformaNoleggioVeicoli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PiattaformaNoleggioVeicoli.Web.Controls
{
    public partial class NoleggioControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }            
        }
        public NoleggiTrovatiModelView Noleggio { get => _noleggio; set => _noleggio = value; }       // serve per ottenere e settare il valore della variabile statica dichiarata sotto in modo che abbia lo stesso valore in tutta la pagina si poteva fare anche utilizzando una viewstate
        private static NoleggiTrovatiModelView _noleggio;
        public void SetNoleggio()        // va a riempire i vari componenti del control in base ai dati della proprietà Veicolo
        {
            if (Noleggio == null)
            {
                Noleggio = new NoleggiTrovatiModelView();
            }
            lblMarca.Text = Noleggio.Marca;
            lblModello.Text = Noleggio.Modello;
            lblTarga.Text = Noleggio.Targa;
            lblIsInCorso.Text = Noleggio.IsInCorso;
            lblDataInizio.Text = Noleggio.DataInizio.ToString();
            lblDataFine.Text = Noleggio.DataFine.ToString();
            lblCognome.Text = Noleggio.Cognome;
            lblNome.Text = Noleggio.Nome;
            lblCodiceFiscale.Text = Noleggio.CodiceFiscale;       
        }
    }
}