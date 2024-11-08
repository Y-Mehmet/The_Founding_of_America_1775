using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utility;
using static ResourceManager;
public class MineUpgradePanel : MonoBehaviour
{
    public TMP_Text MineNameText, productionPerDayText, buyButtonText, instantlyButtonText, MineCountText;
    public List<TMP_Text> reqResValueTextList; //  mine i�saas� i�in gerekli madenlerin harcanacak miktar�
    public List<TMP_Text> reqResCurrentAmountValueTextList;
    public List<Image> reqResIconList;
    public Image MineIcon, resIcon;
    public Button BuyButton, InstantlyButton;
    public TMP_InputField inputField;
    public Button macButton, plusButton;
   

    List<int> RequiredResValueList = new List<int>();
    List<ResourceType> RequiredResTypeValueList = new List<ResourceType>();
    State currentState;
    ResourceType currentResType;
    int quantity=0;
    float buyButtonCoinValue=0, instatnlyButtonGemValue = 0;
    Color originalTextColor= Color.white;
    Color colorRed = Color.red;
    float duration = 1;
    private void Start()
    {
        inputField.characterLimit = InputFieldCaharcterLimit;
        duration = GameManager.gameDayTime;
       
    }
    private void OnEnable()
    {      
      
            ShowInfo(ResourceType.Gold);
            MineManager.instance.OnResourceChanged += ShowInfo;

    }
    private void OnDisable()
    {
      
        macButton.onClick.RemoveListener(MacButtonClicked);
        BuyButton.onClick.RemoveListener(BuyButtonClicked);
        InstantlyButton.onClick.RemoveListener(InstantlyButtonClicked);
        MineManager.instance.OnResourceChanged -= ShowInfo;


    }
    void ShowInfo(ResourceType resource)
    {
        currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        currentResType = MineManager.instance.curentResource;

        inputField.onValueChanged.AddListener(OnInputValueChanged);
        macButton.onClick.AddListener(MacButtonClicked);
        BuyButton.onClick.AddListener(BuyButtonClicked);
        InstantlyButton.onClick.AddListener(InstantlyButtonClicked);
        resIcon.sprite = ResSpriteSO.Instance.resIcon[(int)currentResType];
        MineNameText.text = MineManager.instance.GetMineName();
        RequiredResValueList = MineManager.instance.GetReqResValue();
        RequiredResTypeValueList = MineManager.instance.GetReqResType();
        SetImageSprite();
        ResetUI();
        StartCoroutine(CurrentAmountTextUpdate());
    }
   
