<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestioneNoleggio.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.GestioneNoleggio" %>

<%@ Register Src="~/Controls/ClienteControl.ascx" TagPrefix="cc" TagName="Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel-heading">
        <h3 class="panel-title">Gestione Noleggio</h3>
    </div>

    <div class="form-group" visible="true">
        <label for="txtMarca">Marca</label>
        <asp:TextBox runat="server" ID="txtMarca" CssClass="form-control" ReadOnly="true">
        </asp:TextBox>

        <label for="txtModello">Modello</label>
        <asp:TextBox runat="server" ID="txtModello" CssClass="form-control" ReadOnly="true">
        </asp:TextBox>

        <label for="txtTarga">Targa</label>
        <asp:TextBox runat="server" ID="txtTarga" CssClass="form-control" ReadOnly="true">
        </asp:TextBox>

        <div class="form-group">
            <p>
                <label for="rbtnNuovoCliente">Nuovo Cliente? </label>
                <asp:RadioButton ID="no" Text="No" Checked="True" GroupName="nuovoCliente" runat="server" OnCheckedChanged="no_CheckedChanged" />
                <asp:RadioButton ID="si" Text="Si" GroupName="nuovoCliente" runat="server" OnCheckedChanged="si_CheckedChanged" />
            </p>
        </div>

        <div class="form-group" visible="true">
            <label for="ddlCognome">Cognome</label>
            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCognome" />
            <label for="ddlNome">Nome</label>
            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlNome" />
            <label for="ddlCodiceFiscale">Codice Fiscale</label>
            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCodiceFiscale" />
        </div>

        <div class="panel-body" visible="false">
            <cc:Cliente runat="server" ID="clienteControl" />
        </div>

        <div class="panel-footer" align="center">
            <asp:Button runat="server" ID="btnNoleggiaVeicolo" Text="Noleggia Veicolo" CssClass="btn btn-default" OnClick="btnNoleggiaVeicolo_Click" />
        </div>
    </div>

    <div class="form-group" visible="false">

        <%--<label for="txtMarca">Marca</label>
        <asp:TextBox runat="server" CssClass="form-control" ID="txtMarca" ReadOnly="true"/>

        <label for="txtModello">Modello</label>
        <asp:TextBox runat="server" CssClass="form-control" ID="txtModello" ReadOnly="true"></asp:TextBox>

        <label for="txtTarga">Targa</label>
        <asp:TextBox runat="server" CssClass="form-control" ID="txtTarga" ReadOnly="true"></asp:TextBox>--%>

        <label for="txtDataImmatricolazione">Data Immatricolazione</label>
        <asp:TextBox runat="server" CssClass="form-control" ID="txtDataImmatricolazione" ReadOnly="true"></asp:TextBox>        

        <label for="txtTipoAlimentazione">Tipo Alimentazione</label>
        <asp:TextBox runat="server" CssClass="form-control" ID="txtTipoAlimentazione" ReadOnly="true"/>

        <label for="txtNote">Note</label>
        <asp:TextBox runat="server" CssClass="form-control" ID="txtNote" TextMode="MultiLine" Rows="5" ReadOnly="true"></asp:TextBox>

        <div class="panel-footer" align="center">
            <asp:Button runat="server" ID="btnFineNoleggio" Text="Fine Noleggio" CssClass="btn btn-default" OnClick="btnFineNoleggio_Click" />
        </div>

    </div>

</asp:Content>
