using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateAssingForGenereralCard : MonoBehaviour
{
    public Image stateFlagIcon;
    public TMP_Text navalForceText, landForceText;
    int index;
    private void OnEnable()
    {
         index = transform.GetSiblingIndex() - 1;
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        ShowExportPanelInfo();
        
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnButtonClicked);
    }
    void ShowExportPanelInfo()
    {
       
        
        stateFlagIcon.sprite= GameManager.AllyStateList[index].StateIcon;
        navalForceText.text=GameManager.AllyStateList[index].GetNavalArmySize().ToString();
        landForceText.text= GameManager.AllyStateList[index].GetLandArmySize().ToString() ;

    }
    void OnButtonClicked()
    {
        GeneralManager.AssignGeneralToState(GameManager.AllyStateList[index], GeneralManager.generals[GeneralManager.GeneralIndex]);
        UIManager.Instance.GetComponent<HideLastPanelButton>().DoHidePanel();
    }
}
