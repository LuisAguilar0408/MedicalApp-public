using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftWA
{

    public class CitaHistInfo
    {
        public int IdCita { get; set; }
        public string NombreEspecialidad { get; set; }
        public string NombreMedico { get; set; }
        public DateTime FechaCita { get; set; }
        public string DescripcionHorario { get; set; }
        public string Estado { get; set; } 
        public string ObsMedicas { get; set; } 
    }

    public partial class historial_citas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCitasAtendidas();
            }
        }

        private void CargarCitasAtendidas()
        {


            List<CitaHistInfo> listaDeCitas = GetCitasHistDeEjemplo(); // datos en duro

            if (listaDeCitas != null && listaDeCitas.Any())
            {
                rptHistorial.DataSource = listaDeCitas.OrderBy(c => c.FechaCita).ThenBy(c => c.NombreEspecialidad);
                rptHistorial.DataBind();
                phNoHistorial.Visible = false;
                rptHistorial.Visible = true;
            }
            else
            {
                phNoHistorial.Visible = true;
                rptHistorial.Visible = false;
            }
        }


        private List<CitaHistInfo> GetCitasHistDeEjemplo()
        {
            // citas de ejemplo
            var citas = new List<CitaHistInfo>
            {
                new CitaHistInfo {
                    IdCita = 1,
                    NombreEspecialidad = "Traumatología",
                    NombreMedico = "Dr. José Martinez",
                    FechaCita = DateTime.Today.AddDays(-4),
                    DescripcionHorario = "09:00 a.m.",
                    Estado = "Atendido",
                    ObsMedicas = "Sin observaciones."
                },
                new CitaHistInfo {
                    IdCita = 2,
                    NombreEspecialidad = "Odontología General",
                    NombreMedico = "Dr. Pedro Potter",
                    FechaCita = DateTime.Today.AddDays(-1),
                    DescripcionHorario = "15:30 p.m.",
                    Estado = "Atendido",
                    ObsMedicas = "Requiere reajuste y limpieza en 2 meses."
                },
                new CitaHistInfo {
                    IdCita = 3,
                    NombreEspecialidad = "Pediatría",
                    NombreMedico = "Dra. Sofía Gómez",
                    FechaCita = DateTime.Today.AddDays(2),
                    DescripcionHorario = "10:30 a.m.",
                    Estado = "Pagado",
                    ObsMedicas = "Sin observaciones."
                },

                new CitaHistInfo {
                    IdCita = 4,
                    NombreEspecialidad = "Cardiología",
                    NombreMedico = "Dra. Ana López",
                    FechaCita = DateTime.Today.AddDays(-8),
                    DescripcionHorario = "14:00 p.m.",
                    Estado = "Atendido",
                    ObsMedicas = "Exámenes cardiopáticos pendientes de resultado."
                },

                new CitaHistInfo {
                    IdCita = 5,
                    NombreEspecialidad = "Pediatría",
                    NombreMedico = "Dra. Sofía Gómez",
                    FechaCita = DateTime.Today.AddDays(-16),
                    DescripcionHorario = "16:00 p.m.",
                    Estado = "Atendido",
                    ObsMedicas = "Exámenes de sangre pendientes de resultado."
                },
            };

            // Filtrar solo próximas citas 
            return citas.Where(c => c.Estado == "Atendido").ToList();
        }
    }
}