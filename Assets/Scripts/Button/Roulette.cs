using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
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
        UIManager.Instance.HideAllPanel();
        GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();
        UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.Roulette);
    }
}
