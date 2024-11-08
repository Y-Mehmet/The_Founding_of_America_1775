using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utility;
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
        navalForceText.text = FormatNumber(GameManager.AllyStateList[index].GetNavalArmySize());
        landForceText.text = FormatNumber(GameManager.AllyStateList[index].GetLandArmySize());
    }
    void OnButtonClicked()
    {
        SoundManager.instance.Play("ButtonClick");
        GeneralManager.AssignGeneralToState(GameManager.AllyStateList[index], GeneralManager.generals[GeneralManager.GeneralIndex]);
        UIManager.Instance.GetComponent<HideLastPanelButton>().DoHidePanel();
    }
}
