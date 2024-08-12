using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
    
    public bool �sAttack= false;
    public bool IsRegionPanelOpen = false;
    public bool IsAttackFinish  = true;
    public bool IsSpy= false;

    public float attackFinishDurtion = 10.0f;
    //animasyon i�in 
    public float moveAmount = 0.33f;    // Y ekseninde hareket edilecek mesafe
    public float moveDuration = 0.5f; // Hareket s�resi

    public int thresholdForSuccesfulEspionage = 3;
    public int spyCostModPurchases = 10;//number of spy cost mod purchases
    public bool �sGameOver= false;

    public List<State> allStates = new List<State>();



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

    public void UpdateStatePanel(State state)
    {
        UIManager.Instance.ShowPanel(state);
    }

    public void ChangeIsAttackValueTrue()
    {
        �sAttack = true;
    }
    public void ChangeIsAttackValueFalse()
    {
        �sAttack = false;
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
    
   
   
    
}
