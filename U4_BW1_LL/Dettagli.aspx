<%@ Page Title="Dettaglio Prodotto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dettagli.aspx.cs" Inherits="U4_BW1_LL.Dettagli" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="container">
            <div class="d-flex" style="height: 500px">
                <div class="me-5 h-100" >
                    <asp:Image ID="imgProdotto" runat="server" CssClass="h-100 rounded" />
                </div>
                <div class="d-flex flex-column justify-content-between">
                    <div>
                        <h2 class="h1 sexyPinkDet">
                            <asp:Label ID="lblNomeProdotto" runat="server" /></h2>
                        <p class="sexyPinkDet"><strong>Descrizione:</strong>
                            <asp:Label ID="lblDescrizioneProdotto" runat="server" class="sexyPinkDet" /></p>
                    </div>
                    <div>

                    <p class="sexyPinkDet font-monospace mb-0"><strong>Prezzo (1pz):</strong>
                        <asp:Label ID="lblPrezzoProdotto" runat="server" CssClass="sexyPinkDet" /></p>
                    <div class="d-flex align-items-center">
                        <div>

                        </div>
                        <a href="#" class="btn sexyPinkBg me-2">Acquista</a>
                        <input type="number" id="quantity" name="quantity" value="0" min="1" max="<%# Eval("Qta") %>" step="1" class="quantityInput">
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>