using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Utility;
public class TestGem : MonoBehaviour
{
    public GameObject centralBankIcon;
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

            if (RegionClickHandler.staticState != null && RegionClickHandler.staticState.stateType == StateType.Ally)
            {
                gameObject.GetComponent<TMP_Text>().text = FormatNumber(RegionClickHandler.staticState.GetGemResValue());
                centralBankIcon.SetActive(false);
            }
            else
            {
                gameObject.GetComponent<TMP_Text>().text = FormatNumber(ResourceManager.Instance.GetResourceAmount(ResourceType.Diamond));
                centralBankIcon.SetActive(true);
            }
            yield return new WaitForSeconds(GameManager.gameDayTime);

        }

    }
}
