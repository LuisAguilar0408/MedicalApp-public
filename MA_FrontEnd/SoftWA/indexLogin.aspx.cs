using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftWA
{
    // Define RolUsuario and Usuario class here if not defined elsewhere
    public enum RolUsuario
    {
        Admin,
        Doctor,
        Paciente
    }

    public class Usuario
    {
        public string DNI { get; set; }
        public string Password { get; set; } // ejemplo
        public RolUsuario Rol { get; set; }
    }

    public partial class indexLogin : System.Web.UI.Page
    {
        private static readonly List<Usuario> usuariosRegistrados = new List<Usuario>
        {
            // Administrador
            new Usuario { DNI = "ADM018", Password = "admin", Rol = RolUsuario.Admin },
            // Doctores
            new Usuario { DNI = "22222222", Password = "doctor", Rol = RolUsuario.Doctor },
            new Usuario { DNI = "33333333", Password = "doctor_pass2", Rol = RolUsuario.Doctor },
            // Pacientes
            new Usuario { DNI = "44444444", Password = "paciente", Rol = RolUsuario.Paciente },
            new Usuario { DNI = "55555555", Password = "paciente_pass2", Rol = RolUsuario.Paciente },
            new Usuario { DNI = "66666666", Password = "paciente_pass3", Rol = RolUsuario.Paciente }
        };

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

            string dniIngresado = txtDNI.Text.Trim();
            string passwordIngresado = txtPassword.Text;

            Usuario usuarioAutenticado = usuariosRegistrados.FirstOrDefault(u =>
                u.DNI.Equals(dniIngresado, StringComparison.OrdinalIgnoreCase) &&
                u.Password == passwordIngresado
            );

            if (usuarioAutenticado != null)
            {
                Session["UsuarioLogueado_DNI"] = usuarioAutenticado.DNI;
                Session["UsuarioLogueado_Rol"] = usuarioAutenticado.Rol;


                string targetUrl = "~/Default.aspx";

                switch (usuarioAutenticado.Rol)
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
                }

                Response.Redirect(targetUrl, false);
            }
            else
            {
                MostrarError("DNI o contraseña incorrectos. Por favor, intente de nuevo.");
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