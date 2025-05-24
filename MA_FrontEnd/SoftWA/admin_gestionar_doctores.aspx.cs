using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace SoftWA
{
    // Estructura para representar datos del Doctor
    public class DoctorInfo
    {
        public int IdDoctor { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int IdEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; } 
        public string Email { get; set; }
        public string Telefono { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }

    public class EspecialidadSimple
    {
        public int IdEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; }
    }


    public partial class admin_gestionar_doctores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulateEspecialidadesDropdown();
                BindDoctorList();
            }
        }


        private void BindDoctorList()
        {
            Debug.WriteLine("Llamando a BindDoctorList...");
            List<DoctorInfo> doctores = ObtenerDoctores_Simulado(); // llamada simulada al backend
            lvDoctores.DataSource = doctores;
            lvDoctores.DataBind();
        }

        private List<DoctorInfo> ObtenerDoctores_Simulado()
        {
            //simulacion de base de datos
            return new List<DoctorInfo>
            {
                new DoctorInfo { IdDoctor = 101, Nombre = "Juan", Apellido = "Pérez", IdEspecialidad = 1, NombreEspecialidad = "Cardiología", Email = "jperez@hospital.com", Telefono = "123456789" },
                new DoctorInfo { IdDoctor = 102, Nombre = "Ana", Apellido = "López", IdEspecialidad = 1, NombreEspecialidad = "Cardiología", Email = "alopez@hospital.com", Telefono = "987654321" },
                new DoctorInfo { IdDoctor = 201, Nombre = "Carlos", Apellido = "García", IdEspecialidad = 2, NombreEspecialidad = "Dermatología", Email = "cgarcia@hospital.com", Telefono = "965647326" },
                new DoctorInfo { IdDoctor = 301, Nombre = "Sofía", Apellido = "Gómez", IdEspecialidad = 3, NombreEspecialidad = "Pediatría", Email = "sgomez@hospital.com", Telefono = "112233445" }
            };
        }

        private void PopulateEspecialidadesDropdown()
        {
            Debug.WriteLine("Llamando a PopulateEspecialidadesDropdown...");
            List<EspecialidadSimple> especialidades = ObtenerEspecialidades_Simulado(); // Llamada simulada
            ddlEspecialidadAddEdit.DataSource = especialidades;
            ddlEspecialidadAddEdit.DataTextField = "NombreEspecialidad";
            ddlEspecialidadAddEdit.DataValueField = "IdEspecialidad";
            ddlEspecialidadAddEdit.DataBind();
            if (ddlEspecialidadAddEdit.Items.FindByValue("") == null)
            {
                ddlEspecialidadAddEdit.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
        }

        private List<EspecialidadSimple> ObtenerEspecialidades_Simulado()
        {
            Debug.WriteLine("Simulando: ObtenerEspecialidades_Simulado()");
            // base de datos simulada
            return new List<EspecialidadSimple> {
                new EspecialidadSimple { IdEspecialidad = 1, NombreEspecialidad = "Cardiología" },
                new EspecialidadSimple { IdEspecialidad = 2, NombreEspecialidad = "Dermatología" },
                new EspecialidadSimple { IdEspecialidad = 3, NombreEspecialidad = "Pediatría" },
                new EspecialidadSimple { IdEspecialidad = 4, NombreEspecialidad = "Traumatología" },
                new EspecialidadSimple { IdEspecialidad = 5, NombreEspecialidad = "Odontología General" }
             };
        }

        // --- Lógica del Formulario Add/Edit ---

        protected void btnShowAddPanel_Click(object sender, EventArgs e)
        {
            ResetForm();
            lblFormTitle.Text = "Agregar Nuevo Doctor";
            hfDoctorId.Value = "0"; 
            pnlAddEditDoctor.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlAddEditDoctor.Visible = false;
            ResetForm();
        }

        private void ResetForm()
        {
            hfDoctorId.Value = "0";
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            ddlEspecialidadAddEdit.ClearSelection();
        }


        protected void btnGuardarDoctor_Click(object sender, EventArgs e)
        {
            Page.Validate("AddEditDoctor");
            if (!Page.IsValid)
            {
                return; 
            }

            int doctorId = Convert.ToInt32(hfDoctorId.Value);
            var doctorInfo = new DoctorInfo
            {
                IdDoctor = doctorId, 
                Nombre = txtNombre.Text.Trim(),
                Apellido = txtApellido.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                IdEspecialidad = Convert.ToInt32(ddlEspecialidadAddEdit.SelectedValue)
            };

            bool success = false;
            if (doctorId == 0) 
            {
                success = AgregarDoctor_Simulado(doctorInfo); 
            }
            else // Modo Editar
            {
                Debug.WriteLine($"Intentando actualizar doctor ID: {doctorId}");
                success = ActualizarDoctor_Simulado(doctorInfo); 
            }

            if (success)
            {
                pnlAddEditDoctor.Visible = false;
                ResetForm();
                BindDoctorList(); // recargar
            }
            else
            {
                // error de conexion a bd
            }
        }


        protected void lvDoctores_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                int doctorId = Convert.ToInt32(e.CommandArgument);
                Debug.WriteLine($"Comando Editar para Doctor ID: {doctorId}");

                DoctorInfo doctor = ObtenerDoctorPorId_Simulado(doctorId); 

                if (doctor != null)
                {
                    hfDoctorId.Value = doctor.IdDoctor.ToString();
                    txtNombre.Text = doctor.Nombre;
                    txtApellido.Text = doctor.Apellido;
                    txtEmail.Text = doctor.Email;
                    txtTelefono.Text = doctor.Telefono;
                    ddlEspecialidadAddEdit.SelectedValue = doctor.IdEspecialidad.ToString(); 
                    lblFormTitle.Text = "Editar Doctor";
                    pnlAddEditDoctor.Visible = true;
                }
                else
                {
                    //error
                }
            }
        }

        protected void lvDoctores_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            int doctorId = Convert.ToInt32(lvDoctores.DataKeys[e.ItemIndex].Value);
            bool success = EliminarDoctor_Simulado(doctorId);

            if (success)
            {
                BindDoctorList(); //recargar lista
                pnlAddEditDoctor.Visible = false;
                ResetForm();
            }
            else
            {
                //error
            }

            e.Cancel = true;
        }


        private DoctorInfo ObtenerDoctorPorId_Simulado(int id)
        {
            return ObtenerDoctores_Simulado().FirstOrDefault(d => d.IdDoctor == id);
        }

        private bool AgregarDoctor_Simulado(DoctorInfo doctor)
        {
            return true; 
        }

        private bool ActualizarDoctor_Simulado(DoctorInfo doctor)
        {
            return true; 
        }

        private bool EliminarDoctor_Simulado(int id)
        {
            return true;
        }
    }
}