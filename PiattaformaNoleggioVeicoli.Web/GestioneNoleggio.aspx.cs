using PiattaformaNoleggioVeicoli.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PiattaformaNoleggioVeicoli.Web
{
    public partial class GestioneNoleggio : System.Web.UI.Page
    {
        private ClientiManager _clientiManager { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            _clientiManager = new ClientiManager();
        }         

        protected void btnNoleggiaVeicolo_Click(object sender, EventArgs e)
        {

        }

        protected void btnFineNoleggio_Click(object sender, EventArgs e)
        {

        }

        protected void rbtnNuovoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selezionato = rbtnNuovoCliente.SelectedValue;
            if (selezionato==null)
            {
                return;
            }
            bool parseOk = int.TryParse(selezionato, out int selezionatoInt);       // Restituisce true o false a seconda se riesce o no a fare il parse
            if (!parseOk)
            {
                return ;
            }
            bool? nuovoCliente = Convert.ToBoolean(selezionatoInt);
            if (!nuovoCliente.HasValue)
            {
                return;
            }
            if (nuovoCliente.Value)
            {
                clienteControl.Visible = true;
                btnNoleggiaVeicolo.Visible = true;
                divClienteEsistente.Visible = false;
                btnFineNoleggio.Visible = false;
                btnReset.Visible = true;

            }
            else
            {
                divClienteEsistente.Visible = true;
                btnFineNoleggio.Visible = false;
                clienteControl.Visible = false;
                btnNoleggiaVeicolo.Visible = true;
                btnReset.Visible = true;
            }
        }

        protected void ddlCodiceFiscale_SelectedIndexChanged(object sender, EventArgs e)
        {
            divVeicoloNoleggiato.Visible = true;
            lblCodiceFiscale.Text = ddlCodiceFiscale.SelectedItem.ToString();            
            ddlCodiceFiscale.Enabled = false;
        }

        protected void ddlNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlNome.Enabled = false;
            ddlCodiceFiscale.Enabled = true;
            lblNome.Text = ddlNome.SelectedItem.ToString();
            var ricercaClienteModel = new ClientiManager.RicercaClientiModel()
            {
                Cognome = ddlCognome.SelectedItem.ToString(),
                Nome = ddlNome.SelectedItem.ToString()
            };
            ddlCodiceFiscale.DataSource = _clientiManager.RicercaClienti(ricercaClienteModel);      // richiamo query dei nomi da inserire nella ddl
            ddlCodiceFiscale.DataValueField = "Id";
            ddlCodiceFiscale.DataTextField = "CodiceFiscale";
            ddlCodiceFiscale.DataBind();
        }

        protected void ddlCognome_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCognome.Enabled = false;
            ddlNome.Enabled = true;
            lblCognome.Text = ddlCognome.SelectedItem.ToString();
            var ricercaClienteModel = new ClientiManager.RicercaClientiModel()
            {
                Cognome = ddlCognome.SelectedItem.ToString()
            };
            ddlNome.DataSource = _clientiManager.RicercaClienti(ricercaClienteModel);      // richiamo query dei nomi da inserire nella ddl
            ddlNome.DataValueField = "Id";
            ddlNome.DataTextField = "Nome";
            ddlNome.DataBind();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
    }
}