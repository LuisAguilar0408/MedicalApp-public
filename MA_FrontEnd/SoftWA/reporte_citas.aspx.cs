using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SoftWA
{
    public partial class reporte_citas : System.Web.UI.Page
    {
        protected class CitaAtendida
        {
            public int IdCita { get; set; }
            public string NombrePaciente { get; set; }
            public string Especialidad { get; set; }
            public int IdDoctor { get; set; }
            public string NombreDoctor { get; set; }
            public DateTime FechaCita { get; set; }
            public string Horario { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatosCitas();
            }
        }

        private void CargarDatosCitas()
        {
            // datos ejemplo
            List<CitaAtendida> listaCitas = GenerarDatosMuestra(20);

            lvCitas.DataSource = listaCitas;
            lvCitas.DataBind();

            CalcularYMostrarEstadisticas(listaCitas); 
        }

        // datos ejemplo
        private List<CitaAtendida> GenerarDatosMuestra(int cantidad)
        {
            var lista = new List<CitaAtendida>();
            var rnd = new Random();
            var nombresPacientes = new[] { "Ana García", "Luis Martínez", "María Rodríguez", "José Hernández", "Carmen López", "Javier Pérez", "Isabel Gómez", "Manuel Sánchez", "Laura Díaz", "Pedro Moreno" };
            var especialidades = new[] { "Cardiología", "Dermatología", "Pediatría", "Ginecología", "Medicina General", "Oftalmología" };
            var doctores = new Dictionary<string, List<Tuple<int, string>>>() {
                { "Cardiología", new List<Tuple<int, string>> { Tuple.Create(101, "Dr. Carlos Ruiz"), Tuple.Create(102, "Dra. Elena Castillo") } },
                { "Dermatología", new List<Tuple<int, string>> { Tuple.Create(201, "Dra. Sofia Vargas") } },
                { "Pediatría", new List<Tuple<int, string>> { Tuple.Create(301, "Dr. Andrés Molina"), Tuple.Create(302, "Dra. Paula Navarro") } },
                { "Ginecología", new List<Tuple<int, string>> { Tuple.Create(401, "Dra. Irene Gil") } },
                { "Medicina General", new List<Tuple<int, string>> { Tuple.Create(501, "Dr. Ricardo Soler"), Tuple.Create(502, "Dra. Beatriz Alonso") } },
                { "Oftalmología", new List<Tuple<int, string>> { Tuple.Create(601, "Dr. Fernando Sáez") } }
            };
            var horarios = new[] { "09:00 a.m.", "09:30 a.m.", "10:00 a.m.", "10:30 a.m.", "11:00 a.m.", "11:30 a.m.", "12:00 a.m." };

            for (int i = 1; i <= cantidad; i++)
            {
                string esp = especialidades[rnd.Next(especialidades.Length)];
                var doctoresEnEspecialidad = doctores[esp];
                var doctorSeleccionado = doctoresEnEspecialidad[rnd.Next(doctoresEnEspecialidad.Count)];
                DateTime fecha = DateTime.Today.AddDays(-rnd.Next(1, 30));
                string hora = horarios[rnd.Next(horarios.Length)];
                int minutos = rnd.Next(0, 2) * 30;

                lista.Add(new CitaAtendida
                {
                    IdCita = 1000 + i,
                    NombrePaciente = nombresPacientes[rnd.Next(nombresPacientes.Length)],
                    Especialidad = esp,
                    IdDoctor = doctorSeleccionado.Item1,
                    NombreDoctor = doctorSeleccionado.Item2,
                    FechaCita = new DateTime(fecha.Year, fecha.Month, fecha.Day),
                    Horario = hora
                });
            }
            return lista.OrderByDescending(c => c.FechaCita).ToList();
        }
        private void CalcularYMostrarEstadisticas(List<CitaAtendida> listaCitas)
        {
            if (listaCitas == null || !listaCitas.Any())
            {
                lblMasSolicitadaEspecialidad.Text = "No hay datos disponibles.";
                lblMasSolicitadoDoctor.Text = "No hay datos disponibles.";
                return;
            }

            var topEspecialidad = listaCitas
                .GroupBy(c => c.Especialidad)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            var topDoctor = listaCitas
                .GroupBy(c => c.NombreDoctor)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .FirstOrDefault();

            if (topEspecialidad != null)
            {
                lblMasSolicitadaEspecialidad.Text = $"{topEspecialidad.Name} ({topEspecialidad.Count} {(topEspecialidad.Count == 1 ? "cita" : "citas")})";
            }
            else
            {
                lblMasSolicitadaEspecialidad.Text = "N/A";
            }

            if (topDoctor != null)
            {
                lblMasSolicitadoDoctor.Text = $"{topDoctor.Name} ({topDoctor.Count} {(topDoctor.Count == 1 ? "cita" : "citas")})";
            }
            else
            {
                lblMasSolicitadoDoctor.Text = "N/A";
            }
        }
    }
}