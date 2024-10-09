using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateInfoPanel : MonoBehaviour
{
    public static EnemyStateInfoPanel Instance { get; private set; }
    State currnetState;
    
    public TMP_Text happinesText, totalArmyPowerText, mainResTypeText;
    public Image flagImage;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void OnEnable()
    {
        currnetState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        if(currnetState != null)
        {
            happinesText.text = "?! Send Spy";
            totalArmyPowerText.text = "?! Send Spy";
            mainResTypeText.text = "?! Send Spy";
        }
        else
        {
            Debug.LogError("curent state is null");
        }
    }

    public void ShowInfo(float cost)
    {

        
        if (currnetState != null)
        {
            float thresoldEspinoge = Random.RandomRange(0, cost);
            if ((thresoldEspinoge % GameManager.Instance.spyCostModPurchases) > GameManager.Instance.thresholdForSuccesfulEspionage)
            {
                SuccessfulEspionage();

            }
            else
            {
                FailedEspionage();
            }
        }
        else
        {
            Debug.LogError("curent state is null");
        }




    }
    public void SuccessfulEspionage()
    {
        
        {
            happinesText.text = "Happines: " + currnetState.Morele.ToString();
            totalArmyPowerText.text = "Army: " + currnetState.TotalArmyPower.ToString();
            mainResTypeText.text = "Main Resoruce" + ((MainResourceType)currnetState.Resources).ToString();

        }
    }
  
    public void FailedEspionage()
    {
       


        happinesText.text = "Relations Declined";
        totalArmyPowerText.text = "Spy Attempt Failed";
        mainResTypeText.text = "Spy Attempt Failed";
    }
   
}
