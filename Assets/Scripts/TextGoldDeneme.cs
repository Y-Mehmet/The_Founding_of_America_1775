using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TextGoldDeneme : MonoBehaviour
{
    private void Start()
    {
        
        {
            StartCoroutine(getText());
        }
    }
    IEnumerator getText()
    {
        while (true)
        {
            yield return new WaitForSeconds(GameManager.Instance.gameDayTime);
            if (RegionClickHandler.Instance.currentState!= null && RegionClickHandler.Instance.currentState.TryGetComponent<State>(out State state) )
                gameObject.GetComponent<TMP_Text>().text = "state gold: "+RegionClickHandler.Instance.currentState.GetComponent<State>().resourceData[ResourceType.Gold].currentAmount.ToString();
            
        }
       
    }
}
