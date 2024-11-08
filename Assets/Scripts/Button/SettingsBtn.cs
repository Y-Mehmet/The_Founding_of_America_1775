using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBtn : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnClick);
    }
    void OnClick()
    {
        SoundManager.instance.Play("ButtonClick");
        UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.Settings);
        GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();
    }
}
