using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    Button restartBtn;
  
    private void OnEnable()
    {
        restartBtn = GetComponent<Button>();
        restartBtn.onClick.AddListener(OnButtonCliced);
    }
    private void OnDisable()
    {
        restartBtn.onClick.RemoveListener(OnButtonCliced);
    }
    void OnButtonCliced()
    {
        //  Debug.LogWarning(" on button click çalýþtý");
        SaveGameData.Instance.LoadGame(true);
        UIManager.Instance.GetComponent<HideAllPanelButton>().DoHidePanel();
        GameManager.Instance.IsRegionPanelOpen = false;
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
