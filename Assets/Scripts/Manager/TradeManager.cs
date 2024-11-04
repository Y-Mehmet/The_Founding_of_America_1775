using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDateManager;
public class TradeManager : MonoBehaviour
{
    public static TradeManager instance { get; private set; }
    public static Dictionary<Transform, Trade> stateTransformAndTradeList = new Dictionary<Transform, Trade>();
    public  int maxTransactionCount = 6; // uý gösterilecek max iþlem sayýsý
    public Action onTradeHistoryQueueChanged;
    public Queue<TradeHistory> TradeHistoryQueue = new Queue<TradeHistory>();
    public static List<TradeHistory> TradeTransactionQueue = new List<TradeHistory>();
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(BuyTransaction());
    }
    public static void SortTradeTransactionsByDeliveryTime()
    {
        TradeTransactionQueue.Sort((x, y) => DateTime.Compare(ConvertStringToDate(x.deliveryTime), ConvertStringToDate(y.deliveryTime)));
    }
    public void  AddTransaction(TradeHistory tradeHistory)
    {
        if(TradeHistoryQueue == null)
        {
         //   Debug.LogWarning("yeni TradeHistoryQueue olustutuldu");

        }
        if (TradeHistoryQueue.Count >= maxTransactionCount)
        {
            TradeHistoryQueue.Dequeue();
        }
        
        TradeHistoryQueue.Enqueue(tradeHistory);
     //   Debug.LogWarning("que count "+TradeHistoryQueue.Count);
        onTradeHistoryQueueChanged?.Invoke();
    }
   
    public Queue<TradeHistory> GetTradeHistoryQueue() { return TradeHistoryQueue; }
    IEnumerator BuyTransaction()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if (TradeTransactionQueue.Count > 0)
            {
                if (ConvertStringToDate(TradeTransactionQueue[0].deliveryTime )<= GameDateManager.instance.GetCurrentDate())
                {
                    TradeHistory th= TradeTransactionQueue[0];
                    TradeTransactionQueue[0].tradeState1.BuyyResource((ResourceType)th.productSpriteIndex, th.quantity, 0);
                    TradeTransactionQueue.Remove(th);
                }
            }
            yield return new WaitForSeconds(GameManager.gameDayTime);
        }
    }
}
[Serializable]
public class TradeHistory
{
    public bool payWhitGem;
    public TradeType tradeType;
    public string deliveryTime;
    public int productSpriteIndex;
    public float quantity;
    public float cost;
    public State tradeState1;
    public State tradeState;
    public int tradeLimit;


    // Constructor
    public TradeHistory( TradeType tradeType, DateTime deliveryTime, int productSpriteIndex, float quantity, float cost, State tradeState1, State tradeState,int tradeLimit, bool payWhitGem = false)
    {
        this.payWhitGem = payWhitGem;
        this.tradeType = tradeType;
        this.deliveryTime = GameDateManager.instance.ConvertDateToString(deliveryTime);
        this.productSpriteIndex = productSpriteIndex;
        this.quantity = quantity;
        this.cost = cost;
        this.tradeState1 = tradeState1;
        this.tradeState = tradeState;
        this.tradeLimit = tradeLimit;
    }
}