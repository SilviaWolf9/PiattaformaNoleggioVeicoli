<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DettaglioNoleggio.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.DettaglioNoleggio" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="ic" TagName="Info" %>

<%@ Register Src="~/Controls/NoleggioControl.ascx" TagPrefix="nc" TagName="Noleggio" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <ic:Info runat="server" ID="infoControl" />

    <div class="panel panel-default" style="opacity:0.7">
        <div class="panel-heading" style="opacity:1">
            <h3 class="panel-title">Dettaglio Noleggio</h3>
        </div>
        <nc:Noleggio runat="server" ID="noleggioControl" />
    </div>
    <div class="panel-footer" align="center" style="opacity:1">
    </div>

</asp:Content>
