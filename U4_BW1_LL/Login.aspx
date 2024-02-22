<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="U4_BW1_LL.Login" Async="true"  %>

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
            <div id="mainLoginContent" runat="server" class="container">
                <div class="row justify-content-center">
                    <div class="col-md-6">
                        <div class="card mt-5">
                            <div class="card-header  p-0 bg-dark text-white d-flex justify-content-between">
                                <div runat="server" id="btnAccedi" class="navBarFluo fw-bold w-50 text-center py-2">
                                    Accedi
                                </div>
                                <div runat="server" id="btnRegistrati" class="colorNotFluo fw-bold text-center w-50  py-2">
                                    Registrati 
                                </div>
                            </div>
                            <div class="card-body p-0">
                                <div id="divAccedi" class="colorNotFluo d-flex flex-column p-5 pb-3 position-relative">
                                    <asp:Image ID="heartImg" runat="server" Width="50px" ImageUrl="~/assets/imgs/-1-kxn9tz.svg" CssClass="position-absolute end-0 top-0 m-3 " />
                                    <asp:Label ID="Label1" runat="server"><p class="fw-bold m-0 my-2">Username</p></asp:Label>
                                    <asp:TextBox CssClass="sexyAlert rounded rounded-2 mb-3" ID="accediNome" runat="server"></asp:TextBox>
                                    <asp:Label ID="Label2" runat="server"><p class="fw-bold m-0 my-2">Password</p> </asp:Label>
                                    <asp:TextBox CssClass="sexyAlert rounded rounded-2" TextMode="Password" ID="accediPassword" runat="server"></asp:TextBox>
                                    <div class="d-flex justify-content-center mt-4 mb-0">
                                        <asp:Button ID="BtnPerAccedere" runat="server" Text="Entra nella tana del piacere" CssClass="btn sexyBtn fw-bold p-2 rounded-2 heartbeat" OnClick="accediBtn_Click" />
                                        <asp:Button ID="BtnPerRegistrarti" runat="server" Text="Unisciti alla tana del piacere" CssClass="btn sexyBtn fw-bold p-2 rounded-2 heartbeat d-none" OnClick="BtnPerRegistrarti_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="my-5">
                    <%-- se i campi input vengon lasciati vuoti --%>
                    <div id="alert" runat="server" class="row justify-content-center">
                        <div class="col-6">
                            <div class="text-center sexyAlert py-3 mt-3">
                                <p class="fw-bold mb-0">Inserisci Nome e Password.</p>
                            </div>
                        </div>
                    </div>

                    <%-- se utente non trovato o se login avvenuto con successo --%>
                    <div id="alertNot_foundOrSuccess" runat="server" class="row justify-content-center">
                        <div class="col-6">
                            <div class="text-center sexyAlert py-3 mt-3">
                                <p id="notFound_OrSuccess" runat="server" class="fw-bold mb-0"></p>
                            </div>
                        </div>
                    </div>

                    <%-- alert registrazione avvenuta --%>
                    <div id="alertRegistrationSuccess" runat="server" class="row justify-content-center">
                        <div class="col-6">
                            <div class="text-center sexyAlert py-3 mt-3">
                                <p id="registrationSuccessTxt" runat="server" class="fw-bold mb-0"></p>
                            </div>
                        </div>
                    </div>
                </div>             
            </div>
        </main>
    </form>

    <script>
        var btnEnter = document.getElementById("EnterTheLair");
        var btnAccedi = document.getElementById("btnAccedi");
        var btnRegistrati = document.getElementById("btnRegistrati");
        var divAccedi = document.getElementById("divAccedi");
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
