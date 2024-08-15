using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlunderManager : MonoBehaviour
{
    public static PlunderManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
   public  void PlunderState()
    {
        State defState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastDefendingState).GetComponent<State>();
        State attackState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastAttackingState).GetComponent<State>();
        if (defState != null && attackState!= null)
        {
          //  Debug.Log("yaðam gerçekleþti");
            Dictionary<ResourceType, int> res = defState.PlunderResource();
            if(res != null )
            {
             
                attackState.AddResource(res);
               


            }
            else
            {
                Debug.LogWarning("res is null");
            }
         
            GameManager.Instance.IsAttackFinish = true;
        }
        else
            Debug.LogWarning("def state is null");
    }
}
