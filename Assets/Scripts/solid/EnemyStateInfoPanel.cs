using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static MY.NumberUtilitys.Utility;
using static RegionClickHandler;
public class EnemyStateInfoPanel : MonoBehaviour
{
    public static EnemyStateInfoPanel Instance { get; private set; }
    State currnetState;
    
    public TMP_Text happinesText, totalArmyPowerText, mainResTypeText, stateNameText;
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
    private void Start()
    {
        happinesText.text = "?! Send Spy";
        totalArmyPowerText.text = "?! Send Spy";
        mainResTypeText.text = "?! Send Spy";
    }
    private void OnEnable()
    {
        State currnetState = RegionClickHandler.staticState;

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
                Debug.LogWarning("Flag is null, no icon to display.");
            }
        }
        else
        {
            Debug.LogWarning("Current state is null, skipping icon and text updates.");
        }

    }
    private void OnDisable()
    {
        happinesText.text = "?! Send Spy";
        totalArmyPowerText.text = "?! Send Spy";
        mainResTypeText.text = "?! Send Spy";
    }

    public void ShowInfo(float esca, float sucs)
    {
        State currnetState = RegionClickHandler.staticState;

        if (currnetState != null)
        {
            
            float randomValue = Random.Range(0, 99);
           /// Debug.Log($"ge�emedik  sucs {sucs} esca {esca} random {randomValue}");
            if (esca > randomValue)
            {
             //   Debug.Log($"ge�emedik  sucs {sucs} esca {esca} random {randomValue}");
                if (sucs>randomValue)
                {
                   // Debug.Log($"ge�ebildik  sucs {sucs} esca {esca} random {randomValue}");
                    SuccessfulEspionage();
                }
                else
                {
                  //  Debug.Log($"ge�emedik  sucs {sucs} esca {esca} random {randomValue}");
                    happinesText.text = "Huh ! A close shave";
                    totalArmyPowerText.text = "Huh ! A close shave";
                    mainResTypeText.text = "Huh ! A close shave";
                    MessageManager.AddMessage("Luckily, our casualties managed to escape at the last moment. Let�s be more cautious next time.");
                }

            }
            else
            {
              //  Debug.Log($"ge�emedik  sucs {sucs} esca {esca} random {randomValue}");
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
        MissionsManager.AddTotalSucsessIntel(1);


       
        happinesText.text = "Relations: " + ((int)staticState.Morele).ToString();
        totalArmyPowerText.text = "Army: " + FormatNumber(staticState.GetArmySize());
        mainResTypeText.text = "Main Res: " + ((MainResourceType)staticState.Resources).ToString();


    }
  
    public void FailedEspionage()
    {
       


        happinesText.text = "Relations Declined";
        totalArmyPowerText.text = "Spy Attempt Failed";
        mainResTypeText.text = "Spy Attempt Failed";
        MessageManager.AddMessage("Oh no, our spies have been caught! Relations between the two states ("+RegionClickHandler.Instance.currentState.name+") have deteriorated by 50 points");
        staticState.ReduceEnemyMorale(-50);
    }
   
}
