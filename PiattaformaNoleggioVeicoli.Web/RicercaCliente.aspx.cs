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
        private ClientiManager _clientiManager { get; set; }        // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto clientiManager
        protected void Page_Load(object sender, EventArgs e)
        {
            _clientiManager = new ClientiManager();     // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null

            if (IsPostBack)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");      // settiamo il messaggio vuoto e cancelliamo eventuali messaggi precedenti
                return;
            }
        }
        private void PopolaGridViewCliente(ClientiManager.RicercaClientiModel ricerca)
        {
            var listaClientiTrovati = _clientiManager.RicercaClienti(ricerca);      // richiama la funzione di ricerca
            if (listaClientiTrovati.Count == 0)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Info, "Nessun risultato trovato");
            }
            gvClientiTrovati.Visible = true;        // rende visibile la gridview
            gvClientiTrovati.DataSource = listaClientiTrovati;      // riempie la gridview con la lista dei clienti trovati
            gvClientiTrovati.DataBind();        // mostra i dati del datasource
        }
        protected void gvClientiTrovati_PageIndexChanging(object sender, GridViewPageEventArgs e)       // impaginazione gridview
        {
            if (Session["RicercaClienti"] == null)       // se non esiste la sessione che si chiama ricercaClienti non fa nulla
            {
                return;
            }
            var ricercaClienti = (ClientiManager.RicercaClientiModel)Session["RicercaClienti"];      // fa il cast a ricercaclientimodel della session
            gvClientiTrovati.PageIndex = e.NewPageIndex;
            PopolaGridViewCliente(ricercaClienti);
        }
        protected void gvClientiTrovati_SelectedIndexChanged(object sender, EventArgs e)        // quando selezioniamo un cliente ci rimanda alla pagina di dettaglio del cliente recuperando l'id dal datakey
        {
            var idClientiString = gvClientiTrovati.SelectedDataKey["Id"].ToString();        // recupero id dal datakey
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
            Session["RicercaClienti"] = clientiRicerca;         // carica sulla session il modello da ricercare (fa il cast implicito ad object)
        }
    }
}