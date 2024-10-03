using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plunder : MonoBehaviour
{
    public List<Image> resIconImages = new List<Image>();
    public List<TextMeshProUGUI> resTexts = new List<TextMeshProUGUI>();
    public Button plunderBtn;

    private void OnEnable()
    {
        plunderBtn.onClick.AddListener(ChangeAttackFinisValueTrue);
        ShowPlunderPanel();
    }
    void ChangeAttackFinisValueTrue()
    {
        GameManager.Instance.ChangeAttackFinisValueTrue();
        GameManager.Instance.ChangeIsAttackValueFalse();
    }
    public void ShowPlunderPanel()
    {
        ItemSetActifFalse();

        State defState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastDefendingState).GetComponent<State>();

     //   Debug.LogWarning("lunder panel bilgileri gösterilecek plunder count= " + defState.plunderedResources.Count);

        if ( defState!= null)
        {
            for (int i = 0; i < defState.GetPlundData().Count; i++)
            {
                Debug.Log($" res type ýndex {defState.GetPlundData().ElementAt(i).Key} res value {defState.GetPlundData().ElementAt(i).Value} ");
                resIconImages[i].gameObject.SetActive(true);
                resIconImages[i].sprite = Resources.Load<Sprite>("ResourcesIcon/" + (int) defState.GetPlundData().ElementAt(i).Key);
                resTexts[i].gameObject.SetActive(true);
                resTexts[i].text = defState.GetPlundData().ElementAt(i).Value.ToString();
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

   
}
