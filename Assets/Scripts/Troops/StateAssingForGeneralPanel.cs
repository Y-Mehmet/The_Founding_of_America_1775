using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateAssingForGeneralPanel : MonoBehaviour
{

    
    


    private void OnEnable()
    {
        
       
             
        int count = GameManager.AllyStateList.Count;
        
        GetComponent<GridLayoutGroup>().constraintCount =count;
        foreach (Transform child in transform)
        {
            if (child.GetSiblingIndex() < count)
            {
                child.gameObject.SetActive(true);
            }else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    



}
