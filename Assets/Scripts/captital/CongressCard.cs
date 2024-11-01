using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CongressCard : MonoBehaviour
{
    public ActType ActType;
   
    public Button selectBtn;
    public Color unselectedBtnColor, selectedBtnColor;
    private void Awake()
    {
        
              selectedBtnColor = ColorUtility.TryParseHtmlString("#FDCB6E", out var color) ? color : Color.white;
              unselectedBtnColor = ColorUtility.TryParseHtmlString("#D63031", out var color2) ? color2 : Color.white;
    }

    private void OnEnable()
    {
        selectBtn.onClick.AddListener(OnButtonClicked);
        if(((int)USCongress.currentAct)>=0)
        {
            if (USCongress.currentAct == ActType)
            {
                selectBtn.image.color = selectedBtnColor;
                Debug.Log(" curent act "+USCongress.currentAct);
            }
           
        }
        else
        {
            selectBtn.image.color = unselectedBtnColor;
        }
    }
    private void OnDisable()
    {
        selectBtn.onClick.RemoveListener(OnButtonClicked);
    }
    void OnButtonClicked()
    {
        if( USCongress.currentAct== ActType)
        {
            selectBtn.image.color = unselectedBtnColor;
            USCongress.OnRepealActChange?.Invoke(USCongress.currentAct);
            USCongress.currentAct = ActType.None;

        }
        else
        {

            foreach (Transform childTransform in transform.parent)
            {
                int index = childTransform.GetSiblingIndex();
                if (((int)USCongress.currentAct) == index)
                {
                    childTransform.GetComponent<CongressCard>().selectBtn.image.color = unselectedBtnColor;
                }
            }


            USCongress.OnRepealActChange?.Invoke(USCongress.currentAct);



            switch (((int)ActType))
            {
                case 0:
                    USCongress.currentAct = ActType.Population;
                    USCongress.OnEnactActChange?.Invoke(ActType.Population);
                    break;
                case 1:
                    USCongress.currentAct = ActType.Social;
                    USCongress.OnEnactActChange?.Invoke(ActType.Social);
                    break;
                case 2:
                    USCongress.currentAct = ActType.National;
                    USCongress.OnEnactActChange?.Invoke(ActType.National);
                    break;
                case 3:
                    USCongress.currentAct = ActType.Emancipation;
                    USCongress.OnEnactActChange?.Invoke(ActType.Emancipation);
                    break;
                case 4:
                    USCongress.currentAct = ActType.Labor;
                    USCongress.OnEnactActChange?.Invoke(ActType.Labor);
                    break;
                default:
                    break;

            }
            selectBtn.image.color = selectedBtnColor;
        }
       

    }
}
