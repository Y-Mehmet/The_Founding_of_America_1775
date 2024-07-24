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




}
