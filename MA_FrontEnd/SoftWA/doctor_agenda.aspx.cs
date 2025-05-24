using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace SoftWA
{
    // datos que sacaria de la base de datos
    public class Cita
    {
        public int IdCita { get; set; }
        public string NombrePaciente { get; set; } 
        public DateTime FechaCita { get; set; }
        public string DescripcionHorario { get; set; }
        public string NombreEspecialidad { get; set; }
        
    }

    public partial class doctor_agenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int idDoctorLogueado = ObtenerIdDoctorLogueado();
                if (idDoctorLogueado == 0)
                {
                    // redirigir al login si no esta logeado
                    Response.Redirect("~/Login.aspx"); // a la pagina login
                    return;
                }

                // id del doctor
                lblDoctorInfo.Text = $"Doctor ID: {idDoctorLogueado}"; // O buscar su nombre

                // cargar agenda
                CargarAgendaDoctor(idDoctorLogueado);
            }
        }

        // --- SIMULACIÓN: Obtener ID del Doctor Logueado ---
        private int ObtenerIdDoctorLogueado()
        {
            //conexion a base para devolver el doctor
            return 101; 
        }


        private void CargarAgendaDoctor(int idDoctor)
        {
 
            List<Cita> agenda = ObtenerAgendaDoctor(idDoctor);


            if (agenda != null && agenda.Any())
            {
                rptAgendaDoctor.DataSource = agenda.OrderBy(c => c.FechaCita).ThenBy(c => c.DescripcionHorario); 
                rptAgendaDoctor.DataBind();
                phNoAgenda.Visible = false;
                rptAgendaDoctor.Visible = true;
            }
            else
            {
                phNoAgenda.Visible = true;
                rptAgendaDoctor.Visible = false;
            }
        }

        private List<Cita> ObtenerAgendaDoctor(int idDoctor)
        {

            var todasLasCitas = new List<Cita>
            {
                //llamada a citas que id medico=medico login  (datos de ejemplo)
                new Cita { IdCita = 1, NombrePaciente = "Ana Martínez", FechaCita = DateTime.Today.AddDays(0), DescripcionHorario = "09:00 a.m.", NombreEspecialidad = "Cardiología" },
                new Cita { IdCita = 5, NombrePaciente = "Luis Rodríguez", FechaCita = DateTime.Today.AddDays(0), DescripcionHorario = "13:00 p.m.", NombreEspecialidad = "Cardiología" },
                new Cita { IdCita = 6, NombrePaciente = "Elena Gómez", FechaCita = DateTime.Today.AddDays(1), DescripcionHorario = "14:30 p.m.", NombreEspecialidad = "Cardiología" },

                
                new Cita { IdCita = 2, NombrePaciente = "Pedro Ramírez", FechaCita = DateTime.Today.AddDays(0), DescripcionHorario = "11:00 a.m.", NombreEspecialidad = "Dermatología"},
                new Cita { IdCita = 7, NombrePaciente = "Sofía Castillo", FechaCita = DateTime.Today.AddDays(2), DescripcionHorario = "10:30 a.m.", NombreEspecialidad = "Dermatología"},

                 
                 new Cita { IdCita = 8, NombrePaciente = "Juan Viejo", FechaCita = DateTime.Today.AddDays(-1), DescripcionHorario = "08:00 a.m.", NombreEspecialidad = "Cardiología"},
            };

            return todasLasCitas
                    .Where(c => c.FechaCita >= DateTime.Today)
                    .ToList();


        }



        protected void rptAgendaDoctor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idCita = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "MarcarAtendido")
            {
                System.Diagnostics.Debug.WriteLine($"Acción: Marcar Atendido para Cita ID: {idCita}");
                ((LinkButton)e.CommandSource).Enabled = false;
                ((LinkButton)e.CommandSource).Text = "<i class='fa-solid fa-check'></i> Atendido";
                ((LinkButton)e.CommandSource).CssClass = "btn btn-sm btn-success "; // Cambiar estilo
            }
        }

    }
}