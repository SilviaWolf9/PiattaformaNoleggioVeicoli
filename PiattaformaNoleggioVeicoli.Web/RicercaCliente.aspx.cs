using PiattaformaNoleggioVeicoli.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PiattaformaNoleggioVeicoli.Web
{
    public partial class RicercaCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _clientiManager = new ClientiManager();

            if (IsPostBack)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");
                return;
            }
        }
        private ClientiManager _clientiManager { get; set; }
        private void PopolaGridViewCliente(ClientiManager.RicercaClientiModel ricerca)
        {
            var listaClientiTrovati = _clientiManager.RicercaClienti(ricerca);
            if (listaClientiTrovati.Count == 0)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Info, "Nessun risultato trovato");
            }
            gvClientiTrovati.Visible = true;
            gvClientiTrovati.DataSource = listaClientiTrovati;
            gvClientiTrovati.DataBind();
        }

        protected void gvClientiTrovati_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (Session["RicercaClienti"] == null)       // se non esiste la sessione che si chiama ricercaClienti non fa nulla
            {
                return;
            }
            var ricercaClienti = (ClientiManager.RicercaClientiModel)Session["RicercaClienti"];      // fa il cast a ricercaclientimodel della session
            gvClientiTrovati.PageIndex = e.NewPageIndex;
            PopolaGridViewCliente(ricercaClienti);
        }

        protected void gvClientiTrovati_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idClientiString = gvClientiTrovati.SelectedDataKey["Id"].ToString();
            Response.Redirect("DettaglioCliente.aspx?IdCliente=" + idClientiString);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {            
            txtCognome.Text = "";
            txtNome.Text = "";
            txtCodiceFiscale.Text = "";
            gvClientiTrovati.DataSource = null;
            gvClientiTrovati.DataBind();
            gvClientiTrovati.Visible = false;
        }

        protected void btnRicerca_Click(object sender, EventArgs e)
        {
            var clientiRicerca = new ClientiManager.RicercaClientiModel();
            
            if (!string.IsNullOrWhiteSpace(txtCognome.Text))
            {
                clientiRicerca.Cognome = txtCognome.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtNome.Text))
            {
                clientiRicerca.Nome = txtNome.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtCodiceFiscale.Text))
            {
                clientiRicerca.CodiceFiscale = txtCodiceFiscale.Text;
            }            
            PopolaGridViewCliente(clientiRicerca);
            Session["RicercaClienti"] = clientiRicerca;
        }
    }
}