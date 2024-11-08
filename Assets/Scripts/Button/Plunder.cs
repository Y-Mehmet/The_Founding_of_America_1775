using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utility;
public class Plunder : MonoBehaviour
{
    public List<Image> resIconImages = new List<Image>();
    public List<TextMeshProUGUI> resTexts = new List<TextMeshProUGUI>();
    public Button plunderBtn, addPlunderBtn;

    private void OnEnable()
    {
        plunderBtn.onClick.AddListener(() => PlunderBtnCliecked());

        addPlunderBtn.onClick.AddListener(() => PlunderBtnCliecked(true));
        ShowPlunderPanel();
    }
    void PlunderBtnCliecked(bool isAddTrue=false)
    {
        GameManager.Instance.ChangeAttackFinisValueTrue();
        GameManager.Instance.ChangeIsAttackValueFalse();
        GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
        PlunderManager.Instance.PlunderState(isAddTrue);
        UIManager.Instance.GetComponent<HideAllPanelButton>().DoHidePanel();
        if(!isAddTrue)
        SoundManager.instance.Play("Theme");
    }
   
    public void ShowPlunderPanel()
    {
        ItemSetActifFalse();

        State defState = Usa.Instance.FindStateByName(Attack.Instance.lastDefendingState);
   

     //   Debug.LogWarning("lunder panel bilgileri gösterilecek plunder count= " + defState.plunderedResources.Count);

        if ( defState!= null)
        {
            defState.PlunderResource();
            for (int i = 0; i < defState.GetPlundData().Count; i++)
            {
                Debug.Log($" res type ýndex {defState.GetPlundData().ElementAt(i).Key} res value {defState.GetPlundData().ElementAt(i).Value} ");
                resIconImages[i].gameObject.SetActive(true);
                resIconImages[i].sprite = Resources.Load<Sprite>("ResourcesIcon/" + (int) defState.GetPlundData().ElementAt(i).Key);
                resTexts[i].gameObject.SetActive(true);
                resTexts[i].text = "+" + FormatNumber(defState.GetPlundData().ElementAt(i).Value);
                if(i>=resTexts.Count)
                {
                    Debug.LogWarning(" yeterince text yok");

                }
            }
        }
        else
        { Debug.LogWarning("defstate null"); }
        


    }
    void ItemSetActifFalse()
    {
        foreach (var item in resIconImages)
        {
            item.gameObject.SetActive(false);

        }
        foreach (var item in resTexts)
        {
            item.gameObject.SetActive(false);
        }

    }
    private void OnDisable()
    {
        plunderBtn.onClick.RemoveAllListeners();
        addPlunderBtn.onClick.RemoveAllListeners();
        RegionClickHandler.staticState = null;
    }

}
