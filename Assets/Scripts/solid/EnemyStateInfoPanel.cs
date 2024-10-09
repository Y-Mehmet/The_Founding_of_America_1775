using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        currnetState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        if(currnetState != null)
        {
            stateNameText.text= currnetState.name;

            Sprite flag = currnetState.StateIcon;
            if (flag != null)
            {
                flagImage.sprite = flag;
            }
            else
            {
                Debug.LogError("falg is null");
            }
            
        }
        else
        {
            Debug.LogError("curent state is null");
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

        
        if (currnetState != null)
        {
            
            float randomValue = Random.Range(0, 99);
           /// Debug.Log($"geçemedik  sucs {sucs} esca {esca} random {randomValue}");
            if (esca > randomValue)
            {
             //   Debug.Log($"geçemedik  sucs {sucs} esca {esca} random {randomValue}");
                if (sucs>randomValue)
                {
                   // Debug.Log($"geçebildik  sucs {sucs} esca {esca} random {randomValue}");
                    SuccessfulEspionage();
                }
                else
                {
                  //  Debug.Log($"geçemedik  sucs {sucs} esca {esca} random {randomValue}");
                    happinesText.text = "Huh ! A close shave";
                    totalArmyPowerText.text = "Huh ! A close shave";
                    mainResTypeText.text = "Huh ! A close shave";
                }

            }
            else
            {
              //  Debug.Log($"geçemedik  sucs {sucs} esca {esca} random {randomValue}");
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
        
        
            happinesText.text = "Happines: " + currnetState.Morele.ToString();
            totalArmyPowerText.text = "Army: " + currnetState.TotalArmyPower.ToString();
            mainResTypeText.text = "Main Resoruce: " + ((MainResourceType)currnetState.Resources).ToString();

        
    }
  
    public void FailedEspionage()
    {
       


        happinesText.text = "Relations Declined";
        totalArmyPowerText.text = "Spy Attempt Failed";
        mainResTypeText.text = "Spy Attempt Failed";
    }
   
}
