using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utility;

public class WitchHunt : MonoBehaviour
{
    public Button goldButton, gemButton;
    public Slider slider;
    public TMP_Text goldBtnText, gemBtnText, spreadText, beliveText;
    public List<GameObject> starImageList = new List<GameObject>();
    GameObject enemyState;
    int activeStarIndex = -1;
    int oneWitchCost = 100;
    int witchCost = 0;

    float spread = 0, belive = 0;
    Color originalWitchCostTextColor=Color.white;
    Action<int> onStarLimitChange;

    void OnEnable()
    {
        enemyState= RegionClickHandler.Instance.currentState;
        if( enemyState!= null)
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
           

            slider.value = 0; // Baþlangýç deðeri
            OnSliderValueChanged(slider.value); // Ýlk deðer güncelleme

            goldButton.onClick.AddListener(GoldBtnClicked);
            gemButton.onClick.AddListener(GemBtnClicked);
            onStarLimitChange += ChangeActiveStarGameObject;
        }else
        {
            Debug.LogError("enemy state is null");
        }
        
    }

    private void OnDisable()
    {
        slider.value = 0;
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        goldButton.onClick.RemoveListener(GoldBtnClicked);
        gemButton.onClick.RemoveListener(GemBtnClicked);
        onStarLimitChange -= ChangeActiveStarGameObject;
    }

    public void CalculateValue(float value)
    {
        if (value < 33)
        {
            spread = value * 1.5f;
            belive = value * 0.75f;
            if (activeStarIndex != -1)
            {
                activeStarIndex = -1;
                onStarLimitChange.Invoke(activeStarIndex);
            }
        }
        else if (value < 66)
        {
            spread = value * 1.5f;
            belive = value * 0.75f;
            starImageList[0].gameObject.SetActive(true);
            if (activeStarIndex != 0)
            {
                activeStarIndex = 0;
                onStarLimitChange.Invoke(activeStarIndex);
            }
        }
        else if (value < 97)
        {
            spread = 100;
            belive = 50 + (value - 66) * 1.5f;
            if (activeStarIndex != 1)
            {
                activeStarIndex = 1;
                onStarLimitChange.Invoke(activeStarIndex);
            }
        }
        else
        {
            spread = 100;
            belive = 100;
            if (activeStarIndex != 2)
            {
                activeStarIndex = 2;
                onStarLimitChange.Invoke(activeStarIndex);
            }
        }
    }

    void OnSliderValueChanged(float value)
    {
        int witchCount = Mathf.FloorToInt(value);

        witchCost = witchCount * oneWitchCost;
        ColorManager(witchCost);
      

        goldBtnText.text = FormatNumber(witchCost);
        gemBtnText.text = FormatNumber(GameEconomy.Instance.GetGemValue(witchCost));

        CalculateValue(value);
        beliveText.text = Mathf.Ceil(belive).ToString() + "%";
        spreadText.text = Mathf.Ceil(spread).ToString() + "%";
    }

    void ClearSlider()
    {
        slider.value = 0;
        OnSliderValueChanged(slider.value);
    }

    void GoldBtnClicked()
    {
        int gold = ResourceManager.Instance.GetResourceAmount(ResourceType.Gold);
        if (gold >= witchCost)
        {
            ResourceManager.Instance.ReduceResource(ResourceType.Gold, witchCost);

            SendWitch();
            ClearSlider();
            UIManager.Instance.HideLastPanel();
        }
       
    }
    public void SendWitch()
    {
        float enemyArmySize = enemyState.GetComponent<State>().GetArmySize();
        int newLoss =(int)(enemyArmySize / 100 * spread);
        newLoss= (int) (newLoss/200*belive);
        newLoss = newLoss > 5000 ? 5000 : newLoss;      
      
        enemyState.GetComponent<State>().ReduceArmySize(newLoss);
       
        MessageManager.AddMessage($"A witch hunt in {enemyState.name}'s army resulted in {newLoss} suspected witches being neutralized. The army had {enemyArmySize} soldiers before the hunt and now has {enemyState.GetComponent<State>().GetArmySize()} soldiers remaining.");

    }

    void GemBtnClicked()
    {
        int gem = ResourceManager.Instance.GetResourceAmount(ResourceType.Diamond);
        int gemValue = (int)GameEconomy.Instance.GetGemValue(witchCost);
        if (gem >= gemValue)
        {
            ResourceManager.Instance.ReduceResource(ResourceType.Diamond, gemValue);
            SendWitch();
            ClearSlider();
            UIManager.Instance.HideLastPanel();
        }
        
    }

    void ChangeActiveStarGameObject(int index)
    {
        for (int i = 0; i < 3; i++)
        {
            starImageList[i].SetActive(i < index + 1);
        }
    }
    void ColorManager(int witchCost)
    {
        int gold = ResourceManager.Instance.GetResourceAmount(ResourceType.Gold);
        if (gold >= witchCost)
        {
            goldBtnText.color = originalWitchCostTextColor;
        }
        else
        {
            goldBtnText.color = Color.red;
        }
        int gem = ResourceManager.Instance.GetResourceAmount(ResourceType.Diamond);
        int gemValue = (int)GameEconomy.Instance.GetGemValue(witchCost);
        if (gem >= gemValue)
        {
            gemBtnText.color = originalWitchCostTextColor;
        }
        else
        {
            gemBtnText.color = Color.red;
           // Debug.LogWarning("Yeterli elmas yok, cadý gönderilemiyor.");
        }
    }
}
