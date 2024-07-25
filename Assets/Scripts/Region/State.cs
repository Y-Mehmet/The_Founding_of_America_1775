using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public string StateName ="";
    public float ArmySize = 100, UnitArmyPower=.75f;
    public float TotalArmyPower;
    private void Start()
    {
        StateName= gameObject.name;
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
        if(ArmySize < 0)
            ArmySize = 0;
    }
    public void LostWar(float lossRate)
    {
        float loss = lossRate * ArmySize ;
        ReduceArmySize(loss);
    }




}
