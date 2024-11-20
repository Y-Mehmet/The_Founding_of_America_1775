using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MissionsManager;
public class MissionCard : MonoBehaviour
{
 
    public MissionType Type;
    public TMP_Text nameText, descriptionText, rateText, goldValue;
    public Slider slider;
    private void OnEnable()
    {
        OnClaim += UpdateUI;
        UpdateUI();


    }
    private void OnDisable()
    {
        OnClaim -= UpdateUI;
    }
    void UpdateUI()
    {
        // Mission t�r�ne g�re hangi listeyi ve index'i kullanaca��m�z� belirleyece�iz
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
       
        if (!TroppyMissions[TroppyMissonsIndex].IsCompleted)
        {
            // UI elemanlar�n� g�ncelle
            nameText.text = TroppyMissions[TroppyMissonsIndex].MissionName;
            descriptionText.text = TroppyMissions[TroppyMissonsIndex].Description;
            rateText.text = TroppyMissions[TroppyMissonsIndex].ComplatedMissionCount + "/" + TroppyMissions[TroppyMissonsIndex].MissionCount[TroppyMissions[TroppyMissonsIndex].MissinonIndex];
            slider.maxValue = TroppyMissions[TroppyMissonsIndex].MissionCount[TroppyMissions[TroppyMissonsIndex].MissinonIndex];
            slider.value = TroppyMissions[TroppyMissonsIndex].ComplatedMissionCount;
            goldValue.text = TroppyMissions[TroppyMissonsIndex].RewardGold[TroppyMissions[TroppyMissonsIndex].MissinonIndex].ToString();
        }
        else
        {
            Debug.Log($"Mission {TroppyMissions[TroppyMissonsIndex].MissionName} completed at index {TroppyMissonsIndex}");
            OnComplate?.Invoke(); // G�rev tamamland�ysa event tetikle
            gameObject.SetActive(false); // g�rev tamamland���nda UI'yi gizle
        }
    }
    void UpdateEconomyMissionUI()
    {
      
        if (!EconomyMissions[EconomyMissionsIndex].IsCompleted)
        {
            // UI elemanlar�n� g�ncelle
            nameText.text = EconomyMissions[EconomyMissionsIndex].MissionName;
            descriptionText.text = EconomyMissions[EconomyMissionsIndex].Description;
            rateText.text = EconomyMissions[EconomyMissionsIndex].ComplatedMissionCount + "/" + EconomyMissions[EconomyMissionsIndex].MissionCount[EconomyMissions[EconomyMissionsIndex].MissinonIndex];
            slider.maxValue = EconomyMissions[EconomyMissionsIndex].MissionCount[EconomyMissions[EconomyMissionsIndex].MissinonIndex];
            slider.value = EconomyMissions[EconomyMissionsIndex].ComplatedMissionCount;
            goldValue.text = EconomyMissions[EconomyMissionsIndex].RewardGold[EconomyMissions[EconomyMissionsIndex].MissinonIndex].ToString();
        }
        else
        {
            Debug.Log($"Mission {EconomyMissions[EconomyMissionsIndex].MissionName} completed at index {EconomyMissionsIndex}");
            OnComplate?.Invoke(); // G�rev tamamland�ysa event tetikle
            gameObject.SetActive(false); // g�rev tamamland���nda UI'yi gizle
        }
    }
    void UpdateMiscellaneousMissionUI()
    {
        
        if (!MiscellaneousMissions[MiscellaneousMissionsIndex].IsCompleted)
        {
            // UI elemanlar�n� g�ncelle
            nameText.text = MiscellaneousMissions[MiscellaneousMissionsIndex].MissionName;
            descriptionText.text = MiscellaneousMissions[MiscellaneousMissionsIndex].Description;
            rateText.text = MiscellaneousMissions[MiscellaneousMissionsIndex].ComplatedMissionCount + "/" + MiscellaneousMissions[MiscellaneousMissionsIndex].MissionCount[MiscellaneousMissions[MiscellaneousMissionsIndex].MissinonIndex];
            slider.maxValue = MiscellaneousMissions[MiscellaneousMissionsIndex].MissionCount[MiscellaneousMissions[MiscellaneousMissionsIndex].MissinonIndex];
            slider.value = MiscellaneousMissions[MiscellaneousMissionsIndex].ComplatedMissionCount;
            goldValue.text = MiscellaneousMissions[MiscellaneousMissionsIndex].RewardGold[MiscellaneousMissions[MiscellaneousMissionsIndex].MissinonIndex].ToString();
        }
        else
        {
            Debug.Log($"Mission {MiscellaneousMissions[MiscellaneousMissionsIndex].MissionName} completed at index {MiscellaneousMissionsIndex}");
            OnComplate?.Invoke(); // G�rev tamamland�ysa event tetikle
            gameObject.SetActive(false); // g�rev tamamland���nda UI'yi gizle
        }
    }


}
