using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseNumber : MonoBehaviour
{
    
    public TMP_InputField inputField;



    public void IncreaseValue()
    {
        // Input Field'in dolu olup olmadýðýný kontrol et
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.text = "0"; // Eðer boþsa baþlangýç deðeri olarak 0 ata
        }

        // Input Field'daki deðeri al ve integer'a çevirmeye çalýþ
        int currentValue;
        if (int.TryParse(inputField.text, out currentValue))
        {
            // Deðeri 10 artýr
            currentValue += 10;

            // Artýrýlmýþ deðeri tekrar Input Field'a yaz
            inputField.text = currentValue.ToString();
        }
        else
        {
            // Eðer sayý geçerli deðilse (örneðin, bir metin girilmiþse), hata mesajý gösterebilir veya input'u sýfýrlayabilirsin
            Debug.LogWarning("InputField'daki deðer geçerli bir sayý deðil.");
            inputField.text = "0"; // Alternatif olarak, baþlangýç deðeri olarak 0 ata
        }
    }

}
