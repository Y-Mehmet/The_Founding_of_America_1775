using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Retreat : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(RetreatState);
    }
    void RetreatState()
    {
       
            GameManager.Instance.IsAttackFinish = true;
            GameManager.Instance.ChangeIsAttackValueFalse();
       
    }
}
