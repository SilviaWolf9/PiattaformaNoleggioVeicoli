<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NoleggioControl.ascx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.Controls.NoleggioControl" %>

<div class="panel-body">
            <div class="form-group col-md-12" runat="server" style="opacity:1">
                <div class="form-group col-md-4" style="opacity:1">
                    <label>Marca</label>
                    <asp:Label runat="server" ID="lblMarca" CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group col-md-4" style="opacity:1">
                    <label>Modello</label>
                    <asp:Label runat="server" ID="lblModello" CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group col-md-4" style="opacity:1">
                    <label>Targa</label>
                    <asp:Label runat="server" ID="lblTarga" CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group col-md-4" style="opacity:1">
                    <label>In corso: </label>
                    <asp:Label runat="server" ID="lblIsInCorso" CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group col-md-4" style="opacity:1">
                    <label>Data di Inizio Noleggio</label>
                    <asp:Label runat="server" ID="lblDataInizio" CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group col-md-4" style="opacity:1">
                    <label>Data di Fine Noleggio</label>
                    <asp:Label runat="server" ID="lblDataFine" CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group col-md-4" style="opacity:1">
                    <label>Cognome</label>
                    <asp:Label runat="server" ID="lblCognome" CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group col-md-4" style="opacity:1">
                    <label>Nome</label>
                    <asp:Label runat="server" ID="lblNome" CssClass="form-control"></asp:Label>
                </div>
                <div class="form-group col-md-4" style="opacity:1">
                    <label>Codice Fiscale</label>
                    <asp:Label runat="server" ID="lblCodiceFiscale" CssClass="form-control"></asp:Label>
                </div>                
            </div>
        </div>
        