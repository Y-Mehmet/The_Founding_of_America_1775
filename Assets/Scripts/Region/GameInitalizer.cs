﻿using System.Collections.Generic;
using UnityEngine;

public class GameInitalizer : MonoBehaviour
{
    public Dictionary<string, Region> regions;
    private void Start()
    {

        // Initialize the Neighbor class with city data

        regions = new Dictionary<string, Region>();

        Neighbor game = Neighbor.Instance;
        InitializeCities();
        InitializeNeighbors();
        InitializedStateDataValue();






    }


    private void InitializeCities()
    {
        string[] cities = {
            "Washington", "Oregon", "Idaho", "Montana", "North Dakota", "Minnesota", "Wisconsin",
            "Wyoming", "South Dakota", "Iowa", "Illinois", "Michigan", "New York", "Vermont",
            "New Hampshire", "Maine", "Massachusetts", "New Jersey", "Pennsylvania", "Maryland",
            "Ohio", "Indiana", "Virginia", "North Carolina", "South Carolina", "Georgia",
            "Florida", "Alabama", "Mississippi", "Louisiana", "Texas", "New Mexico", "Arizona",
            "California", "Nevada", "Utah", "Colorado", "Kansas", "Nebraska", "Missouri",
            "Kentucky", "West Virginia", "Tennessee", "Arkansas", "Oklahoma"
        };

        foreach (string city in cities)
        {
            Neighbor.Instance.AddCity(city);
           
        }
    }
 
   
    private void InitializedStateDataValue()
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
        // UnitArmyPower ve ArmySize hesaplama
        System.Random rand = new System.Random();

