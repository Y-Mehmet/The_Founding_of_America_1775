using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static MY.NumberUtilitys.Utility;
public class ResourceManager : MonoBehaviour
{
    // USA CENTER BANK
   
    public static ResourceManager Instance { get; private set; }

    //merkez bankasýnýn  Kaynaklarý saklayacaðýmýz bir Dictionary
    private Dictionary<ResourceType, float> resources = new Dictionary<ResourceType, float>();
    public static  ResourceType curentResource { get;  set; }
    public string curentTradeStateName {  get; set; }
    public  State CurrentTradeState;

    public Action<ResourceType> OnResourceChanged;
    public Action<string> OnStateToTradeChanged;

    public static  float DimondRate = 100;
    public static int InputFieldCaharcterLimit = 6;
    public static float PayBackValue = 100;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Baþlangýç kaynaklarýný tanýmlama
        resources[ResourceType.Gold] = 1000;
        resources[ResourceType.Diamond] = 10;
       
        // Diðer kaynaklar eklenecekse buraya eklenebilir
    }

    private void Start()
    {
        SetDefaultValue();
    }
    public  void SetResoruceAmount(ResourceType resourceType, float amount)
    {
        resources[resourceType] = amount;
    }
    public void SetDefaultValue()
    {
        SetCurrentResource(ResourceType.Diamond);
        SetCurrentTradeState("Texas");
    }
    public void SetCurrentResource(ResourceType resourceType)
    {
        curentResource = resourceType;
        OnResourceChanged?.Invoke(resourceType);
    }
    
    public void SetCurrentTradeState(string selectedTradeStateName)
    {
        curentTradeStateName= selectedTradeStateName;
        CurrentTradeState= Usa.Instance.FindStateByName(curentTradeStateName);
        OnStateToTradeChanged?.Invoke(selectedTradeStateName);
       // Debug.LogError(" yeni state seçildi res manager " + selectedTradeStateName);
      

    }
    public void SetCurrentTradeState2(State tradeState)
    {
        CurrentTradeState = tradeState;
       
        OnStateToTradeChanged?.Invoke(tradeState.name);
        // Debug.LogError(" yeni state seçildi res manager " + selectedTradeStateName);
    }



    // Kaynaðý azaltma metodu
    public void ReduceResource(ResourceType resourceType, float reduceAmount)
    {
        if (resources.ContainsKey(resourceType))
        {
            resources[resourceType] -= (int)reduceAmount;
            // Kaynak miktarý 0'dan az olmamalýdýr
            if (resources[resourceType] < 0)
                resources[resourceType] = 0;
        }
        else
        {
            Debug.LogWarning($"Resource {resourceType} not found.");
        }
    }
    public void AddResource(ResourceType resourceType, float reduceAmount)
    {
        if (resources.ContainsKey(resourceType))
        {
            resources[resourceType] += reduceAmount;
            // Kaynak miktarý 0'dan az olmamalýdýr
            if (resources[resourceType] < 0)
                resources[resourceType] = 0;
        }
        else
        {
            Debug.LogWarning($"Resource {resourceType} not found.");
        }
    }
    public void ChargeTax(ResourceType resourceType, float reduceAmount)
    {
       
        if (resources.ContainsKey(resourceType))
        {
            resources[resourceType] +=  reduceAmount;
            MissionsManager.AddTotalTaxGold(-((int)reduceAmount));
            // Kaynak miktarý 0'dan az olmamalýdýr
            if (resources[resourceType] < 0)
                resources[resourceType] = 0;
        }
        else
        {
            Debug.LogWarning($"Resource {resourceType} not found.");
        }
    }

    // Kaynaðýn miktarýný almak için yardýmcý metod
    public int GetResourceAmount(ResourceType resourceType)
    {
        if (resources.ContainsKey(resourceType))
        {
            return (int) resources[resourceType];
        }
        else
        {
            Debug.LogWarning($"Resource {resourceType} not found.");
            return 0;
        }
    }
    public bool AreThereEnoughResource(ResourceType resourceType,float cost)
    {
        int resource = GetResourceAmount(resourceType);
        if( resource>= cost)
        {
            ReduceResource(resourceType, cost);


            return true;
        }
        else
        {
            Debug.Log($"harcma için {resourceType} hazinede yeteri kadar yok harcama limitin {resource} harcaman {cost}");
            return false; }
    }
}
[Serializable]
public enum ResourceType
{
    Gold,
    Water,
    Salt,
    Meat,
    Fruits,
    Vegetables,
    Wheat,
    Wood,
    Coal,
    Iron,
    Stone,
    Diamond

}
public enum MainResourceType
{
    Goldshire,
    Waterford,
    Saltcrag,
    Meathollow,
    Fruitgrove,
    Greensvale,
    Wheatland,
    Woodcrest,
    Coalridge,
    Ironhaven,
    Stonefall,
    Diamondpeak
}
[System.Serializable]
public class ResourceData
{
    public ResourceType resourceType;
    public float currentAmount;    // mevcut miktar
    public int mineCount;        // maden sayýsý
    public float productionRate; // üretim hýzý
    public float consumptionAmount;// tüketim miktarý
    public float surplus=0;
    public float maxMineCount = 75;
}
