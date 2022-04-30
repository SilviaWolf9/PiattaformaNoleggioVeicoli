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
    public partial class DettaglioCliente : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            _clientiManager = new ClientiManager();
            if (IsPostBack)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");
                return;
            }
            int? id = null;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["IdCliente"]))
            {
                id = int.Parse(Request.QueryString["IdCliente"]);      // serve  recuperare l'id del cliente
            }
            PopolaDettaglioCliente(id);
        }
        private ClientiManager _clientiManager { get; set; }

        private void PopolaDettaglioCliente(int? id)
        {
            if (!id.HasValue)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante il recupero dei dati del cliente");
                return;
            }
            var cliente = _clientiManager.GetCliente(id.Value);
            //clienteControl.Cliente = cliente;
            ViewState["cliente"] = cliente;
            clienteControl.SetCliente(cliente);
        }
        private bool IsFormValido(ClientiModel cliente)     // controlla che il form di inserimento del cliente sia corretto
        {
            if (string.IsNullOrWhiteSpace(cliente.Cognome))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento del cognome");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento del nome");
                return false;
            }
            if (!cliente.DataNascita.HasValue)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nella selezione della data di nascita");
                return false;
            }
            if (cliente.DataNascita > DateTime.Now)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "A meno che il cliente non sia un signore del tempo, hai sbagliato ad inserire la data di nascita");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.CodiceFiscale))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento del codice fiscale");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Patente))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento dela patente");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Telefono))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento del numero di telefono");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento della mail");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Indirizzo))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento dell'indirizzo");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.NumeroCivico))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento del numero civico");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Cap))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento del cap");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Citta))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento della città");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Comune))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento del comune");
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Nazione))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore nell'inserimento della nazione");
                return false;
            }
            return true;
        }

        protected void btnSalvaModifiche_Click(object sender, EventArgs e)
        {
            if (ViewState["cliente"]==null)
            {
                return;
            }
            var cliente = (ClientiModel)ViewState["cliente"];
            var clienteAttuale = clienteControl.GetDatiCliente(cliente);
            if (!IsFormValido(clienteAttuale))
            {
                return;
            }
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Success, "Modifiche effettuate con successo");
            _clientiManager.ModificaCliente(clienteAttuale);
            Response.Redirect("RicercaCliente.aspx");
        }
    }
}