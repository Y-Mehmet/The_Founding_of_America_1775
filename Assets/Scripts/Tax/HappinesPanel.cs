using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class HappinesPanel : MonoBehaviour
{
    // Mutluluk y�zdesi
    public TMP_Text happinessPercentageText;

    

    // Sprite'� g�sterecek Image komponenti
    public Image happinessImage;

    // Mutluluk y�zdesi de�i�ti�inde �a�r�lacak Action
    public Action<float, float> OnHappinessChanged;
    float happinessPercentage;
    int spriteCount;
    private void Start()
    {
        spriteCount = GameIconSO.Instance.HappinesIcon.Count;
    }


    private void OnEnable()
    {
            // Action'a mutluluk sprite'�n� g�ncelleyen metodu ba�la
            OnHappinessChanged += ChangeHappinesImage;
     
    }

    // Mutluluk y�zdesini g�ncelleyen metot
    public void SetHappiness(float percentage, float limit) // max 50 limt 25 value 20 0-25 -5  25/15// 50-0 L�MT 50-L�M�T /3 / RES
    {
        ChangeHappinesImage(percentage, limit);
        CahangeHappinesPersengeText(percentage);



    }
    void ChangeHappinesImage(float percentage, float limit)
    {
        Debug.LogWarning("limit: " + limit);
        /*
         
            min= 0;
            max= 50;
            limit;
            persent;
            resault= persent-limit;
        if( resault>0)
        {
        payla�t�r�lacakAlan= max-limit;
        payla�t��lacakAlanBirimUZunlu�u= payla�t�r�lacakAlan/ (iconCount/2)
        index= resault/ payla�t��lacakAlanBirimUZunlu�u+(iconCount/2)
        }
        else
        {
        payla�t�r�lacakAlan= limit;
         payla�t��lacakAlanBirimUZunlu�u= payla�t�r�lacakAlan/ (iconCount/2)
        index= persent/ payla�t��lacakAlanBirimUZunlu�u
        }


         */
        float result = percentage - limit;
        int index=0;
        if (result >= 0)
        {
            float a = (50 - limit) / (spriteCount / 2);
            index = (int)(result / a) + (spriteCount / 2) ; 
            {
                index = spriteCount - 1;
            }
        }
        else
        {
            float a = limit / (spriteCount / 2);  
            index = (int)(percentage / a);
            if (index < 0)
                index = 0;
        }


        UpdateHappinessSprite(index);
        happinessPercentageText.text = "%" + percentage;
    }
    public void CahangeHappinesPersengeText(float value)
    {
        happinessPercentageText.text = value.ToString();
    }
    

    // Mutluluk y�zdesine g�re do�ru sprite'� se�en metot
    private void UpdateHappinessSprite(int index)
    {
       
        happinessImage.sprite = GameIconSO.Instance.HappinesIcon[index];
    }
    private void OnDisable()
    {
        OnHappinessChanged -= ChangeHappinesImage;
    }
}
