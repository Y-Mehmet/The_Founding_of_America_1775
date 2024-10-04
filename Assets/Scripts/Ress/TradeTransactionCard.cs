using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TradeTransactionCard : MonoBehaviour
{
    public  TMP_Text tradeTypeBtnText, dateText, quantityText, costText;
    public Image  resIcon,stateFlagIcon;
    TradeHistory transaction;
    private void OnEnable()
    {
        if (TradeManager.instance != null)
        {
            int index = gameObject.transform.GetSiblingIndex() ;
            if (index<TradeManager.instance.maxTransactionCount && index< TradeManager.instance.GetTradeHistoryQueue().Count)
            {
                 transaction= TradeManager.instance.GetTradeHistoryQueue().ToArray()[index];
                if(transaction != null )
                {
                   Button buyAgainButton= tradeTypeBtnText.transform.parent.GetComponent<Button>();
                    if (buyAgainButton != null)
                        buyAgainButton.onClick.AddListener(OnBuyAgainButtonListenner);
                    else
                        Debug.LogError("buy button is null");
                    if (transaction.tradeType == 0)
                    {
                        tradeTypeBtnText.text = "Buy";
                    }
                    else
                    {
                        tradeTypeBtnText.text = "Sell";
                    }
                    dateText.text = transaction.dateString;
                    quantityText.text = transaction.quantity.ToString();
                    costText.text=transaction.cost.ToString();
                    resIcon.sprite= ResSpriteSO.Instance.resIcon[(int)transaction.productSpriteIndex];
                    stateFlagIcon.sprite = StateFlagSpritesSO.Instance.flagSpriteLists[transaction.tradeStateFlagIndex];
                    
                   

                }
                
            }
            else
            {
                Debug.LogWarning($" {index} index nolu set actifi false tr count{TradeManager.instance.maxTransactionCount} wue count{TradeManager.instance.GetTradeHistoryQueue().Count}");
                gameObject.SetActive(false);
            }
        }
        else
            Debug.LogWarning("trademanager is null");
    }
    void OnBuyAgainButtonListenner()
    {
        ResourceType type = (ResourceType)transaction.productSpriteIndex;
        float  spending= transaction.cost;
        float quantity= transaction.quantity;
        if (spending > 0)
            {
                State currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
                if (currentState.resourceData[ResourceType.Gold].currentAmount >= spending)
                {
                    currentState.BuyyResource(type, quantity, spending);

                    int stateFlagIndex = currentState.gameObject.transform.GetSiblingIndex();


                    TradeHistory transaction = new TradeHistory(TradeType.Import, GameDateManager.instance.GetCurrentDataString(), (int)type, quantity, spending, stateFlagIndex);
                    TradeManager.instance.AddTransaction(transaction);
                    Debug.LogWarning($"res satýn alýndý quantaty {quantity} harcanan altýn {spending}");
                }
                else
                {
                    Debug.Log(" gold dont eneaugh for buy resource");
                }

            }
            else
                Debug.LogWarning(" spending value 0");
       
    }

}
