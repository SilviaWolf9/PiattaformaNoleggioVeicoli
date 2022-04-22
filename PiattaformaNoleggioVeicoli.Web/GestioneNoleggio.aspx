<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestioneNoleggio.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.GestioneNoleggio" %>

<%@ Register Src="~/Controls/ClienteControl.ascx" TagPrefix="cc" TagName="Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Gestione Noleggio</h3>
        </div>


        <div class="panel-body">
            <div class="form-group col-md-12">
                <div class="col-md-4">
                    <label for="lblMarca">Marca</label>
                    <asp:Label runat="server" ID="lblMarca" CssClass="form-control">
                    </asp:Label>
                </div>
                <div class="col-md-4">
                    <label for="lblModello">Modello</label>
                    <asp:Label runat="server" ID="lblModello" CssClass="form-control">
                    </asp:Label>
                </div>
                <div class="col-md-4">
                    <label for="lblTarga">Targa</label>
                    <asp:Label runat="server" ID="lblTarga" CssClass="form-control">
                    </asp:Label>
                </div>
            </div>


            <div class="form-group">
                <p>
                    <label for="rbtnNuovoCliente">Nuovo Cliente?    </label>
                    <%--<asp:RadioButton ID="si" Text="Si" GroupName="nuovoCliente" runat="server" OnCheckedChanged="si_CheckedChanged" AutoPostBack="true"/>--%>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                        <asp:ListItem Text="Male" Value="M" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                    </asp:RadioButtonList>
                </p>
            </div>


            <div class="form-group" runat="server" visible="false">
                <label for="ddlCognome">Cognome</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCognome" />
                <label for="ddlNome">Nome</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlNome" />
                <label for="ddlCodiceFiscale">Codice Fiscale</label>
                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCodiceFiscale" />
            </div>

            <cc:Cliente runat="server" ID="clienteControl" Visible="false" />

            <div class="form-group" runat="server" visible="false">

                <label for="lblDataImmatricolazione">Data Immatricolazione</label>
                <asp:Label runat="server" CssClass="form-control" ID="lblDataImmatricolazione"></asp:Label>

                <label for="lblTipoAlimentazione">Tipo Alimentazione</label>
                <asp:Label runat="server" CssClass="form-control" ID="lblTipoAlimentazione" />

                <label for="lblNote">Note</label>
                <asp:Label runat="server" CssClass="form-control" ID="lblNote" TextMode="MultiLine" Rows="5"></asp:Label>
            </div>
        </div>

        <div class="panel-footer" align="center">
            <asp:Button runat="server" ID="btnNoleggiaVeicolo" Text="Noleggia Veicolo" CssClass="btn btn-default" OnClick="btnNoleggiaVeicolo_Click" Visible="false" />
            <asp:Button runat="server" ID="btnFineNoleggio" Text="Fine Noleggio" CssClass="btn btn-default" OnClick="btnFineNoleggio_Click" Visible="false" />
        </div>
    </div>


</asp:Content>
