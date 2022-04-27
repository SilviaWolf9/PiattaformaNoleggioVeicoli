<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RicercaCliente.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.RicercaCliente" %>

<%@ Register Src="~/Controls/VeicoloControl.ascx" TagPrefix="vc" TagName="Veicolo" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="ic" TagName="Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ic:Info runat="server" ID="infoControl" />

    <br />
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title">Ricerca Cliente</h1>
        </div>

        <div class="panel-body">

            <div class="form-group col-md-4">
                <label>Cognome</label>
                <asp:TextBox runat="server" ID="txtCognome" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <label>Nome</label>
                <asp:TextBox runat="server" ID="txtNome" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <label>Codice Fiscale</label>
                <asp:TextBox runat="server" ID="txtCodiceFiscale" CssClass="form-control"></asp:TextBox>
            </div>


            <div class="panel-footer col-md-12" align="center">
                <div align="center" class="col-md-6">
                    <asp:Button runat="server" ID="btnRicerca" CssClass="btn" BackColor="LightBlue" BorderColor="LightBlue" BorderWidth="2px" OnClick="btnRicerca_Click" Text="Ricerca" />
                </div>
                <div align="center" class="col-md-6">
                    <asp:Button runat="server" ID="btnReset" CssClass="btn" BorderColor="LightBlue" BorderWidth="2px" OnClick="btnReset_Click" Text="Reset" />
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-default">

        <asp:GridView runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvClientiTrovati_PageIndexChanging" OnSelectedIndexChanged="gvClientiTrovati_SelectedIndexChanged" ID="gvClientiTrovati" CssClass="table table table-bordered table-hover table-striped no-margin" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="Id" Visible="False">
            <Columns>
                <asp:BoundField DataField="Cognome" HeaderText="Cognome">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Nome" HeaderText="Nome">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="CodiceFiscale" HeaderText="Codice Fiscale">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
