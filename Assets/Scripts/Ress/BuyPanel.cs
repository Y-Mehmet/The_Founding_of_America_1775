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
    State currentState; // trade state

    private void Start()
    {
        inputField.characterLimit = ResourceManager.Instance.InputFieldCaharcterLimit;
    }
    private void OnEnable()
    {
        

        
        SetNewTradeState();
        currentState = Usa.Instance.FindStateByName(ResourceManager.Instance.curentTradeStateName).GetComponent<State>();
        // Olaylara abone ol
        ResourceManager.Instance.OnResourceChanged += OnResourceOrStateChanged;
        ResourceManager.Instance.OnStateToTradeChanged += OnStateToTradeChanged;
        // ResourceManager.Instance.OnStateToTradeChanged += OnResourceOrStateChanged;
        // Di�er abonelikler
        inputField.onValueChanged.AddListener(OnInputValueChanged);

        // Daha �nce eklenen listener'lar� kald�r, ard�ndan tekrar ekle.
        macButton.onClick.RemoveListener(MacButtonClicked);
        macButton.onClick.AddListener(MacButtonClicked);

        buyButton.onClick.RemoveListener(BuyButtonClicked); // �nce kald�r
        buyButton.onClick.AddListener(BuyButtonClicked);    // Sonra ekle

        instantlyButton.onClick.RemoveListener(InstantlyButtonClicked);
        instantlyButton.onClick.AddListener(InstantlyButtonClicked);

        // Panel bilgisini g�ster
        ShowPanelInfo();
        originalTextColor = contrackPriceValueText.color;
        Restart();
    }


    private void OnDisable()
    {
        // Olaylardan aboneli�i kald�r
        ResourceManager.Instance.OnResourceChanged -= OnResourceOrStateChanged;
        buyButton.onClick.RemoveListener(BuyButtonClicked); // �nce kald�r
        instantlyButton.onClick.RemoveListener(InstantlyButtonClicked);
        macButton.onClick.RemoveListener(MacButtonClicked);
    }
    void Restart()
    {
        inputField.text = "";
    }
    // Olay tetiklendi�inde bu metod �al��acak
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
        
    }

    void ShowPanelInfo()
    {

        if (rightBox.gameObject.activeSelf)
        {
            ResourceType resourceType = ResourceManager.Instance.curentResource;


             currentState = Usa.Instance.FindStateByName(ResourceManager.Instance.curentTradeStateName).GetComponent<State>();

            if ( currentState!= null)
            {
                foreach (var resType in currentState.exportTrade.resourceTypes)
                {
                    if (resType == resourceType)
                    {
                        if (currentState.exportTrade.resourceTypes.IndexOf(resType) != -1)
                        {
                            indexOfLimit = currentState.exportTrade.resourceTypes.IndexOf(resType);

                        }
                        else
                        {
                            Debug.LogWarning("Hatal� index eri�imi -1 olmamal�");
                        }
                    }
                }
               


                float.TryParse(currentState.exportTrade.contractPrices[indexOfLimit].ToString(), out contrackPrice);

                if (contrackPrice >= 0)
                {
                    contrackPriceValueText.text = contrackPrice.ToString();

                }
                else
                {
                    Debug.LogWarning(" conrrat picie d�f�rdan k���k olamaml� ");
                    contrackPriceValueText.text = "0";

                }
            }

        }

    }
    void OnInputValueChanged(string input)
    {




        if (float.TryParse(input, out quantity))
        {


            float amountAvaible = currentState.resourceData[ResourceManager.Instance.curentResource].currentAmount;
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
                    Debug.LogWarning(" dimond de�erii 0 ");

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

        inputField.text = currentState.resourceData[ResourceManager.Instance.curentResource].currentAmount.ToString();
        int amountAvailable;

        if (int.TryParse(inputField.text, out amountAvailable))
        {
            contrackPriceValueText.text = (amountAvailable * contrackPrice).ToString();
        }
        else
        {
            Debug.LogWarning(" amaoutny avaible inte d�n��t��rlemedi");
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
                    
                     int stateFlagIndex = currentState.gameObject.transform.GetSiblingIndex();
                       
                    
                    TradeHistory transaction = new TradeHistory(TradeType.Import, GameDateManager.instance.GetCurrentDataString(),(int) type, quantity, spending, stateFlagIndex);
                    TradeManager.instance.AddTransaction(transaction);
                    buyButton.GetComponent<HideLastPanelButton>().DoHidePanel();
                    Debug.LogWarning($"res sat�n al�nd� quantaty {quantity} harcanan alt�n {spending}");
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
                if (currentState.resourceData[ResourceType.Diamond].currentAmount > Dimond)
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
                  // Debug.LogWarning("state de�i�ti buyda " + stateTransform.name);
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
         //   Debug.LogWarning("bluann state  de�eri " + test);
            rightBox.SetActive(true);
            emtyStateBox.SetActive(false);
        }


    }
    


}
