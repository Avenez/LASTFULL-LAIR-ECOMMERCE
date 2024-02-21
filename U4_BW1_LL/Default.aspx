<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="U4_BW1_LL._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="container">
            <div id="carouselExampleIndicators" class="carousel slide mb-5">
                <div class="carousel-indicators">
                    <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                    <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="1" aria-label="Slide 2"></button>
                    <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="2" aria-label="Slide 3"></button>
                </div>
                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <img src=".\assets\imgs\caros1.jpg" class="d-block w-100" alt="img1">
                    </div>
                    <div class="carousel-item">
                        <img src=".\assets\imgs\caros2.jpg" class="d-block w-100" alt="img2">
                    </div>
                    <div class="carousel-item">
                        <img src=".\assets\imgs\caros3.jpg" class="d-block w-100" alt="img3">
                    </div>
                </div>
                <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Previous</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Next</span>
                </button>
            </div>

            <h1 class="display-4 sexyPink2 sexyBorderTop pt-5 mb-4">Hot Products</h1>
            <div class="row row-cols-sm-1 row-cols-md-2 row-cols-lg-3 row-cols-xl-4 gy-3">
                <asp:Repeater ID="ProductsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <a href="Dettagli.aspx?IDProdotto=<%# Eval("IDProdotto")  %>" class="text-decoration-none">
                                <div class="card sexyCardBg bg-opacity-25 border border-0 rounded shadow sexyZoom">
                                    <div class="d-flex justify-content-around overflow-hidden">
                                        <img class="card-img-top" src="<%# Eval("ImgUrl") %>" alt="Card image cap" style="height: 350px; width: auto">
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
