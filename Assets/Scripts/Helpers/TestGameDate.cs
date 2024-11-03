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
            
            gameObject.GetComponent<TMP_Text>().text = GameDateManager.currentDate.ToString("MM/dd/yyyy");
            yield return new WaitForSeconds(1);
        }
    }
}
