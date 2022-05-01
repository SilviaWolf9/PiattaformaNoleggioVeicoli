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
        public event EventHandler<TargaUpdatedArgs> EsistenzaTarga;
        public class TargaUpdatedArgs : EventArgs
        {
            public int IdVeicolo { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            instance = SingletonManager.Instance;
            PopolaDDLMarche();
            PopolaDDLTipoAlimentazione();
        }
        private static SingletonManager instance;
        private void PopolaDDLMarche()
        {            
            ddlMarca.DataSource = instance.ListMarche;
            ddlMarca.DataTextField = "Descrizione";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("seleziona", "-1"));
        }
        private void PopolaDDLTipoAlimentazione()
        {
            ddlTipoAlimentazione.DataSource = instance.ListTipoAlimentazione;
            ddlTipoAlimentazione.DataTextField = "Descrizione";
            ddlTipoAlimentazione.DataValueField = "Id";
            ddlTipoAlimentazione.DataBind();
            ddlTipoAlimentazione.Items.Insert(0, new ListItem("seleziona", "-1"));
        }
        public void SetVeicolo(VeicoliModel veicolo)        // va a riempire i vari componenti del control in base ai dati della proprietà Veicolo
        {
            if (veicolo == null)
            {
                veicolo = new VeicoliModel();
                veicolo.IsDisponibile = true;
            }
            txtModello.Text = veicolo.Modello;
            txtTarga.Text = veicolo.Targa;
            txtNote.Text = veicolo.Note;
            if (veicolo.IdMarca.HasValue)
                ddlMarca.SelectedValue = veicolo.IdMarca.Value.ToString();
            else
            {
                ddlMarca.SelectedIndex = -1;
            }
            if (veicolo.IdTipoAlimentazione.HasValue)
                ddlTipoAlimentazione.SelectedValue = veicolo.IdTipoAlimentazione.Value.ToString();
            else
            {
                ddlTipoAlimentazione.SelectedIndex = -1;
            }

            if (veicolo.DataImmatricolazione.HasValue)
            {
                clDataImmatricolazione.SelectedDate = veicolo.DataImmatricolazione.Value;
                clDataImmatricolazione.VisibleDate = clDataImmatricolazione.SelectedDate;
            }
            if (!veicolo.IsDisponibile)
            {
                rbtDisponibilita.SelectedValue = "0";
            }
            else
            {
                rbtDisponibilita.SelectedValue = "1";
            }
            rbtDisponibilita.Enabled = false;
        }
        public VeicoliModel GetDatiVeicolo(VeicoliModel veicolo)        // restituisce i dati del veicolo attuali al chiamante 
        {
            var veicoloAggiornato = veicolo;
            veicoloAggiornato.Modello = txtModello.Text;
            veicoloAggiornato.Targa = txtTarga.Text;
            veicoloAggiornato.Note = txtNote.Text;
            if (clDataImmatricolazione.SelectedDate != DateTime.MinValue)
            {
                veicoloAggiornato.DataImmatricolazione = clDataImmatricolazione.SelectedDate;
            }                
            if (ddlMarca.SelectedIndex != -1)
            {
                veicoloAggiornato.IdMarca = int.Parse(ddlMarca.SelectedValue);
            }                
            if (ddlTipoAlimentazione.SelectedValue != "-1")
            {
                veicoloAggiornato.IdTipoAlimentazione = int.Parse(ddlTipoAlimentazione.SelectedValue);
            }
            var statoVeicolo = rbtDisponibilita.SelectedValue;
            veicoloAggiornato.IsDisponibile = Convert.ToBoolean(statoVeicolo);
            veicoloAggiornato.IdTipoStato = 1;
            return veicoloAggiornato;
        }
        protected void txtTarga_TextChanged(object sender, EventArgs e)          // verifica l'esistenza della targa e nel caso esista, genera l'evento passando la targa
        {
            var veicoliManager = new VeicoliManager();
            var esistenzaTarga = veicoliManager.EsistenzaTarga(txtTarga.Text);
            if (esistenzaTarga.HasValue)
            {
                var targaUpdatedArgs = new TargaUpdatedArgs()
                {
                    IdVeicolo = esistenzaTarga.Value
                };
                EsistenzaTarga(this, targaUpdatedArgs); ;
            }
        }
    }
}