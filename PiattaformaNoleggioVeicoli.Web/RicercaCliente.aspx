﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RicercaCliente.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.RicercaCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <div class="panel panel-default">
        <div class="panel-heading">
            <h1 class="panel-title">Ricerca Cliente</h1>
        </div>

        <div class="panel-body">

            

            <div class="form-group col-md-4">
                <label>Cognome</label>
                <asp:TextBox runat="server" ID="txtCognome" CssClass="form-control"></asp:TextBox>
            </div>

            <div class="form-group col-md-4">
                <label>Nome</label>
                <asp:TextBox runat="server" ID="txtNome" CssClass="form-control"></asp:TextBox>
            </div> 
            
             <div class="form-group col-md-4">
                <label>Codice Fiscale</label>
                <asp:TextBox runat="server" ID="txtCodiceFiscale" CssClass="form-control"></asp:TextBox>
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

    <asp:GridView runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvClientiTrovati_PageIndexChanging" OnSelectedIndexChanged="gvClientiTrovati_SelectedIndexChanged" ID="gvClientiTrovati" CssClass="table table table-bordered table-hover table-striped no-margin" AutoGenerateColumns="False" AutoGenerateSelectButton="True" DataKeyNames="Id" Visible="False">
        <Columns>
            <asp:BoundField DataField="Cognome" HeaderText="Cognome">
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Nome" HeaderText="Nome">
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="CodiceFiscale" HeaderText="Codice Fiscale">
                <HeaderStyle HorizontalAlign="Center" />
            </asp:BoundField>            
        </Columns>
    </asp:GridView>

</asp:Content>