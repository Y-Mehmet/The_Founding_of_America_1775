using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static RegionClickHandler;
using static Utility;

public class ResourceStatsCard : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText, valueText;
    int index;
    private void OnEnable()
    {        
       index= transform.GetSiblingIndex()+1;
        icon.sprite = ResSpriteSO.Instance.resIcon[index];
        nameText.text = ((ResourceType)index).ToString();
        InvokeRepeating("UIUpdate", 0, GameManager.gameDayTime);
        

    }
    private void OnDisable()
    {
        CancelInvoke("UIUpdate");
    }
    void UIUpdate()
    {
        int surplus = ((int)statsState.resourceData[(ResourceType)index].surplus);
        if (surplus>0)
        {
            valueText.text = FormatNumber(((int)statsState.resourceData[(ResourceType)index].currentAmount)) + " +" + FormatNumber(surplus);
            valueText.color = Color.white;
        }
     
        else
        {
            valueText.text = FormatNumber(((int)statsState.resourceData[(ResourceType)index].currentAmount)) + " " + FormatNumber(surplus);
            valueText.color = Color.red;
        }
            
    }
}
