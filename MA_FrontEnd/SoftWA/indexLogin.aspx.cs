using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SoftWA.Models;
using SoftWA.softapi.services; // <<< MODIFICADO
using System.Threading.Tasks; // <<< AÑADIDO

namespace SoftWA
{
    // La clase Usuario y la lista usuariosRegistrados han sido eliminadas.

    public partial class indexLogin : System.Web.UI.Page
    {
        private AuthService _authService = new AuthService(); // <<< AÑADIDO

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ltlMensajeError.Text = string.Empty;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }
            // Registrar la tarea asíncrona
            RegisterAsyncTask(new PageAsyncTask(AttemptLoginAsync));
        }

        private async Task AttemptLoginAsync()
        {
            string dniIngresado = txtDNI.Text.Trim();
            string passwordIngresado = txtPassword.Text;

            LoginRequestModel requestModel = new LoginRequestModel
            {
                NumeroDoc = dniIngresado,
                Contrasenha = passwordIngresado
            };

            LoginResponseModel responseModel = await _authService.LoginAsync(requestModel);

            if (responseModel != null && responseModel.Exito)
            {
                Session["UsuarioLogueado_DNI"] = responseModel.NumeroDoc; // O responseModel.IdCuenta si se prefiere
                Session["UsuarioLogueado_Rol"] = responseModel.Rol;

                string targetUrl = "~/indexLogin.aspx"; // URL por defecto si algo sale mal con el rol

                switch (responseModel.Rol)
                {
                    case RolUsuario.Admin:
                        targetUrl = "~/indexAdmin.aspx";
                        break;
                    case RolUsuario.Doctor:
                        targetUrl = "~/indexMedico.aspx";
                        break;
                    case RolUsuario.Paciente:
                        targetUrl = "~/indexPaciente.aspx";
                        break;
                    default:
                        // Si el rol es Desconocido o no coincide, podría ser un error o un caso no manejado.
                        // Mantenerlo en login o redirigir a una página de error específica.
                        MostrarError("Rol de usuario no reconocido recibido del servidor.");
                        return; // No redirigir
                }
                Response.Redirect(targetUrl, false); // false para no abortar el hilo actual si hay más procesamiento en la página (aunque aquí es lo último)
            }
            else
            {
                string errorMessage = responseModel?.Mensaje ?? "Error desconocido durante el login.";
                MostrarError(errorMessage);
                txtPassword.Text = string.Empty;
                txtPassword.Focus();
            }
        }

        private void MostrarError(string mensaje)
        {
            ltlMensajeError.Text = $"<div class='alert alert-danger alert-dismissible fade show' role='alert'>{HttpUtility.HtmlEncode(mensaje)}<button type='button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
        }
    }
}