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
        //public NoleggiTrovatiModelView Noleggio { get => _noleggio; set => _noleggio = value; }       // serve per ottenere e settare il valore della variabile statica dichiarata sotto in modo che abbia lo stesso valore in tutta la pagina si poteva fare anche utilizzando una viewstate
        //private static NoleggiTrovatiModelView _noleggio;
        public void SetNoleggio(NoleggiTrovatiModelView noleggio)        // va a riempire i vari componenti del control in base ai dati della proprietà Veicolo
        {
            if (noleggio == null)
            {
                noleggio = new NoleggiTrovatiModelView();
            }
            lblMarca.Text = noleggio.Marca;
            lblModello.Text = noleggio.Modello;
            lblTarga.Text = noleggio.Targa;
            lblIsInCorso.Text = noleggio.IsInCorso;
            lblDataInizio.Text = noleggio.DataInizio.ToString();
            lblDataFine.Text = noleggio.DataFine.ToString();
            lblCognome.Text = noleggio.Cognome;
            lblNome.Text = noleggio.Nome;
            lblCodiceFiscale.Text = noleggio.CodiceFiscale;       
        }
    }
}