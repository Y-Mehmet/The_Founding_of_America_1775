using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsFlagCard : MonoBehaviour
{
    int index = 0;
    private void OnEnable()
    {
            index = transform.GetSiblingIndex();       
            GetComponent<Button>().onClick.AddListener(OnButtonClicked);
            GetComponent<Image>().sprite = GameManager.AllyStateList[index].StateIcon;
       
       
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnButtonClicked);
    }
    void OnButtonClicked()
    {
        RegionClickHandler.statsState= GameManager.AllyStateList[index];
        RegionClickHandler.Instance.onStatsStateChanged?.Invoke();
    }
}
