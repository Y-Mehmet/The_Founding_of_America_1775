using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    public GameObject rightBox, emtyStateBox;
    public TMP_InputField inputField;
    public Button  buyButton, instantlyButton, macButton;
    
    public TMP_Text contrackPriceValueText, InstantlyValueText, secondValueText,buyValueTax;
    int indexOfLimit;
    float contrackPrice;
    float quantity;
    float deliveryTime;
    Color originalTextColor;
    

    private void Start()
    {
        inputField.characterLimit = ResourceManager.Instance.InputFieldCaharcterLimit;
        originalTextColor = contrackPriceValueText.color;

    }
    private void OnEnable()
    {

        ResourceManager.Instance.OnResourceChanged += OnResourceOrStateChanged;
        ResourceManager.Instance.OnStateToTradeChanged += OnStateToTradeChanged;
        // Diðer abonelikler

        inputField.onValueChanged.AddListener(OnInputValueChanged);
        macButton.onClick.AddListener(MacButtonClicked);      
        buyButton.onClick.AddListener(BuyButtonClicked);     
        instantlyButton.onClick.AddListener(InstantlyButtonClicked);

        Restart();
       
        SetNewTradeState();
    }


    private void OnDisable()
    {
        // Olaylardan aboneliði kaldýr

        ResourceManager.Instance.OnResourceChanged -= OnResourceOrStateChanged;
        ResourceManager.Instance.OnStateToTradeChanged -= OnStateToTradeChanged;
        // Olaylara abone ol

        inputField.onValueChanged.RemoveListener(OnInputValueChanged);
        macButton.onClick.RemoveListener(MacButtonClicked);
        buyButton.onClick.RemoveListener(BuyButtonClicked); // Önce kaldýr
        instantlyButton.onClick.RemoveListener(InstantlyButtonClicked);

        StopAllCoroutines();


    }
    void Restart()
    {
        inputField.text = "";
    }
    // Olay tetiklendiðinde bu metod çalýþacak
    private void OnResourceOrStateChanged(ResourceType resourceType)
    {
       
        ShowPanelInfo();

    }

    private void OnStateToTradeChanged(string stateName)
    {
      
        
        ShowPanelInfo();


    }
    void SetSecondValueText()
    {
        string currentStateName = RegionClickHandler.Instance.currentState.name;
        string currentTradeStateName = ResourceManager.CurrentTradeState.name;
        if (Neighbor.Instance != null)
        {
            if (Neighbor.Instance.AreNeighbors(currentStateName, currentTradeStateName))
            {
                deliveryTime = GameManager.neigbordTradeTime;
                secondValueText.text = deliveryTime.ToString();
            }
            else
            {
                deliveryTime = GameManager.nonNeigbordTradeTime;
                secondValueText.text = deliveryTime.ToString();
            }
        }
        else
        {
            Debug.LogWarning("neignord is null");
        }
    }

    void ShowPanelInfo()
    {
       
        if (ResourceManager.CurrentTradeState != null)
        {
            if (rightBox.gameObject.activeSelf)
            {
               
                bool test = false; // test biti eðer trade export ya da import listimizde current rres varsa true döncek 
                foreach (var resType in ResourceManager.CurrentTradeState.exportTrade.resourceTypes)
                {
                    if (resType == ResourceManager.curentResource)
                    {
                        if (ResourceManager.CurrentTradeState.exportTrade.resourceTypes.IndexOf(ResourceManager.curentResource) != -1)
                        {
                            indexOfLimit = ResourceManager.CurrentTradeState.exportTrade.resourceTypes.IndexOf(ResourceManager.curentResource);
                            test = true;
                            break;
                        }
                        else
                        {
                            Debug.LogWarning("Hatalý index eriþimi -1 olmamalý");
                        }
                    }
                }
                if (!test)
                {
                    SetNewTradeState();
                }
                else
                {
                    float.TryParse(ResourceManager.CurrentTradeState.exportTrade.contractPrices[indexOfLimit].ToString(), out float price);
                    if (price >= 0)
                    {
                        contrackPrice = price;
                       // Debug.LogWarning("contrat price " + contrackPrice + "curent rade state name " + ResourceManager.CurrentTradeState.name + " currnet res" + ResourceManager.curentResource);
                        contrackPriceValueText.text = contrackPrice.ToString();

                    }
                    else
                    {
                        Debug.LogWarning(" conrrat picie sýfýrdan küçük olamamlý ");
                        contrackPriceValueText.text = "0";

                    }
                    SetSecondValueText();
                }
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


            float amountAvaible = ResourceManager.CurrentTradeState.resourceData[ResourceManager.curentResource].currentAmount;
            float buyPrice = quantity * contrackPrice;
            float goldResAmount = RegionClickHandler.Instance.currentState.GetComponent<State>().resourceData[ResourceType.Gold].currentAmount;
            float dimondResAmount= RegionClickHandler.Instance.currentState.GetComponent<State>().resourceData[ResourceType.Diamond].currentAmount;

            if (amountAvaible >= quantity && goldResAmount>=buyPrice )
            {
                contrackPriceValueText.color = originalTextColor;
                contrackPriceValueText.text = buyPrice.ToString();
            }
            else
            {
                contrackPriceValueText.color = Color.red;
                contrackPriceValueText.text = buyPrice.ToString();

            }
            
            float spending;
            if (float.TryParse(contrackPriceValueText.text, out spending))
            {
                float Dimond = GameEconomy.Instance.GetGemValue(spending);

                if (Dimond >0)
                {
                    if (amountAvaible >= quantity && dimondResAmount>Dimond)
                    {
                        InstantlyValueText.color = originalTextColor;
                    }
                    else
                    {
                        InstantlyValueText.color = Color.red;
                    }
                        InstantlyValueText.text = Dimond.ToString();
                }
                    
                else
                {
                    InstantlyValueText.text = "0";
                    Debug.LogWarning(" dimond deðerii 0 ");

                }

            }
            else
                Debug.LogWarning("dimond value can not parse float");





        }
        else
        {
            InstantlyValueText.text = "0";
            contrackPriceValueText.text = "0";
        }
            


    }
 
    public void MacButtonClicked()
    {

        inputField.text = ResourceManager.CurrentTradeState.resourceData[ResourceManager.curentResource].currentAmount.ToString();
        int amountAvailable;

        if (int.TryParse(inputField.text, out amountAvailable))
        {
            contrackPriceValueText.text = (amountAvailable * contrackPrice).ToString();
        }
        else
        {
            Debug.LogWarning(" amaoutny avaible inte dönüþtüürlemedi");
        }





    }
    public void BuyButtonClicked()
    {
        ResourceType type = ResourceManager.curentResource;
        float spending;
        if (float.TryParse(contrackPriceValueText.text, out spending))
        {
            if (spending > 0)
            {
                State currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
               if (currentState.resourceData[ResourceType.Gold].currentAmount >= spending)
                {
                    if(deliveryTime>0)
                    {
                        currentState.BuyyResource(type, quantity, spending,deliveryTime);
                        ResourceManager.CurrentTradeState.SellResource(type, quantity, spending);

                        int stateFlagIndex = currentState.gameObject.transform.GetSiblingIndex();

                        DateTime deliverTime = GameDateManager.instance.CalculateDeliveryDateTime(deliveryTime);
                        TradeHistory transaction = new TradeHistory(TradeType.Import, deliverTime, (int)type, quantity, spending, stateFlagIndex, ResourceManager.CurrentTradeState);
                        TradeManager.instance.AddTransaction(transaction);
                        buyButton.GetComponent<HideLastPanelButton>().DoHidePanel();
                        Debug.LogWarning($"res satýn alýndý quantaty {quantity} harcanan altýn {spending}");
                    }else
                    { Debug.LogError("delivery time is null"); }
                    
                }
               else
                {
                    Debug.Log(" gold dont eneaugh for buy resource");
                }
                    
            }
            else
                Debug.LogWarning(" spending value 0");
        }
        else
            Debug.LogWarning("spending value can not parse float");


    }
    public void InstantlyButtonClicked()
    {
        ResourceType type = ResourceManager.curentResource;
        float spending;
        if (float.TryParse(contrackPriceValueText.text, out spending))
        {
            float Dimond = GameEconomy.Instance.GetGemValue(spending);

            if (spending > 0)
            {
                if (ResourceManager.CurrentTradeState.resourceData[ResourceType.Diamond].currentAmount > Dimond)
                {
                    RegionClickHandler.Instance.currentState.GetComponent<State>().InstantlyResource(type, quantity, Dimond);
                    buyButton.GetComponent<HideLastPanelButton>().DoHidePanel();
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

        bool haveAnyTradeState = false; // o ürün için ticaret yapan ülke vars test bitibi true yap
        bool shouldTradeStateChange = true;
        ResourceType curretResType = ResourceManager.curentResource;
        string newTradeStatename = "";

        foreach (Transform stateTransform in Usa.Instance.transform)
        {
            // index 0 = import index 1 = export
            State newTradeState = stateTransform.GetComponent<State>();
            Trade trade = newTradeState.GetTrade(1, curretResType);
            if (trade != null)
            {
                if(ResourceManager.CurrentTradeState!= null)
                {
                    if (newTradeState == ResourceManager.CurrentTradeState)
                    {
                        haveAnyTradeState = true;
                        shouldTradeStateChange = false;                      
                        break;
                    }
                    if (haveAnyTradeState == false)
                    {
                        //Debug.LogWarning("state deðiþti: selde  " + stateTransform.name);

                        newTradeStatename = newTradeState.name;
                        haveAnyTradeState = true;

                    }

                }
                else
                {
                  
                }




            }



        }

        if (haveAnyTradeState == false)
        {
            rightBox.SetActive(false);
            emtyStateBox.SetActive(true);

        }
        else
        {
            if (shouldTradeStateChange)
            {
                ResourceManager.Instance.SetCurrentTradeState(newTradeStatename);

            }
            else { ShowPanelInfo(); }
                
            
           
               
            
            
            rightBox.SetActive(true);
            emtyStateBox.SetActive(false);
            

        }


    }




}
