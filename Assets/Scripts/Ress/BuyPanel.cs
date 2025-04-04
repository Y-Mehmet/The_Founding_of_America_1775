using System;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static MY.NumberUtilitys.Utility;
using static ResourceManager;
using System.Collections.Generic;
public class BuyPanel : MonoBehaviour
{
    public GameObject rightBox, emtyStateBox;
    public TMP_InputField inputField;
    public Button  buyButton, instantlyButton, macButton;
    
    public TMP_Text contrackPriceValueText, InstantlyValueText, secondValueText,buyValueTax;
    int indexOfLimit;
    public float contrackPrice;
    public float quantity;
    public float deliveryTime;
    Color originalTextColor, ColorRed=Color.red, inputFieldTextColor=Color.black;
    State currentState, tradeState;

    
    

    private void Start()
    {
        inputField.characterLimit = InputFieldCaharcterLimit;
        originalTextColor = contrackPriceValueText.color;

    }
    private void OnEnable()
    {

        ResourceManager.Instance.OnResourceChanged += OnResourceOrStateChanged;
        ResourceManager.Instance.OnStateToTradeChanged += OnStateToTradeChanged;
        // Di�er abonelikler

        inputField.onValueChanged.AddListener(OnInputValueChanged);
        macButton.onClick.AddListener(MacButtonClicked);      
        buyButton.onClick.AddListener(BuyButtonClicked);     
        instantlyButton.onClick.AddListener(InstantlyButtonClicked);

        Restart();
       
        SetNewTradeState();

    }


    private void OnDisable()
    {
        // Olaylardan aboneli�i kald�r

        ResourceManager.Instance.OnResourceChanged -= OnResourceOrStateChanged;
        ResourceManager.Instance.OnStateToTradeChanged -= OnStateToTradeChanged;
        // Olaylara abone ol

        inputField.onValueChanged.RemoveListener(OnInputValueChanged);
        macButton.onClick.RemoveListener(MacButtonClicked);
        buyButton.onClick.RemoveListener(BuyButtonClicked); // �nce kald�r
        instantlyButton.onClick.RemoveListener(InstantlyButtonClicked);

        StopAllCoroutines();

        ColorReset();


    }
    void ColorReset()
    {
        contrackPriceValueText.color = originalTextColor;
        InstantlyValueText.color = originalTextColor;
        secondValueText.color = originalTextColor;
        buyValueTax.color = originalTextColor;
        inputField.textComponent.color = inputFieldTextColor;
    }
    void Restart()
    {
        inputField.text = "";
    }
    // Olay tetiklendi�inde bu metod �al��acak
    private void OnResourceOrStateChanged(ResourceType resourceType)
    {
        Debug.LogWarning($"res de�i�ti {resourceType} res  {ResourceManager.curentResource} ");
       // SetNewTradeState();

    }

    private void OnStateToTradeChanged(string stateName)
    {
       // Debug.LogWarning($"State de�i�ti {stateName} res  {ResourceManager.curentResource} ");

       // SetNewTradeState();


    }
    void SetSecondValueText()
    {
        string currentStateName = RegionClickHandler.staticState.name;
        string currentTradeStateName = ResourceManager.Instance.CurrentTradeState.name;
        //if (Neighbor.Instance != null)
        //{
        //    if (Neighbor.Instance.AreNeighbors(currentStateName, currentTradeStateName))
        //    {
        //        deliveryTime = GameManager.neigbordTradeTime;
        //        secondValueText.text = FormatNumber(deliveryTime);
        //    }
        //    else
        //    {
        //        deliveryTime = GameManager.nonNeigbordTradeTime;
        //        secondValueText.text = FormatNumber(deliveryTime);
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("neignord is null");
        //}
        List<Node> path = PathFindDeneme.PathInstance.GetPath(currentTradeStateName, currentStateName);
        if (path!= null && path.Count > 0)
        {
          //  Debug.Log("path count " + path.Count);
            deliveryTime = path.Count;
        }
        else
        {
            Debug.Log("path is null");
            deliveryTime = 11;
        }
        secondValueText.text = FormatNumber(deliveryTime);

    }

