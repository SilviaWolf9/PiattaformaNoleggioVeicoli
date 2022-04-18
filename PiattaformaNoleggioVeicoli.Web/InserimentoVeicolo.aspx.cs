using PiattaformaNoleggioVeicoli.Business.Managers;
using PiattaformaNoleggioVeicoli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PiattaformaNoleggioVeicoli.Web
{
    public partial class InserimentoVeicolo : System.Web.UI.Page
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

        public void PopolaDDLMarche()
        {
            var veicoliManager = new VeicoliManager();
            ddlMarca.DataSource = veicoliManager.GetMarcheVeicoliList();
            ddlMarca.DataTextField = "Descrizione";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("seleziona", "-1"));
        }

        public void PopolaDDLTipoAlimentazione()
        {
            var veicoliManager = new VeicoliManager();
            ddlTipoAlimentazione.DataSource = veicoliManager.GetTipoAlimentazioneList();
            ddlTipoAlimentazione.DataTextField = "Descrizione";
            ddlTipoAlimentazione.DataValueField = "Id";
            ddlTipoAlimentazione.DataBind();
            ddlTipoAlimentazione.Items.Insert(0, new ListItem("seleziona", "-1"));
        }

        protected void btnInserisci_Click(object sender, EventArgs e)
        {
            if (!IsFormValido())
            {
                return;
            }

            var veicoloModel = new VeicoliModel()
            {
                IdMarca = int.Parse(ddlMarca.SelectedValue),
                Modello = txtModello.Text,
                Targa = txtTarga.Text,
                DataImmatricolazione = clDataImmatricolazione.SelectedDate,
                IdTipoAlimentazione = int.Parse(ddlTipoAlimentazione.SelectedValue),
                Note = txtNote.Text,
                IsDisponibile = true
            };

            var veicoliManager = new VeicoliManager();

            bool veicoloInserito = veicoliManager.InsertVeicolo(veicoloModel);
            if (!veicoloInserito)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del veicolo");
            }
        }

        private bool IsFormValido()
        {
            if (ddlMarca.SelectedIndex == -1)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtModello.Text))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTarga.Text))
            {
                return false;
            }
            if (clDataImmatricolazione.SelectedDate == DateTime.MinValue)
            {
                return false;
            }
            if (ddlTipoAlimentazione.SelectedIndex == -1)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNote.Text))
            {
                return false;
            }
            return true;
        }

        protected void clDataImmatricolazione_SelectionChanged(object sender, EventArgs e)
        {
            clDataImmatricolazione.SelectedDayStyle.BackColor = System.Drawing.Color.Blue;
        }
    }
}