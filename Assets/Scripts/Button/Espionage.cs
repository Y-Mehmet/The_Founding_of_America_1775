using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Espionage : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text spyCostTxt;
    int oneSpyCost = 10;

    void Start()
    {
        // InputField'�n metin de�i�ikli�i olay�na bir dinleyici ekle
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        Debug.LogWarning("dinleme ba�lad�");
    }
   public void GetEnemyIntel()
    {
        RegionManager.instance.GetEnemyIntel();
    }
    void OnInputValueChanged(string input)
    {
        int spyCount;



        if (int.TryParse(input, out spyCount))
        {
            int spyCost = spyCount * oneSpyCost;
            spyCostTxt.text = spyCost.ToString();

            Debug.LogWarning(spyCost + " spy cost de�eri de�i�ti");

        }
        else
            Debug.LogWarning(" de�er yal��");


    }
}