    void ShowPanelInfo()
    {
       
        if (ResourceManager.Instance.CurrentTradeState != null)
        {
            if (rightBox.gameObject.activeSelf)
            {
               
                bool test = false; // test biti e�er trade export ya da import listimizde current rres varsa true d�ncek 
                foreach (var resType in ResourceManager.Instance.CurrentTradeState.tradeLists[1].resourceTypes)
                {
                    if (resType == ResourceManager.curentResource)
                    {
                        if (ResourceManager.Instance.CurrentTradeState.tradeLists[1].resourceTypes.IndexOf(ResourceManager.curentResource) != -1)
                        {
                            indexOfLimit = ResourceManager.Instance.CurrentTradeState.tradeLists[1].resourceTypes.IndexOf(ResourceManager.curentResource);
                            test = true;
                            break;
                        }
                        else
                        {
                            Debug.LogWarning("Hatal� index eri�imi -1 olmamal�");
                        }
                    }
                }
                if (!test)
                {
                    SetNewTradeState();
                }
                else
                {
                    float.TryParse(ResourceManager.Instance.CurrentTradeState.tradeLists[1].contractPrices[indexOfLimit].ToString(), out float price);
                    if (price >= 0)
                    {
                        contrackPrice = price;
                       // Debug.LogWarning("contrat price " + contrackPrice + "curent rade state name " + ResourceManager.CurrentTradeState.name + " currnet res" + ResourceManager.curentResource);
                        contrackPriceValueText.text = FormatNumber(contrackPrice);

                    }
                    else
                    {
                        Debug.LogWarning(" conrrat picie s�f�rdan k���k olamaml� ");
                        contrackPriceValueText.text = "0";
                        contrackPrice = 0;

                    }
                    SetSecondValueText();
                }
            }
            else
            {
                Debug.LogWarning("rifght box actif self is false");
            }
        }
        else
        {
            Debug.LogWarning("curent trade is null");
        }
    }
    void OnInputValueChanged(string input)
    {




        if (float.TryParse(input, out quantity))
        {


            float amountAvaible = ResourceManager.Instance.CurrentTradeState.GetCurrentResValue(ResourceManager.curentResource);
            float buyPrice = quantity * contrackPrice;
            float goldResAmount = RegionClickHandler.Instance.currentState.GetComponent<State>().GetCurrentResValue(ResourceType.Gold);
            float dimondResAmount= RegionClickHandler.Instance.currentState.GetComponent<State>().GetCurrentResValue(ResourceType.Diamond);
            int limit = (int)tradeState.tradeLists[1].limit[(int)ResourceManager.curentResource - 1];
            if (tradeState.resourceData[ResourceManager.curentResource].currentAmount >= quantity && limit>=quantity )
            {
                inputField.textComponent.color = inputFieldTextColor;
            }else
            {
                inputField.textComponent.color = ColorRed;

            }

                if (amountAvaible >= quantity && goldResAmount>=buyPrice )
            {
                contrackPriceValueText.color = originalTextColor;
                
                contrackPriceValueText.text = FormatNumber(buyPrice);
                
            }
            else
            {
                contrackPriceValueText.color = Color.red;
                contrackPriceValueText.text = FormatNumber(buyPrice);
              

            }
            
            float spending;
            if (true)
            {
                spending = ParseFormattedNumber(contrackPriceValueText.text);

                float Dimond = GameEconomy.Instance.GetGemValue(spending);

                if (Dimond >0)
                {
                    if (amountAvaible >= quantity && dimondResAmount>=Dimond)
                    {
                        InstantlyValueText.color = originalTextColor;
                    }
                    else
                    {
                        InstantlyValueText.color = Color.red;
                    }
                    InstantlyValueText.text = FormatNumber(Dimond);
                }
                    
                else
                {
                    InstantlyValueText.text = "0";
                    Debug.LogWarning(" dimond de�erii 0 ");

                }

            }
            





        }
        else
        {
            InstantlyValueText.text = "0";
            contrackPriceValueText.text = "0";
           
        }
            


    }
 
