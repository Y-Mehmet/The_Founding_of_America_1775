using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class State : MonoBehaviour
{
    public string StateName = "";
    public float ArmySize = 100;
    public float UnitArmyPower = 0.75f;
    public float TotalArmyPower;
    public StateType stateType;
    public float Morale = 1.0f;
    
    public Sprite StateIcon;
    public float Morele;
    public int Population;
    public int Resources;
    
    int firstArmySize = 100; // state el deðitirdikten sonraki army size 
    public float MoraleMultiplier = 0.01f; // Moralin asker artýþýna etkisi
  
    public float PopulationMultiplier = 0.001f; // Nüfusun asker artýþýna etkisi
    private void Start()
    {
        //StateName= gameObject.name;
        TotalArmyPower = ArmySize * UnitArmyPower;
        StartCoroutine(IncreaseArmySizeOverTime());
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
    }

    private IEnumerator IncreaseArmySizeOverTime()
    {
        while (!GameManager.Instance.ÝsGameOver)
        {
            float armyIncreasePerSecond = Morale * MoraleMultiplier * Population * PopulationMultiplier;
            ArmySize += armyIncreasePerSecond;

            TotalArmyPower = ArmySize * UnitArmyPower;
          

            // Her saniye artýþ yap
            yield return new WaitForSeconds(1f);
        }
    }
    public float TotalArmyCalculator()
    {
        TotalArmyPower = ArmySize * UnitArmyPower;
        return TotalArmyPower;
    }
    public void  ReduceArmySize(float loss)
    {
        ArmySize-= (int)loss;
        if(ArmySize <= 0)
        {
            switch (stateType)
            {
                case (StateType.Ally):
                     LostState();
                    break;
                case (StateType.Enemy):
                    OccupyState();
                    break;
                case (StateType.Neutral):
                    RelaseState(); 
                    break;
            }

            
            

        }
            
        TotalArmyPower = ArmySize * UnitArmyPower;

    }
    public void LostWar(float lossRate)
    {
        float loss = lossRate * ArmySize ;
        Debug.LogWarning($"loss: {loss} armysize {ArmySize}  name {gameObject.name} loss rate {lossRate}");
        ReduceArmySize(loss);
        
    }
    void OccupyState()
    {
        Debug.LogWarning("state iþgal edildi");
        ArmySize = firstArmySize;
        stateType = StateType.Ally;
        ChangeCollor.Instance.ChangeGameobjectColor(gameObject, stateType);
        FindISelectibleComponentAndDisable();
        
        AllyState allyState = gameObject.GetComponent<AllyState>();
        if (allyState != null)
        {
            allyState.enabled = true;
        }
        else
        {
            Debug.LogWarning("ally state bulunamadý ");
            gameObject.AddComponent<AllyState>();
        }

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
