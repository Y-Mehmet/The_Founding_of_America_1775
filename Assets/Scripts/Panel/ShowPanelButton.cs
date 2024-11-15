using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    public PanelID panelID;
    public PanelShowBehavior behavior;
    private PanelManager _panelManager;

    private void Start()
    {
        _panelManager = PanelManager.Instance;
    }
    public void DoShowPanel()
    {
       
        _panelManager.ShowPanel(panelID, behavior);

    }
    public void  DoShowPanelWhitId(PanelID panelID)
    {
       // Debug.LogWarning(" panel açýldý " + panelID.ToString());
        _panelManager.ShowPanel(panelID, behavior);
        GameManager.Instance.IsRegionPanelOpen = true;
    }
}
