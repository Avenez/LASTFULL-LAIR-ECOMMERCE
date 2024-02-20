<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="U4_BW1_LL._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="container">
            <h1 class="display-4 sexyPink2 mb-4">Hot Products</h1>
            <div class="row row-cols-sm-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 gy-3">
                <asp:Repeater ID="ProductsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <a href="Dettagli.aspx?IDProdotto=<%# Eval("IDProdotto")  %>" class="text-decoration-none">
                                <div class="card sexyCardBg bg-opacity-25 border border-0 rounded shadow">
                                    <div class="d-flex justify-content-around overflow-hidden">
                                        <img class="card-img-top sexyZoom" src="<%# Eval("ImgUrl") %>" alt="Card image cap" style="height: 350px; width: auto">
                                    </div>
                                    <div class="card-body sexyPink2 rounded-bottom" style="background-color: black; height: 100px">
                                        <h5 class="card-title text-truncate"><%# Eval("Nome") %></h5>
                                        <p class="card-text font-monospace"><%# Eval("Prezzo", "{0:c2}")%></p>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </main>

</asp:Content>
