<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InserimentoVeicolo.aspx.cs" Inherits="PiattaformaNoleggioVeicoli.Web.InserimentoVeicolo" %>

<%@ Register Src="~/Controls/InfoControl.ascx" TagPrefix="ic" TagName="Info" %>

<%@ Register Src="~/Controls/VeicoloControl.ascx" TagPrefix="vc" TagName="Veicolo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title" style="font:900">Inserisci Veicolo</h3>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <ic:Info runat="server" ID="infoControl" />
                <div class="panel-body">
                    <vc:Veicolo runat="server" ID="veicoloControl" OnEsistenzaTarga="veicoloControl_EsistenzaTarga" />
                </div>                                                  
                <div class="panel-footer col-md-12" align="center">
                    <div align="center" class="col-md-6">
                        <asp:Button runat="server" ID="btnInserisci" Text="Inserisci" CssClass="btn" BackColor="LightBlue" BorderColor="LightBlue" BorderWidth="2px" OnClick="btnInserisci_Click" />
                    </div>
                    <div align="center" class="col-md-6">
                        <asp:Button runat="server" ID="btnReset" CssClass="btn" BorderColor="LightBlue" BorderWidth="2px" OnClick="btnReset_Click" Text="Reset" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
