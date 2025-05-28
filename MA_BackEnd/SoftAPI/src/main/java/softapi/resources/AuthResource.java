package softapi.resources;

import jakarta.ws.rs.Consumes;
import jakarta.ws.rs.POST;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import softbusiness.business.LogIn;
import softbusiness.business.CuentaBO; // Para obtener el ID de la cuenta
import softmodel.modelos.LoginRequest;
import softmodel.modelos.LoginResponse;
import softmodel.modelos.CuentaDTO; // Necesario para obtener el rol de CuentaBO (indirectamente)
import softmodel.util.Rol; // Para el switch y la respuesta

@Path("/auth")
public class AuthResource {

    private LogIn logInService;

    public AuthResource() {
        this.logInService = new LogIn(); // Instanciamos el servicio de lógica de negocio
    }

    @POST
    @Path("/login")
    @Consumes(MediaType.APPLICATION_JSON)
    @Produces(MediaType.APPLICATION_JSON)
    public Response login(LoginRequest request) {
        if (request == null || request.getNumeroDoc() == null || request.getNumeroDoc().trim().isEmpty() ||
            request.getContrasenha() == null || request.getContrasenha().isEmpty()) {
            LoginResponse errorResponse = new LoginResponse(false, "Número de documento y contraseña son requeridos.");
            return Response.status(Response.Status.BAD_REQUEST).entity(errorResponse).build();
        }

        try {
            CuentaBO cuentaAutenticada = logInService.iniciarSesion(request.getNumeroDoc(), request.getContrasenha());

            if (cuentaAutenticada != null) {
                // Para obtener el Rol, necesitamos acceder al CuentaDTO original o tener un método en CuentaBO.
                // LogIn.iniciarSesion devuelve un tipo específico (CuentaAdmin, CuentaMedico, CuentaPaciente).
                // Asumiremos que podemos obtener el Rol y el DNI/idCuenta de cuentaAutenticada.
                // Esta parte podría necesitar ajustarse según cómo CuentaBO expone esta información.
                // Por ahora, intentaremos deducirlo o simularlo.

                Integer idCuenta = cuentaAutenticada.getIdCuenta(); // Método público necesario en CuentaBO o subclases
                Rol rolUsuario;
                
                // Determinar el rol basado en la instancia de CuentaBO
                // Esto es una forma de hacerlo si no hay un getter directo para Rol en CuentaBO
                // o si el rol se obtiene a través del CuentaDTO que lo originó.
                // La clase LogIn ya hace un switch similar, idealmente el rol debería ser una propiedad de CuentaBO.
                // Para simplificar, asumimos que LogIn.java ya asignó el rol correctamente y que
                // CuentaBO o sus subclases podrían tener un método getRol() o similar.
                // Si CuentaBO no tiene un getRol(), podríamos necesitar modificar CuentaBO o LogIn.
                // Por ahora, vamos a simular que lo obtenemos.
                // El método iniciarSesion en LogIn.java retorna instancias específicas como CuentaAdmin, CuentaMedico, etc.
                // Podríamos usar instanceof, pero es mejor si CuentaBO tiene un método para obtener el rol.

                // Solución temporal: Re-obtener el DTO para obtener el rol. No es lo ideal.
                // Lo ideal sería que CuentaBO tuviera un método getRol().
                // O que LogIn devolviera directamente un objeto con toda la info necesaria.
                // Dado que LogIn usa CuentaDAO para buscar CuentaDTO, y este tiene el rol...
                // Vamos a asumir que CuentaBO o sus subclases tienen un método getRol()
                // que fue establecido durante su creación en LogIn.java.
                // Si no, esto fallará en tiempo de compilación o ejecución y necesitaremos refactorizar CuentaBO.
                
                // Simulando que CuentaBO tiene un método getRol()
                // Esto es una suposición fuerte. Si no existe, el Worker debe indicarlo.
                // Por ejemplo, si CuentaAdmin extends CuentaBO y tiene un public Rol getRol() { return Rol.ADMINISTRADOR; }

                if (cuentaAutenticada instanceof softbusiness.business.CuentaAdmin) {
                    rolUsuario = Rol.ADMINISTRADOR;
                } else if (cuentaAutenticada instanceof softbusiness.business.CuentaMedico) {
                    rolUsuario = Rol.MEDICO;
                } else if (cuentaAutenticada instanceof softbusiness.business.CuentaPaciente) {
                    rolUsuario = Rol.PACIENTE;
                } else {
                    // Esto no debería ocurrir si LogIn.java funciona como se espera
                    LoginResponse errorResponse = new LoginResponse(false, "Error interno: Rol de usuario desconocido.");
                    return Response.status(Response.Status.INTERNAL_SERVER_ERROR).entity(errorResponse).build();
                }

                LoginResponse successResponse = new LoginResponse(true, "Login exitoso", request.getNumeroDoc(), rolUsuario, idCuenta);
                return Response.ok(successResponse).build();
            } else {
                LoginResponse errorResponse = new LoginResponse(false, "Credenciales inválidas. Por favor, intente de nuevo.");
                return Response.status(Response.Status.UNAUTHORIZED).entity(errorResponse).build();
            }
        } catch (Exception e) {
            // Loggear la excepción aquí
            e.printStackTrace(); // Para depuración, reemplazar con un logger adecuado
            LoginResponse errorResponse = new LoginResponse(false, "Error interno del servidor: " + e.getMessage());
            return Response.status(Response.Status.INTERNAL_SERVER_ERROR).entity(errorResponse).build();
        }
    }
}
