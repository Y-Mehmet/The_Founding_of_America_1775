using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Annex : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(AnnexState);
    }
    void AnnexState()
    {
       State defState= Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastDefendingState).GetComponent<State>();
        if (defState != null)
        {
            defState.OccupyState();
            GameManager.Instance.IsAttackFinish = true;
            GameManager.Instance.ChangeIsAttackValueFalse();
        }
        else
            Debug.LogWarning("def state is null");
    }
}
