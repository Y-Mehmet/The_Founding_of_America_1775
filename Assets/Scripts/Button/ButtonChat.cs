using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChat : MonoBehaviour
{
   public GameObject Alarm;
    public TMP_Text unreadingCountText;
    private void OnEnable()
    {
        MessageManager.OnAddMessage += OnAddMassegeListener;
        GetComponent<Button>().onClick.AddListener(OnClicked);
    }
    private void OnDisable()
    {

        MessageManager.OnAddMessage -= OnAddMassegeListener;
    }
    void OnAddMassegeListener(int count)
    {
        if(count > 0)
        {
            Alarm.gameObject.SetActive(true);
            unreadingCountText.text = count + "+";
        }else
        {
            Alarm.gameObject.SetActive(false);
        }
       
    }
    void OnClicked()
    {

        if(GameManager.Instance.IsAttackFinish)
        {
         //   RegionClickHandler.Instance.CloseBtn_CloseAll();
          //  GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
            UIManager.Instance.HideAllPanel();
            GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();

            UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.MessagePanel);
            MessageManager.unreadMessageCount = 0;
            MessageManager.OnAddMessage?.Invoke(0);
        }

    }
}
