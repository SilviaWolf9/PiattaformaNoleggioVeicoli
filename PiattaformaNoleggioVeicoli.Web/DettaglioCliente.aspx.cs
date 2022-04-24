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
                return;
            }
            int? id = null;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"]))
            {
                id = int.Parse(Request.QueryString["Id"]);      // serve  recuperare l'id del cliente
            }
            PopolaDettaglioCliente(id);
        }
        private ClientiManager _clientiManager { get; set; }

        private void PopolaDettaglioCliente(int? id)
        {
            if (!id.HasValue)
            {
                // messaggio errore
                return;
            }
            var cliente = _clientiManager.GetCliente(id.Value);
            clienteControl.Cliente = cliente;            
            clienteControl.SetCliente();
        }
        private bool IsFormValido(ClientiModel cliente)     // controlla che il form di inserimento del cliente sia corretto
        {
            if (string.IsNullOrWhiteSpace(cliente.Cognome))
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                // messaggio errore
                return false;
            }
            if (!cliente.DataNascita.HasValue)
            {
                // messaggio errore
                return false;
            }
            if (cliente.DataNascita > DateTime.Now)
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.CodiceFiscale))
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Patente))
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Telefono))
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Indirizzo))
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.NumeroCivico))
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Cap))
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Citta))
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Comune))
            {
                // messaggio errore
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Nazione))
            {
                // messaggio errore
                return false;
            }
            return true;
        }

        protected void btnSalvaModifiche_Click(object sender, EventArgs e)
        {
            var cliente = clienteControl.GetDatiCliente();
            if (!IsFormValido(cliente))
            {
                return;
            }
            // messaggio successo
            _clientiManager.ModificaCliente(cliente);
            Response.Redirect("RicercaCliente.aspx");
        }
    }
}