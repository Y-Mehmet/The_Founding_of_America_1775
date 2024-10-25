using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Start()
    {
        LoadGameData();
    }
    public void LoadGameData()
    {
        SaveGameData.Instance.LoadGame();
        GameManager.Instance.OnGameDataLoaded?.Invoke();
    }
}
