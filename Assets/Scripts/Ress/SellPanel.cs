using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ResourceManager;
using static RegionClickHandler;
using static Utility;
public class SellPanel : MonoBehaviour
{
    public GameObject rightBox, emtyStateBox;
    public TMP_InputField inputField;
    public Button plusOneKButton, sellButton, macButton;
    public Image resIconImage;
    public TMP_Text amoutAvableValueText, contrackPriceValueText;
    int indexOfLimit;
    float contrackPrice;
    int amountAvailable;
    float quantity;
    Color originalTextColor;
    State currentState;
    State tradeState;
    public int resLimit = 0;


    private void Start()
    {
        inputField.characterLimit = ResourceManager.Instance.InputFieldCaharcterLimit;
        originalTextColor = Color.black;
    }
    private void OnEnable()
    {
        currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        ResourceManager.Instance.OnResourceChanged += OnResourceOrStateChanged;
        ResourceManager.Instance.OnStateToTradeChanged += OnStateChanged;             
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        macButton.onClick.AddListener(MacButtonClicked);
        sellButton.onClick.AddListener(SellButtonClicked);
        Restart();
        SetNewTradeState();
        tradeState = ResourceManager.Instance.CurrentTradeState;
        resLimit = (int)tradeState.importTrade.limit[(int)curentResource - 1];
    }
    private void OnDisable()
    {
        // Olaylardan aboneliði kaldýr
        ResourceManager.Instance.OnResourceChanged -= OnResourceOrStateChanged;
        ResourceManager.Instance.OnStateToTradeChanged -= OnStateChanged;

        inputField.onValueChanged.RemoveListener(OnInputValueChanged);
        macButton.onClick.RemoveListener(MacButtonClicked);
        sellButton.onClick.RemoveListener(SellButtonClicked);
        StopAllCoroutines();
    }

    void Restart()
    {
        inputField.text = "";
    }
    // Olay tetiklendiðinde bu metod çalýþacak
    private void OnResourceOrStateChanged(ResourceType resourceType)
    {
        //ShowPanelInfo();
    }
    private void OnStateChanged(string stateName)
    {
       // ShowPanelInfo();
    }

