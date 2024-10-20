using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static RegionClickHandler;
using static Utility;
public class troopyStatsCard : MonoBehaviour
{
    
    public TMP_Text  valueText;
    private void OnEnable()
    {

        int index = transform.GetSiblingIndex() ;
       
        switch(index){
            case 0:
                valueText.text = FormatNumber(staticState.GetArmyBarrackSize());
                break;
            case 1:
                valueText.text = FormatNumber(staticState.GetNavalArmySize());
                break;
            case 2:
                valueText.text = FormatNumber(staticState.GetLandArmySize());
                break;
            default:
                break;

        }

        


    }
}
