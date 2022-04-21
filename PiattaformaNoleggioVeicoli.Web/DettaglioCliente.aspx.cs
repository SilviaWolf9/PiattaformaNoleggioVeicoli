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
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                return false;
            }
            if (!cliente.DataNascita.HasValue)
            {
                return false;
            }
            if (cliente.DataNascita > DateTime.Now)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.CodiceFiscale))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Patente))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Telefono))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Indirizzo))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.NumeroCivico))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Cap))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Citta))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Comune))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Nazione))
            {
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
            _clientiManager.ModificaCliente(cliente);
            Response.Redirect("RicercaCliente.aspx");
        }
    }
}