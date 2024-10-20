using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utility;
public class TroopyPanel : MonoBehaviour
{
    public TMP_Text ArmyBarrackSizeText, NavalSizeText, LandSizeText, LeaderText, UnitLandPowerText,
        UnitNavalPowerText, ArmyMoraleText, TotalArmyPowerText, BarrackInstatlyBtnValueText, BarrakBuyBtnValueText, LandInstatlyBtnValueText, LandBuyBtnValueText, NavalInstatlyBtnValueText, NavalBuyBtnValueText;
    public Button BarrackInstalyBtn, BarrackBuyBtn, BarrackMacBtn,
        NavalInstalyBtn, NavalBuyBtn, NavalMacBtn, LandInstalyBtn, LandBuyBtn, LandMacBtn;
    public TMP_InputField BarrackInputField, NavalInputField, LandInputField;
    State currentState;
    int BarrackCost = 100, NavalSoliderCost = 1, LandSoliderCost = 1;
    Color originalTextColor;
    Color redColor = Color.red;

    private void OnEnable()
    {
        currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        if (currentState != null)
        {
            originalTextColor = BarrackInstatlyBtnValueText.color;
            

            // Dinleyicileri ekliyoruz
            BarrackInputField.onValueChanged.AddListener(OnBarrackInputChanged);
            NavalInputField.onValueChanged.AddListener(OnNavalInputChanged);
            LandInputField.onValueChanged.AddListener(OnLandInputChanged);

            BarrackInstalyBtn.onClick.AddListener(() => IncreaseBarrackSize(ResourceType.Diamond));
            BarrackBuyBtn.onClick.AddListener(() => IncreaseBarrackSize(ResourceType.Gold));
            NavalInstalyBtn.onClick.AddListener(() => IncreaseNavalSize(ResourceType.Diamond));
            NavalBuyBtn.onClick.AddListener(() => IncreaseNavalSize(ResourceType.Gold));
            LandInstalyBtn.onClick.AddListener(() => IncreaseLandSize(ResourceType.Diamond));
            LandBuyBtn.onClick.AddListener(() => IncreaseLandSize(ResourceType.Gold));
            BarrackMacBtn.onClick.AddListener(OnBarrakMacClicked);
            LandMacBtn.onClick.AddListener(OnLandMacClicked);
            NavalMacBtn.onClick.AddListener(OnNavalMacClicked);
            float repeatRate = GameManager.gameDayTime;
            BarrackColorManager();
            LandColorManager();
            NavalColorManager();
            InvokeRepeating("UpdateUI", 0, repeatRate);
        }
        else
        {
            Debug.LogWarning("Current state is null");
        }
    }

    private void OnDisable()
    {
        // Input field dinleyicilerini kaldýrýyoruz
        BarrackInputField.onValueChanged.RemoveListener(OnBarrackInputChanged);
        BarrackInputField.text = "0";
        
        NavalInputField.onValueChanged.RemoveListener(OnNavalInputChanged);
        NavalInputField.text = "0";

        LandInputField.onValueChanged.RemoveListener(OnLandInputChanged);
        NavalInputField.text = "0";

        // Butonlarýn onClick dinleyicilerini kaldýrýyoruz
        BarrackInstalyBtn.onClick.RemoveAllListeners();
        BarrackBuyBtn.onClick.RemoveAllListeners();
        NavalInstalyBtn.onClick.RemoveAllListeners();
        NavalBuyBtn.onClick.RemoveAllListeners();
        LandInstalyBtn.onClick.RemoveAllListeners();
        LandBuyBtn.onClick.RemoveAllListeners();
        BarrackMacBtn.onClick.RemoveListener(OnBarrakMacClicked);
        LandMacBtn.onClick.RemoveListener(OnLandMacClicked);
        NavalMacBtn.onClick.RemoveListener(OnNavalMacClicked);
        BarrackInstatlyBtnValueText.color = originalTextColor;
        CancelInvoke("UpdateUI");
    }


