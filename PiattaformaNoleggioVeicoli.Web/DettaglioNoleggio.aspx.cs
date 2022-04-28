using AutoMapper;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            _noleggiManager = new NoleggiManager();

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
            PopolaDettaglioNoleggio(id);
        }
        private NoleggiManager _noleggiManager { get; set; }
        private static SingletonManager instance;
        private static IMapper mapper;

        private void PopolaDettaglioNoleggio(int? id)
        {
            if (!id.HasValue)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante il recupero dei dati del noleggio");
                return;
            }
            var noleggio = _noleggiManager.GetNoleggio(id.Value);
            ViewState["DettaglioNoleggioModelView"] = noleggio;
            noleggioControl.Noleggio = noleggio;
            noleggioControl.SetNoleggio();            
        }
    }
}