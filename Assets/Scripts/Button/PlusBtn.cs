using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlusBtn : MonoBehaviour
{
    public TMP_InputField InputField;  // InputField referans�
    public int AddedValue = 1000;  // Eklenecek de�er

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
        SoundManager.instance.Play("ButtonClick");
        // Mevcut de�eri al
        int currentValue;
        // InputField'daki mevcut de�eri tamsay�ya �evir
        if (int.TryParse(InputField.text, out currentValue))
        {
            // Yeni de�eri hesapla
            currentValue += AddedValue;
            // InputField'a yeni de�eri ata
            InputField.text = currentValue.ToString();
        }
        else
        {
            Debug.LogError("Input field does not contain a valid integer.");
            InputField.text= AddedValue.ToString();
        }
    }
}
