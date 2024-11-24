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
        defState.LandArmySize = 100;
        defState.NavalArmySize = 100;
        State attackState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastAttackingState).GetComponent<State>();
        if (defState != null && attackState!= null)
        {

          
            Dictionary<ResourceType, float> res = defState.plunderedResources;
            
            if (res != null )
            {
                //Debug.LogWarning($" ilk alt�n de�releri eklemmenden �nce  ");
                //Debug.LogWarning($" atafck state {attackState.name} gold value {attackState.resourceData[ResourceType.Gold].currentAmount} ");
                //Debug.LogWarning($" def state {defState.name} gold value {defState.resourceData[ResourceType.Gold].currentAmount} ");

                attackState.SetMorale(5);
                if(isAddTrue)
                {
                    attackState.SetMorale(5);
                    attackState.AddResource(res);

                }
                attackState.AddResource(res);
                defState.RemoveResource(res);
                defState.RemoveResource(res);
                defState.RemoveResource(res);
                defState.RemoveResource(res);
                foreach (var item in res)
                {
                    if (item.Key== ResourceType.Gold)
                    {
                        if( isAddTrue)
                        {
                            MissionsManager.AddTotalPlunderGold(((int)item.Value));
                        }
                        MissionsManager.AddTotalPlunderGold(((int)item.Value));
                    }
                }





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
