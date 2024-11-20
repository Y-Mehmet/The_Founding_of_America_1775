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
        cancelButton.onClick.AddListener(CancelButtonClicked);
        InvokeRepeating("CalculateSellAllPrice", 0, GameManager.gameDayTime);
        
    }
    private void OnDisable()
    {
        
       sellAllButton.onClick.RemoveListener(OnSellAllBtnCliceked);
        cancelButton.onClick.RemoveListener(CancelButtonClicked);
        CancelInvoke("CalculateSellAllPrice");
    }
    int CalculateSellAllPrice()
    {
        totalSellPrice = 0;
        foreach (ResourceData resource in RegionClickHandler.staticState.resourceData.Values)
        {
            if (resource.resourceType != ResourceType.Gold && resource.currentAmount>0)
                totalSellPrice += GameEconomy.Instance.GetGoldValue(resource.resourceType, resource.currentAmount)/4;
        }
        sellAllPriceText.text=Utility.FormatNumber(totalSellPrice);
        return ((int)totalSellPrice);
    }
    void OnSellAllBtnCliceked()
    {
        SoundManager.instance.Play("Cash");
        int earing = 0;
        foreach (ResourceData resource in RegionClickHandler.staticState.resourceData.Values)
        {
            if (resource.resourceType != ResourceType.Gold && resource.currentAmount>0) 
            {
                earing = ((int)GameEconomy.Instance.GetGoldValue(resource.resourceType, resource.currentAmount)) /4;
                RegionClickHandler.staticState.SellResource(resource.resourceType, resource.currentAmount, earing);
                MissionsManager.AddTotalExportGold(((int)earing));
            }
               
        }
        RegionClickHandler.staticState.SetMorale(-10);
        MessageManager.AddMessage("Patrick Henry: It is utterly disgraceful that you would choose to dispose of our precious resources for mere pennies through the 'Sell All' option." +
            " This thoughtless action not only devalues the labor of our citizens but also reveals a complete lack of respect for our efforts." +
            " As a result of your decisions, the morale of the people has dropped by 10 points! We expect our leaders to defend our rights, not to trample on them!");
        totalSellPrice = 0;
        UIManager.Instance.HideLastPanel();
    }
    void CancelButtonClicked()
    {
        SoundManager.instance.Play("ButtonClick");
        UIManager.Instance.HideLastPanel();
    }

}
