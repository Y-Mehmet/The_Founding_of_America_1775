using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class State : MonoBehaviour
{
    public string StateName ="";
    public float ArmySize = 100, UnitArmyPower=.75f;
    public float TotalArmyPower;
    public StateType stateType;
    public Sprite StateIcon;
    public float Morele;
    public int Population;
    public int Resources;
    // state el de�itirdikten sonraki army size 
    int firstArmySize = 100;
    private void Start()
    {
        //StateName= gameObject.name;
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
        Debug.LogWarning("state i�gal edildi");
        ArmySize = firstArmySize;
        stateType = StateType.Ally;
        Color oldGloryBlue;
        UnityEngine.ColorUtility.TryParseHtmlString("#002147", out oldGloryBlue);
        gameObject.GetComponent<Renderer>().material.color = oldGloryBlue;
        FindISelectibleComponentAndDisable();
        
        AllyState allyState = gameObject.GetComponent<AllyState>();
        if (allyState != null)
        {
            allyState.enabled = true;
        }
        else
        {
            Debug.LogWarning("ally state bulunamad� ");
            gameObject.AddComponent<AllyState>();
        }

    }
    void LostState()
    {
        Debug.LogWarning("state kay�bedildi");
        ArmySize = firstArmySize;
        stateType = StateType.Enemy;
        Color oldGloryRed;
        UnityEngine.ColorUtility.TryParseHtmlString("#B22234", out oldGloryRed);
        gameObject.GetComponent<Renderer>().material.color = oldGloryRed;
        
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




}
public enum StateType
{
    Ally,
    Enemy,
    Neutral
}
public class StateData
{
    public string StateName;
    public float ArmySize;
    public float UnitArmyPower;
    public float TotalArmyPower;
    public StateType stateType;
    public Sprite StateImage;
}