    // UI'yi güncelleyen metod
    private void UpdateUI()
    {
        ArmyBarrackSizeText.text = FormatNumber(currentState.GetArmyBarrackSize());
        NavalSizeText.text = FormatNumber(currentState.GetNavalArmySize());
        LandSizeText.text = FormatNumber(currentState.GetLandArmySize());
        if (GeneralManager.stateGenerals.ContainsKey(currentState))
            LeaderText.text = GeneralManager.stateGenerals[currentState].Name.ToString();
        else
            LeaderText.text = "Leaderless";
        UnitLandPowerText.text = currentState.GetUnitLandRate().ToString();
        UnitNavalPowerText.text = currentState.GetUnitNavalRate().ToString();
        ArmyMoraleText.text = currentState.GetMorale().ToString();
        TotalArmyPowerText.text = FormatNumber(currentState.GetTotalArmyPower());
        
    }
    void BarrackColorManager()
    {
        int cost = 0,gemCost=0;

        if(int.TryParse(BarrackInputField.text, out var value)) {
            cost = value * BarrackCost;
            gemCost = GameEconomy.Instance.GetGemValue(cost);
        }
        if (currentState.resourceData[ResourceType.Gold].currentAmount < cost)
        {
            BarrakBuyBtnValueText.color = redColor;
        }
        else
        {
            BarrakBuyBtnValueText.color = originalTextColor;
        }
        if (currentState.resourceData[ResourceType.Diamond].currentAmount < gemCost)
        {
            BarrackInstatlyBtnValueText.color = redColor;
        }
        else
        {
            BarrackInstatlyBtnValueText.color = originalTextColor;
        }
    }
    void NavalColorManager()
    {
        int cost = 0, gemCost = 0;

        if (int.TryParse(NavalInputField.text, out var value))
        {
            cost = value * NavalSoliderCost;
            gemCost = GameEconomy.Instance.GetGemValue(cost);
            int soliderLimit = currentState.GetArmyBarrackSize() - currentState.GetArmySize();
            if (value > soliderLimit)
            {
                ArmyBarrackSizeText.color = redColor;
            }
            else
            {
                ArmyBarrackSizeText.color = originalTextColor;
            }
        }
        if (currentState.resourceData[ResourceType.Gold].currentAmount < cost)
        {
            NavalBuyBtnValueText.color = redColor;
        }
        else
        {
            NavalBuyBtnValueText.color = originalTextColor;
        }
        if (currentState.resourceData[ResourceType.Diamond].currentAmount < gemCost)
        {
            NavalInstatlyBtnValueText.color = redColor;
        }
        else
        {
            NavalInstatlyBtnValueText.color = originalTextColor;
        }
       
    }
    void LandColorManager()
    {
        int cost = 0, gemCost = 0;

        if (int.TryParse(LandInputField.text, out var value))
        {
            cost = value * LandSoliderCost;
            gemCost = GameEconomy.Instance.GetGemValue(cost);
            int soliderLimit = currentState.GetArmyBarrackSize() - currentState.GetArmySize();
            if (value > soliderLimit)
            {
                ArmyBarrackSizeText.color = redColor;
            }
            else
            {
                ArmyBarrackSizeText.color = originalTextColor;
            }
        }
        if (currentState.resourceData[ResourceType.Gold].currentAmount < cost)
        {
            LandBuyBtnValueText.color = redColor;
        }
        else
        {
            LandBuyBtnValueText.color = originalTextColor;
        }
        if (currentState.resourceData[ResourceType.Diamond].currentAmount < gemCost)
        {
            LandInstatlyBtnValueText.color = redColor;
        }
        else
        {
            LandInstatlyBtnValueText.color = originalTextColor;
        }
        
    }
    void OnBarrakMacClicked()
    {
        int maxBarrack = 0;
        int currentGold= currentState.GetGoldResValue();
        int currentGem= currentState.GetGemResValue();
        if(currentGold>0)
         maxBarrack =(int) currentGold / BarrackCost;
        if(currentGem>0)
        {
            int gemMax = (int)((GameEconomy.Instance.GetGoldValue(ResourceType.Diamond, currentGem))/BarrackCost-1);
            maxBarrack= gemMax<maxBarrack?maxBarrack:gemMax;
        }
        BarrackInputField.text = FormatNumber(maxBarrack);
    }
    void OnLandMacClicked()
    {
        int currentGold = currentState.GetGoldResValue();
        int currentGem = currentState.GetGemResValue();
        int soliderQuota = currentState.GetSoliderQuota() < 0 ? 0 : currentState.GetSoliderQuota();
        int maxSolider =(int) currentGold / LandSoliderCost;
        if (currentGem > 0)
        {
            int gemMax = (int)((GameEconomy.Instance.GetGoldValue(ResourceType.Diamond, currentGem)) / LandSoliderCost - 1);
            maxSolider = gemMax < maxSolider ? maxSolider : gemMax;
        }
        LandInputField.text =""+ (soliderQuota > maxSolider ? maxSolider:soliderQuota  );

    }
    void OnNavalMacClicked()
    {
        int currentGold = currentState.GetGoldResValue();
        int currentGem = currentState.GetGemResValue();
        int quata = currentState.GetSoliderQuota()<0?0: currentState.GetSoliderQuota();
        int maxSolider = (int)currentGold / NavalSoliderCost;
        if (currentGem > 0)
        {
            int gemMax = (int)((GameEconomy.Instance.GetGoldValue(ResourceType.Diamond, currentGem)) / NavalSoliderCost - 1);
            maxSolider = gemMax < maxSolider ? maxSolider : gemMax;
        }
        NavalInputField.text = "" + (quata > maxSolider ?  maxSolider: quata);
    }

