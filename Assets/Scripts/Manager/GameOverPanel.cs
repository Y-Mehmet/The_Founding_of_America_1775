using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    Button restartBtn;
    private void Start()
    {
       restartBtn= GetComponent<Button>();
    }
    private void OnEnable()
    {
        restartBtn.onClick.AddListener(OnButtonCliced);
    }
    private void OnDisable()
    {
        restartBtn.onClick.RemoveListener(OnButtonCliced);
    }
    void OnButtonCliced()
    {
        Debug.LogWarning(" on button click çalýþtý");
        SaveGameData.Instance.LoadGame(true);
        UIManager.Instance.GetComponent<HideAllPanelButton>().DoHidePanel();
       
        //GameManager.Instance.OnGameDataLoaded?.Invoke();

    }
}
