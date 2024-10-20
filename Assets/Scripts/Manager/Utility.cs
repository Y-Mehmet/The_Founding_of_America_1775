

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

        return (value / 1000.0).ToString("0") + "B"; // 10000'in üstü için '13.4k' formatý
    }
}
