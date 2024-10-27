using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public Button restartBtn;
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
        UIManager.Instance.GetComponent<HideAllPanelButton>().DoHidePanel();
        SaveGameData.Instance.LoadGame(true);
        GameManager.Instance.OnGameDataLoaded?.Invoke();

    }
}
