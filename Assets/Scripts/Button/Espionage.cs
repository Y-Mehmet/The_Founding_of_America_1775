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
        // InputField'ýn metin deðiþikliði olayýna bir dinleyici ekle
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        Debug.LogWarning("dinleme baþladý");
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

            Debug.LogWarning(spyCost + " spy cost deðeri deðiþti");

        }
        else
            Debug.LogWarning(" deðer yalýþ");


    }
}


