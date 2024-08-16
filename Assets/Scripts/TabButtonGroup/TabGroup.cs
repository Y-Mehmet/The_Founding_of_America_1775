using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons = new List<TabButton>();
    public List<Sprite> sprites = new List<Sprite>();
    public List<GameObject> spwapToPage = new List<GameObject>();
    TabButton selectedTabButton;

    public void Subscribe(TabButton button)
    {
        if(tabButtons==null)
        {
            tabButtons = new List<TabButton>();
        }
        tabButtons.Add(button);
    }
    public void OnTabSelected(TabButton button)
    {
       if(selectedTabButton!=null) 
        {
            selectedTabButton.Deselect();
        }
       
            
            Debug.Log($"{button.gameObject.name} sekmesi seçildi.");
            selectedTabButton = button;
            
            button.Select();
            ResetImage();
            button.background.sprite = sprites[1];
            int index= button.transform.GetSiblingIndex();
            foreach (var item in spwapToPage)
            {
                if (index == item.transform.GetSiblingIndex())
                {
                    item.SetActive(true);
                }
                else
                    item.SetActive(false);
            }
        
    }

   

    private void ResetImage()
    {
        foreach (var item in tabButtons)
        {
            if(tabButtons != null && item != selectedTabButton )
            item.background.sprite = sprites[0];
        }
    }
   
}

