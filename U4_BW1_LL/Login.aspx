<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="U4_BW1_LL.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" />
    <title></title>
</head>
<body class="bg-dark">
    <form id="form1" runat="server">

        <div class="modal fade" id="RegistrationModal" runat="server" tabindex="-1" aria-labelledby="RegistrationModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h1 class="modal-title fs-5" id="RegistrationModalLabel">Registrazione !</h1>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div id="RegistrationModalBody" runat="server" class="modal-body">
                        
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
   
                    </div>
                </div>
            </div>
        </div>



        <div class="offcanvas offcanvas-start show showing" data-bs-backdrop="static" tabindex="-1" id="offcanvasExample" aria-labelledby="offcanvasExampleLabel">
            <div class="offcanvas-header">
                <h5 class="offcanvas-title" id="offcanvasExampleLabel">Lustful Lair <i class="bi bi-gender-female"><i class="bi bi-gender-male"></i></i></h5>

            </div>
            <div class="offcanvas-body">

                <button class="btn btn-dark mt-3" type="button" data-bs-toggle="collapse" data-bs-target="#loginCollapse" aria-expanded="false" aria-controls="collapseExample" id="btnCollapse1">
                    Login
                </button>

                <button class="btn btn-warning mt-3" type="button" data-bs-toggle="collapse" data-bs-target="#registrationCollapse" aria-expanded="false" aria-controls="collapseExample" id="btnCollapse2">
                    Registrati
                </button>



                <div class="collapse mt-2" id="loginCollapse">
                    <div cssclass="d-flex">

                        <div cssclass="d-flex">
                            <div class="mt-2 d-flex flex-column">
                                <asp:Label ID="usernameLabel" runat="server" Text="Username"></asp:Label>
                                <asp:TextBox ID="UserName" runat="server" placeholder="Username"></asp:TextBox>
                                <!--<asp:RequiredFieldValidator ErrorMessage="Nome Richiesto" ControlToValidate="UserName" ValidationExpression="^[a-zA-Z]{3,8}$" runat="server" />-->
                                <!--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UserName" ValidationExpression="^[a-zA-Z]{3,8}$" ErrorMessage="Nome utente richiesto"></asp:RegularExpressionValidator>-->
                            </div>
                            <div class="mt-2 d-flex flex-column">
                                <asp:Label ID="passwordLabel" runat="server" Text="Password"></asp:Label>
                                <asp:TextBox ID="passWord" TextMode="Password" runat="server" placeholder="Password"></asp:TextBox>
                                <!--<asp:RequiredFieldValidator ErrorMessage="Password Richiesta" ControlToValidate="passWord" ValidationExpression="^([a-zA-Z0-9]*).{5,}$" runat="server" /> -->
                                <!--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="passWord" ValidationExpression="^([a-zA-Z0-9]*).{5,}$" ErrorMessage="Almeno 5 Caratteri"></asp:RegularExpressionValidator> -->
                            </div>
                        </div>

                        <asp:Button CssClass="btn btn-dark mt-3" ID="LoginButton" runat="server" Text="Click to Login" OnClick="Login_Click" />

                    </div>

                </div>




                <div class="collapse mt-2" id="registrationCollapse">
                    <div cssclass="d-flex">

                        <div class="mt-2 d-flex flex-column">
                            <asp:Label CssClass="fw-bold" ID="Label3" runat="server" Text="Username"></asp:Label>
                            <asp:TextBox ID="TextBoxUsername" runat="server" placeholder="Username"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Nome Richiesto" ControlToValidate="TextBoxUsername" ValidationExpression="^[a-zA-Z]{3,8}$" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBoxUsername" ValidationExpression="^[a-zA-Z]{3,8}$" ErrorMessage="Nome utente richiesto"></asp:RegularExpressionValidator>
                        </div>
                        <div class="mt-0 d-flex flex-column">
                            <asp:Label CssClass="fw-bold" ID="Label4" runat="server" Text="Password"></asp:Label>
                            <asp:TextBox ID="TextBoxPass" runat="server" placeholder="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Password Richiesta" ControlToValidate="TextBoxPass" ValidationExpression="^([a-zA-Z0-9]*).{5,}$" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBoxPass" ValidationExpression="^([a-zA-Z0-9]*).{5,}$" ErrorMessage="Almeno 5 Caratteri"></asp:RegularExpressionValidator>
                        </div>
                        <div class="mt-0 d-flex flex-column">
                            <asp:Label CssClass="fw-bold" ID="Label5" runat="server" Text="Ripeti Password"></asp:Label>
                            <asp:TextBox ID="TextBoxPassControl" runat="server" placeholder="Ripeti Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ErrorMessage="Password Richiesta" ControlToValidate="TextBoxPassControl" ValidationExpression="^([a-zA-Z0-9]*).{5,}$" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBoxPassControl" ValidationExpression="^([a-zA-Z0-9]*).{5,}$" ErrorMessage="Almeno 5 Caratteri"></asp:RegularExpressionValidator>
                        </div>
                    </div>

                    <asp:Button CssClass="btn btn-dark mt-3" ID="RegistrationButton" runat="server" Text="Click to Register" OnClick="RegistrationButton_Click" />

                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>
    <script>
        $(document).ready(function () {
            $('#btnCollapse1').click(function () {
                // Chiudi il secondo collapse se è aperto
                if ($('#registrationCollapse').hasClass('show')) {
                    $('#registrationCollapse').collapse('hide');
                }
            });
            $('#btnCollapse2').click(function () {
                // Chiudi il primo collapse se è aperto
                if ($('#loginCollapse').hasClass('show')) {
                    $('#loginCollapse').collapse('hide');
                }
            });
        });
    </script>
</body>
</html>
