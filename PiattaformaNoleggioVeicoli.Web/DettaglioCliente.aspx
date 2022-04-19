<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DettaglioCliente.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.DettaglioCliente" %>

<%@ Register Src="~/Controls/ClienteControl.ascx" TagPrefix="cc" TagName="Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Dettaglio Cliente</h3>
        </div>
        <div class="panel-body">
            <cc:Cliente runat="server" ID="clienteControl" />
        </div>
        <div class="panel-footer" align="center">
            <asp:Button runat="server" ID="btnSalvaModifiche" Text="Salva modifiche" CssClass="btn btn-default" OnClick="btnSalvaModifiche_Click" />
        </div>
    </div>

</asp:Content>
