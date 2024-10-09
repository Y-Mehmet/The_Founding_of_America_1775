using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Espionage : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text spyCostTxt;
    int oneSpyCost = 10;
    int spyCost = 0;
    Color originalSpyCostTextColor;

    void OnEnable()
    {
        // InputField'ýn metin deðiþikliði olayýna bir dinleyici ekle
        inputField.onValueChanged.AddListener(OnInputValueChanged);
       // Debug.LogWarning("dinleme baþladý");
        originalSpyCostTextColor= spyCostTxt.color;
        
    }
   public void GetEnemyIntel()
    {
        int gold = ResourceManager.Instance.GetResourceAmount(ResourceType.Gold);
        if(gold>spyCost)
        {
            ResourceManager.Instance.ReduceResource(ResourceType.Gold, spyCost);
            EnemyStateInfoPanel.Instance.ShowInfo(spyCost);
            ClearInputField();
        }
        else
        {
            spyCostTxt.color = Color.red;

            Debug.LogWarning("casus gönderilemiyor gold yetersiz");
        }
        
    }
    void OnInputValueChanged(string input)
    {
        int spyCount;



        if (int.TryParse(input, out spyCount))
        {
             spyCost = spyCount * oneSpyCost;
            
            int gold = ResourceManager.Instance.GetResourceAmount(ResourceType.Gold);
            if (gold >= spyCost)
            {
                spyCostTxt.color = originalSpyCostTextColor;

            }
            else
            {
                spyCostTxt.color = Color.red;

              
            }
            spyCostTxt.text = spyCost.ToString();

            //  Debug.LogWarning(spyCost + " spy cost deðeri deðiþti");

        }
        else
            Debug.LogWarning(" deðer yalýþ");


    }
    void ClearInputField()
    {
        inputField.text = "0";
    }
}


