<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="U4_BW1_LL._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <div class="container">
            <div class="row row-cols-4 gy-3">
                <asp:Repeater ID="ProductsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card border border-0 rounded shadow">
                                <img class="card-img-top" src="<%# Eval("ImgUrl") %>" alt="Card image cap" style="mix-blend-mode: normal">
                                <div class="card-body sexyPink rounded-bottom" style="background-color: black; height: 100px">
                                    <h5 class="card-title text-truncate"><%# Eval("Nome") %></h5>
                                    <p class="card-text font-monospace"><%# Eval("Prezzo", "{0:c2}")%></p>
                                    <a href="Dettagli.aspx?IDProdotto=<%# Eval("IDProdotto") %>"" class="btn btn-primary">Dettaglio</a>
                                   <!-- <div class="d-flex align-items-center">
                                        <a href="#" class="btn sexyPinkBg me-2">Acquista</a>
                                        <input type="number" id="quantity" name="quantity" value="0" min="1" max="<%# Eval("Qta") %>" step="1" class="quantityInput">
                                    </div> -->
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </main>

</asp:Content>
