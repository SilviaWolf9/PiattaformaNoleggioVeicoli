<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VeicoloControl.ascx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.Controls.VeicoloControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<div class="form-group col-md-6">
    <label for="ddlMarca">* Marca</label>
    <%--    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMarca" />--%>
    <div>
        <cc1:ComboBox ID="ddlMarca" runat="server" MaxLength="0" CssClass="text-capitalize" DropDownStyle="DropDownList" AutoCompleteMode="SuggestAppend" AutoPostBack="True" CaseSensitive="False"></cc1:ComboBox>
    </div>
</div>

<div class="form-group col-md-6">
    <label for="txtModello">* Modello</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtModello"></asp:TextBox>
</div>

<div class="form-group col-md-6">
    <label for="txtTarga">* Targa</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtTarga" AutoPostBack="true" OnTextChanged="txtTarga_TextChanged"></asp:TextBox>
</div>

<div class="form-group col-md-6">
    <label for="ddlTipoAlimentazione">* Tipo Alimentazione</label>
    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTipoAlimentazione" />
</div>

<div class="form-group col-md-6">
    <label for="clDataImmatricolazione">* Data Immatricolazione</label>
    <asp:Calendar runat="server" ID="clDataImmatricolazione" SelectionMode="Day">
        <OtherMonthDayStyle ForeColor="LightGray" />
        <DayStyle BackColor="White" />
        <TitleStyle CssClass="text-capitalize" Font-Size="15px" Font-Bold="true" BackColor="LightSeaGreen" />
        <SelectedDayStyle BackColor="LightSeaGreen" Font-Bold="true" />
    </asp:Calendar>
</div>

<div class="form-group col-md-6">
    <label for="txtNote">Note</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtNote" TextMode="MultiLine" Rows="6" Style="resize: none"></asp:TextBox>
</div>

<div class="form-group col-md-12" id="statoVeicolo">
    <label for="rbtnStatoVeicolo">Stato veicolo:</label>
    <asp:RadioButtonList ID="rbtDisponibilita" runat="server" AutoPostBack="true">
        <asp:ListItem Text="Disponibile" Value="1"></asp:ListItem>
        <asp:ListItem Text="Noleggiato" Value="0"></asp:ListItem>
    </asp:RadioButtonList>
</div>
<div class="form-group col-md-12" align="right">
    <p>
        <label>[ i campi contrassegnati con * sono obbligatori ]</label>
    </p>
</div>






