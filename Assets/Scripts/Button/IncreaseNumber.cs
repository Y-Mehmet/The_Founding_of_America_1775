using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseNumber : MonoBehaviour
{
    
    public TMP_InputField inputField;



    public void IncreaseValue()
    {
        // Input Field'in dolu olup olmad���n� kontrol et
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.text = "0"; // E�er bo�sa ba�lang�� de�eri olarak 0 ata
        }

        // Input Field'daki de�eri al ve integer'a �evirmeye �al��
        int currentValue;
        if (int.TryParse(inputField.text, out currentValue))
        {
            // De�eri 10 art�r
            currentValue += 10;

            // Art�r�lm�� de�eri tekrar Input Field'a yaz
            inputField.text = currentValue.ToString();
            SoundManager.instance.Play("ButtonClick");
        }
        else
        {
            SoundManager.instance.Play("Error");
            // E�er say� ge�erli de�ilse (�rne�in, bir metin girilmi�se), hata mesaj� g�sterebilir veya input'u s�f�rlayabilirsin
            Debug.LogWarning("InputField'daki de�er ge�erli bir say� de�il.");
            inputField.text = "0"; // Alternatif olarak, ba�lang�� de�eri olarak 0 ata
        }
    }
    public void IncraseValueWhitParameter(int value)
    {
        // Input Field'in dolu olup olmad���n� kontrol et
        if (string.IsNullOrEmpty(inputField.text))
        {
            inputField.text = "0"; // E�er bo�sa ba�lang�� de�eri olarak 0 ata
        }

        // Input Field'daki de�eri al ve integer'a �evirmeye �al��
        int currentValue;
        if (int.TryParse(inputField.text, out currentValue))
        {
            SoundManager.instance.Play("ButtonClick");
            currentValue += value;

            // Art�r�lm�� de�eri tekrar Input Field'a yaz
            inputField.text = currentValue.ToString();
        }
        else
        {
            SoundManager.instance.Play("Error");
            Debug.LogWarning("InputField'daki de�er ge�erli bir say� de�il.");
            inputField.text = "0";
        }
    }

}
