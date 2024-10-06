using TMPro;
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
    Color originalTextColor;
    State currentTradeState; // trade state

    private void Start()
    {
        inputField.characterLimit = ResourceManager.Instance.InputFieldCaharcterLimit;
    }
    private void OnEnable()
    {
        

        
        SetNewTradeState();
       
        // Olaylara abone ol
        ResourceManager.Instance.OnResourceChanged += OnResourceOrStateChanged;
        ResourceManager.Instance.OnStateToTradeChanged += OnStateToTradeChanged;
        // ResourceManager.Instance.OnStateToTradeChanged += OnResourceOrStateChanged;
        // Diðer abonelikler
        inputField.onValueChanged.AddListener(OnInputValueChanged);

        // Daha önce eklenen listener'larý kaldýr, ardýndan tekrar ekle.
        macButton.onClick.RemoveListener(MacButtonClicked);
        macButton.onClick.AddListener(MacButtonClicked);

        buyButton.onClick.RemoveListener(BuyButtonClicked); // Önce kaldýr
        buyButton.onClick.AddListener(BuyButtonClicked);    // Sonra ekle

        instantlyButton.onClick.RemoveListener(InstantlyButtonClicked);
        instantlyButton.onClick.AddListener(InstantlyButtonClicked);

        // Panel bilgisini göster
        ShowPanelInfo();
        originalTextColor = contrackPriceValueText.color;
        Restart();
    }


    private void OnDisable()
    {
        // Olaylardan aboneliði kaldýr
        ResourceManager.Instance.OnResourceChanged -= OnResourceOrStateChanged;
        buyButton.onClick.RemoveListener(BuyButtonClicked); // Önce kaldýr
        instantlyButton.onClick.RemoveListener(InstantlyButtonClicked);
        macButton.onClick.RemoveListener(MacButtonClicked);
    }
    void Restart()
    {
        inputField.text = "";
    }
    // Olay tetiklendiðinde bu metod çalýþacak
    private void OnResourceOrStateChanged(ResourceType resourceType)
    {
        SetNewTradeState();
        ShowPanelInfo();
    }

    private void OnStateToTradeChanged(string stateName)
    {
        string currentStateName= RegionClickHandler.Instance.currentState.name;
        string currentTradeStateName = ResourceManager.Instance.curentTradeStateName;
        if(Neighbor.Instance!= null)
        {
          if (Neighbor.Instance.AreNeighbors(currentStateName, currentTradeStateName))
            {
                secondValueText.text=GameManager.neigbordTradeTime.ToString();
            }
          else
            {
                secondValueText.text = GameManager.nonNeigbordTradeTime.ToString();
            }
        }
        else
        {
            Debug.LogWarning("neignord is null");
        }
        ShowPanelInfo();


    }

    void ShowPanelInfo()
    {
        if (currentTradeState == null)
            SetNewTradeState();
        if (currentTradeState != null)
        {
            if (rightBox.gameObject.activeSelf)
            {
                ResourceType resourceType = ResourceManager.Instance.curentResource;


               // currentTradeState = Usa.Instance.FindStateByName(ResourceManager.Instance.curentTradeStateName);

                if (currentTradeState != null)
                {
                    foreach (var resType in currentTradeState.exportTrade.resourceTypes)
                    {
                        if (resType == resourceType)
                        {
                            if (currentTradeState.exportTrade.resourceTypes.IndexOf(resType) != -1)
                            {
                                indexOfLimit = currentTradeState.exportTrade.resourceTypes.IndexOf(resType);

                            }
                            else
                            {
                                Debug.LogWarning("Hatalý index eriþimi -1 olmamalý");
                            }
                        }
                    }



                    float.TryParse(currentTradeState.exportTrade.contractPrices[indexOfLimit].ToString(), out contrackPrice);

                    if (contrackPrice >= 0)
                    {
                        Debug.LogWarning("contrat price " + contrackPrice+"curent rade state name "+ currentTradeState.name);
                        contrackPriceValueText.text = contrackPrice.ToString();

                    }
                    else
                    {
                        Debug.LogWarning(" conrrat picie dýfýrdan küçük olamamlý ");
                        contrackPriceValueText.text = "0";

                    }
                }

            }
        }else
        { Debug.LogWarning("curent stat is null"); }
           

    }
    void OnInputValueChanged(string input)
    {




        if (float.TryParse(input, out quantity))
        {


            float amountAvaible = currentTradeState.resourceData[ResourceManager.Instance.curentResource].currentAmount;
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

        inputField.text = currentTradeState.resourceData[ResourceManager.Instance.curentResource].currentAmount.ToString();
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
        ResourceType type = ResourceManager.Instance.curentResource;
        float spending;
        if (float.TryParse(contrackPriceValueText.text, out spending))
        {
            if (spending > 0)
            {
                State currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
               if (currentState.resourceData[ResourceType.Gold].currentAmount >= spending)
                {
                    currentState.BuyyResource(type, quantity, spending);
                    currentTradeState.SellResource(type, quantity, spending);
                    
                     int stateFlagIndex = currentState.gameObject.transform.GetSiblingIndex();
                       
                    
                    TradeHistory transaction = new TradeHistory(TradeType.Import, GameDateManager.instance.GetCurrentDataString(),(int) type, quantity, spending, stateFlagIndex);
                    TradeManager.instance.AddTransaction(transaction);
                    buyButton.GetComponent<HideLastPanelButton>().DoHidePanel();
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
        else
            Debug.LogWarning("spending value can not parse float");


    }
    public void InstantlyButtonClicked()
    {
        ResourceType type = ResourceManager.Instance.curentResource;
        float spending;
        if (float.TryParse(contrackPriceValueText.text, out spending))
        {
            float Dimond = GameEconomy.Instance.GetGemValue(spending);

            if (spending > 0)
            {
                if (currentTradeState.resourceData[ResourceType.Diamond].currentAmount > Dimond)
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

        int test = 0;
        ResourceType curretResType = ResourceManager.Instance.curentResource;

        foreach (Transform stateTransform in Usa.Instance.transform)
        {
            Trade trade = stateTransform.GetComponent<State>().GetTrade(1, curretResType);
            if (trade != null)
            {

               if(test==0)
                {
                  // Debug.LogWarning("state deðiþti buyda " + stateTransform.name);
                    ResourceManager.Instance.SetCurrentTradeState(stateTransform.name);
                }
                    
                test++;

            }


        }
        if (test == 0)
        {
            rightBox.SetActive(false);
            emtyStateBox.SetActive(true);

        }
        else
        {
         //   Debug.LogWarning("bluann state  deðeri " + test);
            rightBox.SetActive(true);
            emtyStateBox.SetActive(false);
        }
        currentTradeState = Usa.Instance.FindStateByName(ResourceManager.Instance.curentTradeStateName).GetComponent<State>();


    }
    


}
