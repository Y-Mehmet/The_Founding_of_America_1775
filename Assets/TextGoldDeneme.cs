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
            yield return new WaitForSeconds(5.0f);
            if (RegionClickHandler.Instance.currentState.TryGetComponent<State>(out State state) )
                gameObject.GetComponent<TMP_Text>().text = RegionClickHandler.Instance.currentState.GetComponent<State>().resourceData[ResourceType.Gold].currentAmount.ToString();
            
        }
       
    }
}
