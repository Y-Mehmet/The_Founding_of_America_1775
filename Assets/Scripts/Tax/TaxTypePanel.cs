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
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
    }

    
    private void OnEnable()
    {
        List<TaxData> TaxDatas = currentState.Taxes;
        {
            foreach (var item in TaxDatas)
            {
                if( item.taxType== taxType )
                {
                    Debug.LogWarning(slider.value);
                    if( taxType== TaxType.StampTax || taxType== TaxType.DirectTax)
                    {
                        item.taxIncome = slider.value * item.unitTaxIncome;
                    }
                    if( taxType== TaxType.ValueAddedTax)
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

    // Slider deðeri deðiþtiðinde çaðrýlacak iþlev
    void OnSliderValueChanged(float value)
    {
        List<TaxData> TaxDatas = RegionClickHandler.Instance.currentState.GetComponent<State>().Taxes;
        {
            foreach (var item in TaxDatas)
            {
                if (item.taxType == taxType)
                {
                    if (taxType == TaxType.StampTax || taxType == TaxType.DirectTax)
                        item.taxIncome = value * item.unitTaxIncome;
                    HappinesPanel.GetComponent<HappinesPanel>().OnHappinessChanged?.Invoke(value,item.toleranceLimit);
                    taxCoinValueText.text = item.taxIncome.ToString();
                    item.currentRate = value;
                }
            }
        }
    }


}
