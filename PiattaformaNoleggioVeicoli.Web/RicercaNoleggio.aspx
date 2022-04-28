<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RicercaNoleggio.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.RicercaNoleggio" %>

<%@ Register Src="~/Controls/VeicoloControl.ascx" TagPrefix="vc" TagName="Veicolo" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="ic" TagName="Info" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <ic:Info runat="server" ID="infoControl" />

    <br />
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title">Ricerca Noleggio</h1>
        </div>

        <div class="panel-body">

            <div class="form-group col-md-4">
                <label>Marca </label>
                <div>
                    <cc1:ComboBox ID="ddlMarca" runat="server" MaxLength="0" CssClass="text-capitalize" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" AutoPostBack="True" CaseSensitive="False"></cc1:ComboBox>
                </div>
                <%--<asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-control" />--%>
            </div>

            <div class="form-group col-md-4">
                <label>Modello</label>
                <asp:TextBox runat="server" ID="txtModello" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <label>Targa</label>
                <asp:TextBox runat="server" ID="txtTarga" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtTarga_TextChanged"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <label>In corso?</label>
                <asp:DropDownList runat="server" ID="ddlIsInCorso" CssClass="form-control" />
            </div>

            <div class="form-group col-md-4">
                <label>Data di Inizio Noleggio</label>
                <asp:Calendar runat="server" ID="cldInizioNoleggio" SelectionMode="Day">
                    <OtherMonthDayStyle ForeColor="LightGray"></OtherMonthDayStyle>
                    <TitleStyle CssClass="text-capitalize" Font-Size="15px" Font-Bold="True" BackColor="LightSeaGreen"/>
                    <DayStyle BackColor="white" />
                    <SelectedDayStyle BackColor="LightSeaGreen" Font-Bold="True" />
                </asp:Calendar>
            </div>

            <div class="form-group col-md-4">
                <label>Data di Fine Noleggio</label>
                <asp:Calendar runat="server" ID="cldFineNoleggio" SelectionMode="Day">
                    <OtherMonthDayStyle ForeColor="LightGray"></OtherMonthDayStyle>
                    <TitleStyle CssClass="text-capitalize" Font-Size="15px" Font-Bold="True" BackColor="LightSeaGreen"/>
                    <DayStyle BackColor="white" />
                    <SelectedDayStyle BackColor="LightSeaGreen" Font-Bold="True" />
                </asp:Calendar>
            </div>
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
    <br />
    <br />  

    <div class="panel panel-default">

        <asp:GridView runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvNoleggiTrovati_PageIndexChanging" OnSelectedIndexChanged="gvNoleggiTrovati_SelectedIndexChanged" ID="gvNoleggiTrovati" CssClass="table table table-bordered table-hover table-striped no-margin" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="Id" Visible="False">
            <Columns>
                <asp:BoundField DataField="Marca" HeaderText="Marca">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Modello" HeaderText="Modello">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Targa" HeaderText="Targa">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="IsInCorso" HeaderText="In Corso">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DataInizio" HeaderText="Data Inizio Noleggio" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="DataFine" HeaderText="Data Fine Noleggio" DataFormatString="{0:dd/MM/yyyy}">
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
