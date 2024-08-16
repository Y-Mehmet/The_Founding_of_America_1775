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
            //  Debug.Log("ya�am ger�ekle�ti");
            defState.PlunderResource();
            Dictionary<ResourceType, float> res = defState.plunderedResources;
            if(res != null )
            {
                //Debug.LogWarning($" ilk alt�n de�releri eklemmenden �nce  ");
                //Debug.LogWarning($" atafck state {attackState.name} gold value {attackState.resourceData[ResourceType.Gold].currentAmount} ");
                //Debug.LogWarning($" def state {defState.name} gold value {defState.resourceData[ResourceType.Gold].currentAmount} ");
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
