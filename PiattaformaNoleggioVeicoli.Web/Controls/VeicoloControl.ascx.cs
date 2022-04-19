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

        public VeicoliModel GetDatiVeicolo()
        {
            var veicolo = new VeicoliModel()
            {
                IdMarca = int.Parse(ddlMarca.SelectedValue),
                Modello = txtModello.Text,
                Targa = txtTarga.Text,
                DataImmatricolazione = clDataImmatricolazione.SelectedDate,
                IdTipoAlimentazione = int.Parse(ddlTipoAlimentazione.SelectedValue),
                Note = txtNote.Text,
                IsDisponibile = true
            };
            return veicolo;
        }
    }
}