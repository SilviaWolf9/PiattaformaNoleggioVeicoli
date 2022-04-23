using PiattaformaNoleggioVeicoli.Business.Managers;
using PiattaformaNoleggioVeicoli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace PiattaformaNoleggioVeicoli.Web
{
    public partial class DettaglioVeicolo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _veicoliManager = new VeicoliManager();

            if (IsPostBack)
            {
                return;
            }

            int? id = null;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]))
            {
                id = int.Parse(Request.QueryString["Id"]);      // serve a recuperare l'id del veicolo
            }
            PopolaDettaglioVeicolo(id);
        }
        private VeicoliManager _veicoliManager { get; set; }
        private void PopolaDettaglioVeicolo(int? id)
        {
            if (!id.HasValue)
            {
                return;
            }
            var veicolo = _veicoliManager.GetVeicolo(id.Value);
            ViewState["DettaglioVeicoloModelView"] = veicolo;
            veicoloControl.Veicolo = new VeicoliModel()
            {
                Id = id.Value,
                IdMarca = veicolo.IdMarca,
                IdTipoAlimentazione = veicolo.IdTipoAlimentazione,
                DataImmatricolazione = veicolo.DataImmatricolazione,
                Modello = veicolo.Modello,
                Note = veicolo.Note,
                Targa = veicolo.Targa,
                IsDisponibile = veicolo.IsDisponibile
            };
            veicoloControl.SetVeicolo();                        
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
                return false;
            }
            if (string.IsNullOrWhiteSpace(veicolo.Modello))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(veicolo.Targa))
            {
                return false;
            }
            if (!veicolo.DataImmatricolazione.HasValue)
            {
                return false;
            }
            if (veicolo.DataImmatricolazione > DateTime.Now)
            {
                return false;
            }
            if (!veicolo.IdTipoAlimentazione.HasValue)
            {
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
            var veicolo = veicoloControl.GetDatiVeicolo();
            if (!IsFormValido(veicolo))     
            {
                return;
            }
            _veicoliManager.ModificaVeicolo(veicolo);
            Response.Redirect("RicercaVeicolo.aspx");       // dopo la modifica ci rimanda alla pagina di ricerca veicolo
        }

        protected void btnEliminaVeicolo_Click(object sender, EventArgs e)
        {
            var veicolo = veicoloControl.GetDatiVeicolo();
            if (!IsFormValido(veicolo))     
            {
                return;
            }
            _veicoliManager.EliminaVeicolo(veicolo);
            Response.Redirect("RicercaVeicolo.aspx");       // dopo l'eliminazione ci rimanda alla pagina di ricerca veicolo
        }
    }
}