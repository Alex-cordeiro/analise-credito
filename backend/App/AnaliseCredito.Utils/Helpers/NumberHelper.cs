using System.Globalization;

namespace AnaliseCredito.Utils.Helpers;

public static class NumberHelper
{
    public static decimal ParseCurrency(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return 0m;
        
        input = input.Replace("R$", "")
            .Trim()
            .Replace(".", "") 
            .Replace(",", ".");
        
        if (decimal.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
            return value;

        return 0m;
    }
}