using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AllyStateInfoPanel : MonoBehaviour
{
    public static AllyStateInfoPanel Instance { get; private set; } 
    State currnetState;
    float repeatTime;
    public TMP_Text happinesText, totalArmyPowerText, mainResTypeText, stateNameText;
    public Image flagImage;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }

    }
    private void OnEnable()
    {
        ShowInfo();
    }

    public void ShowInfo()
    {

        currnetState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        repeatTime = GameManager.gameDayTime;
        stateNameText.text = currnetState.name;
        if (currnetState != null)
        {
            Sprite flag = currnetState.StateIcon;
            if ( flag!= null)
            {
                flagImage.sprite = flag;
            }
            else
            {
                Debug.LogError("falg is null");
            }
            InvokeRepeating("GetIntel", 0, repeatTime);
        }
            
        else
        {
            Debug.LogError("curent state is null");
        }


    }
    public void GetIntel()
    {

        {
            happinesText.text = "Happines: " + currnetState.Morele.ToString();
            totalArmyPowerText.text = "Army: " + currnetState.TotalArmyPower.ToString();
            mainResTypeText.text = "Main Resoruce" + ((MainResourceType)currnetState.Resources).ToString();

        }
    }
}
   
