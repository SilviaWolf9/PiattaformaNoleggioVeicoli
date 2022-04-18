<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InserimentoVeicolo.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.InserimentoVeicolo" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="uc1" TagName="Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <uc1:Info runat="server" ID="infoControl" />

    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title">Inserisci Veicolo</h1>
        </div>
        <div class="panel-body">
            <div class="form-group">
                <label for="ddlMarca">Marca</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMarca" />
            </div>

            <div class="form-group">
                <label for="txtModello">Modello</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtModello"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="txtTarga">Targa</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtTarga"></asp:TextBox>
            </div>

            <div class="form-group">
                <label for="clDataImmatricolazione">Data Immatricolazione</label>
                <asp:Calendar runat="server" ID="clDataImmatricolazione" SelectionMode="Day" OnSelectionChanged="clDataImmatricolazione_SelectionChanged"></asp:Calendar>
            </div>

            <div class="form-group">
                <label for="ddlTipoAlimentazione">Tipo Alimentazione</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTipoAlimentazione" />
            </div>

            <div class="form-group">
                <label for="txtNote">Note</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtNote"></asp:TextBox>
            </div>

            <div class="form-group">
                <asp:Button runat="server" ID="btnInserisci" Text="Inserisci" CssClass="btn btn-default" OnClick="btnInserisci_Click" />
            </div>
        </div>
    </div>
</asp:Content>
