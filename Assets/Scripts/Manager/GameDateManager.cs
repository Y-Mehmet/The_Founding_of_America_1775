using System;
using UnityEngine;

public class GameDateManager : MonoBehaviour
{
    public static GameDateManager instance{get; private set;}
    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public static DateTime currentDate;     // Þu anki oyun tarihi
    private float timeScale ;   // Zaman akýþýný kontrol eden faktör
    private bool isPaused = false;   // Zamanýn durdurulup durdurulmadýðýný kontrol eden deðiþken


    private void OnDisable()
    {
        CancelInvoke("AdvanceOneDay");
    }
    void Start()
    {
        if (GameManager.Instance != null)
        {
            timeScale = GameManager.gameDayTime;
        }
        else
            Debug.LogWarning("gameManager is null");
        // Oyun baþladýðýnda tarihi 19 Nisan 1775 olarak ayarla
        currentDate = new DateTime(1775, 4, 19);

        // Her saniyede bir kez gün artýþý yap
        InvokeRepeating("AdvanceOneDay",  timeScale,  timeScale);
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
