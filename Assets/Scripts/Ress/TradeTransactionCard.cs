
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TradeTransactionCard : MonoBehaviour
{
    public  TMP_Text tradeTypeBtnText, dateText, quantityText, costText;
    public Image  resIcon,stateFlagIcon;
    TradeHistory transaction;
    Button buyAgainButton;// buy or sell duruma göre deðiþir
    Color originalTextColor= Color.white;
    Color errorTextColor = Color.red;

    

    private void OnEnable()
    {
       
        EventManager.Instance.OnProductReceived += TradeTansactionCardUIUpdate;
         buyAgainButton = tradeTypeBtnText.transform.parent.GetComponent<Button>();
        InvokeRepeating("ButtonTextCollorUpdate", 0, GameManager.gameDayTime);
        if (buyAgainButton != null)
        {
            buyAgainButton.onClick.AddListener(OnBuyAgainButtonListenner);
            
        }
        else
        {
            Debug.LogError("buy button is null");
        }

    }
    private void OnDisable()
    {
        EventManager.Instance.OnProductReceived -= TradeTansactionCardUIUpdate;
        resetCollor();
        if (TradeManager.instance != null)
        {
            TradeManager.instance.onTradeHistoryQueueChanged += TradeTansactionCardUIUpdate;
        }
        else
        {
            Debug.LogWarning("trade manager is null");
        }
        buyAgainButton.onClick.RemoveListener(OnBuyAgainButtonListenner);
        tradeTypeBtnText.color = originalTextColor;
    }
    void ButtonTextCollorUpdate()
    {
        if(transaction == null)
        {
            int index = gameObject.transform.GetSiblingIndex();

            transaction = TradeManager.instance.GetTradeHistoryQueue().ToArray()[index];
        }
        if (transaction != null)
        {
            State currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();

            if (transaction.tradeState.resourceData[(ResourceType)transaction.productSpriteIndex].currentAmount >= transaction.quantity )
            { 
                if (transaction.payWhitGem)
                {
                    if (transaction.cost <= currentState.resourceData[ResourceType.Diamond].currentAmount && transaction.tradeState.exportTrade.limit[transaction.productSpriteIndex-1] >= transaction.quantity)
                    {

                        tradeTypeBtnText.color = originalTextColor;
                    }
                    else
                    {
                        tradeTypeBtnText.color = errorTextColor;
                    }
                }
                else
                {
                    if (transaction.tradeType == TradeType.Import)
                    {
                        if (transaction.cost <= currentState.resourceData[ResourceType.Gold].currentAmount && transaction.tradeState.exportTrade.limit[transaction.productSpriteIndex - 1] >= transaction.quantity)
                        {
                            tradeTypeBtnText.color = originalTextColor;
                        }
                        else
                        {
                            tradeTypeBtnText.color = errorTextColor;
                        }
                    }
                    else
                    {
                        if (transaction.quantity <= currentState.resourceData[(ResourceType)transaction.productSpriteIndex].currentAmount && transaction.tradeState.importTrade.limit[transaction.productSpriteIndex - 1] >= transaction.quantity)
                        {
                            tradeTypeBtnText.color = originalTextColor;
                        }
                        else
                        {
                            tradeTypeBtnText.color = errorTextColor;
                        }
                    }
                }
            }
            else
            {
                tradeTypeBtnText.color = errorTextColor;
            }
           
        }
        else
        {
            Debug.LogWarning("tranaction is null");
        }
    }
    void resetCollor()
    {
        tradeTypeBtnText.color = originalTextColor;
    }
    
    public void TradeTansactionCardUIUpdate()
    {
        //Debug.LogWarning("trad uý uodate çalýþtý");
        ButtonTextCollorUpdate();
        if (TradeManager.instance != null)
        {
            int index = gameObject.transform.GetSiblingIndex();

            transaction = TradeManager.instance.GetTradeHistoryQueue().ToArray()[index];
            if (transaction != null)
            {
               
                if (transaction.tradeType == TradeType.Import)
                {
                    tradeTypeBtnText.text = "Buy";
                }
                else
                {
                    tradeTypeBtnText.text = "Sell";
                }


                DateTime deliveryDate = transaction.deliveryTime;


                if (deliveryDate> GameDateManager.currentDate)
                {
                    //Debug.LogWarning($"delivery date {deliveryDate} cuurent date {GameDateManager.currentDate}");
                    dateText.color = errorTextColor;
                }else
                {
                    dateText.color = originalTextColor;
                }
              
                dateText.text = GameDateManager.instance.ConvertDateToString(deliveryDate);
                quantityText.text = transaction.quantity.ToString();
                costText.text = Mathf.CeilToInt( transaction.cost).ToString("F0");
                resIcon.sprite = ResSpriteSO.Instance.resIcon[(int)transaction.productSpriteIndex];
                stateFlagIcon.sprite = transaction.tradeState.StateIcon; //StateFlagSpritesSO.Instance.flagSpriteLists[transaction.tradeStateFlagIndex];



            }


        }
        else
            Debug.LogWarning("trademanager is null");
    }

    void OnBuyAgainButtonListenner()
    {
        if (transaction != null && tradeTypeBtnText.color==originalTextColor)
        {
            if(transaction.payWhitGem )
            {

                float spending = transaction.cost;
                float quantity = transaction.quantity;
                if (spending > 0 && quantity > 0)
                {
                    ResourceType type = (ResourceType)transaction.productSpriteIndex;
                    State currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
                    TradeHistory newTransaction;
                    int stateFlagIndex = currentState.gameObject.transform.GetSiblingIndex();
                        if (currentState.resourceData[ResourceType.Diamond].currentAmount >= spending)
                        {
                        float goldValue = Mathf.Ceil(GameEconomy.Instance.GetGoldValue(type, spending));
                        bool isAllyState = GameManager.AllyStateList.Contains(transaction.tradeState);
                        currentState.InstantlyResource(type, quantity, spending);
                        transaction.tradeState.SellResource(type, quantity, goldValue, isAllyState);
                        
                        DateTime currentDate = GameDateManager.instance.GetCurrentDate();
                        bool payWhitGem = true;
                        int tradeLimit = (int)transaction.tradeState.importTrade.limit[transaction.productSpriteIndex - 1];
                        newTransaction = new TradeHistory(TradeType.Import, currentDate, (int)type, quantity, spending, stateFlagIndex, transaction.tradeState,tradeLimit, payWhitGem);
                            TradeManager.instance.AddTransaction(newTransaction);
                        }
                        else
                        {
                        
                            Debug.Log(" dimond dont eneaugh for buy resource");
                        }
                    // Debug.LogWarning($"res satýn alýndý quantaty {quantity} harcanan altýn {spending}");



                }
                else
                    Debug.LogWarning(" spending or cuantity value 0");
            }
            else
            {

                float spending = transaction.cost;
                float quantity = transaction.quantity;
                if (spending > 0 && quantity > 0)
                {
                    ResourceType type = (ResourceType)transaction.productSpriteIndex;
                    State currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
                    TradeHistory newTransaction;
                    int stateFlagIndex = currentState.gameObject.transform.GetSiblingIndex();

                    if (transaction.tradeType == 0)
                    {
                        if (currentState.resourceData[ResourceType.Gold].currentAmount >= spending)
                        {
                            float deliveryTime;
                            if (/*Neighbor.Instance != null && currentState != null && currentState.name != null && */transaction.tradeState != null )
                            {
                                if (Neighbor.Instance.AreNeighbors(currentState.name, transaction.tradeState.name))
                                {
                                    deliveryTime = GameManager.neigbordTradeTime;
                                }
                                else
                                {
                                    deliveryTime = GameManager.nonNeigbordTradeTime;
                                }
                                   

                                currentState.BuyyResource(type, quantity, spending, deliveryTime);
                                bool isAllyState = GameManager.AllyStateList.Contains(currentState);
                                transaction.tradeState.SellResource(type, quantity, spending, isAllyState);
                                int tradeLimit =(int) transaction.tradeState.importTrade.limit[transaction.productSpriteIndex - 1];
                                newTransaction = new TradeHistory(TradeType.Import, GameDateManager.instance.CalculateDeliveryDateTime(deliveryTime), (int)type, quantity, spending, stateFlagIndex, transaction.tradeState, tradeLimit);
                                TradeManager.instance.AddTransaction(newTransaction);
                            }else
                            {
                                Debug.LogWarning("neigbýrd is null");

                            }
                            
                        }
                        else
                        {
                            Debug.Log(" gold dont eneaugh for buy resource");
                        }
                    }
                    else
                    {
                        if (currentState.resourceData[type].currentAmount >= quantity)
                        {
                            bool isAllyState = GameManager.AllyStateList.Contains(transaction.tradeState);
                            currentState.SellResource(type, quantity, spending, isAllyState);
                            transaction.tradeState.BuyyResource(type, quantity, spending);

                            int tradeLimit = (int)transaction.tradeState.exportTrade.limit[transaction.productSpriteIndex - 1];
                            newTransaction = new TradeHistory(TradeType.Export, GameDateManager.instance.GetCurrentDate(), (int)type, quantity, spending, stateFlagIndex, transaction.tradeState,tradeLimit);
                            TradeManager.instance.AddTransaction(newTransaction);
                        }
                        else
                        {
                            Debug.LogWarning("res not eneught for sell ");
                        }


                    }




                    // Debug.LogWarning($"res satýn alýndý quantaty {quantity} harcanan altýn {spending}");



                }
                else
                    Debug.LogWarning(" spending or cuantity value 0");
            }
        }
        //else
        //    Debug.LogWarning("transaction is null");
        ButtonTextCollorUpdate();
    }


}
