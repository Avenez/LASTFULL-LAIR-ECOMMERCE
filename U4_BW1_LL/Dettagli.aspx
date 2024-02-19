<%@ Page Title="Dettaglio Prodotto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dettagli.aspx.cs" Inherits="U4_BW1_LL.Dettagli" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container">
            <div class="row">
                <asp:Image ID="imgProdotto" runat="server" CssClass="img-fluid w-50 mb-4" />
                <h2><asp:Label ID="lblNomeProdotto" runat="server" CssClass="text-dark" /></h2>
                <p><strong>Prezzo:</strong> <asp:Label ID="lblPrezzoProdotto" runat="server" CssClass="text-success" /></p>
                <p><strong>Descrizione:</strong> <asp:Label ID="lblDescrizioneProdotto" runat="server" CssClass="text-secondary" /></p>
                <asp:Button ID="btnTornaIndietro" runat="server" Text="Torna a Tutti i Prodotti" OnClick="btnTornaIndietro_Click" CssClass="btn btn-secondary" />
            </div>
        </div>
    </main>
</asp:Content>