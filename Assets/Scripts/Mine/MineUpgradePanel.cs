using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MineUpgradePanel : MonoBehaviour
{
    public TMP_Text MineNameText, productionPerDayText, buyButtonText, instantlyButtonText, MineCountText;
    public List<TMP_Text> reqResValueTextList; //  mine iþsaasý için gerekli madenlerin harcanacak miktarý
    public List<TMP_Text> reqResCurrentAmountValueTextList;
    public List<Image> reqResIconList;
    public Image MineIcon, resIcon;
    public Button BuyButton, InstantlyButton;
    public TMP_InputField inputField;
    public Button macButton, plusButton;
    float goldSpending = 0;

    List<int> RequiredResValueList = new List<int>();
    List<ResourceType> RequiredResTypeValueList = new List<ResourceType>();
    State currentState;
    ResourceType currentResType;
    int quantity=0;
    float buyButtonCoinValue=0, instatnlyButtonGemValue = 0;
    Color originalTextColor;
    
    private void Start()
    {
        inputField.characterLimit = ResourceManager.Instance.InputFieldCaharcterLimit;
        originalTextColor= reqResValueTextList[0].color;
        
    }
    private void OnEnable()
    {
        ResetUI();
        currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        currentResType = MineManager.instance.curentResource;
        
        
        MineManager.instance.OnResourceChanged += OnResourceTypeChanged;
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        macButton.onClick.AddListener(MacButtonClicked);
        BuyButton.onClick.AddListener(BuyButtonClicked);
        InstantlyButton.onClick.AddListener(InstantlyButtonClicked);


        ShowPanelInfo();
    }
    private void OnDisable()
    {
        MineManager.instance.OnResourceChanged -= OnResourceTypeChanged;
        macButton.onClick.RemoveListener(MacButtonClicked);
        BuyButton.onClick.RemoveListener(BuyButtonClicked);
        InstantlyButton.onClick.RemoveListener(InstantlyButtonClicked);


    }
    void OnResourceTypeChanged(ResourceType resType)
    {
        currentResType = resType;
        ShowPanelInfo();
    }
    void ShowPanelInfo()
    {
        MineCountText.text= currentState.resourceData[currentResType].mineCount.ToString();
        resIcon.sprite = ResSpriteSO.Instance.resIcon[(int)currentResType];
        MineNameText.text= MineManager.instance.GetMineName();
        RequiredResValueList = MineManager.instance.GetReqResValue();
        RequiredResTypeValueList = MineManager.instance.GetReqResType();
        

        for (int i=0;i<RequiredResTypeValueList.Count;i++)
        {
            // spend value 
            reqResValueTextList[i].text ="- "+ RequiredResValueList[i].ToString();

            // curretn amount
            ResourceType resType = RequiredResTypeValueList[i];
             float resCurrentAmountValue = currentState.resourceData[resType].currentAmount;
            reqResCurrentAmountValueTextList[i].text= resCurrentAmountValue.ToString();

            // res icon
            reqResIconList[i].sprite = ResSpriteSO.Instance.resIcon[(int)resType];


        }
        MineIcon.sprite = MineIConSO.Instance.mineIconSpriteList[(int)currentResType];
        StartCoroutine(CurrentAmountTextUpdate());  

    }
    void ResetUI()
    {
        inputField.text = "0";
        OnInputValueChanged("0");
        productionPerDayText.text = "0";
        instantlyButtonText.text = "0";
        buyButtonText.text= "0";
        buyButtonCoinValue = 0;
        instatnlyButtonGemValue = 0;
    }
    IEnumerator CurrentAmountTextUpdate()
    {
       while(true)
        {
            float duration = GameManager.gameDayTime;
            yield return new WaitForSeconds(duration);



            for (int i = 0; i < RequiredResTypeValueList.Count; i++)
            {


                // curretn amount
                ResourceType resType = RequiredResTypeValueList[i];
                float resCurrentAmountValue = currentState.resourceData[resType].currentAmount;
                reqResCurrentAmountValueTextList[i].text = resCurrentAmountValue.ToString();




            }
        }
    }
    void OnInputValueChanged(string input)
    {




        if (int.TryParse(input, out quantity))
        {

            for (int i = 0; i < RequiredResTypeValueList.Count; i++)
            {
              
                ResourceType resType = RequiredResTypeValueList[i];
                float resCurrentAmountValue = currentState.resourceData[resType].currentAmount;
                

               
                if( quantity* RequiredResValueList[i]> resCurrentAmountValue)
                {
                    

                    reqResValueTextList[i].text = "- " + (RequiredResValueList[i] * quantity);
                    reqResValueTextList[i].color = Color.red;
                }
                else
                {
                    if (GameEconomy.Instance == null)
                        Debug.LogWarning("game oeconomy yok");
                        reqResValueTextList[i].text = "- " + (RequiredResValueList[i] * quantity);
                    float productPerDayValue = (currentState.resourceData[currentResType].productionRate * quantity);
                    productionPerDayText.text = (currentState.resourceData[currentResType].productionRate * currentState.resourceData[currentResType].mineCount)+" + ( " + productPerDayValue.ToString()+" )";
                    float productPerDayToGoldValue = GameEconomy.Instance.GetGoldValue(currentResType, productPerDayValue);
                    buyButtonCoinValue = productPerDayToGoldValue * GameEconomy.Instance.PayBackValue;

                    buyButtonText.text = buyButtonCoinValue.ToString();
                    instatnlyButtonGemValue = GameEconomy.Instance.GetGemValue(buyButtonCoinValue);
                    instantlyButtonText.text = instatnlyButtonGemValue.ToString();
                    if (currentState.resourceData[ResourceType.Diamond].currentAmount> instatnlyButtonGemValue)
                    {
                        
                        instantlyButtonText.color = originalTextColor;
                    }
                    else
                    {
                        
                        instantlyButtonText.color = Color.red;

                    }
                    if (currentState.resourceData[ResourceType.Gold].currentAmount > buyButtonCoinValue)
                    {

                        buyButtonText.color = originalTextColor;
                    }
                    else
                    {

                        buyButtonText.color = Color.red;

                    }

                    reqResValueTextList[i].color = originalTextColor;
                }

            }
          
        }
        else
            Debug.LogWarning(" deðer yalýþ "+ input);


    }
    public void MacButtonClicked()
    {
       float maxProduction = GetMaxProductionMineCount();

        // maxProduction deðerini kullanarak gerekli iþlemleri yapabilirsiniz.
       
        for (int i = 0; i < reqResValueTextList.Count; i++)
        {

            reqResValueTextList[i].text = (maxProduction * RequiredResValueList[i]).ToString();
        }
        inputField.text=maxProduction.ToString();

        


    }
    int GetMaxProductionMineCount()
    {
        float maxProduction = float.MaxValue;  // Baþlangýçta maksimum üretimi sonsuz olarak ayarla.

        for (int i = 0; i < RequiredResTypeValueList.Count; i++)
        {
            ResourceType resType = RequiredResTypeValueList[i];
            float resCurrentAmountValue = currentState.resourceData[resType].currentAmount;
            float requiredAmount = RequiredResValueList[i];

            // Bu kaynaktan kaç tane üretilebileceðini hesapla.
            float possibleProduction = resCurrentAmountValue / requiredAmount;

            // Mevcut minimumu alarak tüm kaynaklar için en fazla kaç tane üretilebileceðini bul.
            maxProduction = Mathf.Min(maxProduction, possibleProduction);
        }

        // maxProduction deðeri artýk tüm kaynaklar için en fazla üretilebilecek miktarý içeriyor.
        maxProduction = Mathf.Floor(maxProduction);  // Üretim tam sayýya yuvarlanýr.
        return (int) maxProduction;
    }



    public void BuyButtonClicked()
    {
        float spending = 0;
        
        int maxProduction = GetMaxProductionMineCount();
        if (currentState.resourceData[ResourceType.Gold].currentAmount > buyButtonCoinValue)
        {
            if (int.TryParse(inputField.text, out quantity))
            {
                // Debug.LogWarning($"cuantati {quantity} maxxxxx" + maxProduction);
                if (quantity <= maxProduction && quantity>0)
                {
                    Dictionary<ResourceType, float> spendRes = new Dictionary<ResourceType, float>();
                    for (int i = 0; i < RequiredResTypeValueList.Count; i++)
                    {
                        ResourceType resType = RequiredResTypeValueList[i];
                        float resCurrentAmountValue = currentState.resourceData[resType].currentAmount;

                        // Üretim için gereken toplam miktarý hesaptan çýkar
                        spending = RequiredResValueList[i] * quantity;
                        spendRes.Add(resType, -spending);

                    }
                    spendRes.Add(ResourceType.Gold, -buyButtonCoinValue);
                    currentState.AddResource(spendRes);
                    
                    currentState.resourceData[currentResType].mineCount += quantity;
                    MineCountText.text = currentState.resourceData[currentResType].mineCount.ToString();
                    inputField.text = "0";

                }
            }
        }
            
      
        

    }
    public void InstantlyButtonClicked()
    {

        if (currentState.resourceData[ResourceType.Diamond].currentAmount> instatnlyButtonGemValue)
        {
            if (int.TryParse(inputField.text, out quantity))
            {
                Dictionary<ResourceType, float> spendRes = new Dictionary<ResourceType, float>();
                spendRes.Add(ResourceType.Diamond, -instatnlyButtonGemValue);
                currentState.AddResource(spendRes);
                currentState.resourceData[currentResType].mineCount += quantity;
                MineCountText.text = currentState.resourceData[currentResType].mineCount.ToString();
                inputField.text = "0";
            }
            else
            {
                Debug.LogWarning("Invalid quantity input.");
            }

        }
        
       
    }




}
