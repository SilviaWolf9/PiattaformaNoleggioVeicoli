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
    public partial class InserimentoVeicolo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }      

        protected void btnInserisci_Click(object sender, EventArgs e)
        {
            var veicoloModel = veicoloControl.GetDatiVeicolo();
            if (!IsFormValido(veicoloModel))
            {
                return;
            }           

            var veicoliManager = new VeicoliManager();

            bool veicoloInserito = veicoliManager.InsertVeicolo(veicoloModel);
            if (!veicoloInserito)
            {
                infoControl.SetMessage(Web.Controls.InfoControl.TipoMessaggio.Danger, "Errore durante l'inserimento del veicolo");
            }
        }
        private bool IsFormValido(VeicoliModel veicolo)
        {
            if (veicolo.IdMarca == -1)
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
            if (veicolo.DataImmatricolazione == DateTime.MinValue)
            {
                return false;
            }
            if (veicolo.IdTipoAlimentazione == -1)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(veicolo.Note))
            {
                return false;
            }
            return true;
        }

    }
}