using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace SoftWA
{
    
    public class MedicoPerfil
    {
        public string IdMedico { get; set; }
        public string CodigoMedico { get; set; }
        public string NombreCompleto { get; set; }
        public string IdEspecialidad { get; set; } 
        public string NombreEspecialidad { get; set; }
    }

    public partial class perfiles_medicos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPerfilesMedicos();
            }
        }

        private void CargarPerfilesMedicos()
        {
            // cargar datos

            List<MedicoPerfil> listaDeMedicos = GetMedicosDeEjemploParaPerfil();

            if (listaDeMedicos != null && listaDeMedicos.Any())
            {
                rptPerfilesMedicos.DataSource = listaDeMedicos.OrderBy(m => m.NombreCompleto);
                rptPerfilesMedicos.DataBind();
                phNoMedicos.Visible = false;
                rptPerfilesMedicos.Visible = true;
            }
            else
            {
                phNoMedicos.Visible = true;
                rptPerfilesMedicos.Visible = false;
            }
        }

        // datos duros
        private List<MedicoPerfil> GetMedicosDeEjemploParaPerfil()
        {
            // logica de obtener de la bd
            return new List<MedicoPerfil>
            {
                new MedicoPerfil {
                    IdMedico = "101", CodigoMedico = "CM-JPEREZ", NombreCompleto = "Dr. Juan Pérez",
                    IdEspecialidad = "1", NombreEspecialidad = "Cardiología",
                },
                new MedicoPerfil {
                    IdMedico = "102", CodigoMedico = "CM-ALOPEZ", NombreCompleto = "Dra. Ana López",
                    IdEspecialidad = "1", NombreEspecialidad = "Cardiología",
                },
                new MedicoPerfil {
                    IdMedico = "201", CodigoMedico = "CM-CGARCIA", NombreCompleto = "Dr. Carlos García",
                    IdEspecialidad = "2", NombreEspecialidad = "Dermatología",
                },
                 new MedicoPerfil {
                    IdMedico = "301", CodigoMedico = "CM-SGOMEZ", NombreCompleto = "Dra. Sofía Gómez",
                    IdEspecialidad = "3", NombreEspecialidad = "Pediatría",
                }
            };
        }
    }
}