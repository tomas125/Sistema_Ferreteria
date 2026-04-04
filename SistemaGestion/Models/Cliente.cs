namespace SistemaGestion.Models;

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public string Pais { get; set; } = "Argentina";
    public string? FechaAlta { get; set; }
}
