<%@ Page Language="C#" AutoEventWireup="true" Title="log-in" CodeBehind="login.aspx.cs" Inherits="paginaOficial.login" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title><%: Page.Title %> - Asamblea Nacional</title>
    <!--Logo de tituo de pagina-->
    <link href="../userscontent/img/logoAsamblea.png" rel="shortcut icon" type="image/x-icon" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/hojaestilo1.css" rel="stylesheet" />
    <script src="Scripts/jquery-3.4.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body class="fondo">
    <form id="form1" runat="server">
        <div class="modal-dialog text-center">
            <div class="col-sm-9 holaestilo1">
                <div class="modal-content">
                    <div class="col-12 user-img">
                        <img class="imagen" src="userscontent/img/logoAsamblea.png" />
                    </div>
                    <asp:Login ID="Login" runat="server" CssClass="d-flex text-center align-items-center justify-content-center pl-3 pr-5 pb-3"
                        LoginButtonText="Iniciar sesion"
                        PasswordLabelText="Contraseña:"
                        RememberMeText="Recordarme la proxima vez"
                        TitleText=""
                        UserNameLabelText="Usuario:"
                        UserNameRequiredErrorMessage="El ID de usuario es obligatorio." DestinationPageUrl="~/Default.aspx" OnAuthenticate="Login_Authenticate1">
                        <LabelStyle ForeColor="White" CssClass="text-center pr-2" />
                        <LoginButtonStyle CssClass="btn btn-success mt-3 mb-3 mr-4 pr-5 pl-5" />
                        <TextBoxStyle CssClass="form-control pt-3 pb-3" />
                        <CheckBoxStyle CssClass="mt-2 pr-2 mb-2" ForeColor="White" HorizontalAlign="Right" />
                    </asp:Login>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
