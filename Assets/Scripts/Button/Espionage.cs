using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Utility;

public class Espionage : MonoBehaviour
{

    public Button gemButton, goldButton;
    public Slider slider; // Slider kullanýlýyor
    public TMP_Text goldBtnText, gemBtnText, succsesText, ecsapeText;
    public List<GameObject> starImageList = new List<GameObject>();
    int activeStarIndex = -1;
    int oneSpyCost = 10;
    int spyCost = 0;
    float sucs=0, esca=0;
    Color originalSpyCostTextColor= Color.white;
    Action<int> onStarLimitChange;

    void OnEnable()
    {
        // Slider'ýn deðer deðiþikliði olayýna bir dinleyici ekle
        slider.onValueChanged.AddListener(OnSliderValueChanged);
     

        // Slider'ý sýfýrlamak için baþlangýç deðerini ayarlayabilirsiniz
        slider.value = 0; // Baþlangýçta 0 spy
        OnSliderValueChanged(slider.value); // Ýlk deðer için güncelle

        goldButton.onClick.AddListener(GoldBtnClicked);
        gemButton.onClick.AddListener(GemBtnClicked);
        onStarLimitChange += ChangeAciveStarGameObjcet;


    }
    private void OnDisable()
    {
        slider.value = 0;
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        goldButton.onClick.RemoveListener(GoldBtnClicked);
        gemButton.onClick.RemoveListener(GemBtnClicked);
        onStarLimitChange += ChangeAciveStarGameObjcet;
    }

    public void CalculateValue(float value )
    {
       
        if (value < 33)
        {
            sucs = value * 1.5f;
            esca = value * 0.75f;//50-25
            if(activeStarIndex!=-1)
            {
                activeStarIndex = -1;
                onStarLimitChange.Invoke(activeStarIndex);
            }
        }
        else if (value<66)
        {
            
            sucs = value * 1.5f;
            esca = value * 0.75f;//50-25
            starImageList[0].gameObject.SetActive(true);
            if (activeStarIndex != 0)
            {
                activeStarIndex = 0;
                onStarLimitChange.Invoke(activeStarIndex);
            }

        }
        else if(value<97)
        {
            sucs = 100;
            esca = 50+(value-66) * 1.5f;//100-50
            if (activeStarIndex != 1)
            {
                activeStarIndex = 1;
                onStarLimitChange.Invoke(activeStarIndex);
            }
        }else
        {
            sucs = 100;
            esca = 100;
            if (activeStarIndex != 2)
            {
                activeStarIndex = 2;
                onStarLimitChange.Invoke(activeStarIndex);
            }
        }
       
    }

    void OnSliderValueChanged(float value)
    {
        int spyCount = Mathf.FloorToInt(value); // Slider'dan tam sayý deðeri al

        spyCost = spyCount * oneSpyCost;
        ColorManager(spyCost);


        goldBtnText.text = FormatNumber(spyCost);
        gemBtnText.text = FormatNumber(GameEconomy.Instance.GetGemValue(spyCost));


        // Debug.LogWarning(spyCost + " spy cost deðeri deðiþti");


        CalculateValue(value);
        ecsapeText.text = Mathf.Ceil(esca).ToString()+"%";
        succsesText.text= Mathf.Ceil(sucs).ToString()+"%";
    }

    void ClearSlider()
    {
        slider.value = 0; // Slider'ý sýfýrla
        OnSliderValueChanged(slider.value); // Deðeri güncelle
    }
    void GoldBtnClicked()
    {
        
        int gold = ResourceManager.Instance.GetResourceAmount(ResourceType.Gold);
        if (gold >= spyCost)
        {
            ResourceManager.Instance.ReduceResource(ResourceType.Gold, spyCost);
            EnemyStateInfoPanel.Instance.ShowInfo(esca, sucs);
            ClearSlider(); // Slider'ý temizle
            UIManager.Instance.HideLastPanel();
            if(spyCost>0)
            SoundManager.instance.Play("Coins");
        }
        else
        {
            goldBtnText.color = Color.red;
            // Debug.LogWarning("Casus gönderilemiyor; altýn yetersiz");
            SoundManager.instance.Play("Error");
        }
       
        
    }
    void GemBtnClicked()
    {
        int gem = ResourceManager.Instance.GetResourceAmount(ResourceType.Diamond);
        int gemValue =(int) GameEconomy.Instance.GetGemValue(spyCost);
        if (gem >= gemValue)
        {
            ResourceManager.Instance.ReduceResource(ResourceType.Diamond, gemValue);
            EnemyStateInfoPanel.Instance.ShowInfo(esca,sucs);
            ClearSlider(); // Slider'ý temizle
            UIManager.Instance.HideLastPanel();
            if(gemValue>0)
            SoundManager.instance.Play("Gem");
        }
        else
        {
            gemBtnText.color = Color.red;
            //Debug.LogWarning($"Casus gönderilemiyor; gem yetersiz {gem} cost { gemValue}");
            SoundManager.instance.Play("Error");
        }
    }
    void ChangeAciveStarGameObjcet(int index)
    {
       
        for (int i=0; i < 3; i++)
        {
            if( i< index + 1)
            {
                starImageList[i].SetActive(true);
            }else
            {
                starImageList[i].SetActive(false);
            }
            
        }
    }
    void ColorManager(int spyCost)
    {
        int gold = ResourceManager.Instance.GetResourceAmount(ResourceType.Gold);
        if (gold >= spyCost)
        {
            goldBtnText.color = originalSpyCostTextColor;
        }
        else
        {
            goldBtnText.color = Color.red;
        }
        int gem = ResourceManager.Instance.GetResourceAmount(ResourceType.Diamond);
        int gemValue = (int)GameEconomy.Instance.GetGemValue(spyCost);
        if (gem >= gemValue)
        {
            gemBtnText.color = originalSpyCostTextColor;
        }
        else
        {
            gemBtnText.color = Color.red;
        }
    }
}
