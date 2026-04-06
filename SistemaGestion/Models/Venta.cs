namespace SistemaGestion.Models;

public class Venta
{
    public int Id { get; set; }
    public int? ClienteId { get; set; }
    public string? FechaVenta { get; set; }
    public decimal Total { get; set; }
    public string FormaPago { get; set; } = string.Empty;
    public string Estado { get; set; } = "FINALIZADO";
    public string? Observaciones { get; set; }
    public string? ClienteNombre { get; set; }
}
