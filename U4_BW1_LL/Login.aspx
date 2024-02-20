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
                            <asp:Label ID="Label1" runat="server"><p class="m-0 maybeSexy my-2 fs-3">Nome</p></asp:Label>
                            <asp:TextBox CssClass="rounded rounded-2" ID="accediNome" runat="server"></asp:TextBox>
                            <asp:Label ID="Label2" runat="server"><p class="m-0 maybeSexy my-2 fs-3">Password</p> </asp:Label>
                            <asp:TextBox TextMode="Password" ID="accediPassword" runat="server"></asp:TextBox>
                            <div class="d-flex justify-content-center mt-4 mb-0">
                                <asp:Button ID="BtnPerAccedere" runat="server" Text="Cedi alla lussuria..." CssClass="maybeSexy colorNotFluo p-2 fs-3 rounded-2 heartbeat"  OnClick="accediBtn_Click"/>
                                  <asp:Button ID="BtnPerRegistrarti" runat="server" Text="Concediti Al peccato Originale..." CssClass="maybeSexy colorNotFluo p-2 fs-3 rounded-2 heartbeat d-none"  OnClick="BtnPerRegistrarti_Click"/>
                            </div>
                        </div>                   
                        
                        
                    </div>

                    <%-- se i campi input vengon lasciati vuoti --%>
                    <div id="alert" runat="server" class="row justify-content-center">
                        <div class="col-12">
                            <div class="text-center colorNotFluo mt-5">
                                <h5 class="maybeSexy fs-1">Inserisci nome e cognome Birichino...</h5>
                            </div>
                        </div>
                    </div>

                    <%-- se utente non trovato o se login avvenuto con successo --%>
                    <div id="alertNot_foundOrSuccess" runat="server" class="row justify-content-center">
                        <div class="col-12">
                            <div class="text-center colorNotFluo mt-5">
                                <h5 id="notFound_OrSuccess" runat="server" class="maybeSexy fs-1"></h5>
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
        var divAccedi = document.getElementById("divAccedi");
        var registerTxt = document.getElementById("registerTxt");
        var accediTxt = document.getElementById("accediTxt");
        var BtnPerAccedere = document.getElementById("BtnPerAccedere");
        var BtnPerRegistrarti = document.getElementById("BtnPerRegistrarti");

        btnAccedi.addEventListener("click", function () {

            if (btnAccedi.classList.contains("colorNotFluo")) {
                btnAccedi.classList.remove("colorNotFluo");
                btnAccedi.classList.add("navBarFluo");
                btnRegistrati.classList.remove("navBarFluo");
                btnRegistrati.classList.add("colorNotFluo");
            }

            divAccedi.classList.add("d-flex");
            divAccedi.classList.remove("d-none");       
            accediTxt.classList.add("d-block");
            accediTxt.classList.remove("d-none");
            registerTxt.classList.add("d-none");
            registerTxt.classList.remove("d-block"); 
           // btnAccedi.classList.add("d-block"); 
            BtnPerAccedere.classList.remove("d-none");
            BtnPerAccedere.classList.add("d-flex");
            BtnPerRegistrarti.classList.add("d-none");
            BtnPerRegistrarti.classList.remove("d-flex");

        });

        btnRegistrati.addEventListener("click", function () {

            if (btnRegistrati.classList.contains("colorNotFluo")) {
                btnRegistrati.classList.remove("colorNotFluo");
                btnRegistrati.classList.add("navBarFluo");
                btnAccedi.classList.remove("navBarFluo");
                btnAccedi.classList.add("colorNotFluo");
            }

           
            accediTxt.classList.add("d-none");
            accediTxt.classList.remove("d-block");
            registerTxt.classList.add("d-block");
            registerTxt.classList.remove("d-none");
            BtnPerRegistrarti.classList.remove("d-none");
            BtnPerRegistrarti.classList.add("d-flex");
            BtnPerAccedere.classList.add("d-none");
            BtnPerAccedere.classList.remove("d-flex");
            
        });

        heartImg.addEventListener("load", () => {
            heartImg.classList.add("TUMTUM"); 
        })
        
    </script>
</body>
</html>
