using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GeneralManager;


public class GeneralInfoPanel : MonoBehaviour
{
    public Image generalCharacterIcon,generalTypeIcon;
    public TMP_Text generalNameText, generalTypeText, rankText, navalRateText, landRateText,
        incramentalEffectText, totalBattelsText, victoriesText, defeatText, dutyStationText, sliderTextExp;
    public Slider generalExperiensSlider;
    private void OnEnable()
    {
        Debug.LogWarning(GeneralManager.generals.Count + " general count"+ GeneralManager.GeneralIndex);
        int index = GeneralManager.GeneralIndex;
        string generalName = GeneralManager.generals[index].Name;
        generalCharacterIcon.sprite = GameDataSo.Instance.GeneralSprite[index];
        generalTypeIcon.sprite = GameDataSo.Instance.GeneralTypeIconSprite[index];
        generalNameText.text= generalName;
        generalTypeText.text = GeneralManager.generals[index].Specialty.ToString();
        string rank = AddSpaceBeforeUpperCase(GeneralManager.generals[index].Rank.ToString());
        rankText.text = rank  ;
        navalRateText.text="+ "+ GeneralManager.generals[index].NavalHelpRate.ToString() ;
        landRateText.text="+ "+ GeneralManager.generals[index].LandHelpRate.ToString();
        incramentalEffectText.text= "+ " + GeneralManager.generals[index].IncraseHeplRate.ToString() ;
        totalBattelsText.text = WarHistory.GetWarCountByGeneral(generalName).ToString();
        victoriesText.text = WarHistory.GetResultCountByGeneral(generalName, WarResultType.Victory).ToString();
        defeatText.text = WarHistory.GetResultCountByGeneral(generalName, WarResultType.Defeat).ToString();
        generalExperiensSlider.minValue = 0;
        generalExperiensSlider.maxValue = GeneralManager.generals[index].ExperienceLimit;
        generalExperiensSlider.value = GeneralManager.generals[index].Experience;
        sliderTextExp.text = GeneralManager.generals[index].Experience + " / " + GeneralManager.generals[index].ExperienceLimit;
        var stateAndGeneral = GeneralManager.stateGenerals.FirstOrDefault(x => (int)x.Value.Specialty == index);

        // stateAndGeneral anahtarýnýn varsayýlan deðer olup olmadýðýný kontrol et
        if (!stateAndGeneral.Equals(default(KeyValuePair<State, General>)))
        {
            // Anahtar bulundu, dutyStationText'e atanacak
            dutyStationText.text = stateAndGeneral.Key.name;
        }
        else
        {
            // Anahtar bulunamadý, alternatif bir iþlem yapabilirsiniz
            dutyStationText.text = "Unassigned";
        }



    }
    private static string AddSpaceBeforeUpperCase(string input)
    {
        return string.Concat(input.Select(c => char.IsUpper(c) && input.IndexOf(c) != 0 ? " " + c.ToString() : c.ToString()));
    }
}
