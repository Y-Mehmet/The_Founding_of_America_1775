
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
    Color originalTextColor = Color.black;
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
            if (transaction.tradeType== TradeType.Import)
            {
                if(transaction.cost <= currentState.resourceData[ResourceType.Gold].currentAmount)
                {
                    tradeTypeBtnText.color = originalTextColor;
                }else
                {
                    tradeTypeBtnText.color = errorTextColor;
                }
            }else
            {
                if(transaction.quantity<= currentState.resourceData[(ResourceType)transaction.productSpriteIndex].currentAmount)
                {
                    tradeTypeBtnText.color = originalTextColor;
                }
                else
                {
                    tradeTypeBtnText.color = errorTextColor;
                }
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
               
                if (transaction.tradeType == 0)
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
                costText.text = transaction.cost.ToString();
                resIcon.sprite = ResSpriteSO.Instance.resIcon[(int)transaction.productSpriteIndex];
                stateFlagIcon.sprite = StateFlagSpritesSO.Instance.flagSpriteLists[transaction.tradeStateFlagIndex];



            }


        }
        else
            Debug.LogWarning("trademanager is null");
    }

    void OnBuyAgainButtonListenner()
    {
        if (transaction != null)
        {

            
            float spending = transaction.cost;
            float quantity = transaction.quantity;
            if (spending > 0 && quantity>0)
            {
                ResourceType type = (ResourceType)transaction.productSpriteIndex;
                State currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
                TradeHistory newTransaction;
                int stateFlagIndex = currentState.gameObject.transform.GetSiblingIndex();

                        if (transaction.tradeType == 0)
                        {
                            if (currentState.resourceData[ResourceType.Gold].currentAmount >= spending)
                            {
                        float deliveryTime = GameManager.nonNeigbordTradeTime;
                        if (Neighbor.Instance.AreNeighbors(currentState.name, transaction.tradeState.name))
                        {
                            deliveryTime = GameManager.neigbordTradeTime;
                        }
                        currentState.BuyyResource(type, quantity, spending,deliveryTime);
                                
                                newTransaction = new TradeHistory(TradeType.Import, GameDateManager.instance.CalculateDeliveryDateTime(deliveryTime), (int)type, quantity, spending, stateFlagIndex,transaction.tradeState);
                                TradeManager.instance.AddTransaction(newTransaction);
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
                                currentState.SellResource(type, quantity, spending);


                                newTransaction = new TradeHistory(TradeType.Export, GameDateManager.instance.GetCurrentDate(), (int)type, quantity, spending, stateFlagIndex, transaction.tradeState);
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
        else
            Debug.LogWarning("transaction is null");
        ButtonTextCollorUpdate();
    }


}
