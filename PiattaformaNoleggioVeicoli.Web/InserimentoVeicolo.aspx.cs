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
        private VeicoliManager _veicoliManager { get; set; }        // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto veicoliManager
        private SingletonManager instance { get; set; }             // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto singleton
        protected void Page_Load(object sender, EventArgs e)
        {
            instance = SingletonManager.Instance;                   // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null
            _veicoliManager = new VeicoliManager();                 // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null
            if (IsPostBack)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");       // settiamo il messaggio vuoto e cancelliamo eventuali messaggi precedenti
                return;
            }
            veicoloControl.SetVeicolo(new VeicoliModel() { IsDisponibile=true });       // carica un nuovo veicolo con lo stato veicolo disponibile
        }   
        protected void btnInserisci_Click(object sender, EventArgs e)
        {
            var veicoloModel = veicoloControl.GetDatiVeicolo(new VeicoliModel() { IsDisponibile = true });          // prende i dati del nuovo veicolo inseriti 
            if (!IsFormValido(veicoloModel))
            {
                return;
            }      
            VeicoliModel veicoloInserito = _veicoliManager.InsertVeicolo(veicoloModel);         // inserisce il veicolo nel DB
            if (veicoloInserito == null)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del veicolo");
                return;
            }
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Success, "Veicolo inserito correttamente");
            instance.AggiornamentoListaMarcheVeicoliPosseduti();            // aggiorna la lista delle marche dei veicoli posseduti
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
        protected void veicoloControl_EsistenzaTarga(object sender, TargaUpdatedArgs e)         // qui si arriva solo se l'evento in veicoloControl viene generato e quindi carica il veicolo associato 
        {
            if (e == null)
            {
                return;
            }
            if (e.IdVeicolo == 0)
            {
                return;
            }            
            Response.Redirect("DettaglioVeicolo.aspx?Id=" + e.IdVeicolo);            // in caso di inserimento di una targa già esistente ci rimanda alla pagina di dettaglio del veicolo associato a quella targa
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            veicoloControl.SetVeicolo(new VeicoliModel());      // svuota i campi dopo l'inserimento
        }        
    }
}