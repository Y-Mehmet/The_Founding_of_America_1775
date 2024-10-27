using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Retreat : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(RetreatState);
    }
    void RetreatState()
    {
       
            GameManager.Instance.IsAttackFinish = true;
            GameManager.Instance.ChangeIsAttackValueFalse();
            RegionClickHandler.staticState = null;
            UIManager.Instance.GetComponent<HideAllPanelButton>().DoHidePanel();
            GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
        if(GameManager.Instance.ÝsGameOver)
        {
            GameManager.Instance.GameOver();
        }


    }
    private void OnDisable()
    {
        gameObject.GetComponent<Button>().onClick.RemoveListener(RetreatState);
    }
}
