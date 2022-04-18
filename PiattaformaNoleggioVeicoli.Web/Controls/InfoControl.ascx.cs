using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PiattaformaNoleggioVeicoli.Web.Controls
{
    public partial class InfoControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void SetMessage(TipoMessaggio tipoMessaggio, string messaggio)
        {
            switch (tipoMessaggio)
            {
                case TipoMessaggio.Info:
                    paragrafoMessaggio.Attributes["class"] = "alert alert-info";
                    break;
                case TipoMessaggio.Success:
                    paragrafoMessaggio.Attributes["class"] = "alert alert-success";
                    break;
                case TipoMessaggio.Warning:
                    paragrafoMessaggio.Attributes["class"] = "alert alert-warning";
                    break;
                case TipoMessaggio.Danger:
                    paragrafoMessaggio.Attributes["class"] = "alert alert-danger";
                    break;
            }
            literalMessaggio.Text = messaggio;
        }

        public enum TipoMessaggio
        {
            Info
            , Success
            , Warning
            , Danger
        }
    }
}