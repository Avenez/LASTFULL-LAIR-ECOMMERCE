<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrello.aspx.cs" Inherits="U4_BW1_LL.Carrello" EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <h3 class="display-4 sexyPink2" style="display: none" runat="server" id="emptyCart">Il carrello è vuoto.</h3>
   <h3 class="display-4 sexyPink2" runat="server" id="boh"></h3>
   <div id="cartRow" style="display: none" class="row" runat="server">
    <asp:Repeater ID="CartRepeater" runat="server">
        <ItemTemplate>
            <div class="col-12">
                <div style="height: 120px" class="d-flex justify-content-between align-items-center border border-dark sexyCardBg rounded">
                    <div class="h-100 d-flex">
                        <div class="d-flex justify-content-center h-100 p-2 me-3" style="width: 90px">
                            <img src='<%# Eval("ImgUrl") %>' class="h-100" alt='<%# Eval("Nome") %>'>
                        </div>
                        <div class="py-2">
                            <h5><%# Eval("Nome") %></h5>
                            <p class="mb-0"><%# Eval("Prezzo", "{0:c2}") %></p>
                            <div class="d-flex mt-2 align-items-baseline">
                                <asp:Button CssClass="btn btn-dark p-1 me-2" ID="ButtonSum" runat="server" Text=" - " CommandArgument='<%# "Sub" + "*" + Eval("Id") %> ' OnClick="ButtonChange_Click" style="height:40px; width:30px" />
                                 <p class=""> Qta: <%# Eval("Qta") %></p> 
                                <asp:Button CssClass="btn btn-dark p-1 ms-2" ID="ButtonSub" runat="server" Text=" + " CommandArgument='<%# "Sum" + "*" +  Eval("Id") %> ' OnClick="ButtonChange_Click" style="height:40px; width:30px"/>
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
            <p id="totalPrice" class="sexyPink2 mb-0" runat="server"></p>
            <asp:Button class="btn sexyBtn" ID="ButtonAcquista" runat="server" Text="Acquista" onClick="ButtonAcquista_Click"/>
        </div>
        <asp:Button CssClass="btn btn-danger" ID="RemoveAllBtn" runat="server" Text="Rimuovi tutto" OnClick="btnSvuotaCarrello_Click"/>
    </div>
</div>
</asp:Content>
