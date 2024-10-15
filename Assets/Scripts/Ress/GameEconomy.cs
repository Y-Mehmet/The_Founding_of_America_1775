using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEconomy : MonoBehaviour
{
    public static GameEconomy Instance { get; private set; }
    private Dictionary<ResourceType, float> resourceToGoldValue;
    
     public float PayBackValue;
    public  float payBackTime = 100;
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
            // Kaynaklar�n Gold cinsinden de�erleri (10 kat azalt�lm��)
            resourceToGoldValue = new Dictionary<ResourceType, float>
        {
            { ResourceType.Gold, 1f },
            { ResourceType.Water, 0.05f },    
            { ResourceType.Salt, 0.08f },     
            { ResourceType.Meat, 0.06f },     
            { ResourceType.Fruits, 0.04f },   
            { ResourceType.Vegetables, 0.04f }, 
            { ResourceType.Wheat, 0.05f },    
            { ResourceType.Wood, 0.03f },
            { ResourceType.Coal, 0.07f },    
            { ResourceType.Iron, 0.09f },     
            { ResourceType.Stone, 0.05f },    
            { ResourceType.Diamond, ResourceManager.DimondRate }   //100  
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
    public int GetGemValue(float goldAmount)
    {
        float gemValue = goldAmount / ResourceManager.DimondRate;
        return (int)Mathf.Ceil( gemValue);
    }

    

}
