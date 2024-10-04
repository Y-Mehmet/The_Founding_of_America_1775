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

    public static DateTime currentDate;     // �u anki oyun tarihi
    private float timeScale ;   // Zaman ak���n� kontrol eden fakt�r
    private bool isPaused = false;   // Zaman�n durdurulup durdurulmad���n� kontrol eden de�i�ken

    

    void Start()
    {
        if (GameManager.Instance != null)
        {
            timeScale = GameManager.gameDayTime;
        }
        else
            Debug.LogWarning("gameManager is null");
        // Oyun ba�lad���nda tarihi 19 Nisan 1775 olarak ayarla
        currentDate = new DateTime(1775, 4, 19);

        // Her saniyede bir kez g�n art��� yap
        InvokeRepeating("AdvanceOneDay",  timeScale,  timeScale);
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

    // Zaman� durdurma fonksiyonu (Sava� ba�lad���nda �a�r�labilir)
    public void PauseTime()
    {
        isPaused = true;
    }

    // Zaman� tekrar ba�latma fonksiyonu
    public void ResumeTime()
    {
        isPaused = false;
    }
    public string GetCurrentDataString()
    {
        return currentDate.ToString("MM/dd/yyyy");
    }
}
