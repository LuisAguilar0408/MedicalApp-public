<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="indexLogin.aspx.cs" Inherits="SoftWA.indexLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Iniciar Sesión - Medical App</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.0/css/all.min.css" />
    <style>
        body {
            background-color: #f8f9fa;
        }
        .login-container {
            max-width: 400px;
            margin: 10vh auto;
            padding: 2rem;
            background-color: #fff;
            border-radius: 0.5rem;
            box-shadow: 0 0.5rem 1rem rgba(0,0,0,.15);
        }
        .login-header {
            text-align: center;
            margin-bottom: 1.5rem;
        }
         .login-header i {
             font-size: 3rem;
             color: #0d6efd;
             margin-bottom: 0.5rem;
         }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate>
        <div class="container">
            <div class="login-container">
                <div class="login-header d-flex flex-column align-items-center text-center">
                <a class="navbar-brand d-flex align-items-center" href="index.aspx">
                    <img src="Images/op3.png" alt="Logo" width="200">
                </a>
                <h2>Medical App</h2>
                <p class="text-muted">Ingrese sus datos</p>
            </div>

                <%-- mensaje error--%>
                <asp:Literal ID="ltlMensajeError" runat="server" EnableViewState="false"></asp:Literal>

                <%-- campo DNI --%>
                <div class="mb-3">
                    <label for="<%=txtDNI.ClientID%>" class="form-label">DNI (Usuario):</label>
                    <div class="input-group">
                         <span class="input-group-text"><i class="fa-solid fa-id-card"></i></span>
                        <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" MaxLength="15" placeholder="Ingrese su DNI"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvDNI" runat="server"
                        ControlToValidate="txtDNI"
                        ErrorMessage="El DNI es obligatorio."
                        CssClass="text-danger small"
                        Display="Dynamic"
                        ValidationGroup="LoginValidation">
                    </asp:RequiredFieldValidator>
                </div>

                <%-- campo contrasena --%>
                <div class="mb-3">
                    <label for="<%=txtPassword.ClientID%>" class="form-label">Contraseña:</label>
                     <div class="input-group">
                        <span class="input-group-text"><i class="fa-solid fa-lock"></i></span>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña"></asp:TextBox>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ControlToValidate="txtPassword"
                        ErrorMessage="La contraseña es obligatoria."
                        CssClass="text-danger small"
                        Display="Dynamic"
                        ValidationGroup="LoginValidation">
                     </asp:RequiredFieldValidator>
                </div>

                <%-- boton login--%>
                <div class="d-grid">
                    <asp:Button ID="btnLogin" runat="server" Text="Ingresar"
                        CssClass="btn btn-primary btn-block" OnClick="btnLogin_Click"
                        ValidationGroup="LoginValidation" />
                </div>
                <%-- recuperar contrasena --%>
                <div class="mb-3 text-center">
                    <a href="#" class="btn btn-link">¿Olvidaste tu contraseña?</a>
                </div>
                <div class="mt-3 text-center">
                    ¿No tienes cuenta? <a href="#" class="btn btn-link">Regístrate aquí</a>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
