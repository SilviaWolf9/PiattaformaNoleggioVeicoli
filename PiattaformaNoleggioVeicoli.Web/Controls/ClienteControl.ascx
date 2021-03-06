<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClienteControl.ascx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.Controls.ClienteControl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<div class="form-group col-md-4" >
    <label for="txtCognome">* Cognome</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtCognome" />
</div>

<div class="form-group col-md-4" >
    <label for="txtNome">* Nome</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtNome"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="clDataNascita">* Data Nascita</label>
    <div>
        <asp:TextBox ID="txtDataNascita" AutoPostBack="true" OnTextChanged="txtDataNascita_TextChanged" runat="server" CssClass="col-md-6" ></asp:TextBox>
        <asp:ImageButton ID="btnMostraCalendario" runat="server" ImageUrl="../sfondi/calendar_office_day_1474.png" CssClass="col-md-2" />
        <ajax:CalendarExtender ID="clndr" runat="server" Format="dd/MM/yyyy" PopupButtonID="btnMostraCalendario" TargetControlID="txtDataNascita"></ajax:CalendarExtender>
    </div>
</div>

<div class="form-group col-md-4" >
</div>
<div class="form-group col-md-4" >
    <label for="txtCodiceFiscale">* Codice Fiscale</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtCodiceFiscale" AutoPostBack="true" OnTextChanged="txtCodiceFiscale_TextChanged"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="txtPatente">* Patente</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtPatente"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="txtTelefono">* Telefono</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefono"></asp:TextBox>
</div>

<div class="form-group col-md-4">
    <label for="txtEmail">* Email</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="txtIndirizzo">* Indirizzo</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtIndirizzo"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="txtNumeroCivico">* Numero Civico</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtNumeroCivico"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="txtCap">* Cap</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtCap"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="txtCitta">* Città</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtCitta"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="txtComune">* Comune</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtComune"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="txtProvincia">* Provincia (sigla)</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtProvincia"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="txtNazione">* Nazione</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtNazione"></asp:TextBox>
</div>

<div class="form-group col-md-4" >
    <label for="txtNote">Note</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtNote" TextMode="MultiLine" Rows="5"></asp:TextBox>
</div>

<div class="form-group col-md-12" align="right">
    <p>
        <label>[ i campi contrassegnati con * sono obbligatori ]</label>
    </p>
</div>