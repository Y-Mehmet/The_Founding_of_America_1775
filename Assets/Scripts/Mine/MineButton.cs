using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            duration = GameManager.Instance.gameDayTime;
            mineCountText.text = currentState.resourceData[resourceType].mineCount.ToString();
            StartCoroutine(ResValueTextUpdate());
        }
        
     

       
    }
    IEnumerator ResValueTextUpdate()
    {
        
        yield return new WaitForSeconds(duration);
        resValueText.text = currentState.resourceData[resourceType].currentAmount.ToString();
    }
    void ButtonisClicked()
    {
        MineManager.instance.SetCurrentResource(resourceType);
    }
}
