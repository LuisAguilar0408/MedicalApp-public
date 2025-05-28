package softapi;

import org.glassfish.grizzly.http.server.HttpServer;
import org.glassfish.jersey.grizzly2.httpserver.GrizzlyHttpServerFactory;
import org.glassfish.jersey.server.ResourceConfig;
import softapi.config.RestApplication; // Nuestra clase de configuración de JAX-RS

import java.io.IOException;
import java.net.URI;
import java.util.logging.Level;
import java.util.logging.Logger;

public class Main {
    // URI base donde la aplicación Jersey será desplegada.
    // El puerto 8080 es un puerto común para aplicaciones web Java.
    // "/api" es el path base para todos nuestros endpoints REST.
    public static final String BASE_URI = "http://localhost:8080/api";
    private static final Logger LOGGER = Logger.getLogger(Main.class.getName());

    /**
     * Inicia el servidor HTTP Grizzly exponiendo la aplicación JAX-RS.
     * @return HttpServer
     */
    public static HttpServer startServer() {
        // Crea una instancia de ResourceConfig que escanea los recursos JAX-RS definidos en RestApplication
        final ResourceConfig rc = new RestApplication();

        // Crea y arranca una instancia del servidor HTTP Grizzly
        LOGGER.info("Jersey app starting with WADL available at " + BASE_URI + "/application.wadl");
        LOGGER.info("Hit Ctrl-C to stop it...");
        return GrizzlyHttpServerFactory.createHttpServer(URI.create(BASE_URI), rc);
    }

    public static void main(String[] args) {
        try {
            final HttpServer server = startServer();

            // Mantener el servidor vivo hasta que se presione Ctrl-C o se detenga de otra forma
            // System.in.read() podría no ser ideal en todos los entornos, pero funciona para un ejemplo simple.
            // Runtime.getRuntime().addShutdownHook() es una forma más robusta de manejar la parada.
            Runtime.getRuntime().addShutdownHook(new Thread(() -> {
                LOGGER.info("Stopping server...");
                server.shutdownNow();
            }));
            
            // Mantenemos el hilo principal vivo
            Thread.currentThread().join();

        } catch (IOException | InterruptedException ex) {
            LOGGER.log(Level.SEVERE, "Error starting or running server", ex);
        }
    }
}
