<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestioneNoleggio.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.GestioneNoleggio" %>

<%@ Register Src="~/Controls/ClienteControl.ascx" TagPrefix="cc" TagName="Cliente" %>

<%@ Register Src="~/Controls/VeicoloControl.ascx" TagPrefix="vc" TagName="Veicolo" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="ic" TagName="Info" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ic:Info runat="server" ID="infoControl" />
    <div class="panel panel-default" >
        <div class="panel-heading">
            <h3 class="panel-title">Gestione Noleggio</h3>
        </div>

        <div class="panel-body">
            <div class="form-group col-md-12">
                <div class="col-md-4" >
                    <label for="lblMarca">Marca</label>
                    <asp:Label runat="server" ID="lblMarca" CssClass="form-control">
                    </asp:Label>
                </div>
                <div class="col-md-4" >
                    <label for="lblModello">Modello</label>
                    <asp:Label runat="server" ID="lblModello" CssClass="form-control">
                    </asp:Label>
                </div>
                <div class="col-md-4" >
                    <label for="lblTarga">Targa</label>
                    <asp:Label runat="server" ID="lblTarga" CssClass="form-control">
                    </asp:Label>
                </div>
            </div>

            <div class="form-group" runat="server" id="divVeicoloNonNoleggiato" visible="false">
                <p>
                    <label for="rbtnNuovoCliente">Nuovo Cliente?    </label>
                    <asp:RadioButtonList ID="rbtnNuovoCliente" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnNuovoCliente_SelectedIndexChanged">
                        <asp:ListItem Text="si" Value="1"></asp:ListItem>
                        <asp:ListItem Text="no" Value="0"></asp:ListItem>
                    </asp:RadioButtonList>
                </p>
            </div>

            <div class="form-group col-lg-12" runat="server" id="divClienteEsistente" visible="false">
                <div class="col-md-4" >
                    <label for="ddlCognome">Cognome</label>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCognome" OnSelectedIndexChanged="ddlCognome_SelectedIndexChanged" AutoPostBack="true" />
                </div>
                <div class="col-md-4" >
                    <label for="ddlNome">Nome</label>
                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="ddlNome" OnSelectedIndexChanged="ddlNome_SelectedIndexChanged" AutoPostBack="true"/>
                </div>
                <div class="col-md-4" >
                    <label for="ddlCodiceFiscale">Codice Fiscale</label>
                    <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="ddlCodiceFiscale" OnSelectedIndexChanged="ddlCodiceFiscale_SelectedIndexChanged" AutoPostBack="true" />
                </div>
            </div>

            <cc:Cliente runat="server" ID="clienteControl" Visible="false" OnEsistenzaCodiceFiscale="clienteControl_EsistenzaCodiceFiscale"/>

            <div class="form-group col-md-12" runat="server" id="divVeicoloNoleggiato" visible="false" >
                <div class="col-md-4" >
                    <label for="lblCognome">Cognome</label>
                    <asp:Label runat="server" CssClass="form-control" ID="lblCognome"></asp:Label>
                </div>
                <div class="col-md-4" >
                    <label for="lblNome">Nome</label>
                    <asp:Label runat="server" CssClass="form-control" ID="lblNome" />
                </div>
                <div class="col-md-4">
                    <label for="lblCodiceFiscale">Codice Fiscale</label>
                    <asp:Label runat="server" CssClass="form-control" ID="lblCodiceFiscale"></asp:Label>
                </div>
            </div>
        </div>

        <div class="panel-footer col-md-12" align="center">            
            <div align="center" class="col-md-6" >
                <asp:Button runat="server" ID="btnNoleggiaVeicolo" Text="Noleggia Veicolo" CssClass="btn" BackColor="LightSeaGreen" BorderColor="LightSeaGreen" BorderWidth="2px" OnClick="btnNoleggiaVeicolo_Click" Visible="false" />
            </div>
            <div align="center" class="col-md-6">
                <asp:Button runat="server" ID="btnReset" CssClass="btn" BorderColor="LightBlue" BorderWidth="2px" OnClick="btnReset_Click" Text="Reset" Visible="false" />
            </div>
            <div align="center" class="col-md-12">
                <asp:Button runat="server" ID="btnFineNoleggio" Text="Fine Noleggio" CssClass="btn" BackColor="Crimson" BorderColor="Crimson" BorderWidth="2px" OnClick="btnFineNoleggio_Click" Visible="false" />
            </div>
        </div>
    </div>

</asp:Content>
