<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RiepilogoOrdiniUtenti.aspx.cs" Inherits="U4_BW1_LL.RiepilogoOrdiniUtenti" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class=" container-fluid">
        <div class="row">
            <div class="col-12">
                <div>
                    <asp:Button CssClass="btn sexyBtnOutline2" ID="tornaIndietro" runat="server" Text="Indietro " OnClick="tornaIndietro_Click" />
                </div>
            </div>
        </div>

        <div class="mt-5">
            <div class="row row-cols-4">
                <asp:Repeater ID="repeaterRiepilogo" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="text-light border border-1 my-3 p-3">
                                 <h5>Id Utente: <%# Eval("idUtente") %></h5>
                                <p>Username: <%# Eval("username") %></p>
                                <p>IdOrdine: <%# Eval("idordine") %></p>
                                <p>PrezzoTotale: <%# Eval("PrezzoTotale") %></p>
                                <p>DataOrdine: <%# Eval("DataOrdine") %></p>
                                <br />
                           
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>


    </div>

</asp:Content>
