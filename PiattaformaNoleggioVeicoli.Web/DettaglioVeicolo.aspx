<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DettaglioVeicolo.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.DettaglioVeicolo" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="ic" TagName="Info" %>

<%@ Register Src="~/Controls/VeicoloControl.ascx" TagPrefix="vc" TagName="Veicolo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <ic:Info runat="server" ID="infoControl" />
    <div class="panel panel-default" style="opacity:0.7">
        <div class="panel-heading" style="opacity:1">
            <h3 class="panel-title">Dettaglio Veicolo</h3>
        </div>
        <div class="panel-body">
            <vc:Veicolo runat="server" ID="veicoloControl" />
            
            <div class="form-group col-md-10" runat="server" id="divCliente" visible="true" style="opacity:1">
                <label for="divCliente">Cliente</label><br />
                <div class="form-group col-md-3" style="opacity:1">
                    <asp:Label runat="server" ID="lblCognome" CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group col-md-3" style="opacity:1">
                    <asp:Label runat="server" ID="lblNome" CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group col-md-4" style="opacity:1">
                    <asp:Label runat="server" ID="lblCodiceFiscale" CssClass="form-control"></asp:Label>
                </div>
            </div>

        </div>
        <div class="panel-footer" align="center" style="opacity:1">
            <asp:Button runat="server" ID="btnGestisciNoleggio" Text="Gestisci Noleggio" CssClass="btn btn-default" OnClick="btnGestisciNoleggio_Click" />
            <asp:Button runat="server" ID="btnSalvaModifiche" Text="Salva modifiche" CssClass="btn btn-default" OnClick="btnSalvaModifiche_Click" />
            <asp:Button runat="server" ID="btnEliminaVeicolo" Text="Elimina veicolo" CssClass="btn btn-default" OnClick="btnEliminaVeicolo_Click" />
        </div>
    </div>
</asp:Content>
