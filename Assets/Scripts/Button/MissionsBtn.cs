using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionsBtn : MonoBehaviour
{
    public GameObject NotifyCountGameObject;
    private void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(Onclicked);
        MissionsManager.OnComplate += NotifyCountUpdate;
        MissionsManager.OnClaim += NotifyCountUpdate;
        NotifyCountUpdate();
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(Onclicked);
        MissionsManager.OnComplate -= NotifyCountUpdate;
        MissionsManager.OnClaim += NotifyCountUpdate;
    }
    void Onclicked()
    {
        if (GameManager.Instance.IsAttackFinish && !GameManager.Instance.ÝsAttack)
        {

            UIManager.Instance.HideAllPanel();
            GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();
            UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.Missions);
        }

    }
    void NotifyCountUpdate()
    {
        NotifyCountGameObject.SetActive(true);
        if (NotifyCountGameObject != null)
        {
            if(MissionsManager.CompletedMissionCount> MissionsManager.ClaimedMissionCount)
            {
                NotifyCountGameObject.GetComponentInChildren<TMP_Text>().text =""+ (MissionsManager.CompletedMissionCount - MissionsManager.ClaimedMissionCount);
            }else
            {
                NotifyCountGameObject.SetActive(false);
            }
        }
    }
}
