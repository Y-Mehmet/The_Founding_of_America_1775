using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Annex : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(AnnexState);
    }
    void AnnexState()
    {
       State defState= Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastDefendingState).GetComponent<State>();
        if (defState != null)
        {
            defState.OccupyState();
            GameManager.AllyStateList.Add(defState);
            GameManager.Instance.onAllyStateChanged?.Invoke(defState,true);
            GameManager.Instance.IsAttackFinish = true;
            GameManager.Instance.ChangeIsAttackValueFalse();
            RegionClickHandler.staticState = null;
            UIManager.Instance.GetComponent<HideAllPanelButton>().DoHidePanel();
            GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
            SoundManager.instance.Play("Theme");
        }
        else
            Debug.LogWarning("def state is null");
    }
    private void OnDisable()
    {
        

        gameObject.GetComponent<Button>().onClick.RemoveListener(AnnexState);
    }
}
