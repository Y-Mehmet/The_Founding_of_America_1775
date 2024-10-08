using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackBtn : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClikedBtn);
            
            
    }
    private void OnDisable()
    {
        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    void OnClikedBtn()
    {
        GameManager.Instance.ChangeIsAttackValueTrue();

    }
}