    public void MacButtonClicked()
    {
        SoundManager.instance.Play("ButtonClick");
      
        int amountAvailable= (int) tradeState.resourceData[ResourceManager.curentResource].currentAmount;
        int limit =(int) tradeState.tradeLists[1].limit[(int)ResourceManager.curentResource - 1];

        int maxQuanty = amountAvailable >limit? limit:amountAvailable;
        int currentSpendLimit = (int)( currentState.GetGoldResValue() / contrackPrice);
        maxQuanty= maxQuanty>currentSpendLimit? currentSpendLimit:maxQuanty;

        inputField.text = maxQuanty.ToString();
        contrackPriceValueText.text = FormatNumber((maxQuanty * contrackPrice));
        
    }
    public void BuyButtonClicked()
    {

        ResourceType type = ResourceManager.curentResource;
        float spending;
      
        {
            spending = ParseFormattedNumber(contrackPriceValueText.text);
            int limit = (int)tradeState.tradeLists[1].limit[(int)ResourceManager.curentResource - 1];
            if (quantity > 0 && quantity<=limit)
            {
               
               if (currentState.GetGoldResValue() >= spending )
                {
                    if(tradeState.resourceData[ResourceManager.curentResource].currentAmount >= quantity)
                    {
                        if (deliveryTime > 0)
                        {
                           // currentState.BuyyResource(type, quantity, spending, deliveryTime);
                            bool isAllyState = GameManager.AllyStateList.Contains(ResourceManager.Instance.CurrentTradeState);
                            tradeState.SellResource(type, quantity, spending, isAllyState);
                            currentState.GoldSpend(((int)spending));
                           MissionsManager.AddTotalImportGold(((int)spending));
                            SoundManager.instance.Play("Coins");
                            // DecraseTradeLimit((int)quantity);

                             State stateFlagIndex = currentState;

                            DateTime deliverTime = GameDateManager.instance.CalculateDeliveryDateTime(deliveryTime);

                            TradeHistory transaction = new TradeHistory(TradeType.Import, deliverTime, (int)type, quantity, spending, stateFlagIndex, ResourceManager.Instance.CurrentTradeState,(int) (limit-quantity));
                            //Debug.LogWarning(" tanaction sate name " + transaction.tradeState.name);
                            TradeManager.instance.AddTransaction(transaction);
                            TradeManager.TradeTransactionQueue.Add(transaction);
                            TradeManager.SortTradeTransactionsByDeliveryTime();
                            UIManager.Instance.GetComponent<HideLastPanelButton>().DoHidePanel();
                            // Debug.LogWarning($"res sat�n al�nd� quantaty {quantity} harcanan alt�n {spending}");
                        }
                        else
                        { Debug.LogError("delivery time is null");
                            SoundManager.instance.Play("Error");
                        }
                   
                    }
                    
                    
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
    public void InstantlyButtonClicked()
    {
        ResourceType type = ResourceManager.curentResource;
        float spending;
        if (true)
        {
            spending = ParseFormattedNumber(contrackPriceValueText.text);
            
            float Dimond = GameEconomy.Instance.GetGemValue(spending);
            spending = Dimond;
            int limit = (int)tradeState.tradeLists[1].limit[(int)type - 1];
            if ( quantity >0 && quantity<=limit)
            {
                if (currentState.GetGemResValue() >= spending)
                {
                    if(ResourceManager.Instance.CurrentTradeState.resourceData[type].currentAmount>=quantity)
                    {
                      State stateFlagIndex = currentState;
                        bool isAllyState = GameManager.AllyStateList.Contains(ResourceManager.Instance.CurrentTradeState);
                        currentState.InstantlyResource(type, quantity, spending);
                        ResourceManager.Instance.CurrentTradeState.SellResource(type, quantity, spending, isAllyState);
                       
                        //DecraseTradeLimit((int)quantity);
                        DateTime deliverTime = GameDateManager.instance.GetCurrentDate();
                        bool payWhitGem = true;
                        TradeHistory transaction = new TradeHistory(TradeType.Import, deliverTime, (int)type, quantity, spending, stateFlagIndex, ResourceManager.Instance.CurrentTradeState, (int)(limit - quantity), payWhitGem);
                        TradeManager.instance.AddTransaction(transaction);
                        UIManager.Instance.GetComponent<HideLastPanelButton>().DoHidePanel();
                        inputField.textComponent.color = inputFieldTextColor;
                    }
                    else
                    {
                        inputField.textComponent.color = Color.red;
                    }
                    
                   
                }
                else
                {
                    Debug.Log("dimond not enaugh for resource");
                }
            }

            else
                Debug.LogWarning(" spending value 0");
        }
        else
            Debug.LogWarning("spending value can not parse float");

    }

    public void SetNewTradeState()
    {
        bool haveAnyTradeState = false; // o �r�n i�in ticaret yapan �lke vars test bitibi true yap
        bool shouldTradeStateChange = true;
        ResourceType curretResType = ResourceManager.curentResource;       
        string newTradeStatename = "";
        tradeState= ResourceManager.Instance.CurrentTradeState;
        foreach (Transform stateTransform in Usa.Instance.transform)
        {
            // index 0 = import index 1 = export
            State newTradeState = stateTransform.GetComponent<State>();
            Trade trade = newTradeState.GetTrade(1, curretResType);
            if (trade != null)
            {
                if(tradeState!= null)
                {
                    if (newTradeState == tradeState)
                    {
                        haveAnyTradeState = true;
                        shouldTradeStateChange = false;                      
                        break;
                    }
                    if (haveAnyTradeState == false)
                    {
                        //Debug.LogWarning("state de�i�ti: selde  " + stateTransform.name);
                        newTradeStatename = newTradeState.name;
                        haveAnyTradeState = true;
                    }
                }
                else
                {
                    //Debug.LogWarning("ResourceManager.CurrentTradeState is null");
                }
            }
        }
        if (haveAnyTradeState == false)
        {
          //  Debug.LogWarning("hi�bir trade yap�lacak state bulunamad� res: " + ResourceManager.curentResource +" "+ curretResType);
            rightBox.SetActive(false);
            emtyStateBox.SetActive(true);
        }
        else
        {
            if (shouldTradeStateChange)
            {
                ResourceManager.Instance.SetCurrentTradeState(newTradeStatename);             
            }
            else
            {
                ShowPanelInfo();
            }
            rightBox.SetActive(true);
            emtyStateBox.SetActive(false);
            ShowPanelInfo();
        }
        currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        tradeState = ResourceManager.Instance.CurrentTradeState;

    }
  
  
}
