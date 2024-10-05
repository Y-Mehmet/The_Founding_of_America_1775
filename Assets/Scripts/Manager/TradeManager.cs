using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
   public static TradeManager instance { get; private set; }
   public  int maxTransactionCount = 6; // uý gösterilecek max iþlem sayýsý
   public Action onTradeHistoryQueueChanged;
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
       // Debug.LogWarning("que count "+TradeHistoryQueue.Count);
        onTradeHistoryQueueChanged?.Invoke();
    }
    public Queue<TradeHistory> GetTradeHistoryQueue() { return TradeHistoryQueue; }
}
public class TradeHistory
{
    public TradeType tradeType;
    public string dateString;
    public int productSpriteIndex;
    public float quantity;
    public float cost;
    public int tradeStateFlagIndex;

    // Constructor
    public TradeHistory(TradeType tradeType, string dateString, int productSpriteIndex, float quantity, float cost, int tradeStateFlagIndex)
    {
        this.tradeType = tradeType;
        this.dateString = dateString;
        this.productSpriteIndex = productSpriteIndex;
        this.quantity = quantity;
        this.cost = cost;
        this.tradeStateFlagIndex = tradeStateFlagIndex;
    }
}