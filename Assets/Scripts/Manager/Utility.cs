

using System;

public static class Utility 
{
    public static string FormatNumber(double value)
    {
        if (value < 25)
        {
            if (value % 1 == 0) // Say�n�n tam say� olup olmad���n� kontrol eder
            {
                return value.ToString("0"); // Tam say� ise, ondal�k olmadan yazd�r
            }
            else
            {
                return value.ToString("F2"); // Ondal�k basama�� varsa, iki ondal�k basamak ile yazd�r
            }
        }


        if (value < 1000)
            return ((int)value).ToString(); // 1000'in alt�nda ise say�y� aynen yaz

        if (value < 1000000)
            return (value / 1000.0).ToString("0.#") + "K"; // 1000-9999 aras� i�in '1.2k' format�

        return (value / 1000000.0).ToString("0.#") + "B"; // 10000'in �st� i�in '13.4b' format�
    }
    public static float  ParseFormattedNumber(string formattedValue)
    {
        // Say�n�n 'K' veya 'B' ile mi bitip bitmedi�ini kontrol eder
        if (formattedValue.EndsWith("K", StringComparison.OrdinalIgnoreCase))
        {
            // 'K' harfini kald�r ve kalan say�y� 1000 ile �arp
            if (float.TryParse(formattedValue.Substring(0, formattedValue.Length - 1), out float value))
                return value * 1000;
        }
        else if (formattedValue.EndsWith("B", StringComparison.OrdinalIgnoreCase))
        {
            // 'B' harfini kald�r ve kalan say�y� 1.000.000 ile �arp
            if (float.TryParse(formattedValue.Substring(0, formattedValue.Length - 1), out float value))
                return value * 1000000;
        }
        else
        {
            // 'K' veya 'B' harfi yoksa do�rudan say�y� d�n��t�r
            if (float.TryParse(formattedValue, out float value))
                return value;
        }

        // Format beklenenden farkl�ysa hata durumu olarak 0 d�ner
        throw new FormatException("The format is invalid.");
    }

}
