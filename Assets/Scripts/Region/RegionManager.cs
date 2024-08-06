using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegionManager : MonoBehaviour
{

    public TextMeshProUGUI regionNameText;
    public TextMeshProUGUI happinessText;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI foodStockText;
    public GameObject infoPanel;
    public Image StateIcon;

    
    public static RegionManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        { Destroy(gameObject); }
    }

    void Start()
    {
     

        


    }
    public void ShowRegionInfo(string regionName)
    {
       
            
            State state = Usa.Instance.gameObject.transform.Find(regionName).gameObject.GetComponent<State>();
            if (state != null)
            {
                regionNameText.text = regionName;
                
                StateIcon.sprite = state.StateIcon;

                if (state.GetComponent<State>().stateType == StateType.Ally )
                {
                    
                    happinessText.text = "Happiness: %" + state.Morele;
                    populationText.text = "Population: " + state.Population;
                    foodStockText.text = "Food Stock: " + state.Resources;
                }
                else
                {
                    happinessText.text = "Relationship: intelligence could not be received";
                    populationText.text = "ArmyPower: intelligence could not be received";
                    foodStockText.text = "Food Stock: intelligence could not be received";
                }

                
            }
            else
            {
                Debug.LogWarning("stateden icon al�namad� state i�ermiyor");
            }
            
           
            


            // Paneli g�ster
            infoPanel.SetActive(true);
        
    }
     public void GetIntel()
    {
        State state = Usa.Instance.gameObject.transform.Find(regionNameText.text).gameObject.GetComponent<State>();
       
        
        happinessText.text = "Relationship : %" + state.Morele ;
        populationText.text = "ArmyPower: " +(int)  state.GetComponent<State>().TotalArmyCalculator();
        foodStockText.text = "Food Stock: " + state.Resources;
    }


}
