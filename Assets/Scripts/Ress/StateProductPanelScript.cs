using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StateProductPanelScript : MonoBehaviour
{
    public Dictionary<Transform, Trade> stateTransformAndTradeList = new Dictionary<Transform, Trade>();
    public TradeType tradeType;


    private void OnEnable()
    {
        if(tradeType== TradeType.Import)
        {
            GetImportTradeList();
            
        }
        else
        {

            GetExportTradeList();
        }
        int count = stateTransformAndTradeList.Count;
       int minCount = count <10 ? 10 : count;
        GetComponent<GridLayoutGroup>().constraintCount = minCount;
        foreach (Transform child in transform)
        {
            if(child.GetSiblingIndex()< count)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
    public Dictionary<Transform, Trade> GetImportTradeList()
    {
      //  Debug.LogWarning(" stateTransformAndTradeList import list count " + stateTransformAndTradeList.Count);
      return  GetTradeList(0);
    }
    public Dictionary<Transform, Trade> GetExportTradeList()
    {
      //  Debug.LogWarning(" stateTransformAndTradeList export list count " + stateTransformAndTradeList.Count);
        return GetTradeList(1);
    }
    
    public Dictionary<Transform, Trade> GetTradeList(int  enumIndexNum )
        {
       // Debug.LogWarning(" get trade �al��t�");
        if(stateTransformAndTradeList!= null)
        stateTransformAndTradeList.Clear();

        ResourceType curretResType = ResourceManager.Instance.curentResource;
      //  Debug.LogWarning("curentr res type " + curretResType);
        if( curretResType!= null )
        {
            foreach (Transform stateTransform in Usa.Instance.transform)
            {
                Trade trade = stateTransform.GetComponent<State>().GetTrade(enumIndexNum, curretResType);
                if (trade != null)
                {
                  //  Debug.LogWarning("buraya export kaynaklar� eklendi");
                    stateTransformAndTradeList.Add(stateTransform, trade);
                }
               // else
                //    Debug.LogWarning(" trade is  *************************null");

            }
            return stateTransformAndTradeList;
        }
        else
        {
          //  Debug.LogWarning("current res is null");
            return null;
        }
       

    }

}
