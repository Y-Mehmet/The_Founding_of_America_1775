using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NaturalStateInfoPanel : MonoBehaviour
{
    public static NaturalStateInfoPanel Instance { get; private set; }
    State currnetState;
    float repeatTime;
    public TMP_Text happinesText, totalArmyPowerText, mainResTypeText;
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
        if (currnetState != null)
            InvokeRepeating("GetIntel", 0, repeatTime);
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
