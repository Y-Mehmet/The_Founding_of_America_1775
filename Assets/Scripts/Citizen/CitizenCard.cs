using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CitizenCard : MonoBehaviour
{
    public Button select_btn;
    public TMP_Text moralAddedValueText, priceText;
    public int price;
    public int moralAddedValue;
    public bool isFreeCard= true; // reklamlý
    Color errorColor= Color.red,  originalColor= Color.white;
    private void OnEnable()
    {
        moralAddedValueText.text = "Morale: "+moralAddedValue;
        if(!isFreeCard)
        {
            select_btn.onClick.AddListener(OnSelectButonClicked);
            InvokeRepeating("ShowInfo", 0, GameManager.gameDayTime);
        }
    }
    private void OnDisable()
    {
        if (!isFreeCard)
        {
            select_btn.onClick.RemoveListener(OnSelectButonClicked);
            CancelInvoke("ShowInfo");
        }
           
    }
    void ShowInfo()
    {
    if(RegionClickHandler.staticState.GetGoldResValue()>price)
        {
            priceText.color = originalColor;
        }else
        {
            priceText.color = errorColor;
        }
    }
    void OnSelectButonClicked()
    {
        if(RegionClickHandler.staticState.GetGoldResValue()> price)
        {
            RegionClickHandler.staticState.GoldSpend(price);
            RegionClickHandler.staticState.SetMorale(moralAddedValue);
        }
    }


}
