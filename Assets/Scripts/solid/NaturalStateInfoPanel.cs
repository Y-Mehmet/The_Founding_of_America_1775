using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utility;

public class NaturalStateInfoPanel : MonoBehaviour
{
    public static NaturalStateInfoPanel Instance { get; private set; }
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
        CancelInvoke("GetIntel");
    }
    public void ShowInfo()
    {
        currnetState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        repeatTime = GameManager.gameDayTime;
        if (currnetState != null)
        {
            stateNameText.text = currnetState.name;
            Sprite flag = currnetState.StateIcon;
            if (flag != null)
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
            happinesText.text = "Happines: " +( (int)currnetState.Morele).ToString();
            totalArmyPowerText.text = "Army: " + FormatNumber(currnetState.GetTotalArmyPower());
            mainResTypeText.text = "Main Resoruce" + ((MainResourceType)currnetState.Resources).ToString();

        }
    }
}
