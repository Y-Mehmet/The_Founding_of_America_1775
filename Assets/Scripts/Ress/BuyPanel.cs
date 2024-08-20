using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    public GameObject rightBox, emtyStateBox;
    public TMP_InputField inputField;
    public Button  buyButton, instantlyButton, macButton;
    
    public TMP_Text contrackPriceValueText, InstantlyValueText;
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
        ResourceManager.Instance.OnStateToTradeChanged += OnResourceOrStateChanged;
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        macButton.onClick.AddListener(MacButtonClicked);
        buyButton.onClick.AddListener(BuyButtonClicked);
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
        ResourceManager.Instance.OnStateToTradeChanged -= OnResourceOrStateChanged;
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

        if (rightBox.gameObject.activeSelf)
        {
            ResourceType resourceType = ResourceManager.Instance.curentResource;


             currentState = Usa.Instance.FindStateByName(ResourceManager.Instance.curentTradeStateName).GetComponent<State>();

            if ( currentState!= null)
            {
                foreach (var resType in currentState.importTrade.resourceTypes)
                {
                    if (resType == resourceType)
                    {
                        if (currentState.importTrade.resourceTypes.IndexOf(resType) != -1)
                        {
                            indexOfLimit = currentState.importTrade.resourceTypes.IndexOf(resType);

                        }
                        else
                        {
                            Debug.LogWarning("Hatalý index eriþimi -1 olmamalý");
                        }
                    }
                }
               


                float.TryParse(currentState.importTrade.contractPrices[indexOfLimit].ToString(), out contrackPrice);

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

    }
    void OnInputValueChanged(string input)
    {




        if (float.TryParse(input, out quantity))
        {


            float amountAvaible = currentState.resourceData[ResourceManager.Instance.curentResource].currentAmount;


            if (amountAvaible >= quantity)
            {
                contrackPriceValueText.color = originalTextColor;
                contrackPriceValueText.text = (quantity * contrackPrice).ToString();
            }
            else
            {
                contrackPriceValueText.color = Color.red;
                contrackPriceValueText.text = (quantity * contrackPrice).ToString();

            }
            float spending;
            if (float.TryParse(contrackPriceValueText.text, out spending))
            {
                float Dimond = (float)Math.Ceiling((spending / ResourceManager.Instance.DimondRate));

                if (Dimond >0)
                {
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
            Debug.LogWarning(" deðer yalýþ");


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
               if (currentState.resourceData[ResourceType.Gold].currentAmount > spending)
                {
                    currentState.BuyyResource(type, quantity, spending);
                    buyButton.GetComponent<HideLastPanelButton>().DoHidePanel();
                }
               else
                {
                    Debug.Log(" gold dont eneaugh for but resource");
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
            Trade trade = stateTransform.GetComponent<State>().GetTrade(0, curretResType);
            if (trade != null)
            {
                ResourceManager.Instance.SetCurrentTradeState(stateTransform.name);
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
            rightBox.SetActive(true);
            emtyStateBox.SetActive(false);
        }


    }
    public void InstantlyButtonClicked()
    {
        ResourceType type = ResourceManager.Instance.curentResource;
        float spending;
        if (float.TryParse(contrackPriceValueText.text, out spending))
        {
            float Dimond = (float) Math.Ceiling((spending / ResourceManager.Instance.DimondRate));
                
            if (spending > 0)
            {
                if (currentState.resourceData[ResourceType.Gold].currentAmount > spending)
                {
                    RegionClickHandler.Instance.currentState.GetComponent<State>().InstantlyResource(type, quantity, Dimond);
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


}
