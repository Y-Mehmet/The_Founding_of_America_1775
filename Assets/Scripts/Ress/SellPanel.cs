using System.Collections;
using System.Collections.Generic;
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
    GameObject currentStateGameObjcet;
    private void Start()
    {
        inputField.characterLimit = ResourceManager.Instance.InputFieldCaharcterLimit;
    }
    private void OnEnable()
    {
       
        SetNewTradeState();
         currentStateGameObjcet = RegionClickHandler.Instance.currentState;
        // Olaylara abone ol
        ResourceManager.Instance.OnResourceChanged += OnResourceOrStateChanged;
       // ResourceManager.Instance.OnStateToTradeChanged += OnResourceOrStateChanged;
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
        //ResourceManager.Instance.OnStateToTradeChanged -= OnResourceOrStateChanged;
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

    private void OnResourceOrStateChanged(string stateName)
    {
        ShowPanelInfo();
    }

    void ShowPanelInfo()
    {
        
       if(rightBox.gameObject.activeSelf)
        {
            ResourceType resourceType = ResourceManager.Instance.curentResource;

            resIconImage.sprite = ResSpriteSO.Instance.resIcon[(int)resourceType];
            State currentState = currentStateGameObjcet.GetComponent<State>();

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
                        Debug.LogWarning("Hatalý index eriþimi -1 olmamalý");
                    }
                }
            }
            StartCoroutine(UpdateAvableValueText(currentState, resourceType));


            float.TryParse(currentState.exportTrade.contractPrices[indexOfLimit].ToString(), out contrackPrice);

            if (contrackPrice >= 0)
            {
                contrackPriceValueText.text = contrackPrice.ToString();

            }
            else
            {
                Debug.LogWarning(" conrrat picie dýfýrdan küçük olamamlý ");
                contrackPriceValueText.text = "0";

            }

        }

    }
    void OnInputValueChanged(string input)
    {



        float resLimit=0;
        if (float.TryParse(input, out quantity))
        {

            State tradeState = Usa.Instance.FindStateByName(ResourceManager.Instance.curentTradeStateName);
            for (int i = 0; i < tradeState.importTrade.resourceTypes.Count; i++)
            {
                if (ResourceManager.Instance.curentResource == tradeState.importTrade.resourceTypes[i])
                {
                    resLimit = tradeState.importTrade.limit[i]; 
                    Debug.LogWarning($" current trade state name {tradeState.name} res type {ResourceManager.Instance.curentResource} res limit {resLimit}");
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
            Debug.LogWarning("reslimit " + resLimit);
            

           

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
               
                currentStateGameObjcet.GetComponent<State>().SellResource(type, quantity, earing);
              //  OnInputValueChanged("0");
                amoutAvableValueText.text= currentStateGameObjcet.GetComponent<State>().resourceData[type].currentAmount.ToString();
            }
               
            else
                Debug.LogWarning(" earing value 0");
        }
        else
            Debug.LogWarning("earing value can not parse float");

        OnInputValueChanged("0");
        amoutAvableValueText.text = currentStateGameObjcet.GetComponent<State>().resourceData[type].currentAmount.ToString();
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

        int test = 0;
        ResourceType curretResType = ResourceManager.Instance.curentResource;
       
            foreach (Transform stateTransform in Usa.Instance.transform)
            {
                Trade trade = stateTransform.GetComponent<State>().GetTrade(1, curretResType);
                if (trade != null)
                {if( test==0)
                {
                   // Debug.LogWarning("state deðiþti: selde  " + stateTransform.name);
                    ResourceManager.Instance.SetCurrentTradeState(stateTransform.name);
                }
                
                   test++;
                   
                }
                

            }
            if( test==0)
        {
            rightBox.SetActive(false);
            emtyStateBox.SetActive(true);

        }
            else
        {
            rightBox.SetActive(true);
            emtyStateBox.SetActive(false);
        }
           
      
    }
   

}
