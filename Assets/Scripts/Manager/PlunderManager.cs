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
   public  void PlunderState(bool isAddTrue=false)
    {
        State defState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastDefendingState).GetComponent<State>();
        State attackState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastAttackingState).GetComponent<State>();
        if (defState != null && attackState!= null)
        {

          
            Dictionary<ResourceType, float> res = defState.plunderedResources;
            
            if (res != null )
            {
                //Debug.LogWarning($" ilk altýn deðreleri eklemmenden önce  ");
                //Debug.LogWarning($" atafck state {attackState.name} gold value {attackState.resourceData[ResourceType.Gold].currentAmount} ");
                //Debug.LogWarning($" def state {defState.name} gold value {defState.resourceData[ResourceType.Gold].currentAmount} ");
                if(isAddTrue)
                {
                   
                    attackState.AddResource(res);

                }
                attackState.AddResource(res);
                defState.RemoveResource(res);
                defState.RemoveResource(res);





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
