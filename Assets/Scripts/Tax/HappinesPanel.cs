using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MY.NumberUtilitys.Utility;

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
        CahangeHappinesPersengeText(percentage);

        float result = percentage - limit;

        int index = 0;
        float a;
        int spriteIndex = 0; ;

        //if (result >= limit)
        //{
        //    // K�zg�n ifade g�stermek i�in hesaplama
        //    a = (50 - limit) / (spriteCount / 2);  // �st limit 50 kabul edildi
        //    index = (int)((result - limit) / a) + (spriteCount / 2);

        //    // E�er index sprite s�n�r�n� a�arsa, max k�zg�n ifade ile s�n�rl�yoruz.
        //    if (index >= spriteCount)
        //        index = spriteCount - 1;
        //}
        //else
        //{
        //    // Mutlu ifade g�stermek i�in hesaplama
        //    a = limit / (spriteCount / 2);
        //    index = (int)(result / a);

        //    // index eksiye d��erse, minimum mutlu ifade ile s�n�rl�yoruz.
        //    if (index < 0)
        //        index = 0;
        //}
        if(result>0)
        {
            spriteIndex = ((int)((50 - limit)/3));
            index = 3+((int)( result/ spriteIndex));
            index= index>spriteCount-1?spriteCount-1:index;
            
        }else
        {
            spriteIndex=(int)((limit)/3);
            index = 2-((int)(-result / spriteIndex));
            index = index < 0 ? 0 : index;
        }


        UpdateHappinessSprite(index);
        happinessPercentageText.text = "%" + percentage.ToString("0");
    }
  
    public void CahangeHappinesPersengeText(float value)
    {
        happinessPercentageText.text = value.ToString("0");
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
