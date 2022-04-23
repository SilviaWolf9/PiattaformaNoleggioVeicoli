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
    public partial class GestioneNoleggio : System.Web.UI.Page
    {
        private ClientiManager _clientiManager { get; set; }
        private NoleggiManager _noleggiManager { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            _clientiManager = new ClientiManager();
            _noleggiManager = new NoleggiManager();

            if (IsPostBack)
            {
                return;
            }
            DettaglioVeicoloModelView veicolo = null;
            if (Request.QueryString["veicolo"]!=null)
            {
                //veicolo = (DettaglioVeicoloModelView)Request.QueryString["veicolo"];
                var dettaglioVeicolo = Server.UrlDecode(Request.QueryString["veicolo"]);
                veicolo = JsonConvert.DeserializeObject<DettaglioVeicoloModelView>(dettaglioVeicolo);
            }
            if (veicolo==null || veicolo.Id <= 0)
            {
                return;
            }
            SetDatiGestioneNoleggio(veicolo);
            ViewState["Veicolo"] = veicolo;
        }         
        private void SetDatiGestioneNoleggio(DettaglioVeicoloModelView veicolo)
        {
            lblMarca.Text = veicolo.Marca;
            lblModello.Text = veicolo.Modello;
            lblTarga.Text = veicolo.Targa;
            if (veicolo.IsDisponibile)
            {
                divVeicoloNonNoleggiato.Visible = true;
                divVeicoloNoleggiato.Visible = false;
                //btnNoleggiaVeicolo.Visible = true;
                btnFineNoleggio.Visible = false;
            }
            else
            {
                divVeicoloNonNoleggiato.Visible = false;
                divVeicoloNoleggiato.Visible = true;
                btnNoleggiaVeicolo.Visible = false;
                btnFineNoleggio.Visible = true;
                lblCognome.Text = veicolo.Cognome;
                lblNome.Text = veicolo.Nome;
                lblCodiceFiscale.Text = veicolo.CodiceFiscale;
            }
        }
        private void PopolaDDLCognome()
        {
            var ricercaClienteModel = new ClientiManager.RicercaClientiModel();
            ddlCognome.DataSource = _clientiManager.RicercaClienti(ricercaClienteModel);      // richiamo query dei nomi da inserire nella ddl
            ddlCognome.DataValueField = "Id";
            ddlCognome.DataTextField = "Cognome";
            ddlCognome.DataBind();
            ddlCognome.Items.Insert(0, new ListItem("seleziona", "-1"));
        }
        protected void btnNoleggiaVeicolo_Click(object sender, EventArgs e)
        {
            ClientiModel cliente = null;
            var veicolo = (DettaglioVeicoloModelView)ViewState["Veicolo"];
            var selezionato = rbtnNuovoCliente.SelectedValue;
            if (selezionato == null)
            {
                return;
            }
            bool parseOk = int.TryParse(selezionato, out int selezionatoInt);       // Restituisce true o false a seconda se riesce o no a fare il parse
            if (!parseOk)
            {
                return;
            }
            bool nuovoCliente = Convert.ToBoolean(selezionatoInt);
            if (nuovoCliente)
            {
                cliente = clienteControl.GetDatiCliente();
            }
            else
            {
                var idCliente = int.Parse(ddlCodiceFiscale.SelectedValue);
                cliente = _clientiManager.GetCliente(idCliente);
            }
            if (cliente == null || cliente.Id <= 0)
            {
                return;
            }
            var noleggioModel = new NoleggiModel()
            {
                IdCliente = cliente.Id,
                IdVeicolo = veicolo.Id,
                DataInizio = DateTime.Now,
                IsInCorso = !veicolo.IsDisponibile
            };
            _noleggiManager.InserisciNoleggio(noleggioModel);           
        }

        protected void btnFineNoleggio_Click(object sender, EventArgs e)
        {
            var veicolo = (DettaglioVeicoloModelView)ViewState["Veicolo"];
            var veicoloModel = new VeicoliModel()
            {
                Id = veicolo.Id                
            };
            var noleggioModel = _noleggiManager.RecuperaNoleggio(veicoloModel);
            _noleggiManager.TerminaNoleggio(noleggioModel);
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
                clienteControl.SetCliente();
                divClienteEsistente.Visible = false;
            }
            else
            {
                divClienteEsistente.Visible = true;
                PopolaDDLCognome();
                clienteControl.Visible = false;                
            }
            btnFineNoleggio.Visible = false;
            btnNoleggiaVeicolo.Visible = true;
            btnReset.Visible = true;
        }

        protected void ddlCodiceFiscale_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCodiceFiscale.SelectedIndex == 0)
            { 
                return;
            }            
            ddlCodiceFiscale.Enabled = false;
            Session["IdClienteSelezionato"] = ddlCodiceFiscale.SelectedValue;
        }

        protected void ddlNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNome.SelectedIndex == 0)
            {
                return;
            }
            ddlNome.Enabled = false;
            ddlCodiceFiscale.Enabled = true;
            var ricercaClienteModel = new ClientiManager.RicercaClientiModel()
            {
                Cognome = ddlCognome.SelectedItem.ToString(),
                Nome = ddlNome.SelectedItem.ToString()
            };
            ddlCodiceFiscale.DataSource = _clientiManager.RicercaClienti(ricercaClienteModel);      // richiamo query dei nomi da inserire nella ddl
            ddlCodiceFiscale.DataValueField = "Id";
            ddlCodiceFiscale.DataTextField = "CodiceFiscale";
            ddlCodiceFiscale.DataBind();
            ddlCodiceFiscale.Items.Insert(0, new ListItem("seleziona", "-1"));
        }

        protected void ddlCognome_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCognome.SelectedIndex == 0)
            {
                return;
            }
            ddlCognome.Enabled = false;
            ddlNome.Enabled = true;
            var ricercaClienteModel = new ClientiManager.RicercaClientiModel()
            {
                Cognome = ddlCognome.SelectedItem.ToString()
            };
            ddlNome.DataSource = _clientiManager.RicercaClienti(ricercaClienteModel);      // richiamo query dei nomi da inserire nella ddl
            ddlNome.DataValueField = "Id";
            ddlNome.DataTextField = "Nome";
            ddlNome.DataBind();
            ddlNome.Items.Insert(0, new ListItem("seleziona", "-1"));
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            var selezionato = rbtnNuovoCliente.SelectedValue;
            if (selezionato == null)
            {
                return;
            }
            bool parseOk = int.TryParse(selezionato, out int selezionatoInt);       // Restituisce true o false a seconda se riesce o no a fare il parse
            if (!parseOk)
            {
                return;
            }
            bool nuovoCliente = Convert.ToBoolean(selezionatoInt);
            if (nuovoCliente)
            {
                clienteControl.Cliente = null;
                clienteControl.SetCliente();
            }
            else
            {
                ddlCognome.SelectedIndex = 0;
                ddlNome.Items.Clear();
                ddlCodiceFiscale.Items.Clear();
                ddlCognome.Enabled = true;
                ddlNome.Enabled = false;
                ddlCodiceFiscale.Enabled = false;
            }                
        }
    }
}


