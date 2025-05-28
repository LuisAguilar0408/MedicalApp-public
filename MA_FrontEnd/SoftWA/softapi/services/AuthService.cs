using SoftWA.Models; // Para LoginRequestModel, LoginResponseModel, RolUsuario
using System;
using System.Net.Http;
using System.Net.Http.Headers; // Para MediaTypeWithQualityHeaderValue
using System.Text; // Para Encoding
using System.Threading.Tasks; // Para Task
using Newtonsoft.Json; // Para JsonConvert

namespace SoftWA.softapi.services // <<< MODIFICADO
{
    public class AuthService
    {
        // Es mejor usar una instancia estática de HttpClient o una gestionada por IHttpClientFactory.
        // Para Web Forms sin DI fácil, una estática es una opción común, pero debe manejarse con cuidado.
        private static readonly HttpClient client = new HttpClient();
        private const string BackendApiBaseUrl = "http://localhost:8080/api/"; // Puerto del backend Java

        // Constructor estático para inicializar HttpClient una sola vez
        static AuthService()
        {
            client.BaseAddress = new Uri(BackendApiBaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            // Se pueden configurar otros valores por defecto aquí, como Timeout
            // client.Timeout = TimeSpan.FromSeconds(30); // Ejemplo de Timeout
        }

        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequest)
        {
            LoginResponseModel loginResponse = null;
            try
            {
                string jsonRequest = JsonConvert.SerializeObject(loginRequest);
                HttpContent content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(jsonResponse);
                }
                else
                {
                    if (response.Content != null &&
                        response.Content.Headers.ContentType != null &&
                        response.Content.Headers.ContentType.MediaType == "application/json")
                    {
                        string errorJson = await response.Content.ReadAsStringAsync();
                        loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(errorJson);
                        if (loginResponse != null && loginResponse.Mensaje == null) // Si la deserialización no produjo un mensaje
                        {
                            loginResponse.Mensaje = $"Error del servidor: {response.StatusCode}";
                        }
                        else if (loginResponse == null) // Si la deserialización falló por completo para el error
                        {
                             loginResponse = new LoginResponseModel
                            {
                                Exito = false,
                                Mensaje = $"Error del servidor: {response.StatusCode}. No se pudo deserializar el cuerpo del error."
                            };
                        }
                    }
                    else
                    {
                        loginResponse = new LoginResponseModel
                        {
                            Exito = false,
                            Mensaje = $"Error: {response.ReasonPhrase} (Código: {response.StatusCode})"
                        };
                    }
                }
            }
            catch (HttpRequestException e)
            {
                loginResponse = new LoginResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error de conexión: {e.Message}. Asegúrate de que el backend Java esté ejecutándose en {BackendApiBaseUrl}."
                };
            }
            catch (JsonException e) // Captura excepciones de Newtonsoft.Json
            {
                loginResponse = new LoginResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error de formato JSON: {e.Message}."
                };
            }
            catch (Exception e) // Otras excepciones
            {
                loginResponse = new LoginResponseModel
                {
                    Exito = false,
                    Mensaje = $"Error inesperado: {e.Message}"
                };
            }

            // Asegurarse de que nunca devolvemos null, sino un objeto con Exito = false.
            if (loginResponse == null)
            {
                loginResponse = new LoginResponseModel { Exito = false, Mensaje = "No se pudo procesar la respuesta del servidor." };
            }

            return loginResponse;
        }
    }
}
