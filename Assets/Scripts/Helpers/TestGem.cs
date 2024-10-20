using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Utility;
public class TestGem : MonoBehaviour
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
       
            if (RegionClickHandler.Instance.currentState != null )
            {
                gameObject.GetComponent<TMP_Text>().text = FormatNumber(RegionClickHandler.staticState.GetGemResValue());
            }
            else
            {
                gameObject.GetComponent<TMP_Text>().text = FormatNumber(ResourceManager.Instance.GetResourceAmount(ResourceType.Diamond));
            }
            yield return new WaitForSeconds(GameManager.gameDayTime);

        }

    }
}
