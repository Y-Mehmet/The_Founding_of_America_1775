using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);

        }else
        {
            Instance = this;
        }
    }
    public bool ÝsAttack= false;
    public bool IsRegionPanelOpen = false;
    public bool IsAttackFinish  = true;
    public bool IsSpy= false;

    public float attackFinishDurtion = 10.0f;

    public List<State> allStates = new List<State>();

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
        ÝsAttack = true;
    }
    public void ChangeIsAttackValueFalse()
    {
        ÝsAttack = false;
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
