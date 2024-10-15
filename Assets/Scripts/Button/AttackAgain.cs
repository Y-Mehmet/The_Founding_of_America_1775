using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackAgain : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnButtonClicked);
    }
    void OnButtonClicked()
    {
        Attack.Instance.AttackAgain();
        UIManager.Instance.GetComponent<HideAllPanelButton>().DoHidePanel();
    }
}
