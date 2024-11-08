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
        SoundManager.instance.Play("ButtonClick");
        UIManager.Instance.HideLastPanel();
        GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
    }
}
