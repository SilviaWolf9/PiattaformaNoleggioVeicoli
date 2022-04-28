<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DettaglioNoleggio.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.DettaglioNoleggio" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="ic" TagName="Info" %>

<%@ Register Src="~/Controls/NoleggioControl.ascx" TagPrefix="nc" TagName="Noleggio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ic:Info runat="server" ID="infoControl" />
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Dettaglio Noleggio</h3>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <ic:Info runat="server" ID="info1" />
                <div class="panel-body">
                    <nc:Noleggio runat="server" ID="noleggioControl" />
                </div>
                <div class="panel-footer" align="center">
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
