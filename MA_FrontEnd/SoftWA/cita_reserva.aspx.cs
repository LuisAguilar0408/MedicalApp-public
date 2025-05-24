using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftWA
{
    public partial class cita_reserva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // para cargar solo la primera vez q se abre
            {
                CargarEspecialidades();
                ddlMedico.Enabled = false;
                divHorarios.Visible = false; 
                lblFechaSeleccionadaInfo.Visible = false;
            }
        }

        private void CargarEspecialidades()
        {
            //aqui se agregaran las especialidades en el ddlEspecialidades desde la base de datos despues

            var especialidadesEjemplo = new List<object>
            {
                new { IdEspecialidad = "1", NombreEspecialidad = "Cardiología" },
                new { IdEspecialidad = "2", NombreEspecialidad = "Dermatología" },
                new { IdEspecialidad = "3", NombreEspecialidad = "Pediatría" },
                new { IdEspecialidad = "4", NombreEspecialidad = "Traumatología" },
                new { IdEspecialidad = "5", NombreEspecialidad = "Odontología General" }
            };


            ddlEspecialidad.DataSource = especialidadesEjemplo;
            ddlEspecialidad.DataTextField = "NombreEspecialidad"; // nombre para el texto
            ddlEspecialidad.DataValueField = "IdEspecialidad";   // nombre para el valor
            ddlEspecialidad.DataBind();
        }

        protected void ddlEspecialidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            // se ejecuta cuando se cambia la seleccion del primero
            CargarMedicos();
            LimpiarSeleccionFechaYHorarios();
        }

        protected void ddlMedico_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            LimpiarSeleccionFechaYHorarios();
            if (calFechaCita.SelectedDate != DateTime.MinValue && calFechaCita.SelectedDate != new DateTime(0001, 1, 1))
            {
                //CargarHorariosDisponibles(calFechaCita.SelectedDate, ddlMedico.SelectedValue);
            }
        }

        private void CargarMedicos()
        {
            ddlMedico.Items.Clear(); 
            ddlMedico.Items.Add(new ListItem("-- Seleccione un médico --", "")); // Añadir ítem por defecto

            string especialidadSeleccionadaId = ddlEspecialidad.SelectedValue;
            // Cargar médicos según la especialidad seleccionada
            if (!string.IsNullOrEmpty(especialidadSeleccionadaId))
            {
                var todosLosMedicos = GetMedicosPorEspecialidad();
                if (todosLosMedicos.ContainsKey(especialidadSeleccionadaId))
                {
                    var medicosDeEspecialidad = todosLosMedicos[especialidadSeleccionadaId];
                    ddlMedico.DataSource = medicosDeEspecialidad;
                    ddlMedico.DataTextField = "NombreMedico";
                    ddlMedico.DataValueField = "IdMedico";
                    ddlMedico.DataBind();
                }
                
            }
            
            ddlMedico.Enabled = !string.IsNullOrEmpty(especialidadSeleccionadaId) && ddlMedico.Items.Count > 1;
        }

        //esta parte del codigo sera util mas adelante cuando el calendario tenga funcionalidad en base a datos

        protected void calFechaCita_DayRender(object sender, DayRenderEventArgs e)
        {

        //    if (e.Day.Date < DateTime.Today)
        //    {
        //        e.Day.IsSelectable = false;
        //        e.Cell.ToolTip = "Fecha no disponible";
        //        e.Cell.ForeColor = System.Drawing.Color.Gray;
        //    }

        //    if (string.IsNullOrEmpty(ddlMedico.SelectedValue) || ddlMedico.SelectedValue == "")
        //    {
        //        e.Day.IsSelectable = false;
        //        if (e.Day.Date >= DateTime.Today) // Solo aplicar tooltip a fechas futuras
        //        {
        //            e.Cell.ToolTip = "Seleccione un médico primero";
        //        }
        //    }
        }

        //funcion para mostrar resultados en base a filtros con mensajes de error por falta de inputs
        //idea de funcionalidad

        protected void calFechaCita_SelectionChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMedico.SelectedValue) || ddlMedico.SelectedValue == "")
            {
                // Si no hay médico seleccionado, no hacemos nada o mostramos un mensaje
                lblErrorHorario.Text = "Por favor, seleccione una especialidad y un médico primero.";
                lblErrorHorario.Visible = true;
                divHorarios.Visible = false;
                lblFechaSeleccionadaInfo.Visible = false;
                
                calFechaCita.SelectedDates.Clear();
                return;
            }

            DateTime fechaSeleccionada = calFechaCita.SelectedDate;
            lblFechaSeleccionadaInfo.Text = "Fecha seleccionada: " + fechaSeleccionada.ToString("dddd, dd 'de' MMMM 'de' yyyy", new CultureInfo("es-ES"));
            lblFechaSeleccionadaInfo.Visible = true;

            //CargarHorariosDisponibles(fechaSeleccionada, ddlMedico.SelectedValue);
        }

        private void LimpiarSeleccionFechaYHorarios()
        {
            calFechaCita.SelectedDates.Clear(); 
            lblFechaSeleccionadaInfo.Visible = false;
            divHorarios.Visible = false;
            rblHorarios.Items.Clear();
            lblErrorHorario.Visible = false;
        }


        private static Dictionary<string, List<object>> GetMedicosPorEspecialidad()
        {// ejemplo hasta que se conecte en la base de datos
            var medicos = new Dictionary<string, List<object>>();

            medicos.Add("1", new List<object> // Cardiología (IdEspecialidad = "1")
            {
                new { IdMedico = "101", NombreMedico = "Dr. Juan Pérez" },
                new { IdMedico = "102", NombreMedico = "Dra. Ana López" }
            });

            medicos.Add("2", new List<object> // Dermatología (IdEspecialidad = "2")
            {
                new { IdMedico = "201", NombreMedico = "Dr. Carlos García" },
                new { IdMedico = "202", NombreMedico = "Dra. Laura Martín" }
            });

            medicos.Add("3", new List<object> // Pediatría (IdEspecialidad = "3")
            {
                new { IdMedico = "301", NombreMedico = "Dra. Sofía Gómez" }
            });
            

            return medicos;
        }

        public class HorarioDisponible
        {
            public string Id { get; set; } 
            public string Descripcion { get; set; } 
        }


        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {   
            string especialidadId = ddlEspecialidad.SelectedValue;
            string medicoId = ddlMedico.SelectedValue;
            DateTime fechaCita = calFechaCita.SelectedDate;
            string horarioSeleccionado = rblHorarios.SelectedValue; 

            // Validaciones
            if (string.IsNullOrEmpty(especialidadId)) { /* Mensaje error */ return; }
            if (string.IsNullOrEmpty(medicoId)) { /* Mensaje error */ return; }
            if (fechaCita == DateTime.MinValue || fechaCita == new DateTime(0001, 1, 1)) { /* Mensaje error: "Seleccione una fecha" */ return; }
            if (string.IsNullOrEmpty(horarioSeleccionado)) { /* Mensaje error: "Seleccione un horario" */ return; }
            Response.Redirect("index.aspx");
        }
    }
}