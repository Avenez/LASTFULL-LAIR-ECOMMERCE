<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Backoffice.aspx.cs" Inherits="U4_BW1_LL.Backoffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">

        <div class="row">
            <h3 class="text-white" id="controllo" runat="server" ></h3>
            <div class="col">
                <div class="d-flex">
                    <div class="input-group mb-3">
                        <asp:Button CssClass="btn btn-outline-light" ID="Button2" runat="server" Text="All" OnClick="ButtonAll_Click" />


                        <select id="SearchType" runat="server" class="form-select">
                            <option selected>Choose...</option>
                            <option value="IDProdotto">Id</option>
                            <option value="Nome">Nome</option>
                            <option value="Prezzo">Prezzo</option>
                        </select>
                        <input type="text" id="SearchKey" runat="server" class="form-control" placeholder="" aria-label="Example text with two button addons">
                        <asp:Button CssClass="btn btn-outline-light" ID="Button1" runat="server" Text="Cerca" OnClick="SearchButton_Click" />

                    </div>
                    <div>
                        <asp:Button ID="BTNvaiAOrdiniUtente" runat="server" Text="Controlla Ordini Utenti " CssClass="btn sexyBtnOutline2"  OnClick="BTNvaiAOrdiniUtente_Click"/>
                    </div>
                </div>
                
            </div>
        </div>

        <div class="row mb-5">
            <div class="Col">
                <div>
                    <div class="row row-cols-3 text-white">

                        <div class="mb-3 col">
                            <label for="FormName" class="form-label">Nome</label>
                            <input type="text" class="form-control" id="FormName" runat="server" aria-describedby="LabelNome">
                        </div>
                        <div class="mb-3 col">
                            <label for="FormDescrizione" class="form-label">Descrizione</label>
                            <input type="text" class="form-control" id="FormDescrizione" runat="server">
                        </div>

                        <div class="mb-3 col">
                            <label for="FormImg" class="form-label">ImgUrl</label>
                            <input type="text" class="form-control" id="FormImg" runat="server">
                        </div>

                        <div class="mb-3  col">
                            <label for="FormPrezzo" class="form-label">Prezzo</label>
                            <input type="text" class="form-control" id="FormPrezzo" runat="server">
                        </div>

                        <div class="mb-3 col">
                            <label for="FormQta" class="form-label">Qta</label>
                            <input type="number" class="form-control" id="FormQta" runat="server" />
                        </div>

                        <div class="mb-3 col">
                            <label for="FormQta" class="form-label">ID</label>
                            <h5 class="mt-2" id="FormId" runat="server"></h5>
                        </div>
                    </div>

                    <asp:Button CssClass="btn btn-outline-warning"  ID="ReturnButton" runat="server" Text="<" Visible="false" OnClick="ReturnButton_Click"/>
                    <asp:Button class="btn btn-dark" ID="SubmitChageButton2" runat="server" Text="Invia Modifiche" OnClick="SubmitChageButton_Click" Visible="false" />
                    <asp:Button id="DeleteButton" CssClass="btn btn-outline-danger" runat="server" Text="Elimina Prodotto" OnClick="DeleteButton_Click" Visible="false"/>
                    <asp:Button ID="AddButton" CssClass="btn btn-outline-warning" runat="server" Text="Aggiungi Prodotto" OnClick="AddButton_Click" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col">
                <asp:Repeater ID="BackOfficeProductsRepaeter" runat="server">
                    <ItemTemplate>
                        <div class="row  mb-1  p-2 text-white row-cols-sm-3  ">
                            <div class="col-2   border  border-2">
                                <h3>ID:</h3>
                                <h4 class="text-truncate"><%#Eval("IDProdotto") %></h4>
                            </div>
                            <div class="col-10   border  border-2 ">
                                <h3>Nome:</h3>
                                <h4 class="text-truncate"><%#Eval("Nome") %></h4>
                            </div>
                            <div class="col-6 border  border-2">
                                <h3>Descrizione:</h3>
                                <h4 class="text-truncate"><%#Eval("Descrizione") %></h4>
                            </div>
                            <div class="col-6  border  border-2 ">
                                <!--<img src="<%#Eval("imgurl") %>" alt="sextoy" /> -->
                                <h3>ImgUrl:</h3>
                                <h4 class="text-truncate"><%#Eval("ImgUrl") %></h4>
                            </div>
                            <div class="col-6  border  border-2">
                                <h3>Prezzo:</h3>
                                <h4 class="text-truncate"><%#Eval("Prezzo", "{0:c2}") %></h4>
                            </div>
                            <div class="col-6  border  border-2">
                                <h3>Qta:</h3>
                                <h4 class="text-truncate "><%#Eval("Qta") %></h4>
                            </div>
                        </div>
                        <div class="row mb-3">
                            <div class="col-12">
                                <asp:Button CssClass="btn btn-outline-light w-100 ms-0" ID="modifyProduct" CommandArgument='<%#Eval("Nome") + "*" + Eval("Descrizione") + "*" + Eval("ImgUrl") + "*" + Eval("Prezzo") + "*" + Eval("Qta") + "*" + Eval("IDProdotto") %>  ' Text="Modifica Prodotto" runat="server" OnClick="modifyProduct_Click" />
                            </div>
                        </div>

                    </ItemTemplate>

                </asp:Repeater>

            </div>
        </div>

    </div>



</asp:Content>