    void ShowPanelInfo()
    {
    
        if(tradeState != null)
        {
            if (rightBox.gameObject.activeSelf)
            {
                ResourceType resourceType = curentResource;

                resIconImage.sprite = ResSpriteSO.Instance.resIcon[(int)resourceType];

                
                if(tradeState.importTrade.limit[(int)resourceType-1]<=0)
                {
                    SetNewTradeState();
                }
               else
                {
                   
                    contrackPrice = tradeState.importTrade.contractPrices[(int)resourceType - 1];


                    if (contrackPrice > 0)
                    {
                        // Debug.LogWarning("contrat price " + contrackPrice + "curent rade state name " + (ResourceManager.Instance.CurrentTradeState.name));
                        contrackPriceValueText.text = FormatNumber(contrackPrice); 

                    }
                    else
                    {
                        Debug.LogWarning(" conrrat picie sýfýrdan küçük olamamlý ");
                        contrackPriceValueText.text = "0";

                    }
                    StopAllCoroutines();
                    StartCoroutine(UpdateAvableValueText(currentState, resourceType));

                }

            }
            else
                Debug.LogWarning("current trade is null");
        }

    }
    void OnInputValueChanged(string input)
    {

       
        if (float.TryParse(input, out quantity))
        {
           // Debug.LogWarning("input" + quantity);
          


            if (resLimit >= quantity )
            {
                contrackPriceValueText.text = FormatNumber((quantity * contrackPrice));
            }
            else
            {       
                inputField.text = resLimit.ToString();
                contrackPriceValueText.text = FormatNumber((resLimit * contrackPrice));
            }
          //  Debug.LogWarning("reslimit " + resLimit);             
        }
        else
            Debug.LogWarning(" input floata dönüþtürülemedi input: "+input);
    }
    private IEnumerator UpdateAvableValueText(State currentState , ResourceType resType)
    {
        while (rightBox.gameObject.activeSelf)
        {

            amoutAvableValueText.text = FormatNumber(currentState.GetCurrentResValue(resType));
                yield return new WaitForSeconds(1.0f);
        }

    }
    public void MacButtonClicked()
    {
        resLimit =(int) tradeState.importTrade.limit[((int)curentResource) - 1];
        amountAvailable = (int)staticState.resourceData[curentResource].currentAmount;
        quantity = (resLimit > amountAvailable ? amountAvailable : resLimit);
        float spendLimit = tradeState.GetGoldResValue() / tradeState.importTrade.contractPrices[((int)curentResource)-1];
        quantity= quantity>spendLimit? spendLimit : quantity;
            inputField.text = FormatNumber(quantity);    
        
            if(int.TryParse( amoutAvableValueText.text, out amountAvailable))
            {
                contrackPriceValueText.text = (quantity * contrackPrice).ToString("F2");
            }
            else
            {
                Debug.LogWarning(" amaoutny avaible inte dönüþtüürlemedi");
            }
    }
    public void SellButtonClicked()
    {
        ResourceType type = curentResource;
        
        float earing;
        if (float.TryParse(contrackPriceValueText.text, out earing))
        {
            int limit =(int) ResourceManager.Instance.CurrentTradeState.importTrade.limit[(int)type - 1];
            if (quantity>0)
            {

                if (quantity<= currentState.resourceData[type].currentAmount)
                {
                    bool isAllyState = GameManager.AllyStateList.Contains(ResourceManager.Instance.CurrentTradeState);
                    currentState.SellResource(type, quantity, earing,isAllyState);
                    ResourceManager.Instance.CurrentTradeState.BuyyResource(type, quantity, earing);
                    
                    amoutAvableValueText.text = FormatNumber(currentState.GetCurrentResValue(type)  );
                    int stateFlagIndex = currentState.gameObject.transform.GetSiblingIndex();
                    DateTime deliverTime = GameDateManager.instance.GetCurrentDate();                 
                    TradeHistory transaction = new TradeHistory(TradeType.Export, deliverTime, (int)type, quantity, earing, stateFlagIndex, ResourceManager.Instance.CurrentTradeState, (int)(limit - quantity));
                    TradeManager.instance.AddTransaction(transaction);
                    OnInputValueChanged("0");
                    UIManager.Instance.GetComponent<HideLastPanelButton>().DoHidePanel();
                }

            }
            else
                Debug.LogWarning(" earing value 0");
        }
        else
            Debug.LogWarning("earing value can not parse float");

        amoutAvableValueText.text = currentState.GetCurrentResValue(type).ToString("F2");
       

    }   
    public void SetNewTradeState( )
    {

        bool haveAnyTradeState = false; // o ürün için ticaret yapan ülke vars test bitibi true yap
        bool shouldTradeStateChange = true;
        ResourceType curretResType =    ResourceManager.curentResource;
        string newTradeStatename = "";
        tradeState = ResourceManager.Instance.CurrentTradeState;

        foreach (Transform stateTransform in Usa.Instance.transform)
        {
            // index 0 = import index 1 = export
            State newTradeState = stateTransform.GetComponent<State>();
            Trade trade = newTradeState.GetTrade(0, curretResType);
            if (trade != null)
            {
                if (tradeState != null)
                {
                    if (newTradeState == tradeState)
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
                    //Debug.LogWarning("ResourceManager.CurrentTradeState is null");
                }
            }
        }

        if ( haveAnyTradeState==false)
        {
            rightBox.SetActive(false);
            emtyStateBox.SetActive(true);

        }
            else
        {
            if(shouldTradeStateChange)
            {
                ResourceManager.Instance.SetCurrentTradeState(newTradeStatename);

            }else
            {
                ShowPanelInfo();
            }
           // Debug.LogWarning("bluann state  deðeri " + test);
            rightBox.SetActive(true);
            emtyStateBox.SetActive(false);
        }
           
      
    }
   

}
