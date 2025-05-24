using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftWA
{
    
    public class CitaInfo
    {
        public int IdCita { get; set; } 
        public string NombreEspecialidad { get; set; }
        public string NombreMedico { get; set; }
        public DateTime FechaCita { get; set; }
        public string DescripcionHorario { get; set; } 
    }

    public partial class proximas_citas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProximasCitas();
            }
        }

        private void CargarProximasCitas()
        {
            

            List<CitaInfo> listaDeCitas = GetCitasDeEjemplo(); // datos en duro

            if (listaDeCitas != null && listaDeCitas.Any())
            {
                rptProximasCitas.DataSource = listaDeCitas.OrderBy(c => c.FechaCita).ThenBy(c => c.DescripcionHorario);
                rptProximasCitas.DataBind();
                phNoCitas.Visible = false;
                rptProximasCitas.Visible = true;
            }
            else
            {
                phNoCitas.Visible = true;
                rptProximasCitas.Visible = false;
            }
        }


        private List<CitaInfo> GetCitasDeEjemplo()
        {
            // citas de ejemplo
            var citas = new List<CitaInfo>
            {
                new CitaInfo {
                    IdCita = 1,
                    NombreEspecialidad = "Cardiología",
                    NombreMedico = "Dr. Juan Pérez",
                    FechaCita = DateTime.Today.AddDays(3),
                    DescripcionHorario = "10:00 a.m."
                },
                new CitaInfo {
                    IdCita = 2,
                    NombreEspecialidad = "Dermatología",
                    NombreMedico = "Dr. Carlos García",
                    FechaCita = DateTime.Today.AddDays(1), // Esta aparecerá antes
                    DescripcionHorario = "19:00 p.m."
                },
                 new CitaInfo {
                    IdCita = 3,
                    NombreEspecialidad = "Pediatría",
                    NombreMedico = "Dra. Sofía Gómez",
                    FechaCita = DateTime.Today.AddDays(7),
                    DescripcionHorario = "8:30 a.m."
                },
               
                 new CitaInfo {
                     IdCita = 4,
                     NombreEspecialidad = "Cardiología",
                     NombreMedico = "Dra. Ana López",
                     FechaCita = DateTime.Today.AddDays(-2), // Fecha pasada
                     DescripcionHorario = "14:00 p.m."
                 }
            };

            // Filtrar solo próximas citas 
            return citas.Where(c => c.FechaCita >= DateTime.Today).ToList();
        }


        protected void rptProximasCitas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Cancelar")
            {
                //logica de cancelar cita
                int idCitaSeleccionada = Convert.ToInt32(e.CommandArgument);

                
            }
            
        }

    }
}