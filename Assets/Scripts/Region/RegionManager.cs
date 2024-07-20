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
        // Bölge verilerini baþlatma
        regions = new Dictionary<string, Region>
        {
            { "New York", new Region("New York", 75.5f, 2000000, 500000) },
            { "Washington", new Region("Washington", 75.5f, 2000000, 500000) },
            { "California", new Region("California", 80.0f, 3000000, 750000) }
            // Diðer bölgeleri ekleyin
        };

       
    }
    public void ShowRegionInfo(string regionName)
    {
        if (regions.ContainsKey(regionName))
        {
            Region region = regions[regionName];

            regionNameText.text = "Name: " + region.Name;
            happinessText.text = "Happiness: " + region.Happiness + "%";
            populationText.text = "Population: " + region.Population;
            foodStockText.text = "Food Stock: " + region.FoodStock;

            // Paneli göster
            infoPanel.SetActive(true);
        }
    }


}
