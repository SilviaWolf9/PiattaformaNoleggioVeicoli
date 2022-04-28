﻿using PiattaformaNoleggioVeicoli.Business.Managers;
using PiattaformaNoleggioVeicoli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using PiattaformaNoleggioVeicoli.Web.Controls;
using static PiattaformaNoleggioVeicoli.Web.Controls.ClienteControl;

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
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.NotSet, "");
                return;
            }
            DettaglioVeicoloModelView veicolo = null;
            if (Request.QueryString["veicolo"]!=null)
            {
                var dettaglioVeicolo = Server.UrlDecode(Request.QueryString["veicolo"]);
                veicolo = JsonConvert.DeserializeObject<DettaglioVeicoloModelView>(dettaglioVeicolo);
            }
            if (veicolo==null || veicolo.Id <= 0)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante la selezione del veicolo");
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
                var clienteDaInserire = clienteControl.GetDatiCliente();
                if (!IsFormValido(clienteDaInserire))
                {
                    return;
                }
                cliente = _clientiManager.InsertCliente(clienteDaInserire);
            }
            else
            {
                
                var idClienteSelezionato = Session["IdClienteSelezionato"].ToString();
                if (string.IsNullOrEmpty(idClienteSelezionato))
                {
                    return;
                }
                var idCliente = int.Parse(idClienteSelezionato);
                cliente = _clientiManager.GetCliente(idCliente);
            }
            if (cliente == null || cliente.Id <= 0)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante il recupero dei dati del cliente");
                return;
            }
            var noleggioModel = new NoleggiModel()
            {
                IdCliente = cliente.Id,
                IdVeicolo = veicolo.Id,
                DataInizio = DateTime.Now,
                IsInCorso = !veicolo.IsDisponibile
            };
            int? idDettaglio = _noleggiManager.InserisciNoleggio(noleggioModel);
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Success, "Noleggio registrato con successo");
            Response.Redirect("DettaglioNoleggio.aspx?Id=" + idDettaglio);
        }

        private bool IsFormValido(ClientiModel clienteDaInserire)
        {            
            if (string.IsNullOrWhiteSpace(clienteDaInserire.Cognome))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del cognome");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.Nome))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del nome");
                return false;
            }
            if (!clienteDaInserire.DataNascita.HasValue)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante la selezione della data di nascita");
                return false;
            }
            if (clienteDaInserire.DataNascita > DateTime.Now)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "A meno che tu non sia un signore del tempo, hai sbagliato ad inserire la data di nascita");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.CodiceFiscale))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del codice fiscale");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.Patente))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento della patente");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.Telefono))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del telefono");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.Email))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento della email");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.Indirizzo))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento dell' indirizzo");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.NumeroCivico))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del numero civico");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.Cap))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del cap");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.Citta))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento della città");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.Comune))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del comune");
                return false;
            }
            if (string.IsNullOrWhiteSpace(clienteDaInserire.Nazione))
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento della nazione");
                return false;
            }
            return true;
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
            infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Success, "Noleggio terminato con successo");
            Response.Redirect("DettaglioVeicolo.aspx?Id=" + veicolo.Id);
        }

        protected void rbtnNuovoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnReset_Click(this, null);
            
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
                if (ddlCognome.Items.Count > 0)
                {
                    ddlCognome.SelectedIndex = 0;
                }
                else
                {
                    ddlCognome.Items.Clear();
                }
                ddlNome.Items.Clear();
                ddlCodiceFiscale.Items.Clear();
                ddlCognome.Enabled = true;
                ddlNome.Enabled = false;
                ddlCodiceFiscale.Enabled = false;
            }                
        }

        protected void clienteControl_EsistenzaCodiceFiscale(object sender, CodiceFiscaleUpdatedArgs e)
        {
            if (e==null)
            {
                return;
            }
            if (e.IdCliente==0)
            {
                return;
            }
            rbtnNuovoCliente.SelectedValue = "0";
            divVeicoloNoleggiato.Visible = true;
            clienteControl.Visible = false;
            var cliente = _clientiManager.GetCliente(e.IdCliente);
            lblCognome.Text = cliente.Cognome;
            lblNome.Text = cliente.Nome;
            lblCodiceFiscale.Text = cliente.CodiceFiscale;
            btnReset.Visible = false;
            Session["IdClienteSelezionato"] = e.IdCliente;

        }
    }
}


