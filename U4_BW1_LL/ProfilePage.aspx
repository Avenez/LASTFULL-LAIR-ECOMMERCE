<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfilePage.aspx.cs" Inherits="U4_BW1_LL.ProfilePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="smaller-main text-light">
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
                    <div id="infoAlCaricamento" runat="server"  class="d-flex justify-content-center align-items-center flex-column vh-100 d-block">

                        <asp:Image ID="ImmagineProfilo" runat="server" CssClass="fotoDimensions"/>
                        <div class="mt-3">
                            <h3 runat="server" id="nomeProfilo" class="fw-normal"></h3>
                        </div>                                             
                    </div>

                    <%-- div dove inserire password per conferma  --%>
                    <div  id="inputConfermaPassword" runat="server" class="d-flex flex-column">
                        <asp:Label ID="Label1" runat="server" Text="Label">Inserisci Password:</asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        
                    </div>

                    <%-- div dove cotrolla vecchio nome e se corrisposnde inserisci nuovo nome --%>
                    <div  id="inputinserisciNomeEPassword" runat="server" class="d-flex flex-column">
                        <asp:Label ID="Label2" runat="server" Text="Label">Inserisci nome:</asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" Text="Label">Inserisci Password:</asp:Label>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    </div>


                    <div class="vh-100 d-flex justify-content-center align-items-center gap-3 flex-column" id="scegliCosaCambiare" runat="server">
                        <div> <button runat="server" class="btn btn-outline-warning fs-3" onclick="Cambia_ImmagineProfilo">Cambia Immagine di profilo</button></div>
                        <div> <button runat="server" class="btn btn-outline-warning fs-3">Cambia nome utente </button></div>
                    </div>

                    

                </div>
            </div>
        </div>
    </main>
</asp:Content>
