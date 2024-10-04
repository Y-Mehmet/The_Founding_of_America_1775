using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
   public static TradeManager instance { get; private set; }
    int maxTransactionCount = 6; // uý gösterilecek max iþlem sayýsý
  
   private Queue<TradeHistory> TradeHistoryQueue = new Queue<TradeHistory>();
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    public void  AddTransaction(TradeHistory tradeHistory)
    {
        if(TradeHistoryQueue == null)
        {
            Debug.LogWarning("yeni TradeHistoryQueue olustutuldu");

        }
        if (TradeHistoryQueue.Count >= maxTransactionCount)
        {
            TradeHistoryQueue.Dequeue();
        }
        TradeHistoryQueue.Enqueue(tradeHistory);
    }
    public Queue<TradeHistory> GetTradeHistoryQueue() { return TradeHistoryQueue; }
}
public class TradeHistory
{
    public  TradeType tradeType;
    public string dateString;
    public int productSpriteIndex;
    public float quantity;
    public float cost;
    public string tradeStateFlagName;
}
