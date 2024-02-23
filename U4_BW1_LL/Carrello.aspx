<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="U4_BW1_LL.Carrello" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <audio src="./assets/music/phSound.mp3"></audio>

    <div class="text-center py-2" style="display: none" runat="server" id="emptyCart">
        <p class="display-6 sexyPink2 bounce-in-top" id="feedCarrelloVuoto"  runat="server" >Il carrello è vuoto</p>
        <p class="display-6 sexyPink2 bounce-in-top d-none" id="feedCarrelloAcquisto"  runat="server" >Grazie per il tuo acquisto, Biscottino</p>
    </div>
   <h3 class="display-4 sexyPink2" runat="server" id="boh"></h3>
<div class="d-flex">
    <asp:Button class="btn sexyBtnOutline2 mb-3" ID="btnTornaIndietro" runat="server" Text="Indietro" OnClick="btnTornaIndietro_Click" />
</div>
   <div id="cartRow" style="display: none" class="row" runat="server">
    <asp:Repeater ID="CartRepeater" runat="server">
        <ItemTemplate>
            <div class="col-12">
                <div style="height: 120px" class="d-flex justify-content-between align-items-center border border-dark sexyCardBg rounded">
                    <div class="h-100 d-flex">
                        <div class="h-100 p-2 me-4" style="width: 90px">
                            <img src='<%# Eval("ImgUrl") %>' class="h-100" alt='<%# Eval("Nome") %>'>
                        </div>
                        <div class="py-2">
                            <h5 class="mb-0"><%# Eval("Nome") %></h5>
                            <p class="font-monospace">Prezzo: <%# Eval("Prezzo", "{0:c2}") %></p>
                            <div class="d-flex mt-2 align-items-baseline">
                                <asp:Button CssClass="btn btn-dark h-25 p-1 me-2" ID="ButtonSum" runat="server" Text=" - " CommandArgument='<%# "Sub" + "*" + Eval("Id") %> ' OnClick="ButtonChange_Click" style="height:40px; width:30px" />
                                 <p class="mb-0"> Qta: <%# Eval("Qta") %></p> 
                                <asp:Button CssClass="btn btn-dark h-25 p-1 ms-2" ID="ButtonSub" runat="server" Text=" + " CommandArgument='<%# "Sum" + "*" +  Eval("Id") %> ' OnClick="ButtonChange_Click" style="height:40px; width:30px"/>
                            </div>                          
                        </div>
                    </div>
                    <div class="me-3">
                        <asp:Button class="btn btn-danger" ID="ButtonRemove" runat="server" Text="Rimuovi" CommandArgument='<%#Eval("Id") %>' OnClick='ButtonRemove_Click' />
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <div class="d-flex justify-content-between align-items-center mt-3">
        <div>
            <p id="totalPriceP" class="sexyPink2 font-monospace mb-0" runat="server"></p>
            <asp:Button class="btn sexyBtn" ID="ButtonAcquista" runat="server" Text="Acquista" onClick="ButtonAcquista_Click"/>
        </div>
        <asp:Button CssClass="btn btn-danger" ID="RemoveAllBtn" runat="server" Text="Rimuovi tutto" OnClick="btnSvuotaCarrello_Click"/>
    </div>
</div>
</asp:Content>
