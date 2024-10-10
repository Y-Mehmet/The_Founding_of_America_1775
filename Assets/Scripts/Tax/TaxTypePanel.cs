using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TaxTypePanel : MonoBehaviour
{
    public GameObject HappinesPanel;
    public TaxType taxType;
    public Slider slider;
    public TMP_Text taxCoinValueText;
    State currentState;
    private void Awake()
    {
        
       
    }

    
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
                        slider.value = item.currentRate;
                        Debug.LogWarning(item.currentRate);
                        slider.onValueChanged.AddListener(OnSliderValueChanged);

                        if (taxType == TaxType.StampTax)
                        {
                            item.taxIncome = slider.value * item.unitTaxIncome;
                        }
                        else
                        {
                            item.taxIncome = 0;
                        }

                        HappinesPanel.GetComponent<HappinesPanel>().SetHappiness(slider.value, item.toleranceLimit);
                        taxCoinValueText.text = item.taxIncome.ToString();
                        item.currentRate = slider.value;
                    }
                }
            }
        }


        

    
       
       
    }
    private void OnDisable()
    {
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    // Slider deðeri deðiþtiðinde çaðrýlacak iþlev
    void OnSliderValueChanged(float value)
    {
      
        {
            List<TaxData> TaxDatas = RegionClickHandler.Instance.currentState.GetComponent<State>().Taxes;
            {
                foreach (var item in TaxDatas)
                {
                    if (item.taxType == taxType)
                    {
                        if (taxType == TaxType.StampTax || taxType == TaxType.DirectTax)
                            item.taxIncome = value * item.unitTaxIncome;
                        HappinesPanel.GetComponent<HappinesPanel>().OnHappinessChanged?.Invoke(value, item.toleranceLimit);
                        taxCoinValueText.text = item.taxIncome.ToString();
                        item.currentRate = value;
                        
                    }
                }
            }
        }
        
    }


}
