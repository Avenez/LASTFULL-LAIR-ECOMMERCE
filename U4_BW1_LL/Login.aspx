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
            </div>
        </main>
    </form>

    <script>

        var btnAccedi = document.getElementById("btnAccedi");
        var btnRegistrati = document.getElementById("btnRegistrati");
        var formLogin = document.getElementById("formLogin");

        btnAccedi.addEventListener("click", function () {

            if (btnAccedi.classList.contains("colorNotFluo")) {
                btnAccedi.classList.remove("colorNotFluo");
                btnAccedi.classList.add("navBarFluo");
                btnRegistrati.classList.remove("navBarFluo");
                btnRegistrati.classList.add("colorNotFluo");               
            }          
        });

        btnRegistrati.addEventListener("click", function () {

              
            if (btnRegistrati.classList.contains("colorNotFluo")) {
                btnRegistrati.classList.remove("colorNotFluo");
                btnRegistrati.classList.add("navBarFluo");
                btnAccedi.classList.remove("navBarFluo");
                btnAccedi.classList.add("colorNotFluo");             
            }
        });

    </script>
</body>
</html>
