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
    private void Start()
    {
        //StateName= gameObject.name;
        TotalArmyPower = ArmySize * UnitArmyPower;
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
        ReduceArmySize(loss);
        
    }
    void OccupyState()
    {
        Debug.LogWarning("state iþgal edildi");
        ArmySize = 0;
        stateType = StateType.Ally;
        Color oldGloryBlue;
        UnityEngine.ColorUtility.TryParseHtmlString("#002147", out oldGloryBlue);
        gameObject.GetComponent<Renderer>().material.color = oldGloryBlue;
    }
    void LostState()
    {
        Debug.LogWarning("state kayýbedildi");
        ArmySize = 0;
        stateType = StateType.Enemy;
        Color oldGloryRed;
        UnityEngine.ColorUtility.TryParseHtmlString("#B22234", out oldGloryRed);
        gameObject.GetComponent<Renderer>().material.color = oldGloryRed;
    }
    void RelaseState()
    {
        Debug.LogWarning("state özgürleitirildi edildi");
        ArmySize = 0;
        stateType = StateType.Neutral;
      
        
        gameObject.GetComponent<Renderer>().material.color = Color.white;
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