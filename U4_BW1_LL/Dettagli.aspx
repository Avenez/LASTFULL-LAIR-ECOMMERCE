<%@ Page Title="Dettaglio Prodotto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dettagli.aspx.cs" Inherits="U4_BW1_LL.Dettagli" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main style="background-color: #FFACD2" class="p-5">
        <div class="container">
            <div class="d-sm-inline-block d-lg-flex row-cols-sm-1 row-cols-lg-2">
                <div class="me-5 h-100 col">
                    <asp:Image ID="imgProdotto" runat="server" CssClass="rounded img-fluid" />
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

                        <p class="sexyPink font-monospace mb-0 mt-5">
                            <strong>Prezzo (1pz):</strong>
                            <asp:Label ID="lblPrezzoProdotto" runat="server" CssClass="sexyPink" />
                        </p>
                        <div class="d-flex align-items-center">
                            <div>
                            </div>
                            <asp:Button ID="ButtonAddToCart" runat="server" Text="Aggiungi" class="btn btn-outline-dark sexyPinkBg me-2" OnClick="AddToCart" />
                            <%--<input type="number" id="quantity" name="quantity" value="0" min="1" max="<%# Eval("Qta") %>" step="1" class="quantityInput">--%>
                            <asp:TextBox runat="server" TextMode="Number" ID="selectedQuantity" value="0" min="1" class="quantityInput" />
                        </div>
                    </div>
                </div>
            </div>

            <section runat="server" id="sectionalertAddTocart" class="mt-4">
                <div class="row">
                    <div class="col-12">
                        <div class="p-3 d-flex justify-content-center w-50 m-auto sexyPinkBg rounded-4">
                            <h4 class="m-auto" runat="server" id="alertProdottoAggiunto">Prodotto Aggiunto al Carrello... Mascalzone...</h4>
                        </div>
                    </div>
                </div>
            </section>
      </div>
        
    </main>
</asp:Content>
   