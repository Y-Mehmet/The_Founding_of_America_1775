using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GeneralInfoPanel : MonoBehaviour
{
    public Image generalCharacterIcon,generalTypeIcon;
    public TMP_Text generalNameText, generalTypeText, rankText, navalRateText, landRateText, incramentalEffectText, totalBattelsText, victoriesText, defeatText;
    public Slider generalExperiensSlider;
    private void OnEnable()
    {
        Debug.LogWarning(GeneralManager.generals.Count + " general count"+ GeneralManager.GeneralIndex);
        int index = GeneralManager.GeneralIndex;
        generalCharacterIcon.sprite = GameDataSo.Instance.GeneralSprite[index];
        generalTypeIcon.sprite = GameDataSo.Instance.GeneralTypeIconSprite[index];
        generalNameText.text= GeneralManager.generals[index].Name;
        generalTypeText.text = GeneralManager.generals[index].Specialty.ToString();
        rankText.text = GeneralManager.generals[index].Rank.ToString()  ;
        navalRateText.text= GeneralManager.generals[index].NavalHelpRate.ToString() ;
        landRateText.text= GeneralManager.generals[index].LandHelpRate.ToString();
        incramentalEffectText.text= GeneralManager.generals[index].IncraseHeplRate.ToString() ;
        
    }
}
