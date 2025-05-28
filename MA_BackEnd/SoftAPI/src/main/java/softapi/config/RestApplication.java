package softapi.config;

import org.glassfish.jersey.server.ResourceConfig;
import org.glassfish.jersey.media.multipart.MultiPartFeature; // Si necesitas subida de archivos en el futuro
import org.glassfish.jersey.jackson.JacksonFeature; // Para la serialización/deserialización JSON con Jackson
import softapi.resources.AuthResource; // El recurso de autenticación
import softapi.filters.CorsFilter; // <<<--- AÑADIR ESTA IMPORTACIÓN

public class RestApplication extends ResourceConfig {
    public RestApplication() {
        // Registrar recursos JAX-RS (nuestras clases @Path)
        register(AuthResource.class);

        // Registrar JacksonFeature para soportar JSON.
        // Jersey usa Jackson por defecto si está en el classpath, pero registrarlo explícitamente es una buena práctica.
        register(JacksonFeature.class);
        
        // Registrar MultiPartFeature si se planea soportar subida de archivos.
        // register(MultiPartFeature.class);

        // Registrar el filtro CORS  // <<<--- AÑADIR ESTA LÍNEA
        register(CorsFilter.class);
    }
}
