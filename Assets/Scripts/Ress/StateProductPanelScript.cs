using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StateProductPanelScript : MonoBehaviour
{
    public Dictionary<Transform, Trade> stateTransformAndTradeList = new Dictionary<Transform, Trade>();
    public TradeType tradeType; // eðer sell yapacaksak bize import trade lazým çünkü biz o ürünü import edecek ülkelerin listesini istiyoruz 


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
        int count = stateTransformAndTradeList.Count+1;
       int minCount = count <10 ? 10 : count;
        GetComponent<GridLayoutGroup>().constraintCount = minCount;
        foreach (Transform child in transform)
        {
            if(child.GetSiblingIndex()< count)
            {
                child.gameObject.SetActive(true);
            }else
            {
                child.gameObject.SetActive(false);
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
    
    public Dictionary<Transform, Trade> GetTradeList(int  enumIndexNum /* trade type belirler */ )
        {
       // Debug.LogWarning(" get trade çalýþtý");
        if(stateTransformAndTradeList!= null)
        stateTransformAndTradeList.Clear();

        ResourceType curretResType = ResourceManager.curentResource;
      //  Debug.LogWarning("curentr res type " + curretResType);
        if( ResourceManager.curentResource != null )
        {
            foreach (Transform stateTransform in Usa.Instance.transform)
            {
                Trade trade = stateTransform.GetComponent<State>().GetTrade(enumIndexNum, curretResType);
                if (trade != null)
                {
                  //  Debug.LogWarning("buraya export kaynaklarý eklendi");
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
