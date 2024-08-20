using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResCardButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        
        gameObject.GetComponent<Button>().onClick.AddListener(SetCurentResource);
    }
    void SetCurentResource()
    {
        if (gameObject.transform.GetChild(1).TryGetComponent<TextMeshProUGUI>(out var resName))
        {
            string resourceString = resName.text.ToString();
            ResourceType resource;



            // Enum.TryParse y�ntemi (daha g�venli)
            if (Enum.TryParse(resourceString, out resource))
            {
                ResourceManager.Instance.SetCurrentResource(resource);

            }
            else
            {
                Debug.LogError("Invalid enum value " + resourceString);
            }

        }
        else
        {
            Debug.LogWarning("texetmedhpro i�ermiyor button");
        }
        

       
         
    }
}
