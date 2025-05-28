package softmodel.modelos;

public class LoginRequest {
    private String numeroDoc;
    private String contrasenha;

    // Constructor por defecto (necesario para deserialización JSON)
    public LoginRequest() {
    }

    public LoginRequest(String numeroDoc, String contrasenha) {
        this.numeroDoc = numeroDoc;
        this.contrasenha = contrasenha;
    }

    // Getters y Setters
    public String getNumeroDoc() {
        return numeroDoc;
    }

    public void setNumeroDoc(String numeroDoc) {
        this.numeroDoc = numeroDoc;
    }

    public String getContrasenha() {
        return contrasenha;
    }

    public void setContrasenha(String contrasenha) {
        this.contrasenha = contrasenha;
    }
}
