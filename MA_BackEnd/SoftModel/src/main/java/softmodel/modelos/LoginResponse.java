package softmodel.modelos;

import softmodel.util.Rol; // Asegúrate que esta importación sea correcta según la ubicación de Rol.java

public class LoginResponse {
    private boolean exito;
    private String mensaje;
    private String numeroDoc;
    private Rol rol; // Usar el enum Rol existente
    private Integer idCuenta; // Opcional: podría ser útil para el frontend

    // Constructor por defecto
    public LoginResponse() {
    }

    public LoginResponse(boolean exito, String mensaje, String numeroDoc, Rol rol, Integer idCuenta) {
        this.exito = exito;
        this.mensaje = mensaje;
        this.numeroDoc = numeroDoc;
        this.rol = rol;
        this.idCuenta = idCuenta;
    }
    
    public LoginResponse(boolean exito, String mensaje) {
        this.exito = exito;
        this.mensaje = mensaje;
    }

    // Getters y Setters
    public boolean isExito() {
        return exito;
    }

    public void setExito(boolean exito) {
        this.exito = exito;
    }

    public String getMensaje() {
        return mensaje;
    }

    public void setMensaje(String mensaje) {
        this.mensaje = mensaje;
    }

    public String getNumeroDoc() {
        return numeroDoc;
    }

    public void setNumeroDoc(String numeroDoc) {
        this.numeroDoc = numeroDoc;
    }

    public Rol getRol() {
        return rol;
    }

    public void setRol(Rol rol) {
        this.rol = rol;
    }

    public Integer getIdCuenta() {
        return idCuenta;
    }

    public void setIdCuenta(Integer idCuenta) {
        this.idCuenta = idCuenta;
    }
}
