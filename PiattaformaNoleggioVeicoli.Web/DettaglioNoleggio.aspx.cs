using AutoMapper;
using Newtonsoft.Json;
using PiattaformaNoleggioVeicoli.Business.Managers;
using PiattaformaNoleggioVeicoli.Business.Models;
using PiattaformaNoleggioVeicoli.Web.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PiattaformaNoleggioVeicoli.Web
{
    public partial class DettaglioNoleggio : System.Web.UI.Page
    {
        private NoleggiManager _noleggiManager { get; set; }        // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto noleggiManager
        private static SingletonManager instance;           // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto singleton
        private static IMapper mapper;          // proprietà istanziata qui per evitare di dichiarare più volte l'oggetto Imapper
        protected void Page_Load(object sender, EventArgs e)
        {
            _noleggiManager = new NoleggiManager();     // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null

            if (IsPostBack)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");      // settiamo il messaggio vuoto e cancelliamo eventuali messaggi precedenti
                return;
            }
            int? id = null;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]))      // verifica che esista una querystring con key=Id
            {
                id = int.Parse(Request.QueryString["Id"]);      // recupera l'id del noleggio
            }
            instance = SingletonManager.Instance;       // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null
            mapper = instance.Mapper;       // assegnamo il valore alla proprietà istanziate sopra che altrimenti sarebbe null
            PopolaDettaglioNoleggio(id);
        }        
        private void PopolaDettaglioNoleggio(int? id)
        {
            if (!id.HasValue)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante il recupero dei dati del noleggio");
                return;
            }
            var noleggio = _noleggiManager.GetNoleggio(id.Value);       // recupera il noleggio dal db tramite l'id ricevuto in input
            ViewState["DettaglioNoleggioModelView"] = noleggio;        // mette sulla viewstate i dati del noleggio    
            noleggioControl.SetNoleggio(noleggio);          // manda al control i dati del noleggio da mostrare
            if (noleggio.IsInCorso == "si")         // se il noleggio è ancora in corso 
            {
                btnGestisciNoleggio.Visible = true;     // rende visibile il tasto di gestione noleggio
                var veicoliManager = new VeicoliManager();      // istanzia il veicoliManager
                var veicolo = veicoliManager.GetVeicolo(noleggio.IdVeicolo);        // recupera il veicolo dal db
                ViewState["DettaglioVeicoloModelView"] = veicolo;       // popola la viewstate con il veicolo recuperato
            }
        }
        protected void btnGestisciNoleggio_Click(object sender, EventArgs e)
        {
            var veicolo = ViewState["DettaglioVeicoloModelView"];       // passa i dati del veicolo che avevamo caricato precedentemente in popolaDettaglioNoleggio
            if (veicolo == null)
            {
                return;
            }
            DettaglioVeicoloModelView dettaglioVeicolo = (DettaglioVeicoloModelView)veicolo; // fa il cast da object a dettaglioVeicoloModelView
            var json = JsonConvert.SerializeObject(dettaglioVeicolo);       // serializza in json il dettaglioVeicolo
            Response.Redirect("GestioneNoleggio.aspx?veicolo=" + Server.UrlEncode(json));       // ci rimanda alla pagina di gestione noleggio e con server.urlencode codifica il json per non avere tutte le informazioni visibili in chiaro nell'url (trovato su internet)
        }
    }
}