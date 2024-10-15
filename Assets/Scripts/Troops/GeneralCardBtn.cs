using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralCardBtn : MonoBehaviour
{
    ShowPanelButton showPanelButton;
    public Button generalBtn, assingBtn;
    private void OnEnable()
    {
       
        
            generalBtn.onClick.AddListener(()=> OnButtonClicKed(PanelID.GeneralInfoPanel));
            assingBtn.onClick.AddListener(() => OnButtonClicKed(PanelID.StateAssingForGeneralPanel));
        
       
    }
    private void OnDisable()
    {
        generalBtn.onClick.RemoveAllListeners();
        assingBtn.onClick.RemoveAllListeners();
    }

    void OnButtonClicKed(PanelID panelID)
    {
       Debug.LogWarning(" butona týklandý "+panelID.ToString());
        GeneralManager.SetGeneralIndex(transform.GetSiblingIndex());
        Debug.LogWarning(GeneralManager.GeneralIndex);
        UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(panelID);
    }
}
