using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaLoader : MonoBehaviour
{
  public static GamaLoader Instance { get; private set; }
   
   
    private void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private IEnumerator LoadGameWithDelay()
    {
        yield return new WaitForSeconds(0.5f); // Gerekli gecikme s�resi
        LoadGameData();
    }

    private void Start()
    {
        StartCoroutine(LoadGameWithDelay());
    }

    public void LoadGameData()
    {
       
        SaveGameData.Instance.LoadGame();
       // GameManager.Instance.OnGameDataLoaded?.Invoke();
        string fullPath = Path.Combine(Application.persistentDataPath, "SaveData_" + 0);
        // Kay�t dosyas�n�n varl���n� kontrol ediyoruz
        if (GameManager.Instance.IsFirstSave)
        {
            SaveGameData.Instance.SaveGame(true);
        }
       
    }
}
