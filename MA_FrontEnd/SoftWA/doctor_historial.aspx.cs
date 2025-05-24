using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftWA
{
    public class CitaHist
    {
        public int IdCita { get; set; }
        public DateTime FechaCita { get; set; } 
        public string NombrePaciente { get; set; }
        public string DescripcionHorario { get; set; }
        public string NombreEspecialidad { get; set; }

    }
    public partial class doctor_historial : System.Web.UI.Page
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
                CargarHistorialDoctor(idDoctorLogueado);
            }
        }

        // --- SIMULACIÓN: Obtener ID del Doctor Logueado ---
        private int ObtenerIdDoctorLogueado()
        {
            //conexion a base para devolver el doctor
            return 101;
        }

        private void CargarHistorialDoctor(int idDoctor)
        {

            List<CitaHist> historial = ObtenerHistorialDoctor(idDoctor);


            if (historial != null && historial.Any())
            {
                rptHistDoctor.DataSource = historial.OrderBy(c => c.DescripcionHorario);
                rptHistDoctor.DataBind();
                phNoHistorial.Visible = false;
                rptHistDoctor.Visible = true;
            }
            else
            {
                phNoHistorial.Visible = true;
                rptHistDoctor.Visible = false;
            }
        }

        private List<CitaHist> ObtenerHistorialDoctor(int idDoctor)
        {

            var todasLasCitas = new List<CitaHist>
            {
                //llamada a citas que id medico=medico login  (datos de ejemplo)
                new CitaHist { IdCita = 1, NombrePaciente = "Ana Martínez", FechaCita = DateTime.Today.AddDays(-2), DescripcionHorario = "10:00 a.m.", NombreEspecialidad = "Cardiología" },
                new CitaHist { IdCita = 5, NombrePaciente = "Luis Rodríguez", FechaCita = DateTime.Today.AddDays(-8), DescripcionHorario = "12:30 p.m.", NombreEspecialidad = "Cardiología" },
                new CitaHist { IdCita = 6, NombrePaciente = "Elena Gómez", FechaCita = DateTime.Today.AddDays(-11), DescripcionHorario = "08:00 a.m.", NombreEspecialidad = "Cardiología" },


                new CitaHist { IdCita = 2, NombrePaciente = "Pedro Ramírez", FechaCita = DateTime.Today.AddDays(-5), DescripcionHorario = "14:30 p.m.", NombreEspecialidad = "Dermatología"},
                new CitaHist { IdCita = 7, NombrePaciente = "Sofía Castillo", FechaCita = DateTime.Today.AddDays(-3), DescripcionHorario = "15:00 p.m.", NombreEspecialidad = "Dermatología"},


                 new CitaHist { IdCita = 8, NombrePaciente = "Juan Viejo", FechaCita = DateTime.Today.AddDays(-1), DescripcionHorario = "16:30 p.m.", NombreEspecialidad = "Cardiología"},
            };

            return todasLasCitas.ToList();


        }

        protected void rptHistDoctor_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idCita = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "VerResultados")
            {
                ((LinkButton)e.CommandSource).Enabled = true;
                ((LinkButton)e.CommandSource).Text = "<i class='fa-solid fa-clipboard-list'></i> Ver Resultados";
                ((LinkButton)e.CommandSource).CssClass = "btn btn-primary ";
            }
        }
    }
}