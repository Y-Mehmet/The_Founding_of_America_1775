using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
public class TroopsStateToglePanel : MonoBehaviour
{
    
    public bool isOriginState = false;
    private void OnEnable()
    {
        if (isOriginState)
        {
            if (TroopyManager.OriginState == null )
            {
                TroopyManager.OriginState = AllyStateList[0];
            }
            OnStateToTradeChanged(TroopyManager.OriginState);



        }
        else
        {
            if(TroopyManager.DestinationState == null)
            TroopyManager.DestinationState = AllyStateList[1];

            OnStateToTradeChanged(TroopyManager.DestinationState);
        }
           
            
        
       

    }

    private void OnStateToTradeChanged(State state)
    {
        //Debug.LogWarning(" state deðiþti statetoogle " + stateName);

        if (gameObject.transform.GetChild(1).TryGetComponent<Image>(out Image stateFlag))
        {

            stateFlag.sprite = state.StateIcon;
        }
        else
        {
            Debug.LogWarning("Toggle button image not found.");
        }

        if (gameObject.transform.GetChild(2).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI nameText))
        {
            nameText.text = state.name;
        }
        else
        {
            Debug.LogWarning("Toggle button text not found.");
        }
    }
    private void OnDisable()
    {
        
    }


}
