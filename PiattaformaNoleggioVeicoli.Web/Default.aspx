<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <body>
        <div class="jumbotron" style="opacity: 0.7">
            <h1>PIATTAFORMA NOLEGGIO VEICOLI</h1>
            <p class="lead">Questa piattaforma ti permette di gestire i noleggi dei tuoi veicoli.</p>
        </div>

        <div class="row">
            <div class="col-md-6">
                <asp:ImageButton ID="btnInserisciVeicolo" runat="server" ImageUrl="../sfondi/autoNera.png" href="~/InserimentoVeicolo" CssClass="col-md-4" Style="opacity: 1" />
                <h2>Inserisci veicolo</h2>
            </div>
            <div class="col-md-6">
                <asp:ImageButton ID="btnRicercaVeicolo" runat="server" ImageUrl="../sfondi/lenteAuto.png" href="~/RicercaVeicolo" CssClass="col-md-4" Style="opacity: 1" />
                <h2>Cerca veicolo</h2>
            </div>
            <div class="col-md-6">
                <asp:ImageButton ID="btnRicercaCliente" runat="server" ImageUrl="../sfondi/ricercaCliente.png" href="~/RicercaCliente" CssClass="col-md-4" Style="opacity: 1" />
                <h2>Cerca cliente</h2>
            </div>
            <div class="col-md-6">
                <asp:ImageButton ID="btnRicercaNoleggio" runat="server" ImageUrl="../sfondi/ricercaNoleggio.png" href="~/ricercaNoleggio" CssClass="col-md-4" Style="opacity: 1" />
                <h2>Cerca noleggio</h2>
            </div>
        </div>
    </body>

    <footer <%--class="col-md-12"--%>>
        <%--<p class="col-md-3" align="right" margin="1px" style="font: bolder; font-size: 18px">prodotto da Silvia Moretti</p>
        <asp:ImageButton class="col-md-1" align="left" ID="btnLinkMio" runat="server" ImageUrl="../sfondi/wolf4.png" href="http://linkedin.com/in/silvia-moretti-797405157"></asp:ImageButton>
        <p class="col-md-8" align="right" style="font: bolder">&copy; <%: DateTime.Now.Year %> - Applicazione ASP.NET</p>--%>
    </footer>
</asp:Content>

