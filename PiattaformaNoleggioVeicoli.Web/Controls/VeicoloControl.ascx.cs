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
        public event EventHandler<TargaUpdatedArgs> EsistenzaTarga;     // dichiara l'evento da generare
        public class TargaUpdatedArgs : EventArgs       // classe che estende eventArgs che viene passata nell'evento esistenza targa
        {
            public int IdVeicolo { get; set; }
        }
        private static SingletonManager instance;       // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto singleton
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            instance = SingletonManager.Instance;       // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null
            PopolaDDLMarche();
            PopolaDDLTipoAlimentazione();
        }        
        private void PopolaDDLMarche()
        {            
            ddlMarca.DataSource = instance.ListMarche;      // usa il singleton per popolare il datasource
            ddlMarca.DataTextField = "Descrizione";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();            
        }
        private void PopolaDDLTipoAlimentazione()
        {
            ddlTipoAlimentazione.DataSource = instance.ListTipoAlimentazione;       // usa il singleton per popolare il datasource
            ddlTipoAlimentazione.DataTextField = "Descrizione";
            ddlTipoAlimentazione.DataValueField = "Id";
            ddlTipoAlimentazione.DataBind();
            ddlTipoAlimentazione.Items.Insert(0, new ListItem("seleziona", "-1"));
        }
        public void SetVeicolo(VeicoliModel veicolo)        // va a riempire i vari componenti del control in base ai dati del veicolo passati in input
        {
            if (veicolo == null)        // ulteriore controllo
            {
                veicolo = new VeicoliModel();
                veicolo.IsDisponibile = true;
            }
            txtModello.Text = veicolo.Modello;
            txtTarga.Text = veicolo.Targa;
            txtNote.Text = veicolo.Note;
            if (veicolo.IdMarca.HasValue)
                ddlMarca.SelectedValue = veicolo.IdMarca.Value.ToString();      // se idMarca ha un valore allora il valore viene convertito a stringa e inoltre questa stringa sarà il valore selezionato dalla comboBox 
            else
            {
                ddlMarca.SelectedIndex = -1;        // altrimenti lascia il campo vuoto
            }
            if (veicolo.IdTipoAlimentazione.HasValue)
                ddlTipoAlimentazione.SelectedValue = veicolo.IdTipoAlimentazione.Value.ToString();      // se idTipoAlimentazione ha un valore allora il valore viene convertito a stringa e inoltre questa stringa sarà il valore selezionato dalla ddl 
            else
            {
                ddlTipoAlimentazione.SelectedIndex = -1;        // altrimenti mette il primo elemento (seleziona)
            }

            if (veicolo.DataImmatricolazione.HasValue)
            {
                clDataImmatricolazione.SelectedDate = veicolo.DataImmatricolazione.Value;
                clDataImmatricolazione.VisibleDate = clDataImmatricolazione.SelectedDate;       // mostra sul calendario la data selezionata
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
            var veicoloAggiornato = veicolo;        // usa una variabile di appoggio per non sovrascrivere il veicoloModel passato in input
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
            var statoVeicolo = rbtDisponibilita.SelectedValue;          // prende il valore selezionato dal rbt
            bool parseOk = int.TryParse(statoVeicolo, out int statoVeicoloInt);        // parsa la stringa statoveicolo in intero    
            veicoloAggiornato.IsDisponibile = Convert.ToBoolean(statoVeicoloInt);       // converte l'intero in booleano (0 = false, 1 = true)
            veicoloAggiornato.IdTipoStato = 1;
            return veicoloAggiornato;
        }
        protected void txtTarga_TextChanged(object sender, EventArgs e)          // verifica l'esistenza della targa e nel caso esista, genera l'evento passando l'id del veicolo con quella targa
        {
            var veicoliManager = new VeicoliManager();
            var esistenzaTarga = veicoliManager.EsistenzaTarga(txtTarga.Text);
            if (esistenzaTarga.HasValue)
            {
                var targaUpdatedArgs = new TargaUpdatedArgs()
                {
                    IdVeicolo = esistenzaTarga.Value
                };
                EsistenzaTarga(this, targaUpdatedArgs);     // scatena l'evento mandando come object this (ovvero txtTarga) e come argomento eventArgs l'id del veicolo relativo a quella targa
            }
        }
    }
}