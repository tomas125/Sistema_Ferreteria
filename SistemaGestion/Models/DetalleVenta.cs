namespace SistemaGestion.Models;

public class DetalleVenta
{
    public int Id { get; set; }
    public int VentaId { get; set; }
    public string Producto { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
