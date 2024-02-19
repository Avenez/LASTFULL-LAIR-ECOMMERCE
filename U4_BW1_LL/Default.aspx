<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="U4_BW1_LL._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main >
        <div class="container">
            <div class="row row-cols-4">
                <asp:Repeater ID="ProductsRepeater" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card">
                                <img class="card-img-top" src="<%# Eval("ImgUrl") %>" alt="Card image cap" style="mix-blend-mode: normal">
                                <div class="card-body" style="background-color: black; color: rgba(236,19,149,255)">
                                    <h5 class="card-title"><%# Eval("Nome") %></h5>
                                    <p class="card-text"><%# Eval("Prezzo", "{0:c2}")%></p>
                                    <a href="Dettagli.aspx?productId=<%# Eval("IDProdotto") %>"" class="btn btn-primary">Dettaglio</a>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </main>

</asp:Content>
