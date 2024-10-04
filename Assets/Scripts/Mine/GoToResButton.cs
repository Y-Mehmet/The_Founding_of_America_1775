using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToResButton : MonoBehaviour
{
    ResourceType resourceType;
    public int index = 0;
    private void OnEnable()
    {
     ResourceType   currentRes = MineManager.instance.curentResource;
        resourceType= MineManager.instance.MineRequiredResList[currentRes].RequiredResTypeList[index];
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonisClicked);
    }
    void ButtonisClicked()
    {
        MineManager.instance.SetCurrentResource(resourceType);
        
    }
    private void OnDisable()
    {
        gameObject.GetComponent<Button>().onClick.RemoveListener(ButtonisClicked);
    }
}
