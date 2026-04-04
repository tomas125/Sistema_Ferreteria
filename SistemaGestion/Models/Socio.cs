namespace SistemaGestion.Models;

public class Socio
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Rol { get; set; } = string.Empty;
    public decimal Porcentaje { get; set; }
}
