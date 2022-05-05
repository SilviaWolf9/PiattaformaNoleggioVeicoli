using PiattaformaNoleggioVeicoli.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PiattaformaNoleggioVeicoli.Web
{
    public partial class RicercaVeicolo : System.Web.UI.Page
    {
        private VeicoliManager _veicoliManager { get; set; }        // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto veicoliManager
        private static SingletonManager instance;               // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto singleton
        protected void Page_Load(object sender, EventArgs e)
        {
            _veicoliManager = new VeicoliManager();     // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null

            if (IsPostBack)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");          // settiamo il messaggio vuoto e cancelliamo eventuali messaggi precedenti
                return;
            }
            instance = SingletonManager.Instance;       // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null
            PopolaDDLMarche();
            PopolaDDLStatoVeicolo();
        }        
        private void PopolaGridViewVeicolo(VeicoliManager.RicercaVeicoliModel ricerca)
        {
            var listaVeicoliTrovati = _veicoliManager.RicercaVeicoli(ricerca);      // richiama la funzione di ricerca
            if (listaVeicoliTrovati.Count == 0)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Info, "Nessun risultato trovato");        
            }
            gvVeicoliTrovati.Visible = true;        // rende visibile la gridview
            gvVeicoliTrovati.DataSource = listaVeicoliTrovati;      // riempie la gridview con la lista dei veicoli trovati
            gvVeicoliTrovati.DataBind();        // mostra i dati del datasource
        }
        private void PopolaDDLMarche()
        {
            ddlMarca.DataSource = instance.ListMarchePossedute;     // usa il singleton per popolare il datasource
            ddlMarca.DataTextField = "Descrizione";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
        }
        private void PopolaDDLStatoVeicolo()
        {
            ddlStatoVeicolo.Items.Add(new ListItem("seleziona", "-1"));
            ddlStatoVeicolo.Items.Add(new ListItem("noleggiato", "0"));
            ddlStatoVeicolo.Items.Add(new ListItem("disponibile", "1"));
        }
        protected void btnRicerca_Click(object sender, EventArgs e)
        {
            var veicoliRicerca = new VeicoliManager.RicercaVeicoliModel();

            if (ddlMarca.SelectedIndex != -1)
            {
                veicoliRicerca.IdMarca = int.Parse(ddlMarca.SelectedValue);
            }    
            if (!string.IsNullOrWhiteSpace(txtModello.Text))
            {
                veicoliRicerca.Modello = txtModello.Text;
            }
            if (!string.IsNullOrWhiteSpace(txtTarga.Text))
            {
                veicoliRicerca.Targa = txtTarga.Text;
            }
            if (cldInizio.SelectedDate != DateTime.MinValue)
            {
                veicoliRicerca.InizioDataImmatricolazione = cldInizio.SelectedDate;
            }
            if (cldFine.SelectedDate != DateTime.MinValue)
            {
                veicoliRicerca.FineDataImmatricolazione = cldFine.SelectedDate;
            }
            if (ddlStatoVeicolo.SelectedValue!="-1")
            {
                int ddlStatoVeicoloValue = int.Parse(ddlStatoVeicolo.SelectedValue);        // prende il valore selezionato dalla ddl e lo parsa a intero
                bool disponibile = Convert.ToBoolean(ddlStatoVeicoloValue);     // converte l'intero in booleano (0 = false, 1 = true)
                veicoliRicerca.IsDisponibile = disponibile;         // mette il valore booleano nella variabile di ricerca
            }
            PopolaGridViewVeicolo(veicoliRicerca);
            Session["Ricerca"] = veicoliRicerca;        // carica sulla session il modello da ricercare (fa il cast implicito ad object)
        }        
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ddlMarca.SelectedIndex = -1;            
            ddlStatoVeicolo.SelectedIndex = -1;
            cldInizio.SelectedDate = DateTime.MinValue;
            cldFine.SelectedDate = DateTime.MinValue;
            txtTarga.Text = "";
            txtModello.Text = "";
            gvVeicoliTrovati.DataSource = null;
            gvVeicoliTrovati.DataBind();
            gvVeicoliTrovati.Visible = false;
        }
        protected void gvVeicoliTrovati_PageIndexChanging(object sender, GridViewPageEventArgs e)       // impaginazione della gridview
        {
            if (Session["Ricerca"]==null)       // se non esiste la sessione che si chiama ricerca non fa nulla
            {
                return;
            }
            var ricerca = (VeicoliManager.RicercaVeicoliModel) Session["Ricerca"];      // fa il cast a ricercaveicolimodel della session
            gvVeicoliTrovati.PageIndex = e.NewPageIndex;
            PopolaGridViewVeicolo(ricerca);
        }
        protected void gvVeicoliTrovati_SelectedIndexChanged(object sender, EventArgs e)        // quando selezioniamo un veicolo ci rimanda alla pagina di dettaglio del veicolo recuperando l'id dal datakey
        {
            var idVeicoloString = gvVeicoliTrovati.SelectedDataKey["Id"].ToString();        // recupero id dal datakey
            Response.Redirect("DettaglioVeicolo.aspx?Id=" + idVeicoloString);
        }
        protected void txtTarga_TextChanged(object sender, EventArgs e)         // controllo per la ricerca a 3 caratteri della targa
        {
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, String.Empty);
            var txtTargaDaControllare = (TextBox)sender;        // fa il cast a textbox dell'oggetto sender 
            if (string.IsNullOrWhiteSpace(txtTargaDaControllare.Text))      // se la textbox della targa è vuota abilita la ricerca
            {
                btnRicerca.Enabled = true;
                return;
            }
            if (txtTargaDaControllare.Text.Trim().Length >= 3)       // dato che non era possibile fare la ricerca a 3 caratteri sul modello poichè ci sono modelli che hanno un solo carattere o 2, ho impostato il controllo a 3 caratteri sulla targa (trim rimuove gli spazi vuoti dalla stringa)
            {
                btnRicerca.Enabled = true;
            }
            else
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Warning, "Inserisci almeno 3 caratteri");
                btnRicerca.Enabled = false;         // se nella textbox inseriamo meno di 3 caratteri (ma almeno uno) disabilita il tasto di ricerca
            }
        }
    }
}