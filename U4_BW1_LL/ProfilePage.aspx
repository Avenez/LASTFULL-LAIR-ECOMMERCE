<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="U4_BW1_LL.ProfilePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="text-light">
        <div class="container">
            <div class="row">
                <%-- aside con riepilogo ordini settings e backoff --%>
                <div class="col-4">
                    <div class=" vh-100 d-flex flex-column justify-content-around align-items-start">
                        
                        <asp:Button ID="btnRiepilogoOrdini" Text="Riepilogo Ordini" runat="server"  CssClass="btn btn-transparent fs-3 text-light"/>
                        <asp:Button ID="btnSettings" Text="Settings" runat="server"  CssClass="btn btn-transparent fs-3 text-light" OnClick="SettingsClick"/>
                        <asp:Button OnClick="BtnBackOffice_Click" ID="BtnBackOffice" Text="BackOffice" runat="server"  CssClass="btn btn-transparent fs-3 text-light"/>
                    </div>
                </div>
                <%-- aside con dettagli dell utente loggato  --%>
                <div class="col-8">

                    <%-- div mostrato al caricamento della pagina con nome immagine dell utente  --%>
                    <div id="infoAlCaricamento" runat="server" class="d-flex justify-content-center align-items-center flex-column vh-100 d-block">

                        <asp:Image ID="ImmagineProfilo" runat="server" CssClass="fotoDimensions" />
                        <div class="mt-3">
                            <h3 runat="server" id="nomeProfilo" class="fw-normal"></h3>
                        </div>
                        <div runat="server" id="divCambiaURL" class="d-flex flex-column  mt-4 text-center">
                            <asp:Label ID="Label4" runat="server" Text="Label">Inserisci URL nuova immagine:</asp:Label>
                            <asp:TextBox CssClass="w-100" ID="TextBoxURLImmagine" runat="server"></asp:TextBox>
                        </div>
                       <button id="buttonApriModale" runat="server" type="button" class="btn btn-outline-warning fs-3 mt-3" data-bs-toggle="modal" data-bs-target="#staticBackdrop">Modifica Immagine</button>
                        <div class="mt-3 fs-3" id="messaggio_Errore" runat="server">
                            <p runat="server" id="urlNonValido"></p>
                        </div>
                    </div>
                    

                    <div class="vh-100 justify-content-center align-items-center d-flex gap-3" id="sceglicosaCambiare" runat="server">
                        <asp:Button CssClass="btn btn-outline-warning fs-3" Text="cambia foto profilo" runat="server" OnClick="makeDivChangeImgVisible_Click" />
                        <asp:Button CssClass="btn btn-outline-warning fs-3" Text="cambia nome Utente" runat="server"  OnClick="makeDivChangeNomeVisible"/>
                    </div>

                    <!-- Modal -->
                    <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h1 class="modal-title fs-5 text-black" id="staticBackdropLabel">Vuoi davvero cambiare l'ìmmagine del profilo?</h1>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                  <p class="text-black"> Queste modifiche non potranno essere annullate</p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Chiudi</button>
                                    <asp:Button OnClick="Cambia_ImmagineProfilo" class="btn btn-primary" ID="Button1" runat="server" Text="Si" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div runat="server" id="divInsertNomePassword" class="d-flex flex-column vh-100 align-items-center justify-content-center">

                        <asp:Label CssClass="fs-5 mt-3" ID="Label1" runat="server" Text="Label">inserisci vecchio nome Utente</asp:Label>
                        <asp:TextBox CssClass="w-50" ID="textBoxVecchioNomeUtente" runat="server"></asp:TextBox>
                        <asp:Label CssClass="fs-5 mt-3" ID="Label2" runat="server" Text="Label">inserisci Password </asp:Label>
                        <asp:TextBox CssClass="w-50" ID="textBoxPassword" runat="server"></asp:TextBox>

                        <asp:Button ID="btnconfermaNomePassword" runat="server" Text="Invia" CssClass="btn btn-outline-warning mt-2" OnClick="btnconfermaNomePassword_Click" />

                        <div id="alertInserisciDati" runat="server">
                            <p runat="server" id="feedbackalert"></p>
                        </div>
                    </div>

                    <div id="divFinaleCambioNome" class="d-flex flex-column vh-100 justify-content-center align-items-center" runat="server" >
                         <asp:Label CssClass="fs-5 mt-3" ID="Label3" runat="server" Text="Label">inserisci Nuovo nome utente </asp:Label>                      
                        <asp:TextBox CssClass="w-50" ID="TxtNuovoNome" runat="server"></asp:TextBox>
                        <asp:Button CssClass="btn btn-warning mt-2" ID="Button2" runat="server" Text="Modifica"  OnClick="FinalNameChange_click" />
                    </div>
                    
                </div>
            </div>
        </div>
    </main>
</asp:Content>
