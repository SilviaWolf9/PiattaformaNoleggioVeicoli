<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClienteControl.ascx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.Controls.ClienteControl" %>

<div class="form-group">
    <label for="txtCognome">Cognome</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtCognome" />
</div>

<div class="form-group">
    <label for="txtNome">Nome</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtNome"></asp:TextBox>
</div>

<div class="form-group">
    <label for="clDataNascita">Data Nascita</label>
    <asp:Calendar runat="server" ID="clDataNascita" SelectionMode="Day">
        <OtherMonthDayStyle ForeColor="LightGray" />
        <DayStyle BackColor="White" />
        <TitleStyle CssClass="text-capitalize" Font-Size="15px" Font-Bold="true" />
        <SelectedDayStyle BackColor="DodgerBlue" Font-Bold="true" />
    </asp:Calendar>
</div>

<div class="form-group">
    <label for="txtCodiceFiscale">Codice Fiscale</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtCodiceFiscale"></asp:TextBox>
</div>

<div class="form-group">
    <label for="txtPatente">Patente</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtPatente"></asp:TextBox>
</div>

<div class="form-group">
    <label for="txtTelefono">Telefono</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefono"></asp:TextBox>
</div>

<div class="form-group">
    <label for="txtEmail">Email</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail"></asp:TextBox>
</div>

<div class="form-group">
    <label for="txtIndirizzo">Indirizzo</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtIndirizzo"></asp:TextBox>
</div>

<div class="form-group">
    <label for="txtNumeroCivico">Numero Civico</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtNumeroCivico"></asp:TextBox>
</div>

<div class="form-group">
    <label for="txtCap">Cap</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtCap"></asp:TextBox>
</div>

<div class="form-group">
    <label for="txtCitta">Città</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtCitta"></asp:TextBox>
</div>

<div class="form-group">
    <label for="txtComune">Comune</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtComune"></asp:TextBox>
</div>

<div class="form-group">
    <label for="txtNazione">Nazione</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtNazione"></asp:TextBox>
</div>

<div class="form-group">
    <label for="txtNote">Note</label>
    <asp:TextBox runat="server" CssClass="form-control" ID="txtNote" TextMode="MultiLine" Rows="5"></asp:TextBox>
</div>

