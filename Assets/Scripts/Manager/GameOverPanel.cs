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
        SaveGameData.Instance.LoadGame(true);
    }
    private void OnDisable()
    {
        restartBtn.onClick.RemoveListener(OnButtonCliced);
    }
    void OnButtonCliced()
    {
        SoundManager.instance.Play("ButtonClick");
      //  SceneManager.LoadScene("LoadScene", LoadSceneMode.Additive);
        SaveGameData.Instance.LoadGame(true);
        UIManager.Instance.GetComponent<HideAllPanelButton>().DoHidePanel();
        GameManager.Instance.IsRegionPanelOpen = false;
      

    }
}
