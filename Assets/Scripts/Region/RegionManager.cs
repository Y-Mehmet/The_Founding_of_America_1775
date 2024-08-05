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

    public Dictionary<string, Region> regions;
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
        // B�lge verilerini ba�latma
        regions = new Dictionary<string, Region>
        {
    { "Washington", new Region("Washington", 60.0f, 10000, 1000) },
    { "Oregon", new Region("Oregon", 55.0f, 8000, 800) },
    { "Idaho", new Region("Idaho", 50.0f, 5000, 500) },
    { "Montana", new Region("Montana", 50.0f, 5000, 500) },
    { "North Dakota", new Region("North Dakota", 50.0f, 5000, 500) },
    { "Minnesota", new Region("Minnesota", 55.0f, 8000, 800) },
    { "Wisconsin", new Region("Wisconsin", 55.0f, 8000, 800) },
    { "Wyoming", new Region("Wyoming", 50.0f, 5000, 500) },
    { "South Dakota", new Region("South Dakota", 50.0f, 5000, 500) },
    { "Iowa", new Region("Iowa", 55.0f, 8000, 800) },
    { "Illinois", new Region("Illinois", 60.0f, 10000, 1000) },
    { "Michigan", new Region("Michigan", 60.0f, 10000, 1000) },
    { "New York", new Region("New York", 75.0f, 15000, 1500) },
    { "Vermont", new Region("Vermont", 50.0f, 5000, 500) },
    { "New Hampshire", new Region("New Hampshire", 50.0f, 5000, 500) },
    { "Maine", new Region("Maine", 50.0f, 5000, 500) },
    { "Massachusetts", new Region("Massachusetts", 60.0f, 10000, 1000) },
    { "New Jersey", new Region("New Jersey", 60.0f, 10000, 1000) },
    { "Pennsylvania", new Region("Pennsylvania", 60.0f, 10000, 1000) },
    { "Maryland", new Region("Maryland", 55.0f, 8000, 800) },
    { "Ohio", new Region("Ohio", 60.0f, 10000, 1000) },
    { "Indiana", new Region("Indiana", 55.0f, 8000, 800) },
    { "Virginia", new Region("Virginia", 60.0f, 10000, 1000) },
    { "North Carolina", new Region("North Carolina", 60.0f, 10000, 1000) },
    { "South Carolina", new Region("South Carolina", 55.0f, 8000, 800) },
    { "Georgia", new Region("Georgia", 60.0f, 10000, 1000) },
    { "Florida", new Region("Florida", 60.0f, 10000, 1000) },
    { "Alabama", new Region("Alabama", 55.0f, 8000, 800) },
    { "Mississippi", new Region("Mississippi", 55.0f, 8000, 800) },
    { "Louisiana", new Region("Louisiana", 55.0f, 8000, 800) },
    { "Texas", new Region("Texas", 70.0f, 12000, 1200) },
    { "New Mexico", new Region("New Mexico", 50.0f, 5000, 500) },
    { "Arizona", new Region("Arizona", 50.0f, 5000, 500) },
    { "California", new Region("California", 75.0f, 15000, 1500) },
    { "Nevada", new Region("Nevada", 50.0f, 5000, 500) },
    { "Utah", new Region("Utah", 50.0f, 5000, 500) },
    { "Colorado", new Region("Colorado", 55.0f, 8000, 800) },
    { "Kansas", new Region("Kansas", 55.0f, 8000, 800) },
    { "Nebraska", new Region("Nebraska", 50.0f, 5000, 500) },
    { "Missouri", new Region("Missouri", 55.0f, 8000, 800) },
    { "Kentucky", new Region("Kentucky", 55.0f, 8000, 800) },
    { "West Virginia", new Region("West Virginia", 50.0f, 5000, 500) },
    { "Tennessee", new Region("Tennessee", 55.0f, 8000, 800) },
    { "Arkansas", new Region("Arkansas", 55.0f, 8000, 800) },
    { "Oklahoma", new Region("Oklahoma", 50.0f, 5000, 500) }
            

        };

        


    }
    public void ShowRegionInfo(string regionName)
    {
        if (regions.ContainsKey(regionName))
        {
            Region region = regions[regionName];
            GameObject state = Usa.Instance.gameObject.transform.Find(regionName).gameObject;
            if (state != null)
            {
                regionNameText.text = "Name: " + region.Name;
                
                StateIcon.sprite = Usa.Instance.gameObject.transform.Find(region.Name).GetComponent<State>().StateIcon;

                if (state.GetComponent<State>().stateType == StateType.Ally )
                {
                   GetIntel();
                }
                else
                {
                    happinessText.text = "Happiness: intelligence could not be received";
                    populationText.text = "Population: intelligence could not be received";
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
    }
     public void GetIntel()
    {
        string regionName = regionNameText.text.Substring(6);
        Region region = regions[regionName];
        GameObject state = Usa.Instance.gameObject.transform.Find(regionName).gameObject;
        happinessText.text = "Happiness: %" + region.Happiness ;
        populationText.text = "Population: " + region.Population;
        foodStockText.text = "Food Stock: " + region.FoodStock;
    }


}
