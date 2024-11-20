using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MissionsManager;
public class MissionPanel : MonoBehaviour
{
   public List<GameObject> complatedList = new List<GameObject>();
   public List<GameObject > missionList = new List<GameObject>();
    private void OnEnable()
    {
        OnComplate += ShowComplatedCard;
        OnClaim += ShowMissionCard;
        ShowComplatedCard();
        ShowMissionCard();
    }
    private void OnDisable()
    {
        OnComplate -= ShowComplatedCard;
        OnClaim -= ShowMissionCard;
    }
    void ShowComplatedCard()
    {
       
        for (int i = 0; i < complatedList.Count; i++)
        {
            //if (i <= missionList.Count - CompletedMissionCount - 1)
            //    missionList[i].SetActive(true);
            //else
            //    missionList[i].SetActive(false);
            complatedList[i].SetActive(true);
        }
    }
    void ShowMissionCard()
    {
 
        for (int i = 0; i < missionList.Count; i++)
        {
            //if (i <= missionList.Count - CompletedMissionCount - 1)
            //    missionList[i].SetActive(true);
            //else
            //    missionList[i].SetActive(false);
            missionList[i].SetActive(true);
        }
    }
}
