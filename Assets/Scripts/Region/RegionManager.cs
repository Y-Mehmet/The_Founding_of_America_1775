using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionManager : MonoBehaviour
{
    public Text regionNameText;
    public Text happinessText;
    public Text populationText;
    public Text foodStockText;
    public GameObject infoPanel;

    private Dictionary<string, Region> regions;

    void Start()
    {
        // Bölge verilerini baþlatma
        regions = new Dictionary<string, Region>
        {
            { "New York", new Region("New York", 75.5f, 2000000, 500000) },
            { "California", new Region("California", 80.0f, 3000000, 750000) }
            // Diðer bölgeleri ekleyin
        };

        // Paneli baþlangýçta gizle
        infoPanel.SetActive(false);
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
