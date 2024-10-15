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
        if(UIManager.Instance.TryGetComponent<ShowPanelButton>(out  showPanelButton))
        {
            generalBtn.onClick.AddListener(()=> OnButtonClicKed(PanelID.GeneralInfoPanel));
            assingBtn.onClick.AddListener(() => OnButtonClicKed(PanelID.GeneralInfoPanel));
        }
        else
        {
            Debug.LogWarning("show panel is null");
        }
       
    }
    private void OnDisable()
    {
        generalBtn.onClick.RemoveListener(() => OnButtonClicKed(PanelID.GeneralInfoPanel));
        assingBtn.onClick.RemoveListener(() => OnButtonClicKed(PanelID.StateAssingForGeneralPanel));
    }

    void OnButtonClicKed(PanelID panelID)
    {
       
        GeneralManager.SetGeneralIndex(transform.GetSiblingIndex());
        Debug.LogWarning(GeneralManager.GeneralIndex);
        showPanelButton.DoShowPanelWhitId(panelID);
    }
}
