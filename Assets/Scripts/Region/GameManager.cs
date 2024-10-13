using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
    
    public bool ÝsAttack= false;
    public bool IsRegionPanelOpen = false;
    public bool IsAttackFinish  = true;
    public bool IsSpy= false;

    public float attackFinishDurtion = 7.0f;
    //animasyon için 
    public float moveAmount = 0.33f;    // Y ekseninde hareket edilecek mesafe
    public float moveDuration = 0.5f; // Hareket süresi

    public static float gameDayTime = 1.0f;
    public static  float  neigbordTradeTime= 3 *gameDayTime;
    public static float  nonNeigbordTradeTime= 9 *gameDayTime;
    public static float ArrmyRatio = 0.25f; // nufusun yüzde kaçý asker olark baþlayacak 
    public static float ArmyBarrackRatio = 0.2f; // kýþla oraný

    public int thresholdForSuccesfulEspionage = 3;
    public int spyCostModPurchases = 10;//number of spy cost mod purchases

    
    public bool isGamePause = false;
    public bool ÝsGameOver= false;

   


    public List<State> allStates = new List<State>();
    public event Action OnAttackStarted;
    public event Action OnAttackStopped;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);

        }
        else
        {
            Instance = this;
        }
    }
    

    public List<State> GetStatesByType(StateType stateType)
    {
        List<State> states = new List<State>();
        foreach (State state in allStates)
        {
            if (state.stateType == stateType)
            {
                states.Add(state);
            }
        }
        return states;
    }

    //public void UpdateStatePanel(State state)
    //{
    //    UIManager.Instance.ShowPanel(state);
    //}

    public void ChangeIsAttackValueTrue()
    {
        ÝsAttack = true;
        isGamePause = true;
        OnAttackStarted?.Invoke();

    }
    public void ChangeIsAttackValueFalse()
    {

        ÝsAttack = false;
        isGamePause = false;
        OnAttackStopped?.Invoke(); // Olayý tetikleyin

    }
    public void ChanngeIsRegionPanelOpenValueTrue()
    {
        IsRegionPanelOpen = true;
    }
    public void ChanngeIsRegionPanelOpenValueFalse()
    {
        IsRegionPanelOpen = false;
    }
    public void ChangeIsSpyValueTrue()
    {
        IsSpy = true;
    }
    public void ChangeAttackFinisValueFalse()
    {
        IsAttackFinish = false;
    }
    public void ChangeAttackFinisValueTrue()
    {
        IsAttackFinish = true;
    }



}
