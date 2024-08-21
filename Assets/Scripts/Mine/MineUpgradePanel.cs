using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MineUpgradePanel : MonoBehaviour
{
    public TMP_Text MineNameText, BuyCoinValueText, InstatnlyDimondValueText;
    public List<TMP_Text> reqResValueTextList; //  mine iþsaasý için gerekli madenlerin harcanacak miktarý
    public List<TMP_Text> reqResCurrentAmountValueTextList;
    public List<Image> reqResIconList;
    public Image MineIcon;
    public Button BuyButton, InstantlyButton;
    public TMP_InputField inputField;
    public Button macButton, plusButton;

    List<int> RequiredResValueList = new List<int>();
    List<ResourceType> RequiredResTypeValueList = new List<ResourceType>();
    State currentState;
    ResourceType currentResType;
    float quantity;
    Color originalTextColor;
    
    private void Start()
    {
        inputField.characterLimit = ResourceManager.Instance.InputFieldCaharcterLimit;
        originalTextColor= reqResValueTextList[0].color;
    }
    private void OnEnable()
    {
        currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        currentResType = MineManager.instance.curentResource;
        RequiredResValueList = MineManager.instance.GetReqResValue();
        RequiredResTypeValueList = MineManager.instance.GetReqResType();
        MineManager.instance.OnResourceChanged += OnResourceTypeChanged;
        inputField.onValueChanged.AddListener(OnInputValueChanged);
        macButton.onClick.AddListener(MacButtonClicked);
        
        ShowPanelInfo();
    }
    private void OnDisable()
    {
        MineManager.instance.OnResourceChanged -= OnResourceTypeChanged;
        ResetUI();

    }
    void OnResourceTypeChanged(ResourceType resType)
    {
        ShowPanelInfo();
    }
    void ShowPanelInfo()
    {
        MineNameText.text= MineManager.instance.GetMineName();
        
       
        for(int i=0;i<RequiredResTypeValueList.Count;i++)
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
        inputField.text = "";
    }
    IEnumerator CurrentAmountTextUpdate()
    {
       while(true)
        {
            float duration = GameManager.Instance.gameDayTime;
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




        if (float.TryParse(input, out quantity))
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
                    reqResValueTextList[i].text = "- " + (RequiredResValueList[i] * quantity);
                    reqResValueTextList[i].color = originalTextColor;
                }

            }
          
        }
        else
            Debug.LogWarning(" deðer yalýþ");


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
        if(quantity<maxProduction)
        {
            Dictionary<ResourceType, float> spendRes = new Dictionary<ResourceType, float>();
            for (int i = 0; i < RequiredResTypeValueList.Count; i++)
            {
                ResourceType resType = RequiredResTypeValueList[i];
                float resCurrentAmountValue = currentState.resourceData[resType].currentAmount;

                // Üretim için gereken toplam miktarý hesaptan çýkar
                spending = RequiredResValueList[i] * quantity * -1;
                spendRes.Add(resType, spending);

            }
            currentState.AddResource(spendRes);
        }

    }
    //public void InstantlyButtonClicked()
    //{
    //    ResourceType type = ResourceManager.Instance.curentResource;
    //    float spending;
    //    if (float.TryParse(contrackPriceValueText.text, out spending))
    //    {
    //        float Dimond = (float)Math.Ceiling((spending / ResourceManager.Instance.DimondRate));

    //        if (spending > 0)
    //        {
    //            if (currentState.resourceData[ResourceType.Gold].currentAmount > spending)
    //            {
    //                RegionClickHandler.Instance.currentState.GetComponent<State>().InstantlyResource(type, quantity, Dimond);
    //            }
    //            else
    //            {
    //                Debug.Log("dimond not enaugh for resource");
    //            }
    //        }

    //        else
    //            Debug.LogWarning(" spending value 0");
    //    }
    //    else
    //        Debug.LogWarning("spending value can not parse float");

    //}



}
