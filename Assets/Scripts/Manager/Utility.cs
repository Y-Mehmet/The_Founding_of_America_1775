

using System;

public static class Utility 
{
    public static string FormatNumber(double value)
    {
        if (value < 25)
        {
            if (value % 1 == 0) // Sayýnýn tam sayý olup olmadýðýný kontrol eder
            {
                return value.ToString("0"); // Tam sayý ise, ondalýk olmadan yazdýr
            }
            else
            {
                return value.ToString("F2"); // Ondalýk basamaðý varsa, iki ondalýk basamak ile yazdýr
            }
        }


        if (value < 1000)
            return ((int)value).ToString(); // 1000'in altýnda ise sayýyý aynen yaz

        if (value < 1000000)
            return (value / 1000.0).ToString("0.#") + "K"; // 1000-9999 arasý için '1.2k' formatý

        return (value / 1000000.0).ToString("0.#") + "M"; // 10000'in üstü için '13.4m' formatý
    }
    public static float  ParseFormattedNumber(string formattedValue)
    {
        // Sayýnýn 'K' veya 'B' ile mi bitip bitmediðini kontrol eder
        if (formattedValue.EndsWith("K", StringComparison.OrdinalIgnoreCase))
        {
            // 'K' harfini kaldýr ve kalan sayýyý 1000 ile çarp
            if (float.TryParse(formattedValue.Substring(0, formattedValue.Length - 1), out float value))
                return value * 1000;
        }
        else if (formattedValue.EndsWith("B", StringComparison.OrdinalIgnoreCase))
        {
            // 'B' harfini kaldýr ve kalan sayýyý 1.000.000 ile çarp
            if (float.TryParse(formattedValue.Substring(0, formattedValue.Length - 1), out float value))
                return value * 1000000;
        }
        else
        {
            // 'K' veya 'B' harfi yoksa doðrudan sayýyý dönüþtür
            if (float.TryParse(formattedValue, out float value))
                return value;
        }

        // Format beklenenden farklýysa hata durumu olarak 0 döner
        throw new FormatException("The format is invalid.");
    }

}
