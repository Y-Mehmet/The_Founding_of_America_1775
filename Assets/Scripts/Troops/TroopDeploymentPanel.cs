using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TroopyManager;
using static GameManager;
using static MY.NumberUtilitys.Utility;
using static ResourceManager;
public class TroopDeploymentPanel : MonoBehaviour
{
    public TMP_InputField navalInputField, landInputField;
    public Button buyButton, instantlyButton;
    public TMP_Text InstantlyValueText, secondValueText, supplyCostText;

    public int totalCost = 0, totalSecond = 0, totalGemCost = 0;
    public int landCost = 0, navalCost = 0;
    public int landSecond = 0, navalSecond = 0;
    public int navalCount = 0, landCount = 0;

    Color originalTextColor= Color.white;
    Color red = Color.red;
    State currnetState;

    private void Start()
    {
        int charLimit = InputFieldCaharcterLimit;
        navalInputField.characterLimit = charLimit;
        landInputField.characterLimit = charLimit;
       
    }

    private void OnEnable()
    {
        currnetState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        if (AllyStateList != null && currnetState != null)
        {
            if (OriginState == null)
            {
                OriginState = AllyStateList[0];
            }

            if (DestinationState == null)
            {
                DestinationState = AllyStateList[1];

                
            }
            buyButton.onClick.AddListener(BuyButtonOnClicked);
            instantlyButton.onClick.AddListener(InstantlyButtonOnClicked);

            navalInputField.onValueChanged.AddListener(NavalInputValueChanged);
            landInputField.onValueChanged.AddListener(LandValueChanged);

            UIUpdate();
        }
        else
        {
            Debug.LogError("Ally state list is null");
        }
    }

    private void OnDisable()
    {
        resetInput();
        buyButton.onClick.RemoveListener(BuyButtonOnClicked);
        instantlyButton.onClick.RemoveListener(InstantlyButtonOnClicked);
        navalInputField.onValueChanged.RemoveAllListeners();
        landInputField.onValueChanged.RemoveAllListeners();
       


    }
    void resetInput()
    {
        navalInputField.text = "0";
        landInputField.text = "0";
    }

    void UIUpdate()
    {
        totalCost = CalculateSupplyCostValue();
        totalSecond = CalculateSecondValue();
        totalGemCost = CalculateInstanlyValue();

        secondValueText.text = FormatNumber(totalSecond);
        InstantlyValueText.text = FormatNumber(totalGemCost);
        supplyCostText.text = FormatNumber(totalCost);

        // UI renkleri ayarlama
        InstantlyValueText.color = (totalGemCost > currnetState.GetGemResValue()) ? red : originalTextColor;
        supplyCostText.color = (totalCost > currnetState.GetGoldResValue()) ? red : originalTextColor;
    }

    void NavalInputValueChanged(string input)
    {
        if (int.TryParse(input, out navalCount))
        {
            int navalArmySize = OriginState.GetNavalArmySize();
            int quata = DestinationState.GetSoliderQuota() - landCount;
            quata = quata > 0 ? quata : 0;
            int limit = quata > navalArmySize ? navalArmySize : quata;


            if (navalCount> limit)
            {
                navalInputField.text = limit.ToString();
            }
           
            navalCost = GetSupplyCost(navalCount);
            navalSecond = GetSecondValue(navalCount);
        }
        else
        {
            Debug.LogWarning("Input is not a digit");
            navalInputField.text = "0";
        }

        UIUpdate();
    }

    void LandValueChanged(string input)
    {
        if (int.TryParse(input, out landCount))
        {
            int landArmySize = OriginState.GetLandArmySize();
            int quata= DestinationState.GetSoliderQuota()-navalCount;
            quata = quata > 0 ? quata : 0;
           int limit= quata>landArmySize?landArmySize:quata;
            if (landCount> limit)
            {
                landInputField.text = limit.ToString();
            }
            
            landCost = GetSupplyCost(landCount);
            landSecond = GetSecondValue(landCount);
        }
        else
        {
            Debug.LogWarning("Input is not a digit");
            landInputField.text = "0";
        }

        UIUpdate();
    }

    int CalculateInstanlyValue()
    {
        totalCost = CalculateSupplyCostValue();
        totalSecond = CalculateSecondValue();
        totalGemCost = GetDimondValue(totalCost, totalSecond);
        return totalGemCost/10;
    }

    int CalculateSupplyCostValue()
    {
        totalCost = landCost + navalCost;
        return totalCost;
    }

    int CalculateSecondValue()
    {
        totalSecond = landSecond + navalSecond;
        return totalSecond;
    }

    void BuyButtonOnClicked()
    {
        if (currnetState.GetGoldResValue() >= totalCost)
        {
            currnetState.GoldSpend(totalCost);
            SoundManager.instance.Play("Coins");
            TroopyManager.Instance.SendTropy(totalSecond, navalCount, landCount);
            Debug.Log($"Asker gönderildi: Deniz {navalCount}, Kara {landCount}");
        }
        else
        {
            Debug.LogWarning("Yeterli altýn yok");
        }
        resetInput();
    }

    void InstantlyButtonOnClicked()
    {
        if (currnetState.GetGemResValue() >= totalGemCost)
        {
            currnetState.GemSpend(totalGemCost);
            SoundManager.instance.Play("Gem");
            TroopyManager.Instance.SendTropy(0, navalCount, landCount);
            Debug.Log($"Asker gönderildi: Deniz {navalCount}, Kara {landCount}");
        }
        else
        {
            Debug.LogWarning("Yeterli elmas yok");
        }
        resetInput();
    }
}
