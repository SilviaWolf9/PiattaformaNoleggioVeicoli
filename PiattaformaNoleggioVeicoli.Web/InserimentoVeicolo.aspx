<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InserimentoVeicolo.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.InserimentoVeicolo" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="ic" TagName="Info" %>

<%@ Register Src="~/Controls/VeicoloControl.ascx" TagPrefix="vc" TagName="Veicolo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ic:Info runat="server" ID="infoControl" />
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Inserisci Veicolo</h3>
        </div>
        <div class="panel-body">
            <vc:Veicolo runat="server" ID="veicoloControl" />
        </div>
        <div class="panel-footer" align="center">
            <asp:Button runat="server" ID="btnInserisci" Text="Inserisci" CssClass="btn" BackColor="LightBlue" BorderColor="LightBlue" BorderWidth="2px" OnClick="btnInserisci_Click" />
        </div>
    </div>
</asp:Content>
