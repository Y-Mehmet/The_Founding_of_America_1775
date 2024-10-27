using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SellAllPanel : MonoBehaviour
{
    public TMP_Text sellAllPriceText;
    public Button sellAllButton, cancelButton;
    float totalSellPrice = 0;
    private void OnEnable()
    {
        sellAllButton.onClick.AddListener(OnSellAllBtnCliceked);
        InvokeRepeating("CalculateSellAllPrice", 0, GameManager.gameDayTime);
        
    }
    private void OnDisable()
    {
        
       sellAllButton.onClick.RemoveListener(OnSellAllBtnCliceked);
        CancelInvoke("CalculateSellAllPrice");
    }
    int CalculateSellAllPrice()
    {
        totalSellPrice = 0;
        foreach (ResourceData resource in RegionClickHandler.staticState.resourceData.Values)
        {
            if (resource.resourceType != ResourceType.Gold && resource.currentAmount>0)
                totalSellPrice += GameEconomy.Instance.GetGoldValue(resource.resourceType, resource.currentAmount);
        }
        sellAllPriceText.text=Utility.FormatNumber(totalSellPrice/2);
        return ((int)totalSellPrice)/2;
    }
    void OnSellAllBtnCliceked()
    {
        int earing = 0;
        foreach (ResourceData resource in RegionClickHandler.staticState.resourceData.Values)
        {
            if (resource.resourceType != ResourceType.Gold && resource.currentAmount>0) 
            {
                earing = ((int)GameEconomy.Instance.GetGoldValue(resource.resourceType, resource.currentAmount)) / 2;
                RegionClickHandler.staticState.SellResource(resource.resourceType, resource.currentAmount, earing);
            }
               
        }
        totalSellPrice = 0;
    }

}
