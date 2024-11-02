using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMessageBtn : MonoBehaviour
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
      
        UIManager.Instance.HideLastPanel();
        GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
    }
}
