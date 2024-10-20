using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegionManager : MonoBehaviour
{

    public TextMeshProUGUI a_regionNameText;
    public TextMeshProUGUI a_happinessText;
    public TextMeshProUGUI a_populationText;
    public TextMeshProUGUI a_foodStockText;
    public TextMeshProUGUI e_regionNameText;
    public TextMeshProUGUI e_happinessText;
    public TextMeshProUGUI e_populationText;
    public TextMeshProUGUI e_foodStockText;
    public TextMeshProUGUI n_regionNameText;
    public TextMeshProUGUI n_happinessText;
    public TextMeshProUGUI n_populationText;
    public TextMeshProUGUI n_foodStockText;
    public GameObject infoPanel;
    public Image a_StateIcon;
    public Image e_StateIcon;
    public Image n_StateIcon;
    

    public static RegionManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        { Destroy(gameObject); }
    }

   
    public void ShowAllyRegionInfo(string regionName)
    {
       
            
            //State state = Usa.Instance.gameObject.transform.Find(regionName).gameObject.GetComponent<State>();
           State state= RegionClickHandler.Instance.currentState.GetComponent<State>();
            if (state != null)
            {
                a_regionNameText.text = regionName;
                a_StateIcon.sprite = state.StateIcon;

                a_happinessText.text = "Happiness: %" + state.Morele;
                a_populationText.text = "Population: " + state.Population;
                a_foodStockText.text = "Fav Resource: " + state.Resources;

            state.OnMoreleChanged += UpdateAllayUIInfoPanel;

        }
            else
            {
                Debug.LogWarning("stateden icon al�namad� state i�ermiyor");
            }
            
           
            


            // Paneli g�ster
            infoPanel.SetActive(true);
        
    }
    void UpdateAllayUIInfoPanel(float Morele, State state2)
    {
        State state = RegionClickHandler.Instance.currentState.GetComponent<State>();
        a_happinessText.text = "Happiness: %" + state.Morele;
        a_populationText.text = "Population: " + state.Population;
        a_foodStockText.text = "Fav Resource: " + state.Resources;
    }
    public void ShowEnemyRegionInfo(string regionName)
    {


        State state = Usa.Instance.gameObject.transform.Find(regionName).gameObject.GetComponent<State>();
        if (state != null)
        {
            e_regionNameText.text = regionName;

            e_StateIcon.sprite = state.StateIcon;

            e_happinessText.text = "Relationship: intelligence could not be received";
            e_populationText.text = "ArmyPower: intelligence could not be received";
            e_foodStockText.text = "Food Stock: intelligence could not be received";




        }
        else
        {
            Debug.LogWarning("stateden icon al�namad� state i�ermiyor");
        }





        // Paneli g�ster
        infoPanel.SetActive(true);

    }
    public void ShowNaturalRegionInfo(string regionName)
    {


        State state = Usa.Instance.gameObject.transform.Find(regionName).gameObject.GetComponent<State>();
        if (state != null)
        {
            n_regionNameText.text = regionName;

            n_StateIcon.sprite = state.StateIcon;




            n_happinessText.text = "Happiness: %" + state.Morele;
            n_populationText.text = "Population: " + state.Population;
            n_foodStockText.text = "Food Stock: " + state.Resources;




        }
        else
        {
            Debug.LogWarning("stateden icon al�namad� state i�ermiyor");
        }





        // Paneli g�ster
        infoPanel.SetActive(true);

    }

    //    a_happinessText.text = "Relationship: intelligence could not be received";
    //    a_populationText.text = "ArmyPower: intelligence could not be received";
    //    a_foodStockText.text = "Food Stock: intelligence could not be received";
    public void GetEnemyIntel(float cost)
    {

        float thresoldEspinoge = Random.Range(0, cost);
        if((thresoldEspinoge % GameManager.Instance.spyCostModPurchases )>GameManager.Instance.thresholdForSuccesfulEspionage)
        {
            SuccessfulEspionage();

        }
        else
        {
            FailedEspionage();
        }
       
    }
    public void FailedEspionage()
    {
         State state = Usa.Instance.gameObject.transform.Find(e_regionNameText.text).gameObject.GetComponent<State>();


        e_happinessText.text = "Relationship: Your relationship level dropped because the spies were caught";
        e_populationText.text = "ArmyPower: intelligence could not be received";
        e_foodStockText.text = "Food Stock: intelligence could not be received";
    }
    void SuccessfulEspionage()
    {
        State state = Usa.Instance.gameObject.transform.Find(e_regionNameText.text).gameObject.GetComponent<State>();


        e_happinessText.text = "Relationship : %" + state.Morele;
        e_populationText.text = "ArmyPower: " + (int)state.GetComponent<State>().TotalArmyCalculator();
        e_foodStockText.text = "Food Stock: " + state.Resources;
    }
    public void GetNaturalIntel()
    {
        State state = Usa.Instance.gameObject.transform.Find(n_regionNameText.text).gameObject.GetComponent<State>();


        n_happinessText.text = "Relationship : %" + state.Morele;
        n_populationText.text = "ArmyPower: " + (int)state.GetComponent<State>().TotalArmyCalculator();
        n_foodStockText.text = "Food Stock: " + state.Resources;
    }


}
