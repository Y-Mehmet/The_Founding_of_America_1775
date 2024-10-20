using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utility;


public class HappinesPanel : MonoBehaviour
{
    // Mutluluk yüzdesi
    public TMP_Text happinessPercentageText;
   

    

    // Sprite'ý gösterecek Image komponenti
    public Image happinessImage;

    // Mutluluk yüzdesi deðiþtiðinde çaðrýlacak Action
    public Action<float, float> OnHappinessChanged;
    float happinessPercentage;
    int spriteCount;
    private void Start()
    {
        spriteCount = GameIconSO.Instance.HappinesIcon.Count;
        
    }


    private void OnEnable()
    {
            // Action'a mutluluk sprite'ýný güncelleyen metodu baðla
            OnHappinessChanged += ChangeHappinesImage;
     
    }

    // Mutluluk yüzdesini güncelleyen metot
    public void SetHappiness(float percentage, float limit) // max 50 limt 25 value 20 0-25 -5  25/15// 50-0 LÝMT 50-LÝMÝT /3 / RES
    {
        ChangeHappinesImage(percentage, limit);
        CahangeHappinesPersengeText(percentage);



    }
    void ChangeHappinesImage(float percentage, float limit)
    {
     
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
        happinessPercentageText.text = "%" + percentage.ToString("0");
    }
  
    public void CahangeHappinesPersengeText(float value)
    {
        happinessPercentageText.text = value.ToString("0");
    }
    

    // Mutluluk yüzdesine göre doðru sprite'ý seçen metot
    private void UpdateHappinessSprite(int index)
    {
       
        happinessImage.sprite = GameIconSO.Instance.HappinesIcon[index];
    }
    private void OnDisable()
    {
        OnHappinessChanged -= ChangeHappinesImage;
    }
}
