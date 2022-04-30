<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RicercaVeicolo.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.RicercaVeicolo" %>

<%@ Register Src="~/Controls/VeicoloControl.ascx" TagPrefix="vc" TagName="Veicolo" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="ic" TagName="Info" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <ic:Info runat="server" ID="infoControl" />
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h1 class="panel-title">Ricerca Veicolo</h1>
                </div>
                <div class="panel-body">
                    <div class="form-group col-md-4">
                        <label>Marca </label>
                        <div>
                            <cc1:ComboBox ID="ddlMarca" runat="server" MaxLength="0" CssClass="text-capitalize" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" AutoPostBack="True" CaseSensitive="False"></cc1:ComboBox>
                        </div>
                    </div>

                    <div class="form-group col-md-4">
                        <label>Modello</label>
                        <asp:TextBox runat="server" ID="txtModello" CssClass="form-control"></asp:TextBox>
                    </div>

                    <div class="form-group col-md-4">
                        <label>Targa</label>
                        <asp:TextBox runat="server" ID="txtTarga" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtTarga_TextChanged"></asp:TextBox>
                    </div>
                    <br />
                    <div class="form-group col-md-4">
                        <label>Inizio Data Immatricolazione</label>
                        <asp:Calendar runat="server" ID="cldInizio" SelectionMode="Day">
                            <OtherMonthDayStyle ForeColor="LightGray"></OtherMonthDayStyle>
                            <TitleStyle CssClass="text-capitalize" Font-Size="15px" Font-Bold="True" BackColor="LightSeaGreen" />
                            <DayStyle BackColor="white" />
                            <SelectedDayStyle BackColor="LightSeaGreen" Font-Bold="True" />
                        </asp:Calendar>
                    </div>

                    <div class="form-group col-md-4">
                        <label>Fine Data Immatricolazione</label>
                        <asp:Calendar runat="server" ID="cldFine" SelectionMode="Day">
                            <OtherMonthDayStyle ForeColor="LightGray"></OtherMonthDayStyle>
                            <TitleStyle CssClass="text-capitalize" Font-Size="15px" Font-Bold="True" BackColor="LightSeaGreen" />
                            <DayStyle BackColor="white" />
                            <SelectedDayStyle BackColor="LightSeaGreen" Font-Bold="True" />
                        </asp:Calendar>
                    </div>

                    <div class="form-group col-md-4">
                        <label for="rbtnStatoVeicolo">Stato veicolo:</label>
                        <asp:DropDownList ID="ddlStatoVeicolo" runat="server" CssClass="form-control">
                        </asp:DropDownList>
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
                <asp:GridView runat="server" RowStyle-CssClass="rowGridView" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvVeicoliTrovati_PageIndexChanging" OnSelectedIndexChanged="gvVeicoliTrovati_SelectedIndexChanged" ID="gvVeicoliTrovati" CssClass="table table table-bordered table-hover table-striped no-margin" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="Id" Visible="False">
                    <Columns>
                        <asp:BoundField DataField="Marca" HeaderText="Marca">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Modello" HeaderText="Modello">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DataImmatricolazione" HeaderText="Data Immatricolazione" DataFormatString="{0:dd/MM/yyyy}">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="IsDisponibile" HeaderText="Disponibile">
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
