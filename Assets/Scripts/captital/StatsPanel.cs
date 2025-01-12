using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static RegionClickHandler;
using static MY.NumberUtilitys.Utility;
public class StatsPanel : MonoBehaviour
{
    public Image falg, happines;
    public TMP_Text stateName, happinesRate, taxValue;
    public Slider slider;
    float totalTaxIncome = 0;
    [Header("content")]
    public GameObject parentGameObject;
    int allyStataCount=1;
    private void OnEnable()
    {
        if(statsState== null)
        {
            statsState = staticState;
        }
        falg.sprite = statsState.StateIcon;
        stateName.text = statsState.StateName;
        allyStataCount = GameManager.AllyStateList.Count;
        FlagActiveded();
        InvokeRepeating("UIUpdate", 0, 1);
        Instance.onStatsStateChanged += StatsStateChange;

       

    }
    private void OnDisable()
    {
        CancelInvoke("UIUpdate"); // Tekrar eden çaðrýyý durdurur.
        RegionClickHandler.Instance.onStatsStateChanged -= StatsStateChange;
    }
    void FlagActiveded()
    {
        foreach (Transform child in parentGameObject.transform)
        {
            if(child.GetSiblingIndex()< allyStataCount)
            {
                child.gameObject.SetActive(true);
            }else
            { child.gameObject.SetActive(false); }
        }
    }
      void  StatsStateChange()
    {
        falg.sprite = statsState.StateIcon;
        stateName.text = statsState.StateName;
        happinesRate.text = ((int)statsState.Morele).ToString() + "%";
        slider.value = ((int)statsState.Morele);
        CalculateTax();
    }

    void CalculateTax()
    {
        totalTaxIncome = 0;
        foreach (var item in statsState.Taxes)
        {
            if (item.taxType == TaxType.StampTax)
            {
                totalTaxIncome += item.currentRate * item.unitTaxIncome * statsState.Population;
                //  Debug.Log($"slider {slider.value} unit {item.unitTaxIncome} population {RegionClickHandler.staticState.Population}");
            }
            else if (item.taxType == TaxType.IncomeTax)
            {
                totalTaxIncome += item.currentRate * (statsState.resourceData[ResourceType.Gold].mineCount * statsState.resourceData[ResourceType.Gold].productionRate) * USCongress.ProductionAddedValue / 100 / 100;
            }

            //   Debug.Log($"tax name {item.taxType} " + totalTaxIncome);

        }
        taxValue.text = FormatNumber(totalTaxIncome);
    }
    void UIUpdate()
    {
        happinesRate.text = ((int)statsState.Morele).ToString() + "%";
        slider.value = ((int)statsState.Morele);
        CalculateTax();
    }
}
