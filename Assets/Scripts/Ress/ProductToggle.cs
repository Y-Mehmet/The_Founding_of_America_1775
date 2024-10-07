using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProductToggle : MonoBehaviour
{
   
    private void OnEnable()
    {
        if (ResourceManager.Instance != null)
        {
            ResourceManager.Instance.OnResourceChanged += OnResourceTypeChanged;
            OnResourceTypeChanged(ResourceManager.curentResource);
          

        }
        else
        {
            Debug.LogWarning("ResourceManager instance is not available.");
        }
    }

    private void OnResourceTypeChanged(ResourceType type)
    {
        

        if (gameObject.transform.GetChild(1).TryGetComponent<Image>(out Image resIcon))
        {
            resIcon.sprite = ResSpriteSO.Instance.resIcon[(int)type];
        }
        else
        {
            Debug.LogWarning("Toggle button image not found.");
        }

        if (gameObject.transform.GetChild(2).TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI resName))
        {
            resName.text = type.ToString();
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
            ResourceManager.Instance.OnResourceChanged -= OnResourceTypeChanged;
            
        }
        else
        {
            Debug.LogWarning("ResourceManager instance is not available.");
        }
    }
}
