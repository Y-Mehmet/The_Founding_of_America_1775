using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static RegionClickHandler;
using static MY.NumberUtilitys.Utility;
public class troopyStatsCard : MonoBehaviour
{
    
    public TMP_Text  valueText;
    int index;
    private void OnEnable()
    {

        index = transform.GetSiblingIndex() ;
        RegionClickHandler.Instance.onStatsStateChanged += ShowInfo;
        ShowInfo();


    }
    private void OnDisable()
    {
        RegionClickHandler.Instance.onStatsStateChanged-= ShowInfo;
    }
    void ShowInfo()
    {
        switch (index)
        {
            case 0:
                valueText.text = FormatNumber(statsState.GetArmyBarrackSize());
                break;
            case 1:
                valueText.text = FormatNumber(statsState.GetNavalArmySize());
                break;
            case 2:
                valueText.text = FormatNumber(statsState.GetLandArmySize());
                break;
            default:
                break;

        }
    }
}