    void SetImageSprite()
    {
        MineCountText.text = FormatNumber(currentState.resourceData[currentResType].mineCount);
        MineIcon.sprite = ResSpriteSO.Instance.resIcon[(int)currentResType];
        for (int i=0;i<RequiredResTypeValueList.Count;i++)
        {           
            ResourceType resType = RequiredResTypeValueList[i];
            reqResIconList[i].sprite = ResSpriteSO.Instance.resIcon[(int)resType];
           
        }
    }
    void ResetUI()
    {
        instantlyButtonText.text = "0";
        buyButtonText.text = "0";
        buyButtonCoinValue = 0;
        instatnlyButtonGemValue = 0;
        inputField.text = "1";
        OnInputValueChanged("1");
        productionPerDayText.text = FormatNumber((currentState.resourceData[currentResType].productionRate * currentState.resourceData[currentResType].mineCount));
        
    }
    IEnumerator CurrentAmountTextUpdate()
    {
       while(true)
        {
           for (int i = 0; i < RequiredResTypeValueList.Count; i++)
            {
               // curretn amount
                ResourceType resType = RequiredResTypeValueList[i];
                float resCurrentAmountValue = currentState.resourceData[resType].currentAmount;
                reqResCurrentAmountValueTextList[i].text = FormatNumber(resCurrentAmountValue);
                if (RequiredResValueList[i]>quantity* resCurrentAmountValue)
                {
                    reqResCurrentAmountValueTextList[i].color = colorRed;
                }else
                {
                    reqResCurrentAmountValueTextList[i].color = originalTextColor;
                }

            }
            buyButtonText.color = originalTextColor;
            buyButtonText.color = originalTextColor;
            if (currentState.GetGoldResValue()< buyButtonCoinValue)
            {
                buyButtonText.color = colorRed;
            }
           if(  currentState.GetGemResValue()< instatnlyButtonGemValue)
            {
                instantlyButtonText.color = colorRed;
            }
            yield return new WaitForSeconds(duration);
        }
    }
    void OnInputValueChanged(string input)
    {

        if (int.TryParse(input, out quantity))
        {
            if(quantity > 0)
            {
                float  reqResGoldValue = 0;
                for (int i = 0; i < RequiredResTypeValueList.Count; i++)
                {
                    ResourceType resType = RequiredResTypeValueList[i];
                    int reqResCount = RequiredResValueList[i] * quantity;
                    reqResValueTextList[i].text = "- " + FormatNumber(reqResCount);
                    reqResGoldValue += GameEconomy.Instance.GetGoldValue(resType, reqResCount);                  
                                    
                }
                float productionRate = currentState.resourceData[currentResType].productionRate;
                float productPerDayValue = productionRate * quantity;
                float mineCount = currentState.resourceData[currentResType].mineCount;
                float totalProduction = productionRate * mineCount;

                productionPerDayText.text = FormatNumber(totalProduction) + " + ( " + FormatNumber(productPerDayValue) + " )";
                float productPerDayToGoldValue = GameEconomy.Instance.GetGoldValue(currentResType, productPerDayValue);
                buyButtonCoinValue = productPerDayToGoldValue * PayBackValue;

                buyButtonText.text = FormatNumber(buyButtonCoinValue);
                float instatnlyBtnGoldValue = reqResGoldValue + buyButtonCoinValue;
                instatnlyButtonGemValue = GameEconomy.Instance.GetGemValue(instatnlyBtnGoldValue*2);
                instantlyButtonText.text = FormatNumber(instatnlyButtonGemValue);
            }
            else
            {
                // cuantity 0
                inputField.text = "1";
                OnInputValueChanged("1");
            }
           
          
        }
        
           


    }
    public void MacButtonClicked()
    {
        SoundManager.instance.Play("ButtonClick");
       float maxProduction = GetMaxProductionMineCount();

        // maxProduction de�erini kullanarak gerekli i�lemleri yapabilirsiniz.
       
        for (int i = 0; i < reqResValueTextList.Count; i++)
        {

            reqResValueTextList[i].text = FormatNumber((maxProduction * RequiredResValueList[i]));
        }
        inputField.text=maxProduction.ToString();

        


    }
    int GetMaxProductionMineCount()
    {
        float maxProduction = currentState.resourceData[currentResType].maxMineCount; // Ba�lang��ta maksimum �retimi sonsuz olarak ayarla.

        for (int i = 0; i < RequiredResTypeValueList.Count; i++)
        {
            ResourceType resType = RequiredResTypeValueList[i];
            float resCurrentAmountValue = currentState.resourceData[resType].currentAmount;
            float requiredAmount = RequiredResValueList[i];

            // Bu kaynaktan ka� tane �retilebilece�ini hesapla.
            float possibleProduction = resCurrentAmountValue / requiredAmount;

            // Mevcut minimumu alarak t�m kaynaklar i�in en fazla ka� tane �retilebilece�ini bul.
            maxProduction = Mathf.Min(maxProduction, possibleProduction);
        }

        // maxProduction de�eri art�k t�m kaynaklar i�in en fazla �retilebilecek miktar� i�eriyor.
        maxProduction = Mathf.Floor(maxProduction);  // �retim tam say�ya yuvarlan�r.
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

                        // �retim i�in gereken toplam miktar� hesaptan ��kar
                        spending = RequiredResValueList[i] * quantity;
                        spendRes.Add(resType, -spending);

                    }
                    spendRes.Add(ResourceType.Gold, -buyButtonCoinValue);
                    currentState.AddResource(spendRes);
                    SoundManager.instance.Play("Upgrade");
                    
                    currentState.resourceData[currentResType].mineCount += quantity;
                    MineCountText.text = FormatNumber(currentState.resourceData[currentResType].mineCount);
                    inputField.text = "0";

                }else
                {

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
                SoundManager.instance.Play("Upgrade");
                Dictionary<ResourceType, float> spendRes = new Dictionary<ResourceType, float>();
                spendRes.Add(ResourceType.Diamond, -instatnlyButtonGemValue);
                currentState.AddResource(spendRes);
                currentState.resourceData[currentResType].mineCount += quantity;
                MineCountText.text = FormatNumber(currentState.resourceData[currentResType].mineCount);
                inputField.text = "0";
            }
            else
            {
                Debug.LogWarning("Invalid quantity input.");
            }

        }
        
       
    }




}
