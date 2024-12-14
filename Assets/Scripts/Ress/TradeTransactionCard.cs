
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TradeTransactionCard : MonoBehaviour
{
    public  TMP_Text tradeTypeBtnText, dateText, quantityText, costText;
    public Image  resIcon,stateFlagIcon;
    TradeHistory transaction;
    Button buyAgainButton;// buy or sell duruma g�re de�i�ir
    Color originalTextColor= Color.white;
    Color errorTextColor = Color.red;

    

    private void OnEnable()
    {
       
        EventManager.Instance.OnProductReceived += TradeTansactionCardUIUpdate;
         buyAgainButton = tradeTypeBtnText.transform.parent.GetComponent<Button>();
        InvokeRepeating("ButtonTextCollorUpdate", 0,1/* GameManager.gameDayTime*/);
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
        CancelInvoke("ButtonTextCollorUpdate");
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
                    if (transaction.cost <= currentState.resourceData[ResourceType.Diamond].currentAmount && transaction.tradeState.tradeLists[1].limit[transaction.productSpriteIndex-1] >= transaction.quantity)
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
                        if (transaction.cost <= currentState.resourceData[ResourceType.Gold].currentAmount && transaction.tradeState.tradeLists[1].limit[transaction.productSpriteIndex - 1] >= transaction.quantity)
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
                        if (transaction.quantity <= currentState.resourceData[(ResourceType)transaction.productSpriteIndex].currentAmount && transaction.tradeState.tradeLists[0].limit[transaction.productSpriteIndex - 1] >= transaction.quantity)
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
        //Debug.LogWarning("trad u� uodate �al��t�");
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


                DateTime deliveryDate = GameDateManager.ConvertStringToDate(transaction.deliveryTime);


                if (deliveryDate> GameDateManager.currentDate)
                {
                    //Debug.LogWarning($"delivery date {deliveryDate} cuurent date {GameDateManager.currentDate}");
                    dateText.color = errorTextColor;
                }else
                {
                    dateText.color = originalTextColor;
                }
              
                dateText.text = GameDateManager.instance.ConvertDateToString(deliveryDate);
                quantityText.text = Utility.FormatNumber(transaction.quantity);
                costText.text = Utility.FormatNumber(Mathf.CeilToInt(transaction.cost));
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
                if (spending > 0 && quantity > 0 && transaction.tradeLimit> quantity)
                {
                    ResourceType type = (ResourceType)transaction.productSpriteIndex;
                    State currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
                    TradeHistory newTransaction;
                    State stateFlagIndex = currentState;
                        if (currentState.resourceData[ResourceType.Diamond].currentAmount >= spending)
                        {
                        SoundManager.instance.Play("Gem");
                        float goldValue = Mathf.Ceil(GameEconomy.Instance.GetGoldValue(type, spending));
                        bool isAllyState = GameManager.AllyStateList.Contains(transaction.tradeState);
                        currentState.InstantlyResource(type, quantity, spending);
                        transaction.tradeState.SellResource(type, quantity, goldValue, isAllyState);
                        
                        DateTime currentDate = GameDateManager.instance.GetCurrentDate();
                        bool payWhitGem = true;
                        int tradeLimit = (int)(transaction.tradeState.tradeLists[0].limit[transaction.productSpriteIndex - 1]- quantity);
                        
                        newTransaction = new TradeHistory(TradeType.Import, currentDate, (int)type, quantity, spending, stateFlagIndex, transaction.tradeState,tradeLimit, payWhitGem);
                            TradeManager.instance.AddTransaction(newTransaction);

                            
                        }
                        else
                        {
                        SoundManager.instance.Play("Error");
                      //  Debug.Log(" dimond dont eneaugh for buy resource");
                        }
                    // Debug.LogWarning($"res sat�n al�nd� quantaty {quantity} harcanan alt�n {spending}");



                }
                else
                {
                    SoundManager.instance.Play("Error");
                    Debug.LogWarning(" spending or cuantity value 0");
                }
                   
            }
            else
            {

                float spending = transaction.cost;
                float quantity = transaction.quantity;
                if (spending > 0 && quantity > 0 && transaction.tradeLimit > quantity)
                {
                    ResourceType type = (ResourceType)transaction.productSpriteIndex;
                    State currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
                    TradeHistory newTransaction;
                    State stateFlagIndex = currentState;

                    if (transaction.tradeType == 0)
                    {
                        if (currentState.resourceData[ResourceType.Gold].currentAmount >= spending)
                        {
                            float deliveryTime;
                            if (/*Neighbor.Instance != null && currentState != null && currentState.name != null && */transaction.tradeState != null)
                            {
                               
                                if (Neighbor.Instance.AreNeighbors(currentState.name, transaction.tradeState.name))
                                {
                                    deliveryTime = 2;
                                }
                                else
                                {

                                    List<Node> path = PathFindDeneme.PathInstance.GetPath(currentState.name, transaction.tradeState.name);
                                    deliveryTime = 11;
                                    if (path != null && path.Count > 0)
                                    {
                                        //   Debug.Log("path count " + path.Count);
                                        deliveryTime = path.Count;
                                    }
                                    else
                                    {
                                        Debug.Log("path is null");

                                    }
                                }


                                // currentState.BuyyResource(type, quantity, spending, deliveryTime);
                                bool isAllyState = GameManager.AllyStateList.Contains(currentState);
                                transaction.tradeState.SellResource(type, quantity, spending, isAllyState);
                                int tradeLimit = (int)transaction.tradeState.tradeLists[0].limit[transaction.productSpriteIndex - 1]- ((int)quantity);
                                newTransaction = new TradeHistory(TradeType.Import, GameDateManager.instance.CalculateDeliveryDateTime(deliveryTime), (int)type, quantity, spending, stateFlagIndex, transaction.tradeState, tradeLimit);
                                TradeManager.instance.AddTransaction(newTransaction);
                                TradeManager.TradeTransactionQueue.Add(newTransaction);
                                stateFlagIndex.GoldSpend(((int)spending));
                                MissionsManager.AddTotalImportGold(((int)spending));
                                SoundManager.instance.Play("Coins");
                                TradeManager.SortTradeTransactionsByDeliveryTime();
                            }
                            else
                            {
                                Debug.LogWarning("neigb�rd is null");

                            }

                        }
                        else
                        {
                            SoundManager.instance.Play("Error");
                            Debug.Log(" gold dont eneaugh for buy resource");
                        }
                    }
                    else
                    {
                        if (currentState.resourceData[type].currentAmount >= quantity && transaction.tradeLimit> quantity)
                        {
                            SoundManager.instance.Play("Cash");
                            bool isAllyState = GameManager.AllyStateList.Contains(transaction.tradeState);
                            currentState.SellResource(type, quantity, spending, isAllyState);
                            MissionsManager.AddTotalExportGold(((int)spending));
                            transaction.tradeState.BuyyResource(type, quantity, spending);

                            int tradeLimit = (int)transaction.tradeState.tradeLists[1].limit[transaction.productSpriteIndex - 1]- ((int)quantity);
                            newTransaction = new TradeHistory(TradeType.Export, GameDateManager.instance.GetCurrentDate(), (int)type, quantity, spending, stateFlagIndex, transaction.tradeState, tradeLimit);
                            TradeManager.instance.AddTransaction(newTransaction);
                        }
                        else
                        {
                            SoundManager.instance.Play("Error");
                            //Debug.LogWarning("res not eneught for sell ");
                        }


                    }




                    // Debug.LogWarning($"res sat�n al�nd� quantaty {quantity} harcanan alt�n {spending}");



                }
                else
                    SoundManager.instance.Play("Error");
            }
        }
        //else
        //    Debug.LogWarning("transaction is null");
        ButtonTextCollorUpdate();
    }


}