    // Barrack input deðeri deðiþtiðinde çaðrýlýr
    void OnBarrackInputChanged(string input)
    {
        if (int.TryParse(input, out int value))
        {
            int cost = value * BarrackCost;
            int gemCost = GameEconomy.Instance.GetGemValue(cost);
            BarrakBuyBtnValueText.text= FormatNumber(cost);
            BarrackInstatlyBtnValueText.text = FormatNumber(gemCost );
            

        }
        BarrackColorManager();
        LandColorManager();
        NavalColorManager();
    }

    // Naval input deðeri deðiþtiðinde çaðrýlýr
    void OnNavalInputChanged(string input)
    {
        if (int.TryParse(input, out int value))
        {
            int cost = value * NavalSoliderCost;
            int gemCost = GameEconomy.Instance.GetGemValue(cost);
            NavalBuyBtnValueText.text = FormatNumber(cost);
            NavalInstatlyBtnValueText.text = FormatNumber(gemCost);
            
        }
        BarrackColorManager();
        LandColorManager();
        NavalColorManager();
    }

    // Land input deðeri deðiþtiðinde çaðrýlýr
    void OnLandInputChanged(string input)
    {
        if (int.TryParse(input, out int value))
        {
            int cost = value * LandSoliderCost;
            int gemCost = GameEconomy.Instance.GetGemValue(cost);
            LandBuyBtnValueText.text = FormatNumber(cost);
            LandInstatlyBtnValueText.text = FormatNumber(gemCost);
           
           
        }
        BarrackColorManager();
        LandColorManager();
        NavalColorManager();
    }

    // Barrack büyüklüðünü arttýrmak için butona týklandýðýnda çaðrýlýr
    void IncreaseBarrackSize(ResourceType resType)
    {
        if (int.TryParse(BarrackInputField.text, out int value))
        {
            int cost = resType == ResourceType.Gold ? value * BarrackCost : (int)(  GameEconomy.Instance.GetGemValue(value*BarrackCost));

            if (currentState.resourceData[resType].currentAmount >= cost)
            {
                currentState.IncreaseArmyBarrackSize(value);
                if (resType == ResourceType.Gold)
                    currentState.GoldSpend(cost);
                else
                    currentState.GemSpend(cost);

                BarrackInputField.text = "0";
            }
            else
            {
               
            }

            UpdateUI(); // UI'yi güncelle
        }
    }

    // Naval büyüklüðünü arttýrmak için butona týklandýðýnda çaðrýlýr
    void IncreaseNavalSize(ResourceType resType)
    {
        if (int.TryParse(NavalInputField.text, out int value))
        {
            int cost = resType == ResourceType.Gold ? value * NavalSoliderCost : (int)(GameEconomy.Instance.GetGemValue(value *NavalSoliderCost));
            int soliderLimit = currentState.GetArmyBarrackSize() - currentState.GetArmySize();
            if (currentState.resourceData[resType].currentAmount >= cost && value <=soliderLimit)
            {
                currentState.IncreaseNavalArmySize(value);
                if (resType == ResourceType.Gold)
                    currentState.GoldSpend(cost);
                else
                    currentState.GemSpend(cost);

                NavalInputField.text = "0";
            }
            else
            {
               
            }

            UpdateUI(); // UI'yi güncelle
        }
    }

    // Land büyüklüðünü arttýrmak için butona týklandýðýnda çaðrýlýr
    void IncreaseLandSize(ResourceType resType)
    {
        if (int.TryParse(LandInputField.text, out int value))
        {
            int cost = resType == ResourceType.Gold ? value * LandSoliderCost : (int)(GameEconomy.Instance.GetGemValue(value * LandSoliderCost));
            int soliderLimit = currentState.GetArmyBarrackSize() - currentState.GetArmySize();
            if (currentState.resourceData[resType].currentAmount >= cost&& value<=soliderLimit)
            {
                currentState.IncreaseLandArmySize(value);
                if (resType == ResourceType.Gold)
                    currentState.GoldSpend(cost);
                else
                    currentState.GemSpend(cost);

                LandInputField.text = "0";
            }
            else
            {
              
            }

            UpdateUI(); // UI'yi güncelle
        }
    }
}
