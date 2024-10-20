using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Utility;
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
           
            if (RegionClickHandler.Instance.currentState!= null && RegionClickHandler.Instance.currentState.TryGetComponent<State>(out State state) )
            {
                  gameObject.GetComponent<TMP_Text>().text = FormatNumber(RegionClickHandler.staticState.GetGoldResValue());
            }else
            {
                gameObject.GetComponent<TMP_Text>().text= FormatNumber(ResourceManager.Instance.GetResourceAmount(ResourceType.Gold));
            }
            yield return new WaitForSeconds(GameManager.gameDayTime);

        }
       
    }
}
