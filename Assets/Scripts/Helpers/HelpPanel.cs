using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static InfoManager;
public class HelpPanel : MonoBehaviour
{
    public TMP_Text info;
    private void OnEnable()
    {
        PanelID lastPanel = LastActivePanel.Peek();
        Debug.Log(lastPanel);
        info.text = HelpInfoLists[lastPanel].ToString();
        
        
    }
   
}