        foreach (var region in regions.Values)
        {
           
            
            region.UnitArmyPower = (float)(0.75 + rand.NextDouble() * 0.75); // 0.75 ile 1.5 arasında rastgele
            region.ArmySize = (int)(region.Population * 0.25); // Nüfusun %25'i
        }
        foreach (Transform state in Usa.Instance.transform)
        {
            if (state.GetComponent<State>() != null )
            {
                State s = state.GetComponent<State>();
                if (regions.ContainsKey(state.name))
                {
                    Region region = regions[state.name];
                    s.StateName = region.Name;
                    s.UnitArmyPower =  region.UnitArmyPower;
                    s.ArmySize= region.ArmySize;
                    s.Morele = region.Morale;
                    s.Resources= region.Resources;
                    s.Population= region.Population;
                    Debug.Log($"{s.name} adlı statenin bilgileri {s.Morele}");
                    


                }
                else
                {
                    Debug.LogWarning(" state ismi regen arrayde bulunamadı "+state.name);
                }
            }
            else
            {
                Debug.LogWarning(" state bulunamadı ");
            }
            

        }
        
     }
    private void InitializeNeighbors()
    {
        // Komşuluk ilişkilerini ekle
        Neighbor.Instance.AddNeighbor("Washington", "Oregon");
        Neighbor.Instance.AddNeighbor("Washington", "Idaho");

        Neighbor.Instance.AddNeighbor("Oregon", "Washington");
        Neighbor.Instance.AddNeighbor("Oregon", "Idaho");
        Neighbor.Instance.AddNeighbor("Oregon", "California");
        Neighbor.Instance.AddNeighbor("Oregon", "Nevada");

        Neighbor.Instance.AddNeighbor("Idaho", "Washington");
        Neighbor.Instance.AddNeighbor("Idaho", "Oregon");
        Neighbor.Instance.AddNeighbor("Idaho", "Nevada");
        Neighbor.Instance.AddNeighbor("Idaho", "Utah");
        Neighbor.Instance.AddNeighbor("Idaho", "Wyoming");
        Neighbor.Instance.AddNeighbor("Idaho", "Montana");

        Neighbor.Instance.AddNeighbor("Montana", "Idaho");
        Neighbor.Instance.AddNeighbor("Montana", "Wyoming");
        Neighbor.Instance.AddNeighbor("Montana", "North Dakota");
        Neighbor.Instance.AddNeighbor("Montana", "South Dakota");

        Neighbor.Instance.AddNeighbor("North Dakota", "Montana");
        Neighbor.Instance.AddNeighbor("North Dakota", "South Dakota");
        Neighbor.Instance.AddNeighbor("North Dakota", "Minnesota");

        Neighbor.Instance.AddNeighbor("Minnesota", "North Dakota");
        Neighbor.Instance.AddNeighbor("Minnesota", "South Dakota");
        Neighbor.Instance.AddNeighbor("Minnesota", "Iowa");
        Neighbor.Instance.AddNeighbor("Minnesota", "Wisconsin");

        Neighbor.Instance.AddNeighbor("Wisconsin", "Minnesota");
        Neighbor.Instance.AddNeighbor("Wisconsin", "Iowa");
        Neighbor.Instance.AddNeighbor("Wisconsin", "Illinois");

        Neighbor.Instance.AddNeighbor("Wyoming", "Montana");
        Neighbor.Instance.AddNeighbor("Wyoming", "South Dakota");
        Neighbor.Instance.AddNeighbor("Wyoming", "Nebraska");
        Neighbor.Instance.AddNeighbor("Wyoming", "Colorado");
        Neighbor.Instance.AddNeighbor("Wyoming", "Utah");
        Neighbor.Instance.AddNeighbor("Wyoming", "Idaho");

        Neighbor.Instance.AddNeighbor("South Dakota", "North Dakota");
        Neighbor.Instance.AddNeighbor("South Dakota", "Minnesota");
        Neighbor.Instance.AddNeighbor("South Dakota", "Iowa");
        Neighbor.Instance.AddNeighbor("South Dakota", "Nebraska");
        Neighbor.Instance.AddNeighbor("South Dakota", "Wyoming");
        Neighbor.Instance.AddNeighbor("South Dakota", "Montana");

        Neighbor.Instance.AddNeighbor("Iowa", "Minnesota");
        Neighbor.Instance.AddNeighbor("Iowa", "Wisconsin");
        Neighbor.Instance.AddNeighbor("Iowa", "Illinois");
        Neighbor.Instance.AddNeighbor("Iowa", "Missouri");
        Neighbor.Instance.AddNeighbor("Iowa", "Nebraska");
        Neighbor.Instance.AddNeighbor("Iowa", "South Dakota");

        Neighbor.Instance.AddNeighbor("Illinois", "Wisconsin");
        Neighbor.Instance.AddNeighbor("Illinois", "Iowa");
        Neighbor.Instance.AddNeighbor("Illinois", "Missouri");
        Neighbor.Instance.AddNeighbor("Illinois", "Kentucky");
        Neighbor.Instance.AddNeighbor("Illinois", "Indiana");

        Neighbor.Instance.AddNeighbor("Michigan", "Wisconsin");
        Neighbor.Instance.AddNeighbor("Michigan", "Indiana");
        Neighbor.Instance.AddNeighbor("Michigan", "Ohio");

        Neighbor.Instance.AddNeighbor("New York", "Pennsylvania");
        Neighbor.Instance.AddNeighbor("New York", "New Jersey");
        Neighbor.Instance.AddNeighbor("New York", "Vermont");
        Neighbor.Instance.AddNeighbor("New York", "Massachusetts");
        Neighbor.Instance.AddNeighbor("New York", "Connecticut");

        Neighbor.Instance.AddNeighbor("Vermont", "New York");
        Neighbor.Instance.AddNeighbor("Vermont", "New Hampshire");
        Neighbor.Instance.AddNeighbor("Vermont", "Massachusetts");

        Neighbor.Instance.AddNeighbor("New Hampshire", "Vermont");
        Neighbor.Instance.AddNeighbor("New Hampshire", "Maine");
        Neighbor.Instance.AddNeighbor("New Hampshire", "Massachusetts");

        Neighbor.Instance.AddNeighbor("Maine", "New Hampshire");

        Neighbor.Instance.AddNeighbor("Massachusetts", "New York");
        Neighbor.Instance.AddNeighbor("Massachusetts", "Vermont");
        Neighbor.Instance.AddNeighbor("Massachusetts", "New Hampshire");
        Neighbor.Instance.AddNeighbor("Massachusetts", "Connecticut");
        Neighbor.Instance.AddNeighbor("Massachusetts", "Rhode Island");

        Neighbor.Instance.AddNeighbor("New Jersey", "New York");
        Neighbor.Instance.AddNeighbor("New Jersey", "Pennsylvania");

        Neighbor.Instance.AddNeighbor("Pennsylvania", "New York");
        Neighbor.Instance.AddNeighbor("Pennsylvania", "New Jersey");
        Neighbor.Instance.AddNeighbor("Pennsylvania", "Delaware");
        Neighbor.Instance.AddNeighbor("Pennsylvania", "Maryland");
        Neighbor.Instance.AddNeighbor("Pennsylvania", "West Virginia");
        Neighbor.Instance.AddNeighbor("Pennsylvania", "Ohio");

        Neighbor.Instance.AddNeighbor("Maryland", "Pennsylvania");
        Neighbor.Instance.AddNeighbor("Maryland", "Delaware");
        Neighbor.Instance.AddNeighbor("Maryland", "Virginia");
        Neighbor.Instance.AddNeighbor("Maryland", "West Virginia");

        Neighbor.Instance.AddNeighbor("Ohio", "Pennsylvania");
        Neighbor.Instance.AddNeighbor("Ohio", "West Virginia");
        Neighbor.Instance.AddNeighbor("Ohio", "Kentucky");
        Neighbor.Instance.AddNeighbor("Ohio", "Indiana");
        Neighbor.Instance.AddNeighbor("Ohio", "Michigan");

        Neighbor.Instance.AddNeighbor("Indiana", "Michigan");
        Neighbor.Instance.AddNeighbor("Indiana", "Ohio");
        Neighbor.Instance.AddNeighbor("Indiana", "Kentucky");
        Neighbor.Instance.AddNeighbor("Indiana", "Illinois");

        Neighbor.Instance.AddNeighbor("Virginia", "Maryland");
        Neighbor.Instance.AddNeighbor("Virginia", "West Virginia");
        Neighbor.Instance.AddNeighbor("Virginia", "Kentucky");
        Neighbor.Instance.AddNeighbor("Virginia", "Tennessee");
        Neighbor.Instance.AddNeighbor("Virginia", "North Carolina");

        Neighbor.Instance.AddNeighbor("North Carolina", "Virginia");
        Neighbor.Instance.AddNeighbor("North Carolina", "Tennessee");
        Neighbor.Instance.AddNeighbor("North Carolina", "South Carolina");
        Neighbor.Instance.AddNeighbor("North Carolina", "Georgia");

        Neighbor.Instance.AddNeighbor("South Carolina", "North Carolina");
        Neighbor.Instance.AddNeighbor("South Carolina", "Georgia");

        Neighbor.Instance.AddNeighbor("Georgia", "North Carolina");
        Neighbor.Instance.AddNeighbor("Georgia", "South Carolina");
        Neighbor.Instance.AddNeighbor("Georgia", "Tennessee");
        Neighbor.Instance.AddNeighbor("Georgia", "Alabama");
        Neighbor.Instance.AddNeighbor("Georgia", "Florida");

        Neighbor.Instance.AddNeighbor("Florida", "Georgia");
        Neighbor.Instance.AddNeighbor("Florida", "Alabama");

        Neighbor.Instance.AddNeighbor("Alabama", "Tennessee");
        Neighbor.Instance.AddNeighbor("Alabama", "Georgia");
        Neighbor.Instance.AddNeighbor("Alabama", "Florida");
        Neighbor.Instance.AddNeighbor("Alabama", "Mississippi");

        Neighbor.Instance.AddNeighbor("Mississippi", "Tennessee");
        Neighbor.Instance.AddNeighbor("Mississippi", "Alabama");
        Neighbor.Instance.AddNeighbor("Mississippi", "Louisiana");
        Neighbor.Instance.AddNeighbor("Mississippi", "Arkansas");

        Neighbor.Instance.AddNeighbor("Louisiana", "Arkansas");
        Neighbor.Instance.AddNeighbor("Louisiana", "Mississippi");
        Neighbor.Instance.AddNeighbor("Louisiana", "Texas");

        Neighbor.Instance.AddNeighbor("Texas", "Oklahoma");
        Neighbor.Instance.AddNeighbor("Texas", "Arkansas");
        Neighbor.Instance.AddNeighbor("Texas", "Louisiana");
        Neighbor.Instance.AddNeighbor("Texas", "New Mexico");

        Neighbor.Instance.AddNeighbor("New Mexico", "Texas");
        Neighbor.Instance.AddNeighbor("New Mexico", "Oklahoma");
        Neighbor.Instance.AddNeighbor("New Mexico", "Colorado");
        Neighbor.Instance.AddNeighbor("New Mexico", "Arizona");

        Neighbor.Instance.AddNeighbor("Arizona", "California");
        Neighbor.Instance.AddNeighbor("Arizona", "Nevada");
        Neighbor.Instance.AddNeighbor("Arizona", "Utah");
        Neighbor.Instance.AddNeighbor("Arizona", "Colorado");
        Neighbor.Instance.AddNeighbor("Arizona", "New Mexico");

        Neighbor.Instance.AddNeighbor("California", "Oregon");
        Neighbor.Instance.AddNeighbor("California", "Nevada");
        Neighbor.Instance.AddNeighbor("California", "Arizona");

        Neighbor.Instance.AddNeighbor("Nevada", "Oregon");
        Neighbor.Instance.AddNeighbor("Nevada", "Idaho");
        Neighbor.Instance.AddNeighbor("Nevada", "Utah");
        Neighbor.Instance.AddNeighbor("Nevada", "Arizona");
        Neighbor.Instance.AddNeighbor("Nevada", "California");

        Neighbor.Instance.AddNeighbor("Utah", "Idaho");
        Neighbor.Instance.AddNeighbor("Utah", "Wyoming");
        Neighbor.Instance.AddNeighbor("Utah", "Colorado");
        Neighbor.Instance.AddNeighbor("Utah", "Arizona");
        Neighbor.Instance.AddNeighbor("Utah", "Nevada");

        Neighbor.Instance.AddNeighbor("Colorado", "Wyoming");
        Neighbor.Instance.AddNeighbor("Colorado", "Nebraska");
        Neighbor.Instance.AddNeighbor("Colorado", "Kansas");
        Neighbor.Instance.AddNeighbor("Colorado", "Oklahoma");
        Neighbor.Instance.AddNeighbor("Colorado", "New Mexico");
        Neighbor.Instance.AddNeighbor("Colorado", "Utah");

        Neighbor.Instance.AddNeighbor("Kansas", "Nebraska");
        Neighbor.Instance.AddNeighbor("Kansas", "Missouri");
        Neighbor.Instance.AddNeighbor("Kansas", "Oklahoma");
        Neighbor.Instance.AddNeighbor("Kansas", "Colorado");

        Neighbor.Instance.AddNeighbor("Nebraska", "South Dakota");
        Neighbor.Instance.AddNeighbor("Nebraska", "Iowa");
        Neighbor.Instance.AddNeighbor("Nebraska", "Missouri");
        Neighbor.Instance.AddNeighbor("Nebraska", "Kansas");
        Neighbor.Instance.AddNeighbor("Nebraska", "Colorado");
        Neighbor.Instance.AddNeighbor("Nebraska", "Wyoming");

        Neighbor.Instance.AddNeighbor("Missouri", "Iowa");
        Neighbor.Instance.AddNeighbor("Missouri", "Nebraska");
        Neighbor.Instance.AddNeighbor("Missouri", "Kansas");
        Neighbor.Instance.AddNeighbor("Missouri", "Oklahoma");
        Neighbor.Instance.AddNeighbor("Missouri", "Arkansas");
        Neighbor.Instance.AddNeighbor("Missouri", "Tennessee");
        Neighbor.Instance.AddNeighbor("Missouri", "Kentucky");
        Neighbor.Instance.AddNeighbor("Missouri", "Illinois");

        Neighbor.Instance.AddNeighbor("Kentucky", "Illinois");
        Neighbor.Instance.AddNeighbor("Kentucky", "Indiana");
        Neighbor.Instance.AddNeighbor("Kentucky", "Ohio");
        Neighbor.Instance.AddNeighbor("Kentucky", "West Virginia");
        Neighbor.Instance.AddNeighbor("Kentucky", "Virginia");
        Neighbor.Instance.AddNeighbor("Kentucky", "Tennessee");
        Neighbor.Instance.AddNeighbor("Kentucky", "Missouri");

        Neighbor.Instance.AddNeighbor("West Virginia", "Ohio");
        Neighbor.Instance.AddNeighbor("West Virginia", "Pennsylvania");
        Neighbor.Instance.AddNeighbor("West Virginia", "Maryland");
        Neighbor.Instance.AddNeighbor("West Virginia", "Virginia");
        Neighbor.Instance.AddNeighbor("West Virginia", "Kentucky");

        Neighbor.Instance.AddNeighbor("Tennessee", "Kentucky");
        Neighbor.Instance.AddNeighbor("Tennessee", "Virginia");
        Neighbor.Instance.AddNeighbor("Tennessee", "North Carolina");
        Neighbor.Instance.AddNeighbor("Tennessee", "Georgia");
        Neighbor.Instance.AddNeighbor("Tennessee", "Alabama");
        Neighbor.Instance.AddNeighbor("Tennessee", "Mississippi");
        Neighbor.Instance.AddNeighbor("Tennessee", "Arkansas");
        Neighbor.Instance.AddNeighbor("Tennessee", "Missouri");

        Neighbor.Instance.AddNeighbor("Arkansas", "Missouri");
        Neighbor.Instance.AddNeighbor("Arkansas", "Tennessee");
        Neighbor.Instance.AddNeighbor("Arkansas", "Mississippi");
        Neighbor.Instance.AddNeighbor("Arkansas", "Louisiana");
        Neighbor.Instance.AddNeighbor("Arkansas", "Texas");
        Neighbor.Instance.AddNeighbor("Arkansas", "Oklahoma");

        Neighbor.Instance.AddNeighbor("Oklahoma", "Colorado");
        Neighbor.Instance.AddNeighbor("Oklahoma", "Kansas");
        Neighbor.Instance.AddNeighbor("Oklahoma", "Missouri");
        Neighbor.Instance.AddNeighbor("Oklahoma", "Arkansas");
        Neighbor.Instance.AddNeighbor("Oklahoma", "Texas");
        Neighbor.Instance.AddNeighbor("Oklahoma", "New Mexico");

       
    }


  
}
