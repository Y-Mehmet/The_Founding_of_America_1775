using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    // Kaynaklar� saklayaca��m�z bir Dictionary
    private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Ba�lang�� kaynaklar�n� tan�mlama
        resources[ResourceType.Gold] = 1000;
       
        // Di�er kaynaklar eklenecekse buraya eklenebilir
    }

    // Kayna�� azaltma metodu
    public void ReduceResource(ResourceType resourceType, float reduceAmount)
    {
        if (resources.ContainsKey(resourceType))
        {
            resources[resourceType] -= (int)reduceAmount;
            // Kaynak miktar� 0'dan az olmamal�d�r
            if (resources[resourceType] < 0)
                resources[resourceType] = 0;
        }
        else
        {
            Debug.LogWarning($"Resource {resourceType} not found.");
        }
    }

    // Kayna��n miktar�n� almak i�in yard�mc� metod
    public int GetResourceAmount(ResourceType resourceType)
    {
        if (resources.ContainsKey(resourceType))
        {
            return resources[resourceType];
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
            ReduceResource(resourceType, (int)cost);


            return true;
        }
        else
        {
            Debug.Log($"harcma i�in {resourceType} hazinede yeteri kadar yok harcama limitin {resource} harcaman {cost}");
            return false; }
    }
}

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
[System.Serializable]
public class ResourceData
{
    public int currentAmount;    
    public int mineCount;        
    public float productionRate;
    public int consumptionAmount;
}
