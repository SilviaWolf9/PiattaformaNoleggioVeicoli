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

namespace PiattaformaNoleggioVeicoli.Web
{
    public partial class DettaglioVeicolo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _veicoliManager = new VeicoliManager();

            if (IsPostBack)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");
                return;
            }

            int? id = null;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]))
            {
                id = int.Parse(Request.QueryString["Id"]);      // serve a recuperare l'id del veicolo
            }
            instance = SingletonManager.Instance;
            mapper = instance.Mapper;
            PopolaDettaglioVeicolo(id);
        }
        private VeicoliManager _veicoliManager { get; set; }
        private static SingletonManager instance;
        private static IMapper mapper;
        private void PopolaDettaglioVeicolo(int? id)
        {
            if (!id.HasValue)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante il recupero dei dati del veicolo");
                return;
            }            
            var veicolo = _veicoliManager.GetVeicolo(id.Value);
            ViewState["DettaglioVeicoloModelView"] = veicolo;
            //veicoloControl.Veicolo = new VeicoliModel()
            //{
            //    Id = id.Value,
            //    IdMarca = veicolo.IdMarca,
            //    IdTipoAlimentazione = veicolo.IdTipoAlimentazione,
            //    DataImmatricolazione = veicolo.DataImmatricolazione,
            //    Modello = veicolo.Modello,
            //    Note = veicolo.Note,
            //    Targa = veicolo.Targa,
            //    IsDisponibile = veicolo.IsDisponibile
            //};

            var veicoliModel = mapper.Map<DettaglioVeicoloModelView, VeicoliModel>(veicolo);
            veicoloControl.SetVeicolo(veicoliModel);                        
            if (!veicolo.IsDisponibile)
            {
                lblCognome.Text = veicolo.Cognome;
                lblNome.Text = veicolo.Nome;
                lblCodiceFiscale.Text = veicolo.CodiceFiscale;
            }
            else
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
            var veicolo = ViewState["DettaglioVeicoloModelView"];
            if (veicolo == null)
            {
                return;
            }
            DettaglioVeicoloModelView dettaglioVeicolo = (DettaglioVeicoloModelView) veicolo;
            var json = JsonConvert.SerializeObject(dettaglioVeicolo);            
            Response.Redirect("GestioneNoleggio.aspx?veicolo="+Server.UrlEncode(json));
        }

        protected void btnSalvaModifiche_Click(object sender, EventArgs e)
        {
            if (ViewState["DettaglioVeicoloModelView"] == null)
            {
                return;
            }
            var veicoloModelView = (DettaglioVeicoloModelView)ViewState["DettaglioVeicoloModelView"];
            if (veicoloModelView == null)
            {
                return;
            }
            if (veicoloModelView == new DettaglioVeicoloModelView())
            {
                return;
            }

            var veicoloModel = mapper.Map<DettaglioVeicoloModelView, VeicoliModel>(veicoloModelView);
            var veicolo = veicoloControl.GetDatiVeicolo(veicoloModel);
            if (!IsFormValido(veicolo))     
            {
                return;
            }
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Success, "Modifiche effettuate con successo");
            _veicoliManager.ModificaVeicolo(veicolo);
            Response.Redirect("RicercaVeicolo.aspx");       // dopo la modifica ci rimanda alla pagina di ricerca veicolo
        }




        protected void btnEliminaVeicolo_Click(object sender, EventArgs e)
        {
            if (ViewState["DettaglioVeicoloModelView"] == null)
            {
                return;
            }
            var veicoloModelView = (DettaglioVeicoloModelView)ViewState["DettaglioVeicoloModelView"];
            if (veicoloModelView == null)
            {
                return;
            }
            if (veicoloModelView == new DettaglioVeicoloModelView())
            {
                return;
            }

            var veicoloModel = mapper.Map<DettaglioVeicoloModelView, VeicoliModel>(veicoloModelView);
            var veicolo = veicoloControl.GetDatiVeicolo(veicoloModel);
            if (!IsFormValido(veicolo))     
            {
                return;
            }
            if (!veicolo.IsDisponibile)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "ATTENZIONE! Impossibile eliminare un veicolo noleggiato");
                return;
            }
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Success, "Veicolo eliminato con successo");
            _veicoliManager.EliminaVeicolo(veicolo);
            Response.Redirect("RicercaVeicolo.aspx");       // dopo l'eliminazione ci rimanda alla pagina di ricerca veicolo
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            //if (IsPostBack)
            //{
            //    return;
            //}
            //veicoloControl.Veicolo = null;
            //veicoloControl.SetVeicolo();
        }
    }
}