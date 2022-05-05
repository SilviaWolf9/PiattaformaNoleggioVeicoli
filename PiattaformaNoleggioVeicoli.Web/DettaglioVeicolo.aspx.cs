using PiattaformaNoleggioVeicoli.Business.Managers;
using PiattaformaNoleggioVeicoli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using AutoMapper;
using PiattaformaNoleggioVeicoli.Web.Controls;
using static PiattaformaNoleggioVeicoli.Web.Controls.VeicoloControl;

namespace PiattaformaNoleggioVeicoli.Web
{
    public partial class DettaglioVeicolo : System.Web.UI.Page
    {
        private VeicoliManager _veicoliManager { get; set; }        // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto veicoliManager
        private static SingletonManager instance;           // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto singleton
        private static IMapper mapper;              // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto Imapper
        protected void Page_Load(object sender, EventArgs e)
        {
            _veicoliManager = new VeicoliManager();     // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null

            if (IsPostBack)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");      // settiamo il messaggio vuoto e cancelliamo eventuali messaggi precedenti
                return;
            }
            int? id = null;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]))      // verifica che esista una querystring con key=Id
            {
                id = int.Parse(Request.QueryString["Id"]);      // recupera l'id del veicolo
            }
            instance = SingletonManager.Instance;       // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null
            mapper = instance.Mapper;       // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null
            PopolaDettaglioVeicolo(id);
        }       
        private void PopolaDettaglioVeicolo(int? id)
        {
            if (!id.HasValue)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante il recupero dei dati del veicolo");
                return;
            }            
            var veicolo = _veicoliManager.GetVeicolo(id.Value);         // recupera il veicolo dal db tramite l'id ricevuto in input
            ViewState["DettaglioVeicoloModelView"] = veicolo;       // mette sulla viewstate i dati del veicolo
            var veicoliModel = mapper.Map<DettaglioVeicoloModelView, VeicoliModel>(veicolo);        // fa il cast da dettaglioVeicoloModelView a veicoliModel tramite l'automapper
            veicoloControl.SetVeicolo(veicoliModel);      // manda al control i dati del veicolo da mostrare                  
            if (!veicolo.IsDisponibile)         // se il veicolo è noleggiato mostra i dati del cliente
            {
                lblCognome.Text = veicolo.Cognome;
                lblNome.Text = veicolo.Nome;
                lblCodiceFiscale.Text = veicolo.CodiceFiscale;
            }
            else            // altrimenti nasconde il div del cliente
            {
                divCliente.Visible = false;
            }
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
            if (veicolo.DataImmatricolazione > DateTime.Now)
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
        protected void btnGestisciNoleggio_Click(object sender, EventArgs e)
        {            
            var veicolo = ViewState["DettaglioVeicoloModelView"];       // passa i dati del veicolo che avevamo caricato precedentemente in popolaDettaglioVeicolo
            if (veicolo == null)
            {
                return;
            }
            DettaglioVeicoloModelView dettaglioVeicolo = (DettaglioVeicoloModelView) veicolo;       // fa il cast da object a dettaglioVeicoloModelView
            var json = JsonConvert.SerializeObject(dettaglioVeicolo);        // serializza in json il dettaglioVeicolo    
            Response.Redirect("GestioneNoleggio.aspx?veicolo="+Server.UrlEncode(json));     // ci rimanda alla pagina di gestione noleggio e con server.urlencode codifica il json per non avere tutte le informazioni visibili in chiaro nell'url (trovato su internet)
        }
        protected void btnSalvaModifiche_Click(object sender, EventArgs e)
        {
            if (ViewState["DettaglioVeicoloModelView"] == null)
            {
                return;
            }
            var veicoloModelView = (DettaglioVeicoloModelView)ViewState["DettaglioVeicoloModelView"];       // fa il cast da object a dettaglioVeicoloModelView
            if (veicoloModelView == null)
            {
                return;
            }
            if (veicoloModelView == new DettaglioVeicoloModelView())        // se l'oggetto castato è un oggetto vuoto e quindi non ha un id non fa modifiche
            {
                return;
            }
            var veicoloModel = mapper.Map<DettaglioVeicoloModelView, VeicoliModel>(veicoloModelView);       // fa il cast da dettaglioVeicoloModelView a veicoliModel tramite l'automapper
            var veicolo = veicoloControl.GetDatiVeicolo(veicoloModel);      // restituisce i dati aggiornati del veicolo già esistente
            if (!IsFormValido(veicolo))     
            {
                return;
            }
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Success, "Modifiche effettuate con successo");
            _veicoliManager.ModificaVeicolo(veicolo);       // richiama la funzione di modifica
            Response.Redirect("RicercaVeicolo.aspx");       // dopo la modifica ci rimanda alla pagina di ricerca veicolo
        }
        protected void btnEliminaVeicolo_Click(object sender, EventArgs e)
        {
            if (ViewState["DettaglioVeicoloModelView"] == null)
            {
                return;
            }
            var veicoloModelView = (DettaglioVeicoloModelView)ViewState["DettaglioVeicoloModelView"];        // fa il cast da object a dettaglioVeicoloModelView
            if (veicoloModelView == null)
            {
                return;
            }
            if (veicoloModelView == new DettaglioVeicoloModelView())        // se l'oggetto castato è un oggetto vuoto e quindi non ha un id non fa modifiche
            {
                return;
            }
            var veicoloModel = mapper.Map<DettaglioVeicoloModelView, VeicoliModel>(veicoloModelView);       // fa il cast da dettaglioVeicoloModelView a veicoliModel tramite l'automapper
            var veicolo = veicoloControl.GetDatiVeicolo(veicoloModel);          // restituisce i dati aggiornati del veicolo già esistente
            if (!IsFormValido(veicolo))     
            {
                return;
            }
            if (!veicolo.IsDisponibile)     // se il veicolo è noleggiato non ci permette di eliminarlo
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "ATTENZIONE! Impossibile eliminare un veicolo noleggiato");
                return;
            }
            _veicoliManager.EliminaVeicolo(veicolo);        // richiama la funzione elimina
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Success, "Veicolo eliminato con successo");           
            Response.Redirect("RicercaVeicolo.aspx");       // dopo l'eliminazione ci rimanda alla pagina di ricerca veicolo
        }
        protected void veicoloControl_EsistenzaTarga(object sender, TargaUpdatedArgs e)         // qui si arriva solo se l'evento in veicoloControl viene generato e quindi carica il veicolo associato 
        {
            if (e == null)          //in caso nella modifica mettessimo una targa già esistente ci carica il veicolo associato alla targa inserita non facendoci modificare il veicolo precedente
            {
                return;
            }
            if (e.IdVeicolo == 0)
            {
                return;
            }
            Response.Redirect("DettaglioVeicolo.aspx?Id=" + e.IdVeicolo);
        }
    }
}