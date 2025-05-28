// Se puede añadir el namespace que corresponda al proyecto, ej: SoftWA.Models
// Si Newtonsoft.Json es necesario para atributos, se añadiría:
// using Newtonsoft.Json;

namespace SoftWA.Models // Asumiendo que este es el namespace raíz del proyecto
{
    public class LoginRequestModel
    {
        // Para Newtonsoft.Json, si los nombres de propiedad en Java y C# no coinciden exactamente
        // (y se quiere respetar la capitalización de Java, ej. numeroDoc), se usaría:
        // [JsonProperty("numeroDoc")] 
        public string NumeroDoc { get; set; }

        // [JsonProperty("contrasenha")]
        public string Contrasenha { get; set; }
    }
}
