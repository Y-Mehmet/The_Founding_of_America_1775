using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Utility;
public class TextGoldDeneme : MonoBehaviour
{
    public GameObject centralBankIcon;
    private void Start()
    {
        
        
            StartCoroutine(getText());
        
    }
    IEnumerator getText()
    {
        while (true)
        {
           
            if (RegionClickHandler.staticState!= null && RegionClickHandler.staticState.stateType== StateType.Ally)
            {
                  gameObject.GetComponent<TMP_Text>().text = FormatNumber(RegionClickHandler.staticState.GetGoldResValue());
                centralBankIcon.SetActive(false);
            }
            else
            {
                gameObject.GetComponent<TMP_Text>().text= FormatNumber(ResourceManager.Instance.GetResourceAmount(ResourceType.Gold));
                centralBankIcon.SetActive(true);
            }
            yield return new WaitForSeconds(1);

        }
       
    }
}
