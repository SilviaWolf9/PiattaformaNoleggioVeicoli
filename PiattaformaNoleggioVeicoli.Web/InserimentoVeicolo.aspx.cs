using PiattaformaNoleggioVeicoli.Business.Managers;
using PiattaformaNoleggioVeicoli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static PiattaformaNoleggioVeicoli.Web.Controls.VeicoloControl;
using AutoMapper;

namespace PiattaformaNoleggioVeicoli.Web
{
    public partial class InserimentoVeicolo : System.Web.UI.Page
    {
        private VeicoliManager _veicoliManager { get; set; }
        private SingletonManager instance { get; set; }       
        protected void Page_Load(object sender, EventArgs e)
        {
            instance = SingletonManager.Instance;
            _veicoliManager = new VeicoliManager();
            if (IsPostBack)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");                
                return;
            }
            veicoloControl.SetVeicolo(new VeicoliModel() { IsDisponibile=true });
        }   
        protected void btnInserisci_Click(object sender, EventArgs e)
        {
            var veicoloModel = veicoloControl.GetDatiVeicolo(new VeicoliModel() { IsDisponibile = true });
            if (!IsFormValido(veicoloModel))
            {
                return;
            }      
            VeicoliModel veicoloInserito = _veicoliManager.InsertVeicolo(veicoloModel);
            if (veicoloInserito == null)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del veicolo");
                return;
            }
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Success, "Veicolo inserito correttamente");
            instance.AggiornamentoListaMarcheVeicoliPosseduti();
            btnReset_Click(sender, e);          // svuota i campi dopo l'inserimento            
        }
        private bool IsFormValido(VeicoliModel veicolo)     // controlla che il form di inserimento del veicolo sia corretto
        {
            if (!veicolo.IdMarca.HasValue)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante la selezione della marca");
                return false;
            }
            if (string.IsNullOrWhiteSpace(veicolo.Modello))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del modello"); 
                return false;
            }
            if (string.IsNullOrWhiteSpace(veicolo.Targa))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento della targa"); 
                return false;
            }
            if (!veicolo.DataImmatricolazione.HasValue)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante la selezione della data d'immatricolazione"); 
                return false;
            }
            if (veicolo.DataImmatricolazione>DateTime.Now)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "A meno che tu non sia un signore del tempo, hai sbagliato ad inserire la data d'immatricolazione"); 
                return false;
            }
            if (!veicolo.IdTipoAlimentazione.HasValue)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante la selezione del tipo di alimentazione");
                return false;
            }            
            return true;
        }
        protected void veicoloControl_EsistenzaTarga(object sender, TargaUpdatedArgs e)         // all'inserimento della targa va a controllare se è già presente nel db e in caso fosse presente carica i dati del veicolo
        {
            if (e == null)
            {
                return;
            }
            if (e.IdVeicolo == 0)
            {
                return;
            }            
            Response.Redirect("DettaglioVeicolo.aspx?Id=" + e.IdVeicolo);            
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            veicoloControl.SetVeicolo(new VeicoliModel());      // svuota i campi dopo l'inserimento
        }        
    }
}