using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlunderBtn : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnClicked);
    }
    void OnClicked()
    {
     
        GameManager.Instance.ChangeAttackFinisValueFalse();
        UIManager.Instance.GetComponent<HideLastPanelButton>().DoHidePanel();
        UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.PlunderPanel);
        
    }
}
