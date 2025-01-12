using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
using static TroopyManager;
using static MY.NumberUtilitys.Utility;
public class StateAssingForDeploymentCard : MonoBehaviour
{
    public Image stateFlagIcon;
    public TMP_Text navalForceText, landForceText;
    int index;
    public bool isOrigin = false;
    private void OnEnable()
    {
        index = transform.GetSiblingIndex() - 1;
        GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        ShowExportPanelInfo();

    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveListener(OnButtonClicked);
    }
    void ShowExportPanelInfo()
    {

        if (AllyStateList[index]== OriginState && !isOrigin)
        {
            gameObject.SetActive(false);
        }
        else
        {            
            stateFlagIcon.sprite = AllyStateList[index].StateIcon;
            navalForceText.text = FormatNumber(AllyStateList[index].GetNavalArmySize());
            landForceText.text = FormatNumber(AllyStateList[index].GetLandArmySize());
        }
        

    }
    void OnButtonClicked()
    {
      if(isOrigin)
        {
            OriginState = AllyStateList[index];
            if (AllyStateList[index]== DestinationState)
            {
                if(index==0)
                {
                    DestinationState= AllyStateList[1];
                }else
                {
                    DestinationState = AllyStateList[0];
                }
            }
        }else
        {
            DestinationState = AllyStateList[index];
        }
        SoundManager.instance.Play("ButtonClick");
        UIManager.Instance.GetComponent<HideLastPanelButton>().DoHidePanel();
    }
}
