using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBtn : MonoBehaviour
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
        UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.HelpPanel);
    }
}
