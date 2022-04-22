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
    public partial class VeicoloControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            PopolaDDLMarche();
            PopolaDDLTipoAlimentazione();
        }

        public VeicoliModel Veicolo { get => _veicolo; set => _veicolo = value; }       // serve per ottenere e settare il valore della variabile statica dichiarata sotto in modo che abbia lo stesso valore in tutta la pagina si poteva fare anche utilizzando una viewstate
        private static VeicoliModel _veicolo;        

        private void PopolaDDLMarche()
        {
            var veicoliManager = new VeicoliManager();
            ddlMarca.DataSource = veicoliManager.GetMarcheVeicoliList();
            ddlMarca.DataTextField = "Descrizione";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("seleziona", "-1"));
        }

        private void PopolaDDLTipoAlimentazione()
        {
            var veicoliManager = new VeicoliManager();
            ddlTipoAlimentazione.DataSource = veicoliManager.GetTipoAlimentazioneList();
            ddlTipoAlimentazione.DataTextField = "Descrizione";
            ddlTipoAlimentazione.DataValueField = "Id";
            ddlTipoAlimentazione.DataBind();
            ddlTipoAlimentazione.Items.Insert(0, new ListItem("seleziona", "-1"));
        }

        public void SetVeicolo()        // va a riempire i vari componenti del control in base ai dati della proprietà Veicolo
        {
            if (Veicolo == null)
            {
                Veicolo = new VeicoliModel();
            }
            txtModello.Text = Veicolo.Modello;
            txtTarga.Text = Veicolo.Targa;
            txtNote.Text = Veicolo.Note;
            if (Veicolo.IdMarca.HasValue)
                ddlMarca.SelectedValue = Veicolo.IdMarca.Value.ToString();
            else
            {
                ddlMarca.SelectedIndex = -1;
            }
            if (Veicolo.IdTipoAlimentazione.HasValue)
                ddlTipoAlimentazione.SelectedValue = Veicolo.IdTipoAlimentazione.Value.ToString();
            else
            {
                ddlTipoAlimentazione.SelectedIndex = -1;
            }

            if (Veicolo.DataImmatricolazione.HasValue)
            {
                clDataImmatricolazione.SelectedDate = Veicolo.DataImmatricolazione.Value;
            }
            if (!Veicolo.IsDisponibile)
            {
                rbtDisponibile.Checked = false;
            }
            else
            {
                rbtDisponibile.Checked = true;

            }
        }

        public VeicoliModel GetDatiVeicolo()        // restituisce i dati del veicolo attuali al chiamante 
        {
            Veicolo.Modello = txtModello.Text;
            Veicolo.Targa = txtTarga.Text;
            Veicolo.Note = txtNote.Text;
            if (clDataImmatricolazione.SelectedDate != DateTime.MinValue)
            {
                Veicolo.DataImmatricolazione = clDataImmatricolazione.SelectedDate;
            }                
            if (ddlMarca.SelectedValue != "-1")
            {
                Veicolo.IdMarca = int.Parse(ddlMarca.SelectedValue);
            }                
            if (ddlTipoAlimentazione.SelectedValue != "-1")
            {
                Veicolo.IdTipoAlimentazione = int.Parse(ddlTipoAlimentazione.SelectedValue);
            }                
            Veicolo.IsDisponibile = rbtDisponibile.Checked;
            Veicolo.IdTipoStato = 1;
            return Veicolo;
        }
    }
}