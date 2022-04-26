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
        protected void Page_Load(object sender, EventArgs e)
        {
            _veicoliManager = new VeicoliManager();
            
            if (IsPostBack)
            {
                return;
            }
            PopolaDDLMarche();
            PopolaDDLStatoVeicolo();
        }
        private VeicoliManager _veicoliManager { get; set; }
        private void PopolaGridViewVeicolo(VeicoliManager.RicercaVeicoliModel ricerca)
        {
            gvVeicoliTrovati.Visible = true;
            gvVeicoliTrovati.DataSource = _veicoliManager.RicercaVeicoli(ricerca);
            gvVeicoliTrovati.DataBind();
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
            Session["Ricerca"] = veicoliRicerca;
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

        protected void gvVeicoliTrovati_PageIndexChanging(object sender, GridViewPageEventArgs e)       // evento di impaginazione
        {
            if (Session["Ricerca"]==null)       // se non esiste la sessione che si chiama ricerca non fa nulla
            {
                return;
            }
            var ricerca = (VeicoliManager.RicercaVeicoliModel) Session["Ricerca"];      // fa il cast a ricercaveicolimodel della session
            gvVeicoliTrovati.PageIndex = e.NewPageIndex;
            PopolaGridViewVeicolo(ricerca);
        }

        protected void gvVeicoliTrovati_SelectedIndexChanged(object sender, EventArgs e)
        {
            var idVeicoloString = gvVeicoliTrovati.SelectedDataKey["Id"].ToString();
            Response.Redirect("DettaglioVeicolo.aspx?Id=" + idVeicoloString);
        }

        protected void txtTarga_TextChanged(object sender, EventArgs e)
        {
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, String.Empty);
            var txtTargaDaControllare = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txtTargaDaControllare.Text))
            {
                btnRicerca.Enabled = true;
            }

            if (txtTargaDaControllare.Text.Trim().Length > 3)       // dato che non era possibile fare la ricerca a 3 caratteri sul modello poichè ci sono modelli che hanno un solo carattere o 2, ho impostato il controllo a 3 caratteri sulla targa (trim rimuove gli spazi vuoti dalla stringa)
            {
                btnRicerca.Enabled = true;
            }
            else
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Warning, "Inserisci almeno 3 caratteri");
                btnRicerca.Enabled = false;
            }
        }
    }
}