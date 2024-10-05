using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateToogle : MonoBehaviour
{
    private void OnEnable()
    {
        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.OnStateToTradeChanged += OnStateToTradeChanged;
            OnStateToTradeChanged(ResourceManager.Instance.curentTradeStateName);


        }
        else
        {
            Debug.LogWarning("ResourceManager instance is not available.");
        }
    }

    private void OnStateToTradeChanged(string stateName)
    {
        //Debug.LogWarning(" state deðiþti statetoogle " + stateName);

        if (gameObject.transform.GetChild(1).TryGetComponent<Image>(out Image stateFlag))
        {

            int index = Usa.Instance.transform.Find(stateName).transform.GetSiblingIndex();
           // Debug.LogWarning(index + " index numarsý gameobjectin");
            stateFlag.sprite = StateFlagSpritesSO.Instance.flagSpriteLists[index];
        }
        else
        {
            Debug.LogWarning("Toggle button image not found.");
        }

        if (gameObject.transform.GetChild(2).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI nameText))
        {
           nameText.text= stateName;
        }
        else
        {
            Debug.LogWarning("Toggle button text not found.");
        }
    }

    private void OnDisable()
    {
        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.OnStateToTradeChanged -= OnStateToTradeChanged;

        }
        else
        {
            Debug.LogWarning("ResourceManager instance is not available.");
        }
    }
}
