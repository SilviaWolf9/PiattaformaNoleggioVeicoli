<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="jumbotron">
        <h1>PIATTAFORMA NOLEGGIO VEICOLI</h1>
        <p class="lead">Questa piattaforma ti permette di gestire i noleggi dei tuoi veicoli.</p>
    </div>

    <div class="col-md-12 paginaDefault">
        <div class="col-md-3">
            <a class="collegamenti" href="~/InserimentoVeicolo">
                <img class="col-md-12" src="../sfondi/IconaInserisciVeicolo.png" />
                <h2>Inserisci veicolo</h2>
            </a>
        </div>
        <div class="col-md-3">
            <a class="collegamenti" href="~/RicercaVeicolo">
                <img class="col-md-12" src="../sfondi/IconaRicercaVeicolo.png" />
                <h2>Cerca veicolo</h2>
            </a>
        </div>
        <div class="col-md-3">
            <a class="collegamenti" href="~/RicercaCliente">
                <img class="col-md-12" src="../sfondi/IconaRicercaCliente.png" />
                <h2>Cerca cliente</h2>
            </a>
        </div>
        <div class="col-md-3">
            <a class="collegamenti" href="~/RicercaNoleggio">
                <img class="col-md-12" src="../sfondi/IconaRicercaNoleggio.png" />
                <h2>Cerca noleggio</h2>
            </a>
        </div>        
    </div>
</asp:Content>

