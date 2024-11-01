using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
    public static List<State> AllyStateList = new List<State>();
    public Action<State> onAllyStateChanged;
    public static int tottalPopulation = 0;
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
    public static float ArrmyRatio = 0.1f; // nufusun yüzde kaçý asker olark baþlayacak 
    public static float ArmyBarrackRatio = 0.2f; // kýþla oraný

    public int thresholdForSuccesfulEspionage = 3;
    public int spyCostModPurchases = 10;//number of spy cost mod purchases

    
    public bool isGamePause = false;
    public bool ÝsGameOver= false;
    public bool IsFirstSave = true;

   


    public List<State> allStates = new List<State>();
    public event Action OnAttackStarted;
    public event Action OnAttackStopped;
    public Action OnGameDataLoaded;



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
    private void Start()
    {
        OnGameDataLoaded += GameLoaded;
      
       
    }
    void GameLoaded()
    {
        AllyStateList.Clear();
        AllyStateList = GetStatesByType(StateType.Ally);
    }
    public static void TotalPopulationManager( int addedValue)
    {
        tottalPopulation += addedValue;
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
    // kullanýlmýyor
    public void OnAllyStateChanged()
    {
        AllyStateList.Clear();
        foreach (State state in allStates)
        {
            if (state.stateType == StateType.Ally)
            {
                allStates.Add(state);
            }
        }
        
    }
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
    public void GameOver()
    {
       
        UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.GameOverPanel);
        GameManager.Instance.IsRegionPanelOpen = true;
    }
    IEnumerator OpenGameOverPanel()
    {
        yield return new WaitForSeconds(10);

       
    }



}
[Serializable]
public enum ActType
{
    Population,
    Social,
    National,
    Emancipation,
    Labor,
    None

}
