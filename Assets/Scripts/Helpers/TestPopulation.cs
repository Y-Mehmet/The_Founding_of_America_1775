using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Utility;
using static GameManager;
public class TestPopulation : MonoBehaviour
{
    private void Start()
    {

        {
            StartCoroutine(getText());
        }
    }
    IEnumerator getText()
    {
        int addedValue = 0;
        while (true)
        {
            
            if (RegionClickHandler.Instance.currentState != null)
            {
                addedValue = RegionClickHandler.staticState.populationAddedValue;
                if (addedValue<0)
                gameObject.GetComponent<TMP_Text>().text = FormatNumber(RegionClickHandler.staticState.Population)+" "+FormatNumber(addedValue);
                else
                    gameObject.GetComponent<TMP_Text>().text = FormatNumber(RegionClickHandler.staticState.Population) + " +" + FormatNumber(addedValue);

            }
            else
            {
                gameObject.GetComponent<TMP_Text>().text = FormatNumber(tottalPopulation);
            }
            yield return new WaitForSeconds(gameDayTime);


        }

    }
}
