using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CongressCard : MonoBehaviour
{
    public ActType ActType;
   
    public Button selectBtn;
    private void OnEnable()
    {
        selectBtn.onClick.AddListener(OnButtonClicked);
    }
    private void OnDisable()
    {
        selectBtn.onClick.RemoveListener(OnButtonClicked);
    }
    void OnButtonClicked()
    {
        if(USCongress.currentAct!= null)
        {
            USCongress.OnRepealActChange?.Invoke(USCongress.currentAct);
        }
        switch (((int)ActType))
        {
            case 0:
                
                USCongress.currentAct = ActType.Population;
                USCongress.OnEnactActChange?.Invoke(ActType.Population);
                break;
            default:
                break;

        }
    }
}
