<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="U4_BW1_LL.ProfilePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="text-light">
        <div class="container">
            <div class="row">
                <%-- aside con riepilogo ordini settings e backoff --%>
                <div class="col-3">
                    <div class="sexyStickyCol sexyBorderRight siteMinHeight2 d-flex flex-column justify-content-between align-items-start ">
                        <div class="w-100 d-flex flex-column align-items-start pb-2 sexyBorderBottom">
                            <asp:Button ID="btnSettings" Text="Settings" runat="server" CssClass="btn btn-transparent sexyPink2 fs-5" OnClick="SettingsClick" />
                            <asp:Button ID="btnRiepilogoOrdini" Text="Riepilogo Ordini" runat="server" CssClass="btn btn-transparent sexyPink2 fs-5" OnClick="ShowOrders" />
                        </div>
                        <asp:Button OnClick="BtnBackOffice_Click" ID="BtnBackOffice" Text="BackOffice" runat="server" CssClass="btn btn-transparent sexyPink2 fs-5" />
                    </div>
                </div>
                <%-- aside con dettagli dell utente loggato  --%>
                <div class="col">
                    <%-- div mostrato al caricamento della pagina con nome immagine dell utente  --%>
                    <div id="infoAlCaricamento" runat="server" class="d-flex flex-column px-3">

                        <div class="d-flex flex-column align-items-center mb-5">
                            <asp:Image ID="ImmagineProfilo" runat="server" CssClass="profilePic mb-2" />
                            <asp:LinkButton ID="changePropic" runat="server" class="btn sexyBtnOutline" title="modificaImmagine" OnClick="showChangeImg" OnClientClick="switchBUttons();"><i class="bi bi-pencil sexyPink2"></i></asp:LinkButton>
                            <div runat="server" id="divCambiaURL" class="d-flex flex-column text-center">
                                <div>
                                    <div class="mb-2">
                                        <asp:Label ID="Label4" runat="server" Text="Label">Inserisci URL nuova immagine:</asp:Label>
                                        <asp:TextBox CssClass="w-100" ID="TextBoxURLImmagine" runat="server"></asp:TextBox>
                                    </div>
                                    <asp:Button ID="Button3" runat="server" Text="Modifica immagine" CssClass="btn sexyBtnOutline2" OnClick="Cambia_ImmagineProfilo" />
                                </div>
                            </div>
                            <div class="sexyPinkBg2 rounded px-3 py-2 mt-3" id="messaggio_Errore" runat="server">
                                <p class="mb-0" runat="server" id="urlNonValido"></p>
                            </div>
                        </div>


                        <div class="d-flex flex-column">
                            <div class="d-flex align-items-center">
                                <p runat="server" id="nomeProfilo" class="display-5 sexyPink2 me-2 mb-0"></p>
                                <small>
                                    <asp:LinkButton ID="changeName" runat="server" class="btn sexyBtnOutline" title="modificaImmagine" OnClientClick="switchBUttons();" OnClick="btnconfermaNomePassword_Click">
                                         <i class="bi bi-pencil sexyPink2"></i></asp:LinkButton>
                                </small>
                            </div>
                            <div class="d-flex  align-items-start mb-3 w-100">


                                <div runat="server" id="divInsertNomePassword" class="d-flex flex-column align-items-start justify-content-center">

                                    <asp:Label CssClass="fs-5 mt-3" ID="Label1" runat="server" Text="Label">inserisci vecchio nome Utente</asp:Label>
                                    <asp:TextBox CssClass="w-100" ID="textBoxVecchioNomeUtente" runat="server"></asp:TextBox>
                                    <asp:Label CssClass="fs-5 mt-3" ID="Label2" runat="server" Text="Label">inserisci Password </asp:Label>
                                    <asp:TextBox CssClass="w-100" TextMode="Password" ID="textBoxPassword" runat="server"></asp:TextBox>

                                    <asp:Button ID="btnconfermaNomePassword" runat="server" Text="Invia" CssClass="btn sexyBtnOutline2 mt-2" OnClick="btnconfermaNomePassword_Click" />

                                    <div id="alertInserisciDati" runat="server">
                                        <p runat="server" id="feedbackalert"></p>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="d-flex align-items-center mb-3">
                            <p runat="server" class="display-5 sexyPink2 me-2 mb-0">Cambio Password</p>
                            <small>
                                <asp:LinkButton ID="changePassword" runat="server" class="btn sexyBtnOutline" title="modificaImmagine" OnClientClick="switchBUttons();"  OnClick="cambiaPassword_Click">
                                      <i class="bi bi-pencil sexyPink2"></i></asp:LinkButton>
                            </small>
                        </div>

                        <div id="divCambiaPassword" runat="server" class="d-flex flex-column">
                            <div class="d-flex flex-column mb-2">
                                <asp:Label ID="labelInsertPassword" runat="server" Text="Label" CssClass="mb-1">Nuova password</asp:Label>
                                <asp:TextBox ID="insertPassword" runat="server"></asp:TextBox>
                            </div>
                            <div class="d-flex flex-column">
                                <asp:Label ID="label5" runat="server" Text="Label" CssClass="mb-1">Conferma nuova password</asp:Label>
                                <asp:TextBox ID="confirmPassword" runat="server"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnmodificaPassword" runat="server" Text="Modifica Password" class="btn sexyBtnOutline2 mt-2"  OnClick="ModificaPassword"/>
                            <div id="alertErroreCambiaPassword" runat="server">
                                <p runat="server" id="P1alertCambiaPassword"></p>
                            </div>
                        </div>

                    </div>                   

                    <div id="divFinaleCambioNome" class="d-flex flex-column justify-content-center align-items-start" runat="server">
                        <asp:Label CssClass="fs-5 mt-3" ID="Label3" runat="server" Text="Label">inserisci Nuovo nome utente </asp:Label>
                        <asp:TextBox CssClass="w-50" ID="TxtNuovoNome" runat="server"></asp:TextBox>
                        <asp:Button CssClass="btn sexyBtnOutline2 mt-2" ID="Button2" runat="server" Text="Modifica" OnClick="FinalNameChange_click" />
                    </div>

                    <div id="riepilogoOrdini" runat="server">
                        <div id="noOrder" runat="server" class="text-center sexyPink2 py-3" style="display: none">
                            <p>Non ci sono ordini da visualizzare.</p>
                        </div>
                        <div class="row row-cols-1">
                            <asp:Repeater ID="OrderRepeater" runat="server" OnItemDataBound="rptOrdini_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col">
                                        <div class="sexyBorder rounded p-2 mb-2">
                                            <asp:Repeater ID="OrderDetailsRepeater" runat="server">
                                                <ItemTemplate>
                                                    <div style="height: 90px" class="d-flex justify-content-between border border-dark sexyCardBg rounded">
                                                        <div class="h-100 d-flex">
                                                            <div class="d-flex justify-content-center h-100 p-2 me-3" style="width: 90px">
                                                                <img src='<%# Eval("ImgUrl") %>' class="h-100" alt='<%# Eval("Name") %>'>
                                                            </div>
                                                            <div class="d-flex flex-column justify-content-center py-2">
                                                                <h5 class="mb-0"><%# Eval("Name") %></h5>
                                                                <p class="mb-0">Qta: <%# Eval("Qta") %></p>
                                                            </div>
                                                        </div>
                                                        <div class="d-flex align-items-center me-3">
                                                            <p class="font-monospace mb-0">Prezzo: <%# Eval("Prezzo", "{0:c2}") %></p>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <div class="d-flex justify-content-between">
                                                <p class="mb-0">Ordine effettuato in data: <%# Eval("OrderDate") %></p>
                                                <p class="font-monospace mb-0">Prezzo totale: <%# Eval("TotalPrice", "{0:c2}") %></p>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
