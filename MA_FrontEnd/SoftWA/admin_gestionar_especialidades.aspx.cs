using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftWA
{
    public partial class gestionar_especialidades : System.Web.UI.Page
    {
        private static List<Especialidades> listaEspecialidades;

        public class Especialidades
        {
            public int ID { get; set; }
            public string NombreEspecialidad { get; set; }
            public double PrecioConsulta { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosEspecialidades();
                BindEspecialidades();
            }
        }

        private void CargarDatosEspecialidades()
        {
            if (listaEspecialidades == null || !listaEspecialidades.Any())
            {
                listaEspecialidades = new List<Especialidades>
            {
                new Especialidades { ID = 1, NombreEspecialidad = "Traumatología ", PrecioConsulta = 59.90 },
                new Especialidades { ID = 2, NombreEspecialidad = "Cardiología", PrecioConsulta = 59.90 },
                new Especialidades { ID = 3, NombreEspecialidad = "Pediatría", PrecioConsulta = 49.90 },
                new Especialidades { ID = 4, NombreEspecialidad = "Medicina General", PrecioConsulta = 49.90 }
            };
            }
        }

        private void BindEspecialidades()
        {
            rptEspecialidades.DataSource = listaEspecialidades;
            rptEspecialidades.DataBind();
            // Controlar la visibilidad del panel de "no hay médicos"
            if (listaEspecialidades == null || !listaEspecialidades.Any())
            {
                //para que muestre una lista vacia pero con un footer para agregar nuevo médico
                phNoEspecialidad.Visible = true;
            }
            else
            {
                phNoEspecialidad.Visible = false;
            }
            //modEspecialidades.Update(); // Asegura que el panel se refresque
        }

        protected void rptMedicos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            // Ocultar el panel de "no hay médicos" al hacer clic en un botón
            phNoEspecialidad.Visible = false;
            int medicoId = 0;
            // CommandArgument solo será válido si viene de un ítem, no del footer.
            if (e.CommandArgument != null && !string.IsNullOrEmpty(e.CommandArgument.ToString()))
            {
                medicoId = Convert.ToInt32(e.CommandArgument);
            }
            //modMedicos.Update();
        }

        private bool EliminarMedicoDeLista(int medicoId)
        {
            var medicoAEliminar = listaEspecialidades.FirstOrDefault(m => m.ID == medicoId);
            if (medicoAEliminar != null)
            {
                listaEspecialidades.Remove(medicoAEliminar);
                return true;
            }
            return false;
        }

        // Manejador para el LinkButton "Agregar nuevo médico"
        protected void lnkAgregarNuevaEspecialidad_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Acción: Agregar nueva especialidad. Implementar lógica de formulario/modal.');", true);
        }
    }
}
