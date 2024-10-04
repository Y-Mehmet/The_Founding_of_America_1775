using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TestGameDate : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(UIUpdate());
    }
    IEnumerator UIUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(GameManager.Instance.gameDayTime);
            gameObject.GetComponent<TMP_Text>().text = GameDateManager.instance.currentDate.ToString("MM/dd/yyyy");
        }
    }
}
