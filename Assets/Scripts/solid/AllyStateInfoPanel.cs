using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utility;
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
    private void OnDisable()
    {
        CancelInvoke("GetIntel"); // Tekrar eden çaðrýyý durdurur.
    }

    public void ShowInfo()
    {

        currnetState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        repeatTime = 1;
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
            InvokeRepeating("GetIntel", 0, 1);
        }
            
        else
        {
            Debug.LogError("curent state is null");
        }


    }
    public void GetIntel()
    {

        {
            happinesText.text = "Happines: " + ((int)currnetState.Morele).ToString();
            totalArmyPowerText.text = "Army: " +FormatNumber(currnetState.GetArmySize());
            mainResTypeText.text = "Main Res" + ((MainResourceType)currnetState.Resources).ToString();

        }
    }
}
   
