using PiattaformaNoleggioVeicoli.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PiattaformaNoleggioVeicoli.Web
{
    public partial class RicercaNoleggio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _noleggiManager = new NoleggiManager();

            if (IsPostBack)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");
                return;
            }
            PopolaDDLMarche();
            PopolaDDLIsInCorso();
        }
        private NoleggiManager _noleggiManager { get; set; }
        private void PopolaGridViewNoleggio(NoleggiManager.RicercaNoleggiModel ricerca)
        {
            //gvNoleggiTrovati.DataSource = null;
            var listaNoleggiTrovati = _noleggiManager.RicercaNoleggi(ricerca);
            if (listaNoleggiTrovati.Count==0)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Info, "Nessun risultato trovato");                
            }            
            gvNoleggiTrovati.Visible = true;
            gvNoleggiTrovati.DataSource = listaNoleggiTrovati;
            gvNoleggiTrovati.DataBind();
        }
        private void PopolaDDLMarche()
        {
            var instance = SingletonManager.Instance;
            ddlMarca.DataSource = instance.ListMarche;
            ddlMarca.DataTextField = "Descrizione";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            //ddlMarca.Items.Insert(0, new ListItem("seleziona", "-1"));
        }
        private void PopolaDDLIsInCorso()
        {
            ddlIsInCorso.Items.Add(new ListItem("seleziona", "-1"));
            ddlIsInCorso.Items.Add(new ListItem("no", "0"));
            ddlIsInCorso.Items.Add(new ListItem("si", "1"));
        }
        protected void btnRicerca_Click(object sender, EventArgs e)
        {
            var noleggiRicerca = new NoleggiManager.RicercaNoleggiModel();

            if (ddlMarca.SelectedIndex != -1)
            {
                noleggiRicerca.IdMarca = int.Parse(ddlMarca.SelectedValue);
            }

            if (!string.IsNullOrWhiteSpace(txtModello.Text))
            {
                noleggiRicerca.Modello = txtModello.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtTarga.Text))
            {
                noleggiRicerca.Targa = txtTarga.Text;
            }
            if (cldInizioNoleggio.SelectedDate != DateTime.MinValue)
            {
                noleggiRicerca.DataInizio = cldInizioNoleggio.SelectedDate;
            }
            if (cldFineNoleggio.SelectedDate != DateTime.MinValue)
            {
                noleggiRicerca.DataFine = cldFineNoleggio.SelectedDate;
            }

            if (ddlIsInCorso.SelectedValue != "-1")
            {
                int ddlIsInCorsoValue = int.Parse(ddlIsInCorso.SelectedValue);        // prende il valore selezionato dalla ddl e lo parsa a intero
                bool si = Convert.ToBoolean(ddlIsInCorsoValue);     // converte l'intero in booleano (0 = false, 1 = true)
                noleggiRicerca.IsInCorso = si;         // mette il valore booleano nella variabile di ricerca
            }
            if (!string.IsNullOrWhiteSpace(txtCognome.Text))
            {
                noleggiRicerca.Cognome = txtCognome.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                noleggiRicerca.Nome = txtNome.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtCodiceFiscale.Text))
            {
                noleggiRicerca.CodiceFiscale = txtCodiceFiscale.Text;
            }
            PopolaGridViewNoleggio(noleggiRicerca);
            Session["RicercaNoleggi"] = noleggiRicerca;
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlMarca.SelectedIndex = -1;
            ddlIsInCorso.SelectedIndex = -1;
            cldInizioNoleggio.SelectedDate = DateTime.MinValue;
            cldFineNoleggio.SelectedDate = DateTime.MinValue;
            txtTarga.Text = "";
            txtModello.Text = "";
            txtCognome.Text = "";
            txtNome.Text = "";
            txtCodiceFiscale.Text = "";
            gvNoleggiTrovati.DataSource = null;
            gvNoleggiTrovati.DataBind();
            gvNoleggiTrovati.Visible = false;
        }
        protected void gvNoleggiTrovati_PageIndexChanging(object sender, GridViewPageEventArgs e)           //impaginazione gridview
        {
            if (Session["RicercaNoleggi"] == null)       // se non esiste la sessione che si chiama ricerca non fa nulla
            {
                return;
            }
            var ricerca = (NoleggiManager.RicercaNoleggiModel)Session["RicercaNoleggi"];      // fa il cast a ricercaveicolimodel della session
            gvNoleggiTrovati.PageIndex = e.NewPageIndex;
            PopolaGridViewNoleggio(ricerca);
        }
        protected void gvNoleggiTrovati_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idNoleggiString = gvNoleggiTrovati.SelectedDataKey["Id"].ToString();
            Response.Redirect("DettaglioNoleggio.aspx?Id=" + idNoleggiString);
        }
        protected void txtTarga_TextChanged(object sender, EventArgs e)         // controllo per la ricerca a 3 caratteri della targa
        {
            var txtTargaDaControllare = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txtTargaDaControllare.Text))
            {
                btnRicerca.Enabled = true;
            }
            if (txtTargaDaControllare.Text.Trim().Length >= 3)       // dato che non era possibile fare la ricerca a 3 caratteri sul modello poichè ci sono modelli che hanno un solo carattere o 2, ho impostato il controllo a 3 caratteri sulla targa (trim rimuove gli spazi vuoti dalla stringa)
            {
                btnRicerca.Enabled = true;
            }
            else
            {
                btnRicerca.Enabled = false;
            }
        }
    }
}