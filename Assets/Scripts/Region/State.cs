using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class State : MonoBehaviour
{
  
    private Coroutine increaseArmySizeCoroutine;
    private Coroutine resourceProductionCoroutine;

    public string StateName = "";
    public float ArmySize = 100;
    public float UnitArmyPower = 0.75f;
    public float TotalArmyPower;
    public StateType stateType;
    public float Morale = 1.0f;
    
    public Sprite StateIcon;
    public float Morele;
    public int Population;
    public int Resources; // fav resources 
    public float loss;
    public int attackCanvasButtonPanelIndex = 1;

    public float IncomeTax = 10; //üreteim vergisi
    public float SalesTax = 10;  //  ticaret vergisi
    public float DirectTax = 10; //  doðrudan vergi

    public Dictionary<ResourceType, ResourceData> resourceData = new Dictionary<ResourceType, ResourceData>();
    public  Dictionary<ResourceType, float> plunderedResources = new Dictionary<ResourceType, float>();

    int firstArmySize = 100; // state el deðitirdikten sonraki army size 
    public float MoraleMultiplier = 0.01f; // Moralin asker artýþýna etkisi
  
    public float PopulationMultiplier = 0.001f; // Nüfusun asker artýþýna etkisi
    private void Start()
    {
        TotalArmyPower = ArmySize * UnitArmyPower;
        
        FindISelectibleComponentAndDisable();
        switch (stateType)
        {
            case (StateType.Ally):
                gameObject.GetComponent<AllyState>().enabled= true;
                break;
            case (StateType.Enemy):
                gameObject.GetComponent<EnemyState>().enabled = true;
                break;
            case (StateType.Neutral):
                gameObject.GetComponent<NaturalState>().enabled = true;
                break;
        }
        GameManager.Instance.OnAttackStarted += HandleAttackStarted;
        GameManager.Instance.OnAttackStopped += HandleAttackStopped;
        HandleAttackStopped();
    }
    private void HandleAttackStarted()
    {
        if (increaseArmySizeCoroutine != null)
        {
            StopCoroutine(increaseArmySizeCoroutine);
            increaseArmySizeCoroutine = null;
        }

        if (resourceProductionCoroutine != null)
        {
            StopCoroutine(resourceProductionCoroutine);
            resourceProductionCoroutine = null;
        }
    }

    private void HandleAttackStopped()
    {
        if (increaseArmySizeCoroutine == null)
            increaseArmySizeCoroutine = StartCoroutine(IncreaseArmySizeOverTime());

        if (resourceProductionCoroutine == null)
            resourceProductionCoroutine = StartCoroutine(ResourceProduction());
    }

    private IEnumerator IncreaseArmySizeOverTime()
    {
        while (!GameManager.Instance.ÝsGameOver && !GameManager.Instance.isGamePause)
        {
            float armyIncreasePerSecond = Morale * MoraleMultiplier * Population * PopulationMultiplier;
            ArmySize += armyIncreasePerSecond;
            TotalArmyPower = ArmySize * UnitArmyPower;
            yield return new WaitForSeconds(GameManager.Instance.gameDayTime);
        }
    }

    private IEnumerator ResourceProduction()
    {
        while (!GameManager.Instance.ÝsGameOver && !GameManager.Instance.isGamePause)
        {
            foreach (var item in resourceData)
            {
                float productionAmount = (item.Value.mineCount * item.Value.productionRate);
                if (item.Key== ResourceType.Gold)
                {
                   
                    float tax = productionAmount /IncomeTax;
                    ResourceManager.Instance.ChargeTax(ResourceType.Gold, tax);
                    productionAmount -= tax;
                }
               
                item.Value.currentAmount += productionAmount;
                item.Value.currentAmount -= (item.Value.consumptionAmount);

            }
            
            yield return new WaitForSeconds(GameManager.Instance.gameDayTime);
        }
    }

    public void IncraseMetod()
    {
        if (increaseArmySizeCoroutine == null)
            increaseArmySizeCoroutine = StartCoroutine(IncreaseArmySizeOverTime());

        if (resourceProductionCoroutine == null)
            resourceProductionCoroutine = StartCoroutine(ResourceProduction());
    }
    public float TotalArmyCalculator()
    {
        TotalArmyPower = ArmySize * UnitArmyPower;
        return TotalArmyPower;
    }
    public void  ReduceArmySize(float loss)
    {
        ArmySize-= (int)loss;
        if(ArmySize <= 20)
        {
            
            switch (stateType)
            {
                case (StateType.Ally):

                     LostState();
                    attackCanvasButtonPanelIndex = 3;
                    break;
                case (StateType.Enemy):
                    
                    attackCanvasButtonPanelIndex = 2;
                    break;
                case (StateType.Neutral):
                    RelaseState();
                    attackCanvasButtonPanelIndex = 2;
                    break;
            }

            
            

        }
        else
        {
            attackCanvasButtonPanelIndex = 1;

        }

        TotalArmyPower = ArmySize * UnitArmyPower;

    }
    public void LostWar(float lossRate)
    {
        loss = lossRate * ArmySize ;
       // Debug.LogWarning($"loss: {loss} armysize {ArmySize}  name {gameObject.name} loss rate {lossRate}");
        ReduceArmySize(loss);
        
    }
    public  void OccupyState()
    {
        if (stateType == StateType.Enemy)
        {
            Debug.LogWarning("state iþgal edildi");
            ArmySize = firstArmySize;
            stateType = StateType.Ally;
            ChangeCollor.Instance.ChangeGameobjectColor(gameObject, stateType);
            FindISelectibleComponentAndDisable();

            AllyState allyState = gameObject.GetComponent<AllyState>();
            if (allyState != null)
            {
                Debug.LogWarning($"ally stete {allyState.name} bulundu ve eneblesi actif edildi");
                allyState.enabled = true;
            }
            else
            {
                Debug.LogWarning("ally state bulunamadý ");
                gameObject.AddComponent<AllyState>();
            }
        }
        else
            Debug.LogWarning("elegeçirmeye çalýþtýðýn satte enmey deðil ");

    }
    void LostState()
    {
        Debug.LogWarning("state kayýbedildi");
        ArmySize = firstArmySize;
        stateType = StateType.Enemy;
        ChangeCollor.Instance.ChangeGameobjectColor(gameObject, stateType);

        FindISelectibleComponentAndDisable();

       EnemyState enemyState= gameObject.GetComponent<EnemyState>();
        if(enemyState != null)
        {
            enemyState.enabled = true;
        }
        else
        {
            Debug.LogWarning("enmey state bulunamadý ");
            gameObject.AddComponent<EnemyState>();
        }
    }
  void RelaseState()
    {
        Debug.LogWarning("state özgürleitirildi edildi");
        ArmySize = firstArmySize;
        stateType = StateType.Neutral;
        ChangeCollor.Instance.ChangeGameobjectColor(gameObject, stateType);


        FindISelectibleComponentAndDisable();
        gameObject.AddComponent<NaturalState>();
        NaturalState naturalState = gameObject.GetComponent<NaturalState>();
        if (naturalState != null)
        {
            naturalState.enabled = true;
        }
        else
        {
            Debug.LogWarning("natural state bulunamadý ");
            gameObject.AddComponent<NaturalState>();
        }
    }
    private void FindISelectibleComponentAndDisable()
    {
        // ISelectable arayüzünü implemente eden tüm bileþenleri bul
        MonoBehaviour[] components = gameObject.GetComponents<MonoBehaviour>();

        foreach (var component in components)
        {
            if (component is ISelectable)
            {
                
                component.enabled = false;

            }
        }
    }
    public Dictionary<ResourceType, float>  PlunderResource()
    {
        plunderedResources.Clear();


       
        ResourceType resourceType = (ResourceType)Resources;
       
        
            plunderedResources.Add(ResourceType.Gold, resourceData[ResourceType.Gold].currentAmount);
            resourceData[ResourceType.Gold].currentAmount = 0;
            plunderedResources.Add(resourceType, resourceData[resourceType].currentAmount);
            resourceData[resourceType].currentAmount = 0;

           // Debug.LogWarning("altýn ve facv kaynak eklaendi "+ resourceType);
       

        // Eðer baþka kaynaklar da yaðmalanacaksa buraya ekleyebilirsiniz

        return plunderedResources;
    }
    
    public void AddResource(Dictionary<ResourceType, float> plunderedResources)
{
    foreach (var resource in plunderedResources)
    {
        if (resourceData.ContainsKey(resource.Key))
        {
            // Eðer kaynak zaten mevcutsa, miktarý güncelleyebilirsiniz
            resourceData[resource.Key].currentAmount += resource.Value;
        }
        else
        {
                Debug.LogWarning("yeni bir tip kaynak eklemeye çalýþýyorsun ");
        }
    }
}






}
public enum StateType
{
    Ally,
    Enemy,
    Neutral
}
[Serializable]
public class StateData
{
    public string StateName;
    public float ArmySize;
    public float UnitArmyPower;
    public float TotalArmyPower;
    public StateType stateType;
    public float Morele;
    public int Population;
    public int Resources;
}
