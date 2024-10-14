using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TroopyPanel : MonoBehaviour
{
    public TMP_Text ArmyBarrackSizeText, NavalSizeText, LandSizeText, LeaderText, UnityArmyPowerText, 
        ArmySizeText, ArmyMoraleText, TotalArmyPowerText, instatlyBtnValueText, BuyBtnValueText;
    public Button BarrackInstalyBtn, BarrackBuyBtn,BarrackMacBtn,
        NavalInstalyBtn, NavalBuyBtn,NavalMacBtn, LandInstalyBtn, LandBuyBtn, LandMacBtn;
    public TMP_InputField BarrackInputField,NavalInputField, LandInputField;
    State currentState;
    private void OnEnable()
    {
        currentState = RegionClickHandler.Instance.currentState.GetComponent<State>();
        if(currentState != null )
        {
            ArmyBarrackSizeText.text = currentState.ArmyBarrackSize.ToString();
           // NavalSizeText.text= currentState.

        }
        else
        { Debug.LogWarning("current state is null"); }
        

    }
    private void OnDisable()
    {
        
    }
}
