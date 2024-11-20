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

                // G�rev index'ini ve tamamlanma durumunu g�ncelle
                TroppyMissions[TroppyMissonsIndex].MissinonIndex++;
                TroppyMissions[TroppyMissonsIndex].IsCompleted = false;

                // Mission count g�ncellemesi
              //  TroppyMissions[TroppyMissonsIndex].ComplatedMissionCount--;

                // Bir sonraki g�reve ge�i� (e�er liste sonuna gelinirse ba�a sar)
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
            gameObject.SetActive(false);  // E�er g�rev tamamlanm��sa UI'yi gizle
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

                // G�rev index'ini ve tamamlanma durumunu g�ncelle
                EconomyMissions[EconomyMissionsIndex].MissinonIndex++;
                EconomyMissions[EconomyMissionsIndex].IsCompleted = false;

                // Mission count g�ncellemesi
              //  EconomyMissions[EconomyMissionsIndex].ComplatedMissionCount--;

                // Bir sonraki g�reve ge�i� (e�er liste sonuna gelinirse ba�a sar)
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
            gameObject.SetActive(false);  // E�er g�rev tamamlanm��sa UI'yi gizle
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

                // G�rev index'ini ve tamamlanma durumunu g�ncelle
                MiscellaneousMissions[MiscellaneousMissionsIndex].MissinonIndex++;
                MiscellaneousMissions[MiscellaneousMissionsIndex].IsCompleted = false;

                // Mission count g�ncellemesi
                //  currentMission.ComplatedMissionCount--;

                // Bir sonraki g�reve ge�i� (e�er liste sonuna gelinirse ba�a sar)
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
            gameObject.SetActive(false);  // E�er g�rev tamamlanm��sa UI'yi gizle
        }
    }



}
