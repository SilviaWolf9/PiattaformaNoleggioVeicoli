<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="jumbotron">
        <h1>PIATTAFORMA NOLEGGIO VEICOLI</h1>
        <p class="lead">Questa piattaforma ti permette di gestire i noleggi dei tuoi veicoli.</p>
    </div>

    <div class="col-md-12 paginaDefault">
        <div class="col-md-3">
            <a class="collegamenti" href="InserimentoVeicolo.aspx">
                <img class="col-md-12" src="../sfondi/IconaInserisciVeicolo2.png" />
                <h2 align="center">Inserisci veicolo</h2>
            </a>
        </div>
        <div class="col-md-3">
            <a class="collegamenti" href="RicercaVeicolo.aspx">
                <img class="col-md-12" src="../sfondi/IconaRicercaVeicolo.png" />
                <h2 align="center">Cerca veicolo</h2>
            </a>
        </div>
        <div class="col-md-3">
            <a class="collegamenti" href="RicercaCliente.aspx">
                <img class="col-md-12" src="../sfondi/IconaRicercaCliente.png" />
                <h2 align="center">Cerca cliente</h2>
            </a>
        </div>
        <div class="col-md-3">
            <a class="collegamenti" href="RicercaNoleggio.aspx">
                <img class="col-md-12" src="../sfondi/IconaRicercaNoleggio.png" />
                <h2 align="center">Cerca noleggio</h2>
            </a>
        </div>  
    </div>

</asp:Content>

