package softapi.filters;

import jakarta.ws.rs.container.ContainerRequestContext;
import jakarta.ws.rs.container.ContainerResponseContext;
import jakarta.ws.rs.container.ContainerResponseFilter;
import jakarta.ws.rs.core.Response; // Importación que faltaba
import jakarta.ws.rs.ext.Provider;

import java.io.IOException;

@Provider // Esta anotación lo registra automáticamente si el escaneo de proveedores está habilitado, o lo podemos registrar manualmente.
public class CorsFilter implements ContainerResponseFilter {

    @Override
    public void filter(ContainerRequestContext requestContext, ContainerResponseContext responseContext)
            throws IOException {
        // Para desarrollo, podríamos usar "*", pero para producción es mejor especificar el dominio del frontend.
        // Ejemplo: String frontendOrigin = "http://localhost:4200"; // Puerto típico de Angular en desarrollo
        // String frontendOrigin = "http://localhost:PUERTO_IIS_EXPRESS"; // Reemplazar con el puerto real
        String origin = requestContext.getHeaderString("Origin");
        
        // Permitir cualquier origen por ahora para simplificar, pero esto debería restringirse en producción.
        responseContext.getHeaders().add("Access-Control-Allow-Origin", origin != null ? origin : "*");
        responseContext.getHeaders().add("Access-Control-Allow-Credentials", "true");
        responseContext.getHeaders().add("Access-Control-Allow-Headers", "origin, content-type, accept, authorization");
        responseContext.getHeaders().add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, HEAD");

        // Para las solicitudes OPTIONS (pre-vuelo CORS)
        if (requestContext.getMethod().equalsIgnoreCase("OPTIONS")) {
            responseContext.setStatus(Response.Status.OK.getStatusCode());
        }
    }
}
