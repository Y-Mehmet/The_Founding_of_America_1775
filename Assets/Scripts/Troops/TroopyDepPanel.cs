using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GameManager;
using static TroopyManager;
    public class TroopyDepPanel : MonoBehaviour
{
    State currnetState;
    public GameObject box, emtyStateBox;
    private void OnEnable()
    {
        currnetState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        if (AllyStateList != null && currnetState != null)
        {
           
            if (AllyStateList.Count > 1)
            {
                box.SetActive(true);
               
            }
            else
            {
                emtyStateBox.SetActive(true);
                box.SetActive(false);

            }
        }
        else
        {
            Debug.LogError(" ally state list is null");
        }

    }
    private void OnDisable()
    {
        emtyStateBox.SetActive(false);
        box.SetActive(false);
    }
}
