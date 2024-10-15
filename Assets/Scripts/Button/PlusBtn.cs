using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlusBtn : MonoBehaviour
{
    public TMP_InputField InputField;  // InputField referansý
    public int AddedValue = 1000;  // Eklenecek deðer

    private void OnEnable()
    {
        if (InputField != null)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.LogError("Input field is null");
        }
    }

    private void OnDisable()
    {
        gameObject.GetComponent<Button>().onClick.RemoveListener(OnButtonClicked);
    }

    void OnButtonClicked()
    {
        // Mevcut deðeri al
        int currentValue;
        // InputField'daki mevcut deðeri tamsayýya çevir
        if (int.TryParse(InputField.text, out currentValue))
        {
            // Yeni deðeri hesapla
            currentValue += AddedValue;
            // InputField'a yeni deðeri ata
            InputField.text = currentValue.ToString();
        }
        else
        {
            Debug.LogError("Input field does not contain a valid integer.");
            InputField.text= AddedValue.ToString();
        }
    }
}
