using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static MissionsManager;

public class ComplatedMissionCard : MonoBehaviour
{
    public MissionType Type;
    public TMP_Text nameText,   goldValue;
    public Button claimBtn;
  
    private void OnEnable()
    {
        OnComplate += UpdateUI;

        UpdateUI();
    }
    private void OnDisable()
    {
        claimBtn.onClick.RemoveAllListeners();
        OnComplate -= UpdateUI;
    }
    void UpdateUI()
    {
        switch (Type)
        {
            case MissionType.Troopy:
                UpdateTroopyMissionUI();
                break;

            case MissionType.Economy:
                UpdateEconomyMissionUI();
                break;

            case MissionType.Miscellaneous:
                UpdateMiscellaneousMissionUI();
                break;

            default:
                break;
        }
    }

    void UpdateTroopyMissionUI()
    {
     

        if (TroppyMissions[TroppyMissonsIndex].IsCompleted)
        {
            nameText.text = TroppyMissions[TroppyMissonsIndex].MissionName;
            goldValue.text = TroppyMissions[TroppyMissonsIndex].RewardGold[TroppyMissions[TroppyMissonsIndex].MissinonIndex].ToString();

            claimBtn.onClick.AddListener(() =>
            {
                SoundManager.instance.Play("Cash");
                ResourceManager.Instance.AddResource(ResourceType.Gold, TroppyMissions[TroppyMissonsIndex].RewardGold[TroppyMissions[TroppyMissonsIndex].MissinonIndex]);
                ClaimedMissionCount++;
                Debug.LogWarning($"{TroppyMissions[TroppyMissonsIndex].MissionName} mission clicked at index {TroppyMissonsIndex}");

                // Görev index'ini ve tamamlanma durumunu güncelle
                TroppyMissions[TroppyMissonsIndex].MissinonIndex++;
                TroppyMissions[TroppyMissonsIndex].IsCompleted = false;

                // Mission count güncellemesi
              //  TroppyMissions[TroppyMissonsIndex].ComplatedMissionCount--;

                // Bir sonraki göreve geçiþ (eðer liste sonuna gelinirse baþa sar)
                TroppyMissonsIndex++;
                if (TroppyMissonsIndex >= TroppyMissions.Count)
                {
                    TroppyMissonsIndex = 0;
                }

                OnClaim?.Invoke();
                gameObject.SetActive(false);  // UI'yi gizle
            });
        }
        else
        {
            Debug.Log($"{TroppyMissions[TroppyMissonsIndex].MissionName} at index {TroppyMissonsIndex} is already completed");
            gameObject.SetActive(false);  // Eðer görev tamamlanmýþsa UI'yi gizle
        }
    }
    void UpdateEconomyMissionUI()
    {
      

        if (EconomyMissions[EconomyMissionsIndex].IsCompleted)
        {
            nameText.text = EconomyMissions[EconomyMissionsIndex].MissionName;
            goldValue.text = EconomyMissions[EconomyMissionsIndex].RewardGold[EconomyMissions[EconomyMissionsIndex].MissinonIndex].ToString();

            claimBtn.onClick.AddListener(() =>
            {
                SoundManager.instance.Play("Cash");
                ResourceManager.Instance.AddResource(ResourceType.Gold, EconomyMissions[EconomyMissionsIndex].RewardGold[EconomyMissions[EconomyMissionsIndex].MissinonIndex]);
                ClaimedMissionCount++;
                Debug.LogWarning($"{EconomyMissions[EconomyMissionsIndex].MissionName} mission clicked at index {EconomyMissionsIndex}");

                // Görev index'ini ve tamamlanma durumunu güncelle
                EconomyMissions[EconomyMissionsIndex].MissinonIndex++;
                EconomyMissions[EconomyMissionsIndex].IsCompleted = false;

                // Mission count güncellemesi
              //  EconomyMissions[EconomyMissionsIndex].ComplatedMissionCount--;

                // Bir sonraki göreve geçiþ (eðer liste sonuna gelinirse baþa sar)
                EconomyMissionsIndex++;
                if (EconomyMissionsIndex >= EconomyMissions.Count)
                {
                    EconomyMissionsIndex = 0;
                }

                OnClaim?.Invoke();
                gameObject.SetActive(false);  // UI'yi gizle
            });
        }
        else
        {
            Debug.Log($"{EconomyMissions[EconomyMissionsIndex].MissionName} at index {EconomyMissionsIndex} is already completed");
            gameObject.SetActive(false);  // Eðer görev tamamlanmýþsa UI'yi gizle
        }
    }
    void UpdateMiscellaneousMissionUI()
    {
      

        if (MiscellaneousMissions[MiscellaneousMissionsIndex].IsCompleted)
        {
            nameText.text = MiscellaneousMissions[MiscellaneousMissionsIndex].MissionName;
            goldValue.text = MiscellaneousMissions[MiscellaneousMissionsIndex].RewardGold[MiscellaneousMissions[MiscellaneousMissionsIndex].MissinonIndex].ToString();

            claimBtn.onClick.AddListener(() =>
            {
                SoundManager.instance.Play("Cash");
                ResourceManager.Instance.AddResource(ResourceType.Gold, MiscellaneousMissions[MiscellaneousMissionsIndex].RewardGold[MiscellaneousMissions[MiscellaneousMissionsIndex].MissinonIndex]);
                ClaimedMissionCount++;
                Debug.LogWarning($"{MiscellaneousMissions[MiscellaneousMissionsIndex].MissionName} mission clicked at index {MiscellaneousMissionsIndex}");

                // Görev index'ini ve tamamlanma durumunu güncelle
                MiscellaneousMissions[MiscellaneousMissionsIndex].MissinonIndex++;
                MiscellaneousMissions[MiscellaneousMissionsIndex].IsCompleted = false;

                // Mission count güncellemesi
                //  currentMission.ComplatedMissionCount--;

                // Bir sonraki göreve geçiþ (eðer liste sonuna gelinirse baþa sar)
                MiscellaneousMissionsIndex++;
                if (MiscellaneousMissionsIndex >= MiscellaneousMissions.Count)
                {
                    MiscellaneousMissionsIndex = 0;
                }

                OnClaim?.Invoke();
                gameObject.SetActive(false);  // UI'yi gizle
            });
        }
        else
        {
            Debug.Log($"{MiscellaneousMissions[MiscellaneousMissionsIndex].MissionName} at index {MiscellaneousMissionsIndex} is already completed");
            gameObject.SetActive(false);  // Eðer görev tamamlanmýþsa UI'yi gizle
        }
    }



}
