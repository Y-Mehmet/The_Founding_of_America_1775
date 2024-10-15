using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateCloseBtn : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonCliced);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnButtonCliced);
    }
    void OnButtonCliced()
    {
        RegionClickHandler.Instance.CloseBtn_CloseAll();
        GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
        UIManager.Instance.HideAllPanel();
    }
}
