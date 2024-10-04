using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MineButton : MonoBehaviour
{
    public TMP_Text mineCountText, resValueText;
    public ResourceType resourceType;
    State currentState;
    float duration;
    
    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonisClicked);
        currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        if(currentState != null)
        {
            duration = GameManager.gameDayTime;
            mineCountText.text = currentState.resourceData[resourceType].mineCount.ToString();
            StartCoroutine(ResValueTextUpdate());
        }
        
     

       
    }

    IEnumerator ResValueTextUpdate()
    {
        
       while(true)
        {
            resValueText.text = currentState.resourceData[resourceType].currentAmount.ToString();
            mineCountText.text = currentState.resourceData[resourceType].mineCount.ToString();
            yield return new WaitForSeconds(duration);
           
        }
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
