<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VeicoloControl.ascx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.Controls.VeicoloControl" %>


<div class="form-group col-md-6">
    <label for="ddlMarca">Marca</label>
    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMarca" />
</div>

<div class="form-group col-md-6">
    <label for="txtModello">Modello</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtModello"></asp:TextBox>
</div>

<div class="form-group col-md-6">
    <label for="txtTarga">Targa</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtTarga"></asp:TextBox>
</div>

<div class="form-group col-md-6">
    <label for="ddlTipoAlimentazione">Tipo Alimentazione</label>
    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTipoAlimentazione" />
</div>

<div class="form-group col-md-6">
    <label for="clDataImmatricolazione">Data Immatricolazione</label>
    <asp:Calendar runat="server" ID="clDataImmatricolazione" SelectionMode="Day">
        <OtherMonthDayStyle ForeColor="LightGray" />
        <DayStyle BackColor="White" />
        <TitleStyle CssClass="text-capitalize" Font-Size="15px" Font-Bold="true" />
        <SelectedDayStyle BackColor="Aquamarine" Font-Bold="true" />
    </asp:Calendar>
</div>

<div class="form-group col-md-6">
    <label for="txtNote">Note</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtNote" TextMode="MultiLine" Rows="8" Style="resize:none"></asp:TextBox>
</div>

<div class="form-group col-md-12">
    <p>
        <label for="rbtnStatoVeicolo">Stato veicolo:</label>
        <asp:RadioButton ID="rbtDisponibile" Text="Disponibile" GroupName="statoVeicolo" runat="server" />        
    </p>
</div>


