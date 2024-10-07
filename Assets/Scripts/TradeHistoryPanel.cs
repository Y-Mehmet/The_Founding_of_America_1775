using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeHistoryPanel : MonoBehaviour
{
    
    private void OnEnable()
    {
        ChilGameObjcetActiveted();
      

        if (TradeManager.instance != null)
        {
            TradeManager.instance.onTradeHistoryQueueChanged += NextChildGameObjcetActiveted;
        }
        else
        {
            Debug.LogWarning("trade manager is null");
        }

    }
    private void OnDisable()
    {
       
        if (TradeManager.instance != null)
        {
            TradeManager.instance.onTradeHistoryQueueChanged -= NextChildGameObjcetActiveted;
        }
    }
    private void ChilGameObjcetActiveted()
    {
        
        for (int index = 0; index < transform.childCount; index++)
        {

            if (index < TradeManager.instance.maxTransactionCount && index < TradeManager.instance.GetTradeHistoryQueue().Count)
            {
                GameObject childGameObject = transform.GetChild(index).gameObject;
                childGameObject.SetActive(true);
         
                if (childGameObject.TryGetComponent<TradeTransactionCard>(out TradeTransactionCard tradeTransactionCard))
                {
                    tradeTransactionCard.TradeTansactionCardUIUpdate();
                }
                else
                {
                    Debug.LogWarning(" childde component yok");
                }
            }
                
        }
    }
    private void NextChildGameObjcetActiveted()
    {
     
        int nextIndex = TradeManager.instance.GetTradeHistoryQueue().Count - 1;
        
       if(nextIndex>=0)
        {
            if(nextIndex<5)
            {
                GameObject childGameObject = transform.GetChild(nextIndex).gameObject;
                if (!childGameObject.activeSelf)
                    childGameObject.SetActive(true);
                if (childGameObject.TryGetComponent<TradeTransactionCard>(out TradeTransactionCard tradeTransactionCard))
                {
                    tradeTransactionCard.TradeTansactionCardUIUpdate();
                }
                else
                {
                    Debug.LogWarning(" childde component yok");
                }
            }else if(nextIndex==5)
            {
                ChilGameObjcetActiveted();
            }
           
        }
       else
        {
            Debug.LogWarning(" nextChild index is " + nextIndex);
        }
            

    }
   
}
