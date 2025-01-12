using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MY.NumberUtilitys.Utility;

public class TaxTypePanel : MonoBehaviour
{
    public GameObject HappinesPanel;
    public TaxType taxType;
    public Slider slider;
    public TMP_Text taxCoinValueText;
    State currentState;
       
    private void OnEnable()
    {

        currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        if (currentState == null)
        {
            Debug.LogWarning("current state is null"); 
        }else
        {
            List<TaxData> TaxDatas = currentState.Taxes;
            {
                foreach (var item in TaxDatas)
                {
                    if (item.taxType == taxType)
                    {
                        slider.value = ((int)item.currentRate);
                        //Debug.LogWarning(item.currentRate);
                        slider.onValueChanged.AddListener(OnSliderValueChanged);
                        

                        if (taxType == TaxType.StampTax)
                        {
                            item.taxIncome = slider.value * item.unitTaxIncome*RegionClickHandler.staticState.Population;
                            //  Debug.Log($"slider {slider.value} unit {item.unitTaxIncome} population {RegionClickHandler.staticState.Population}");
                        }else if( taxType== TaxType.IncomeTax)
                        item.taxIncome = slider.value * (RegionClickHandler.staticState.resourceData[ResourceType.Gold].mineCount * RegionClickHandler.staticState.resourceData[ResourceType.Gold].productionRate) * USCongress.ProductionAddedValue / 100 / 100;
                        else
                        {
                            item.taxIncome = 0;
                        }

                        HappinesPanel.GetComponent<HappinesPanel>().SetHappiness(slider.value, item.toleranceLimit);
                        taxCoinValueText.text = item.taxIncome.ToString("0");
                        item.currentRate = slider.value;
                        OnSliderValueChanged(item.currentRate);
                    }
                }
            }
        }


        

    
       
       
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    // Slider de�eri de�i�ti�inde �a�r�lacak i�lev
    void OnSliderValueChanged(float value)
    {
      
        {
            List<TaxData> TaxDatas = RegionClickHandler.Instance.currentState.GetComponent<State>().Taxes;
            {
                foreach (var item in TaxDatas)
                {
                    if (item.taxType == taxType)
                    {
                        if (taxType == TaxType.StampTax)
                        {
                            item.taxIncome = slider.value * item.unitTaxIncome * RegionClickHandler.staticState.Population;
                           // Debug.Log($"slider {slider.value} unit {item.unitTaxIncome} population {RegionClickHandler.staticState.Population}");
                        }else if(taxType == TaxType.IncomeTax)
                        {
                            item.taxIncome = slider.value* (RegionClickHandler.staticState.resourceData[ResourceType.Gold].mineCount*RegionClickHandler.staticState.resourceData[ResourceType.Gold].productionRate)*USCongress.ProductionAddedValue/100/100;
                        }

                       
                        HappinesPanel.GetComponent<HappinesPanel>().OnHappinessChanged?.Invoke(value, item.toleranceLimit);
                        taxCoinValueText.text = item.taxIncome.ToString("0");
                        item.currentRate = value;
                        
                    }
                }
            }
        }
        
    }


}
