<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreSite.aspx.cs" Inherits="U4_BW1_LL.PreSite" %>

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
            <%-- Disclaimer iniziale --%>
            <div id="disclaimer" class="disclaimerSettings d-flex flex-column justify-content-center align-items-center" runat="server">
                <div class="position-relative mb-2">
                    <img src="assets/imgs/LustLogoOpen.jpg" style="width: 600px" />
                    <img src="assets/imgs/LustLogo.png" id="imgTop" runat="server"/>
                </div>
                <asp:Button ID="EnterTheLair" runat="server" Text="Ho più di 18 anni." CssClass="btn sexyBtn fs-4 px-4" OnClick="EnterTheLair_Click" />
            </div>
        </main>
    </form>
</body>
</html>
