<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="U4_BW1_LL.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
<link rel="stylesheet" href=".\assets\css\login.css" />
    <title></title>
</head>
<body  class="bg-black">
    <form id="formLogin" runat="server">

        <nav class="navbar navbar-expand-sm navbar-toggleable-sm navBarFluo" >
            <div class="container justify-content-center">
                <a class="navbar-brand" runat="server" href="~/">
                    <img src=".\assets\imgs\LustLogo2.jpg" style="width: 180px" alt="LustLogo" />
                </a>
            </div>
        </nav>
        <main>
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-md-6">
                        <div class="card mt-5">
                            <div class="card-header  p-0 bg-dark text-white d-flex justify-content-between">
                                <div runat="server" id="btnAccedi" class="navBarFluo w-50 text-center py-2">
                                    Accedi
                                </div>
                                <div runat="server" id="btnRegistrati" class="colorNotFluo text-center w-50  py-2">
                                    Registrati 
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="my-5">
                    <div class="row justify-content-center">
                        <div class="col-12">
                            <div class="text-center colorNotFluo mb-3 colorNotFluo">
                                <h5 id="accediTxt" class=" maybeSexy fs-1">Accedi </h5>
                                <h5 id="registerTxt" class="d-none maybeSexy fs-1">Registrati </h5>
                            </div>
                        </div>

                        <div id="divAccedi" class="colorNotFluo d-flex flex-column w-50 p-5 pb-3 border border-1 rounded-3 border-white position-relative">
                            <asp:Image ID="heartImg" runat="server" Width="50px" ImageUrl="~/assets/imgs/-1-kxn9tz.svg" CssClass="position-absolute end-0 top-0 m-3 " />
                            <asp:Label ID="nome" runat="server"><p class="m-0 maybeSexy my-2 fs-3">Nome</p></asp:Label>
                            <asp:TextBox CssClass="rounded rounded-2" ID="accediNome" runat="server"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server"><p class="m-0 maybeSexy my-2 fs-3">Password</p> </asp:Label>
                            <asp:TextBox ID="accediCognome" runat="server"></asp:TextBox>
                            <div class="text-center mt-4 mb-0">
                                <asp:Button ID="accediBtn" runat="server" Text="Cedi alla lussuria..." CssClass="maybeSexy colorNotFluo p-2 fs-3 rounded-2 heartbeat"  OnClick="accediBtn_Click"/>
                            </div>
                        </div>


                        <div id="divRegistrati" class="colorNotFluo d-none d-flex flex-column w-50 p-5 pb-3 border border-1 rounded-3  border-white">
                            <asp:Label ID="Label3" runat="server"><p class="m-0 maybeSexy my-2 fs-3">Nome</p></asp:Label>
                            <asp:TextBox CssClass="rounded rounded-2" ID="TextBox3" runat="server"></asp:TextBox>
                            <asp:Label ID="Label4" runat="server"><p class="m-0 maybeSexy my-2 fs-3">Password</p></asp:Label>
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                              <div class="text-center mt-3">
                                <asp:Button ID="registratiBtn" runat="server" Text="Concediti Al peccato Originale..." CssClass="maybeSexy colorNotFluo p-2 fs-3 rounded-2 heartbeat" />
                            </div>
                        </div>                                                                        
                    </div>

                    <div id="alert" runat="server" class="row justify-content-center">
                        <div class="col-12">
                            <div class="text-center colorNotFluo mt-5">
                                <h5 class="maybeSexy fs-1">Inserisci nome e cognome Birichino...</h5>
                            </div>
                        </div>
                    </div>

                </div>             
            </div>
        </main>
    </form>

    <script>
        var btnAccedi = document.getElementById("btnAccedi");
        var btnRegistrati = document.getElementById("btnRegistrati");
        var formLogin = document.getElementById("formLogin");
        var divAccedi = document.getElementById("divAccedi");
        var divRegistrati = document.getElementById("divRegistrati");
        var registerTxt = document.getElementById("registerTxt");
        var accediTxt = document.getElementById("accediTxt");

        btnAccedi.addEventListener("click", function () {

            if (btnAccedi.classList.contains("colorNotFluo")) {
                btnAccedi.classList.remove("colorNotFluo");
                btnAccedi.classList.add("navBarFluo");
                btnRegistrati.classList.remove("navBarFluo");
                btnRegistrati.classList.add("colorNotFluo");
            }

            divAccedi.classList.add("d-flex");
            divAccedi.classList.remove("d-none");
            divRegistrati.classList.add("d-none");
            divRegistrati.classList.remove("d-flex");
            accediTxt.classList.add("d-block");
            accediTxt.classList.remove("d-none");
            registerTxt.classList.add("d-none");
            registerTxt.classList.remove("d-block"); 

        });

        btnRegistrati.addEventListener("click", function () {

            if (btnRegistrati.classList.contains("colorNotFluo")) {
                btnRegistrati.classList.remove("colorNotFluo");
                btnRegistrati.classList.add("navBarFluo");
                btnAccedi.classList.remove("navBarFluo");
                btnAccedi.classList.add("colorNotFluo");
            }

            divAccedi.classList.add("d-none");
            divAccedi.classList.remove("d-flex");
            divRegistrati.classList.remove("d-none");
            divRegistrati.classList.add("d-flex");
            accediTxt.classList.add("d-none");
            accediTxt.classList.remove("d-block");
            registerTxt.classList.add("d-block");
            registerTxt.classList.remove("d-none");
        });

        heartImg.addEventListener("load", () => {
            heartImg.classList.add("TUMTUM"); 
        })
        
    </script>
</body>
</html>
