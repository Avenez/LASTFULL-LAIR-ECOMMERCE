<%@ Page Title="Dettaglio Prodotto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dettagli.aspx.cs" Inherits="U4_BW1_LL.Dettagli" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main style="background-color: #FFACD2" class="p-5">
        <div class="container">
            <div class="d-sm-inline-block d-lg-flex row-cols-sm-1 row-cols-lg-2">
                <div class="me-5 h-100 col">
                    <asp:Image ID="imgProdotto" runat="server" CssClass="rounded img-fluid w-100" />
                </div>
                <div class="d-flex flex-column justify-content-between col">
                    <div>
                        <h2 class="h1 sexyPink">
                            <asp:Label ID="lblNomeProdotto" runat="server" /></h2>

                        <div class="sexyPink" style="height: 350px;">
                            <strong>Descrizione:</strong>
                            <p id="lblDescrizioneProdotto" runat="server" class="sexyPink h-100 scrollPersonalize" style="overflow-x: clip; overflow-y: auto"></p>
                        </div>

                    </div>
                    <div class="mt-2">


                        <p class="sexyPink font-monospace mb-0">
                            <strong>Prezzo (1pz):</strong>
                            <asp:Label ID="lblPrezzoProdotto" runat="server" CssClass="sexyPink" />
                        </p>
                        <h6 id="noStock" runat="server" class=" mt-2">Il prodotto che stai cercando è momentaneamente non disponibile</h6>
                        <div id="dettagliAquisto" runat="server" class="d-flex align-items-center">
                            <div>
                            </div>
                            <asp:Button ID="ButtonAddToCart" runat="server" Text="Aggiungi" class="btn btn-outline-dark sexyPinkBg me-2" OnClick="AddToCart" />
                            <asp:TextBox runat="server" TextMode="Number" ID="selectedQuantity" value="1" min="1" class="quantityInput me-5" />
                            <div runat="server" id="sectionalertAddTocart">
                                <div class="rounded px-2 py-1" style="background-color: #f565a7">
                                    <p class="m-auto" runat="server" id="alertProdottoAggiunto">Aggiunto al carrello</p>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
