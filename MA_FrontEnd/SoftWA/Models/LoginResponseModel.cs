// using Newtonsoft.Json;
// using Newtonsoft.Json.Converters; // Para la conversión de enums a string si es necesario

namespace SoftWA.Models
{
    // Se debe definir o referenciar el enum RolUsuario que ya existe en indexLogin.aspx.cs
    // Si está en el mismo namespace o es accesible, no se necesita redefinir.
    // Si no, se puede mover a este archivo o a un archivo de enums compartido.
    // Por ahora, asumimos que SoftWA.RolUsuario está accesible.

    public class LoginResponseModel
    {
        // [JsonProperty("exito")]
        public bool Exito { get; set; }

        // [JsonProperty("mensaje")]
        public string Mensaje { get; set; }

        // [JsonProperty("numeroDoc")]
        public string NumeroDoc { get; set; }

        // [JsonProperty("rol")]
        // Si el backend envía el rol como string, y RolUsuario es un enum C#:
        // [JsonConverter(typeof(StringEnumConverter))] 
        public RolUsuario Rol { get; set; } // Asumiendo que RolUsuario es el enum definido en indexLogin.aspx.cs

        // [JsonProperty("idCuenta")]
        public int? IdCuenta { get; set; } // Hacerlo nullable por si no siempre viene
    }
}
