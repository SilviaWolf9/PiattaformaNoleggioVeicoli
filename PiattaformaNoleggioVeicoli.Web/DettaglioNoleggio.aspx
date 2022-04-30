<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DettaglioNoleggio.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.DettaglioNoleggio" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="ic" TagName="Info" %>

<%@ Register Src="~/Controls/NoleggioControl.ascx" TagPrefix="nc" TagName="Noleggio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <ic:Info runat="server" ID="infoControl" />
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Dettaglio Noleggio</h3>
                </div>
                <div class="panel-body">
                    <nc:Noleggio runat="server" ID="noleggioControl" />
                </div>
                <div class="panel-footer" align="center">
                    <asp:Button runat="server" ID="btnGestisciNoleggio" Visible="false" Text="Gestisci Noleggio" CssClass="btn btn-default" OnClick="btnGestisciNoleggio_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
