using System;
using UnityEngine;

public class GameDateManager : MonoBehaviour
{
    public static GameDateManager instance{get; private set;}
    public static DateTime currentDate;     // �u anki oyun tarihi
    private float timeScale;   // Zaman ak���n� kontrol eden fakt�r
    private bool isPaused = false;   // Zaman�n durdurulup durdurulmad���n� kontrol eden de�i�ken
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

        // Her saniyede bir kez g�n art��� yap
        InvokeRepeating("AdvanceOneDay", 0, timeScale);
        if (GameManager.Instance != null)
        {
            timeScale = GameManager.gameDayTime;
            GameManager.Instance.OnGameDataLoaded += GameDataLoaded;
        }
        else
            Debug.LogWarning("gameManager is null");
        // Oyun ba�lad���nda tarihi 19 Nisan 1775 olarak ayarla
       
      
    }

    // Bir g�n ekleme fonksiyonu
    void AdvanceOneDay()
    {
        if (!isPaused)
        {
            currentDate = currentDate.AddDays(1);
          //  Debug.Log("Current Game Date: " + currentDate.ToString("MM/dd/yyyy")); // Amerikan tarih format�
        }
    }
    public void GameDataLoaded()
    {
        //Debug.LogWarning("game data loadded date timede managerde �al�s�t�");
        CancelInvoke("AdvanceOneDay");
        InvokeRepeating("AdvanceOneDay", 0, timeScale);
    }

    // Zaman� durdurma fonksiyonu (Sava� ba�lad���nda �a�r�labilir)
    public void PauseTime()
    {
        isPaused = true;
    }
    public void ResumeTime()

    // Zaman� tekrar ba�latma fonksiyonu
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
