using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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
    int witchPower = 30; // Her bir cadý kaç asker öldürecek
    int sliderValue = 0;
    float spread = 0, belive = 0;
    Color originalWitchCostTextColor;
    Action<int> onStarLimitChange;

    void OnEnable()
    {
        enemyState= RegionClickHandler.Instance.currentState;
        if( enemyState!= null)
        {
            slider.onValueChanged.AddListener(OnSliderValueChanged);
            originalWitchCostTextColor = goldBtnText.color;

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

        int gold = ResourceManager.Instance.GetResourceAmount(ResourceType.Gold);
        if (gold >= witchCost)
        {
            goldBtnText.color = originalWitchCostTextColor;
        }
        else
        {
            goldBtnText.color = Color.red;
        }

        goldBtnText.text = witchCost.ToString();
        gemBtnText.text = GameEconomy.Instance.GetGemValue(witchCost).ToString();

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
        else
        {
            goldBtnText.color = Color.red;
            Debug.LogWarning("Yeterli altýn yok, cadý gönderilemiyor.");
        }
    }
    public void SendWitch()
    {
        float enemyArmySize = enemyState.GetComponent<State>().ArmySize;
        int newLoss =(int)(enemyArmySize / 100 * spread);
        newLoss= (int) (newLoss/100*belive);
       
        Debug.LogWarning($" {enemyState.name}'inde orduda baþlatýlan cadý avýnda  {newLoss} tane cadý elegeçirildi  avdan önceki asker sayýsý {enemyArmySize}");
        enemyState.GetComponent<State>().ReduceArmySize(newLoss);
        Debug.LogWarning($"avdan sonra asker sayýsý {enemyState.GetComponent<State>().ArmySize}");

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
        else
        {
            gemBtnText.color = Color.red;
            Debug.LogWarning("Yeterli elmas yok, cadý gönderilemiyor.");
        }
    }

    void ChangeActiveStarGameObject(int index)
    {
        for (int i = 0; i < 3; i++)
        {
            starImageList[i].SetActive(i < index + 1);
        }
    }
}
