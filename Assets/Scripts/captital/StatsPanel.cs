using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static RegionClickHandler;
using static Utility;
public class StatsPanel : MonoBehaviour
{
    public Image falg, happines;
    public TMP_Text stateName, happinesRate, taxValue;
    public Slider slider;
    float totalTaxIncome = 0;
    private void OnEnable()
    {
        falg.sprite = staticState.StateIcon;
        stateName.text = staticState.StateName;
        InvokeRepeating("UIUpdate", 0, GameManager.gameDayTime);
       

    }
    private void OnDisable()
    {
        CancelInvoke("UIUpdate"); // Tekrar eden çaðrýyý durdurur.
    }

    void UIUpdate()
    {
        happinesRate.text = ((int)staticState.Morele).ToString() + "%";
        slider.value = ((int)staticState.Morele);
         totalTaxIncome = 0;
        foreach (var item in staticState.Taxes)
        {
            totalTaxIncome += item.taxIncome;
            Debug.Log($"tax name {item.taxType} " + totalTaxIncome);

        }
        taxValue.text = FormatNumber(totalTaxIncome);
    }
}
