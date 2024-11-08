using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloseBtn : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonCliced);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnButtonCliced);
    }
    void OnButtonCliced()
    {
        SoundManager.instance.Play("ButtonClick");
        UIManager.Instance.HideLastPanel();
    }
}
