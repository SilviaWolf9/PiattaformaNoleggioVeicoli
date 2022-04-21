<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RicercaNoleggio.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.RicercaNoleggio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title">Ricerca Noleggio</h1>
        </div>

        <div class="panel-body">

            <%--<div class="form-group col-md-4">
                <label>Marca </label>
                <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-control" />
            </div>

            <div class="form-group col-md-4">
                <label>Modello</label>
                <asp:TextBox runat="server" ID="txtModello" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <label>Targa</label>
                <asp:TextBox runat="server" ID="txtTarga" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <label>Inizio Data Immatricolazione</label>
                <asp:Calendar runat="server" ID="cldInizio" SelectionMode="Day">
                    <OtherMonthDayStyle ForeColor="LightGray"></OtherMonthDayStyle>
                    <TitleStyle CssClass="text-capitalize" Font-Size="15px" Font-Bold="True" />
                    <DayStyle BackColor="white" />
                    <SelectedDayStyle BackColor="Aquamarine" Font-Bold="True" />
                </asp:Calendar>
            </div>

            <div class="form-group col-md-4">
                <label>Fine Data Immatricolazione</label>
                <asp:Calendar runat="server" ID="cldFine" SelectionMode="Day">
                    <OtherMonthDayStyle ForeColor="LightGray"></OtherMonthDayStyle>
                    <TitleStyle CssClass="text-capitalize" Font-Size="15px" Font-Bold="True" />
                    <DayStyle BackColor="white" />
                    <SelectedDayStyle BackColor="Aquamarine" Font-Bold="True" />
                </asp:Calendar>
            </div>

            <div class="form-group col-md-4">
                <label for="rbtnStatoVeicolo">Stato veicolo:</label>
                <asp:DropDownList ID="ddlStatoVeicolo" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="form-group col-md-12">
                <div align="left" class="col-md-6">
                    <asp:Button runat="server" ID="btnRicerca" CssClass="btn btn-info" OnClick="btnRicerca_Click" Text="Ricerca" />
                </div>

                <div align="left" class="col-md-6">
                    <asp:Button runat="server" ID="btnReset" CssClass="btn btn-warning" OnClick="btnReset_Click" Text="Reset" />
                </div>
            </div>

        </div>

    </div>

    <asp:GridView runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvVeicoliTrovati_PageIndexChanging" OnSelectedIndexChanged="gvVeicoliTrovati_SelectedIndexChanged" ID="gvVeicoliTrovati" CssClass="table table table-bordered table-hover table-striped no-margin" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="Id" Visible="False">
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
    </asp:GridView>--%>

</asp:Content>
