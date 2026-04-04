using System.Globalization;

namespace SistemaGestion.Helpers;

public static class CurrencyFormat
{
    /// <summary>Montos en pesos ($), formato regional Argentina.</summary>
    public static CultureInfo Pesos => CultureInfo.GetCultureInfo("es-AR");

    public static string FormatMoney(decimal amount) =>
        amount.ToString("C2", Pesos);
}
