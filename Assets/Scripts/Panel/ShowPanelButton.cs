using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelButton : MonoBehaviour
{
    public string panelID;
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
}
