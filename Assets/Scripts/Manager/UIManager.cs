using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public static Action HideAllPanelClicked;
    public static Action ShowPanelClicked;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }
    public  void HideLastPanel()
    {
        GetComponent<HideLastPanelButton>().DoHidePanel();
    }
    public void HideAllPanel()
    {
       // HideAllPanelClicked?.Invoke();
        GetComponent<HideAllPanelButton>().DoHidePanel();
        RegionClickHandler.Instance.CloseBtn_CloseAll();
        GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
    }

}
