using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SellPanel : MonoBehaviour
{
    public GameObject rightBox, emtyStateBox;
    public TMP_InputField inputField;
    public Button plusOneKButton, sellButton, cancelButton, macButton;
    public Image resIconImage;
    public TMP_Text amoutAvableValueText, contrackPriceValueText;
    int indexOfLimit;
    float contrackPrice;
    int amountAvailable;
    float quantity;
    Color originalTextColor;
    State currentState;
    
    private void Start()
    {
        inputField.characterLimit = ResourceManager.Instance.InputFieldCaharcterLimit;
    }
    private void OnEnable()
    {
        
       
            SetNewTradeState();
       
        
         currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        // Olaylara abone ol
        ResourceManager.Instance.OnResourceChanged += OnResourceOrStateChanged;
        ResourceManager.Instance.OnStateToTradeChanged += OnStateChanged;
        
        
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        macButton.onClick.AddListener(MacButtonClicked);
        sellButton.onClick.AddListener(SellButtonClicked);

        // Panel bilgisini göster
        ShowPanelInfo();
        originalTextColor = amoutAvableValueText.color;
        Restart();

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


        ShowPanelInfo();

    }
    private void OnStateChanged(string stateName)
    {
        ShowPanelInfo();
    }



    void ShowPanelInfo()
    {

        
        if(ResourceManager.CurrentTradeState != null)
        {
            if (rightBox.gameObject.activeSelf)
            {
                ResourceType resourceType = ResourceManager.Instance.curentResource;

                resIconImage.sprite = ResSpriteSO.Instance.resIcon[(int)resourceType];


                foreach (var resType in (ResourceManager.CurrentTradeState.importTrade.resourceTypes))
                {
                    if (resType == resourceType)
                    {
                        if ((ResourceManager.CurrentTradeState.importTrade.resourceTypes.IndexOf(resType) != -1))
                        {
                            indexOfLimit = (ResourceManager.CurrentTradeState.importTrade.resourceTypes.IndexOf(resType));

                        }
                        else
                        {
                            Debug.LogWarning("Hatalý index eriþimi -1 olmamalý");
                        }
                    }
                }
                StopAllCoroutines();
                StartCoroutine(UpdateAvableValueText(currentState, resourceType));


                float.TryParse(ResourceManager.CurrentTradeState.importTrade.contractPrices[indexOfLimit].ToString(), out contrackPrice);

                if (contrackPrice > 0)
                {
                    Debug.LogWarning("contrat price " + contrackPrice + "curent rade state name " + (ResourceManager.CurrentTradeState.name));
                    contrackPriceValueText.text = contrackPrice.ToString();

                }
                else
                {
                    Debug.LogWarning(" conrrat picie sýfýrdan küçük olamamlý ");
                    contrackPriceValueText.text = "0";

                }

            }
            else
                Debug.LogWarning("current trade is null");
        }

    }
    void OnInputValueChanged(string input)
    {

        float resLimit=0;
        if (float.TryParse(input, out quantity))
        {
           // Debug.LogWarning("input" + quantity);

            State tradeState = Usa.Instance.FindStateByName(ResourceManager.Instance.curentTradeStateName);
            for (int i = 0; i < tradeState.importTrade.resourceTypes.Count; i++)
            {
                if (ResourceManager.Instance.curentResource == tradeState.importTrade.resourceTypes[i])
                {
                    resLimit = tradeState.importTrade.limit[i]; 
                  //  Debug.LogWarning($" current trade state name {tradeState.name} res type {ResourceManager.Instance.curentResource} res limit {resLimit}");
                }
            }





            if (resLimit >= quantity )
                {
                contrackPriceValueText.text = (quantity * contrackPrice).ToString();
                }
                else
                {
                inputField.text = resLimit.ToString();
                contrackPriceValueText.text = (resLimit * contrackPrice).ToString();

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
            
            amoutAvableValueText.text = currentState.resourceData[resType].currentAmount.ToString();
            yield return new WaitForSeconds(1.0f);
        }

    }
    public void MacButtonClicked()
    {
      
        inputField.text = amountAvailable.ToString();
     
        
            if(int.TryParse( amoutAvableValueText.text, out amountAvailable))
            {
                contrackPriceValueText.text = (amountAvailable * contrackPrice).ToString();
            }
            else
            {
                Debug.LogWarning(" amaoutny avaible inte dönüþtüürlemedi");
            }
           
        
       
        

    }
    public void SellButtonClicked()
    {
        ResourceType type = ResourceManager.Instance.curentResource;
        
        float earing;
        if (float.TryParse(contrackPriceValueText.text, out earing))
        {
            if (earing > 0)
            {

                if (quantity<= currentState.resourceData[type].currentAmount)
                {
                    this.currentState.SellResource(type, quantity, earing);
                    ResourceManager.CurrentTradeState.BuyyResource(type, quantity, earing);

                    amoutAvableValueText.text = this.currentState.resourceData[type].currentAmount.ToString();



                    int stateFlagIndex = currentState.gameObject.transform.GetSiblingIndex();

                    TradeHistory transaction = new TradeHistory(TradeType.Export, GameDateManager.instance.GetCurrentDataString(), (int)type, quantity, earing, stateFlagIndex);

                    TradeManager.instance.AddTransaction(transaction);
                    OnInputValueChanged("0");
                    sellButton.GetComponent<HideLastPanelButton>().DoHidePanel();
                }


            }
            else
                Debug.LogWarning(" earing value 0");
        }
        else
            Debug.LogWarning("earing value can not parse float");

        //OnInputValueChanged("0");
        amoutAvableValueText.text = currentState.resourceData[type].currentAmount.ToString();
        State tradeState = Usa.Instance.FindStateByName(ResourceManager.Instance.curentTradeStateName);
        if (tradeState!= null)
        {
            foreach (var resType in tradeState.importTrade.resourceTypes)
            {
                if(resType==type)
                {

                }
            }
        }

    }
    
    public void SetNewTradeState( )
    {

        bool test = false; // o ürün için ticaret yapan ülke vars test bitibi true yap
        bool shouldTradeStateChange = true;
        ResourceType curretResType = ResourceManager.Instance.curentResource;
        string newTradeStatename = "";
       
            foreach (Transform stateTransform in Usa.Instance.transform)
            {
            // index 0 = import index 1 = export
                State newTradeState=stateTransform.GetComponent<State>();
                Trade trade = newTradeState.GetTrade(0, curretResType);
                if (trade != null)
                    {
                        if(newTradeState== ResourceManager.CurrentTradeState)
                            {
                                 test = true;
                                 shouldTradeStateChange = false;
                    Debug.LogWarning("eski stateden devam");
                                break;
                            }
                        if( test==false)
                            {
                                //Debug.LogWarning("state deðiþti: selde  " + stateTransform.name);
                           
                                newTradeStatename = newTradeState.name;
                                test = true;
                            
                            }
                
                       
                
                   
                    }
                
                

            }
            
            if( test==false)
        {
            rightBox.SetActive(false);
            emtyStateBox.SetActive(true);

        }
            else
        {
            if(shouldTradeStateChange)
            {
                ResourceManager.Instance.SetCurrentTradeState(newTradeStatename);
            }
           // Debug.LogWarning("bluann state  deðeri " + test);
            rightBox.SetActive(true);
            emtyStateBox.SetActive(false);
        }
           
      
    }
   

}
