using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class State : MonoBehaviour
{
  
    private Coroutine increaseArmySizeCoroutine;
    private Coroutine resourceProductionCoroutine;
    private Coroutine moreleCoroutine;
    private Coroutine incrasePopulationCoroutine;
    private Coroutine decreasePopulationCoroutine;
    public Action<float, State> OnMoreleChanged;

    public int HierarchicalIndex;
    public string StateName = "";
    public float ArmySize ;
    public float UnitArmyPower ;
    public float TotalArmyPower;
    public StateType stateType;
    public float Morele ;
    
    public float TotalMoraleImpact=0; // kullan�lm�yor
    
    public Sprite StateIcon;
    public int Population;
    public int Resources; // fav resources 
    public float loss;
    public int attackCanvasButtonPanelIndex = 1;

  
    public List<TaxData> Taxes = new List<TaxData>();

    public Dictionary<ResourceType, ResourceData> resourceData = new Dictionary<ResourceType, ResourceData>();
    public Trade importTrade;
    public Trade exportTrade;
    
    public  Dictionary<ResourceType, float> plunderedResources = new Dictionary<ResourceType, float>();

    int firstArmySize = 100; // state el de�itirdikten sonraki army size 
    public float MoraleMultiplier = 0.01f; // Moralin asker art���na etkisi
  
    public float PopulationMultiplier = 0.001f; // N�fusun asker art���na etkisi
    private float populationGrowthRateMultiplier=0.00001f; // population growth rate multiplier

    public void SetTotalMoraleImpact(float impact)
    {
        TotalMoraleImpact += impact;
    }
    




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
    private float GetTaxSatisfactionRate()
    {
        float taxSatisfactionRate = 0;
        foreach (var tax in Taxes)
        {
            float result = tax.currentRate - tax.toleranceLimit;

            if (result > 0)
            {
                // Exponential etki: Fark�n karesi veya ba�ka bir �s
                // Burada 2. dereceden bir etki uyguluyoruz, fark daha h�zl� artacak
                taxSatisfactionRate -= Mathf.Pow(result, 1.5f); // Fark�n karesi (result^2)

               
            }
        }
        return taxSatisfactionRate * 0.01f;
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
        if (moreleCoroutine != null)
        {
            StopCoroutine(moreleCoroutine);
            moreleCoroutine = null;
        }
        if(incrasePopulationCoroutine != null)
        {
            StopCoroutine(incrasePopulationCoroutine);
            StopCoroutine(decreasePopulationCoroutine);
            incrasePopulationCoroutine = null;
            decreasePopulationCoroutine = null;
        }
           
    }

    private void HandleAttackStopped()
    {
        if (increaseArmySizeCoroutine == null)
            increaseArmySizeCoroutine = StartCoroutine(IncreaseArmySizeOverTime());

        if (resourceProductionCoroutine == null)
            resourceProductionCoroutine = StartCoroutine(ResourceProduction());
        if (moreleCoroutine == null)
            moreleCoroutine = StartCoroutine(ChangeMorale());
        if (incrasePopulationCoroutine == null)
        {
            incrasePopulationCoroutine = StartCoroutine(IncrasePopulationOverTime());

            decreasePopulationCoroutine = StartCoroutine(ReducePopulationOverTime());
        }
       
    }
    private IEnumerator ChangeMorale()
    {
        while (!GameManager.Instance.�sGameOver && !GameManager.Instance.isGamePause && GameManager.Instance.IsAttackFinish)
        {
            float addedTaxValue= GetTaxSatisfactionRate();
            
            {
                Morele += addedTaxValue;
                Morele = Mathf.Clamp(Morele, 0, 100);
                State state = gameObject.GetComponent<State>();
               
               
                {
                   
                    {
                        OnMoreleChanged?.Invoke(Morele, state);
                      //  Debug.LogWarning($" morale: " + Morele);
                    }
                }


            }


            yield return new WaitForSeconds(GameManager.gameDayTime);
        }
    }
    private IEnumerator IncreaseArmySizeOverTime()
    {
        while (!GameManager.Instance.�sGameOver && !GameManager.Instance.isGamePause && GameManager.Instance.IsAttackFinish)
        {
            float armyIncreasePerSecond = Morele * MoraleMultiplier * Population * PopulationMultiplier;
            ArmySize += armyIncreasePerSecond;
            TotalArmyPower = ArmySize * UnitArmyPower;
            yield return new WaitForSeconds(GameManager.gameDayTime);
        }
    }
    private IEnumerator IncrasePopulationOverTime()
    {

        while (!GameManager.Instance.�sGameOver && !GameManager.Instance.isGamePause && GameManager.Instance.IsAttackFinish)
        {
            int populationIncreasePerSecond =(int)( Morele * Population * populationGrowthRateMultiplier);
            Population += populationIncreasePerSecond;
            
            yield return new WaitForSeconds(GameManager.gameDayTime);
        }
    }
    private IEnumerator ReducePopulationOverTime()
    {

        while (!GameManager.Instance.�sGameOver && !GameManager.Instance.isGamePause && GameManager.Instance.IsAttackFinish)
        {
            int populationIncreasePerSecond = (int)((100-Morele) * Population * populationGrowthRateMultiplier);
            Population -= populationIncreasePerSecond;

            yield return new WaitForSeconds(GameManager.gameDayTime);
        }
    }

    private IEnumerator ResourceProduction()
    {
        while (!GameManager.Instance.�sGameOver && !GameManager.Instance.isGamePause && GameManager.Instance.IsAttackFinish )
        {
            foreach (var item in resourceData)
            {
                float productionAmount = (item.Value.mineCount * item.Value.productionRate) / 100 * Morele;
                if (item.Key== ResourceType.Gold)
                {
                    foreach (var item1 in Taxes)
                    {
                        if(item1.taxType==TaxType.IncomeTax)
                        {
                            float tax = (productionAmount / 100)* item1.currentRate;
                           // Debug.LogWarning(tax);
                            item1.taxIncome= tax;
                            ResourceManager.Instance.ChargeTax(ResourceType.Gold, tax);
                            productionAmount -= tax;

                        }
                        else if(item1.taxType== TaxType.StampTax)
                        {
                            float tax =  item1.taxIncome;
                            
                           ResourceManager.Instance.ChargeTax(ResourceType.Gold, tax);
                          
                        }
                        
                    }
                    
                }
               
                item.Value.currentAmount += productionAmount;
                item.Value.currentAmount -= (item.Value.consumptionAmount);

            }

            
            yield return new WaitForSeconds(GameManager.gameDayTime);
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
        ArmySize = Mathf.Clamp(ArmySize, 0, 9999999999999);
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
            Debug.LogWarning("state i�gal edildi");
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
                Debug.LogWarning("ally state bulunamad� ");
                gameObject.AddComponent<AllyState>();
            }
        }
        else
            Debug.LogWarning("elege�irmeye �al��t���n satte enmey de�il ");

    }
    void LostState()
    {
        Debug.LogWarning("state kay�bedildi");
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
            Debug.LogWarning("enmey state bulunamad� ");
            gameObject.AddComponent<EnemyState>();
        }
    }
  void RelaseState()
    {
        Debug.LogWarning("state �zg�rleitirildi edildi");
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
            Debug.LogWarning("natural state bulunamad� ");
            gameObject.AddComponent<NaturalState>();
        }
    }
    private void FindISelectibleComponentAndDisable()
    {
        // ISelectable aray�z�n� implemente eden t�m bile�enleri bul
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
        plunderedResources = new Dictionary<ResourceType, float>();
    plunderedResources.Clear();


       
        ResourceType resourceType = (ResourceType)Resources;

        //Debug.LogWarning($"g�ncel alt�n ilk  durmu plunderliste uzunlu�u {plunderedResources.Count} {resourceData[ResourceType.Gold].currentAmount} " + name);

        plunderedResources.Add(ResourceType.Gold, resourceData[ResourceType.Gold].currentAmount);
            resourceData[ResourceType.Gold].currentAmount = 0;
            plunderedResources.Add(resourceType, resourceData[resourceType].currentAmount);
            resourceData[resourceType].currentAmount = 0;

        //   Debug.LogWarning($"g�ncel alt�n durmu {resourceData[ResourceType.Gold].currentAmount} "+ name);
       

        // E�er ba�ka kaynaklar da ya�malanacaksa buraya ekleyebilirsiniz

        return plunderedResources;
    }
    public Dictionary<ResourceType, float> GetPlundData()
    {
        return plunderedResources;
    }


    
    public void AddResource(Dictionary<ResourceType, float> plunderedResources)
{
        
    foreach (var resource in plunderedResources)
    {
        if (resourceData.ContainsKey(resource.Key))
        {
                if(resource.Key== ResourceType.Gold)
                {
                    foreach (var item1 in Taxes)
                    {
                        if (item1.taxType == TaxType.DirectTax)// victory 
                        {
                            float tax = (resource.Value / 100) * item1.currentRate;
                            Debug.LogWarning("plunder tax: "+tax);
                            item1.taxIncome = tax;
                            ResourceManager.Instance.ChargeTax(ResourceType.Gold, tax);
                            resourceData[ResourceType.Gold].currentAmount -= tax;
                                
                        }
                    }
                    
                }
                
                resourceData[resource.Key].currentAmount += resource.Value;
        }
        else
        {
                Debug.LogWarning("u try new resoruce type  ");
            }
    }
}
    public void SellResource(ResourceType resType, float quantity, float earing)
    {
        
            if (resourceData.ContainsKey(resType))
            {
                // E�er kaynak zaten mevcutsa, miktar� g�ncelleyebilirsiniz
                resourceData[resType].currentAmount -= quantity;
            foreach (var item in Taxes)
            {
                if(item.taxType== TaxType.ValueAddedTax)
                {
                    float tax = (earing / 100) * item.currentRate;
                    ResourceManager.Instance.ChargeTax(ResourceType.Gold, tax);
                    earing -= tax;
                }
            }
            resourceData[ResourceType.Gold].currentAmount += earing;
            }
            else
            {
                Debug.LogWarning("u try new resoruce type  ");
            }
        
    }
    public void BuyyResource(ResourceType resType, float quantity, float spending)
    {

        if (resourceData.ContainsKey(resType))
        {
            
            resourceData[resType].currentAmount += quantity;
            resourceData[ResourceType.Gold].currentAmount -= spending;
        }
        else
        {
            Debug.LogWarning("u try new resoruce type  ");
        }

    }
    public void InstantlyResource(ResourceType resType, float quantity, float spending)
    {

        if (resourceData.ContainsKey(resType))
        {

            resourceData[resType].currentAmount += quantity;
            resourceData[ResourceType.Diamond].currentAmount -= spending;
        }
        else
        {
            Debug.LogWarning("u try new resoruce type  ");
        }

    }
    public Trade GetTrade( int  index, ResourceType currentResType)
    {
       // Debug.LogWarning($"state name {gameObject.name} exporttrade value {exportTrade.resourceTypes[0]} {exportTrade.resourceTypes[1]} ");
        if (index == 0)
        {
            foreach (var resType in importTrade.resourceTypes)
            {
                if (resType == currentResType)
                {
                    return importTrade;
                }
                //else
               // { Debug.LogWarning($"state res type {resType}  curent res type {currentResType}"); }
            }
        }
        else
        {
            if (index == 1)
            {
                foreach (var resType in exportTrade.resourceTypes)
                {
                    if (resType == currentResType)
                    {
                       // Debug.LogWarning($"e�le�me  tamam  {resType}  curent res type {currentResType}");

                        return exportTrade;
                    }
                    //else
                   // { Debug.LogWarning($"state res type {resType}  curent res type {currentResType}"); }
                }
            }

        }
       // Debug.Log("wxport ya da import type e�le�mesi olmad� ******************"+ index);
        return null;
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
public enum TaxType
{
    IncomeTax,
    ValueAddedTax,
    DirectTax, //  victory
    StampTax
}

[Serializable]
public class TaxData
{
    public TaxType taxType;
    public float currentRate; // Mevcut vergi oran�
    public float toleranceLimit; // Tolerans e�i�i
    public float taxIncome; // Bu verginin getirdi�i gelir
    public float unitTaxIncome;
}