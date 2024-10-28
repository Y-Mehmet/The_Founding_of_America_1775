using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class USCongress : MonoBehaviour
{
    public static USCongress Instance;
    public static ActType currentAct;

    public static Action<ActType> OnEnactActChange;
    public static Action<ActType> OnRepealActChange;
    public static bool  PopulationStabilityAct= false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
 
    
}
