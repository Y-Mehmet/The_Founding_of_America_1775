using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
[Serializable]
public class USCongress : MonoBehaviour
{
    public static USCongress Instance;
    public static ActType currentAct= ActType.None;

    public static Action<ActType> OnEnactActChange;
    public static Action<ActType> OnRepealActChange;
    public static bool  PopulationStabilityAct= false;
    public static float MoralAddedValue =0;
    public static int ConsumptionAddedValue = 100;
    public static float UnitArmyPowerAddedValue = 0;
    public static int ProductionAddedValue = 100;
    public static float PopulationAddedValue = 100;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
 
    
}
