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
      
            // Eðer staticState null ise, tüm GameObject'leri aktif et
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(true);
            }
        
        
    }
    void UpdateMenuDisActiveted()
    {

        // Eðer staticState null ise, tüm GameObject'leri aktif et
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }


    }
}
