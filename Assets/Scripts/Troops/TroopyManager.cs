using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopyManager : MonoBehaviour
{
    public static TroopyManager Instance { get; private set; }
    public static State OriginState;
    public static State DestinationState;
    public static float supplyCost = 0.1f;
    public static float nonNeigbordMultiplier = 2;
    public static float TravelDuration = GameManager.gameDayTime;
    public static float UnitTropyDurationMultiplier = 0.001f;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public static int GetSupplyCost(int troopyCount)
    {
         bool isNeigbor = Neighbor.Instance.AreNeighbors(OriginState.name, DestinationState.name);
        float  cost = supplyCost; 
        if(!isNeigbor)
        {
            cost = cost* nonNeigbordMultiplier;
        }
        cost= cost* troopyCount;
        return (int)Mathf.Ceil(cost);

    }
    public static int GetSecondValue(int troopyCount)
    {
        bool isNeigbor = Neighbor.Instance.AreNeighbors(OriginState.name, DestinationState.name);
        float cost = TravelDuration;
        if (!isNeigbor)
        {
            cost = cost * nonNeigbordMultiplier;
        }
        cost = cost * troopyCount* UnitTropyDurationMultiplier;
        return (int)Mathf.Ceil( cost);

    }
    public static int GetDimondValue(int supplyCost, int secondValue)
    {
        int gem = 0;
       
       gem= GameEconomy.Instance.GetGemValue(supplyCost, secondValue);

        return gem;
    }
    public  void SendTropy( int travelDuration, int navalForceCount, int landForceCount)
    {
        StartCoroutine(StartTravel(travelDuration, navalForceCount, landForceCount));
    }
    IEnumerator StartTravel(int travelDuration, int navalForceCount, int landForceCount)
    {
        Debug.Log("currotine basladi ");
        OriginState.DeployTroops(landForceCount, navalForceCount);
    yield return new WaitForSeconds(travelDuration);
        DestinationState.ReinForceTroops(landForceCount, navalForceCount);
        Debug.Log("coorutune is finish ");
    }

}
