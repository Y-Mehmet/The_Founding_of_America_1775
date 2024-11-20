using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtomMenu : MonoBehaviour
{
    private void OnEnable()
    {
        UIManager.HideAllPanelClicked += UpdateMenuActiveted;
        UIManager.ShowPanelClicked += UpdateMenuDisActiveted;
    }

    private void OnDisable()
    {
        UIManager.HideAllPanelClicked -= UpdateMenuActiveted;
        UIManager.ShowPanelClicked -= UpdateMenuDisActiveted;
    }

    void UpdateMenuActiveted()
    {
      
            // E�er staticState null ise, t�m GameObject'leri aktif et
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(true);
            }
        
        
    }
    void UpdateMenuDisActiveted()
    {

        // E�er staticState null ise, t�m GameObject'leri aktif et
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }


    }
}
