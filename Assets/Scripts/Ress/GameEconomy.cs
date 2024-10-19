using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEconomy : MonoBehaviour
{
    public static GameEconomy Instance { get; private set; }
    private Dictionary<ResourceType, float> resourceToGoldValue;
    
     public float PayBackValue;
    public  float payBackTime = 10;
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        PayBackValue = GameManager.gameDayTime * payBackTime;
    }

    public GameEconomy()
        {
            // Kaynaklarýn Gold cinsinden deðerleri (10 kat azaltýlmýþ)
            resourceToGoldValue = new Dictionary<ResourceType, float>
        {
            { ResourceType.Gold, 1f },
            { ResourceType.Water, 0.015f },    
            { ResourceType.Salt, 0.12f },     
            { ResourceType.Meat, 0.15f },     
            { ResourceType.Fruits, 0.20f },   
            { ResourceType.Vegetables, 0.12f }, 
            { ResourceType.Wheat, 0.2f },    
            { ResourceType.Wood, 0.2f },
            { ResourceType.Coal, 0.09f },    
            { ResourceType.Iron, 0.08f },     
            { ResourceType.Stone, 0.09f },    
            { ResourceType.Diamond, ResourceManager.DimondRate }   
        };
        }

        public float GetGoldValue(ResourceType resourceType, float amount=1)
        {
            if (resourceToGoldValue.TryGetValue(resourceType, out float valuePerUnit))
            {
                return valuePerUnit * amount;
            }
            else
            {
                Debug.LogWarning("Resource type not found.");
                return 0f;
            }
        }
    public int GetGemValue(float goldAmount, int time=0)
    {
        State currnetState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        int goldMineCount = currnetState.resourceData[ResourceType.Gold].mineCount;
        float prodictionRate = currnetState.resourceData[ResourceType.Gold].productionRate;
        float timeValue = goldMineCount * prodictionRate;
        goldAmount += time * timeValue;
        float gemValue = goldAmount / ResourceManager.DimondRate;
        return (int)Mathf.Ceil( gemValue);
    }

    

}
