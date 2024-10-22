using System;
using UnityEngine;

public class GameDateManager : MonoBehaviour
{
    public static GameDateManager instance{get; private set;}
    public static DateTime currentDate;     // Þu anki oyun tarihi
    private float timeScale;   // Zaman akýþýný kontrol eden faktör
    private bool isPaused = false;   // Zamanýn durdurulup durdurulmadýðýný kontrol eden deðiþken
    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

   


    private void OnDisable()
    {
        CancelInvoke("AdvanceOneDay");
    }
    void Start()
    {
        currentDate = new DateTime(1775, 4, 19);

        // Her saniyede bir kez gün artýþý yap
        InvokeRepeating("AdvanceOneDay", 0, timeScale);
        if (GameManager.Instance != null)
        {
            timeScale = GameManager.gameDayTime;
            GameManager.Instance.OnGameDataLoaded += GameDataLoaded;
        }
        else
            Debug.LogWarning("gameManager is null");
        // Oyun baþladýðýnda tarihi 19 Nisan 1775 olarak ayarla
       
      
    }

    // Bir gün ekleme fonksiyonu
    void AdvanceOneDay()
    {
        if (!isPaused)
        {
            currentDate = currentDate.AddDays(1);
          //  Debug.Log("Current Game Date: " + currentDate.ToString("MM/dd/yyyy")); // Amerikan tarih formatý
        }
    }
    public void GameDataLoaded()
    {
        //Debug.LogWarning("game data loadded date timede managerde çalýsþtý");
        CancelInvoke("AdvanceOneDay");
        InvokeRepeating("AdvanceOneDay", 0, timeScale);
    }

    // Zamaný durdurma fonksiyonu (Savaþ baþladýðýnda çaðrýlabilir)
    public void PauseTime()
    {
        isPaused = true;
    }
    public void ResumeTime()

    // Zamaný tekrar baþlatma fonksiyonu
    {
        isPaused = false;
    }
    public string GetCurrentDataString()
    {
        return currentDate.ToString("MM/dd/yyyy");
    }
    public string ConvertDateToString(DateTime date)
    {
        return date.ToString("MM/dd/yyyy");
    }
    public static DateTime ConvertStringToDate(string dateString)
    {
        return DateTime.ParseExact(dateString, "MM/dd/yyyy", null);
    }

    public DateTime GetCurrentDate()
    {
        return currentDate;
    }
    public DateTime CalculateDeliveryDateTime(float addedTime = 0)
    {
        DateTime deliveryTime = currentDate.AddDays(addedTime);
        return deliveryTime;
    }
}
