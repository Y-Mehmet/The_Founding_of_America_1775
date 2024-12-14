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
        SoundManager.instance.Stop("ChurchBell");
        SoundManager.instance.Play("ButtonClick");
        
        SaveGameData.Instance.LoadGame(true);
        UIManager.Instance.GetComponent<HideAllPanelButton>().DoHidePanel();
        GameManager.Instance.IsRegionPanelOpen = false;
        foreach (Transform state in Usa.Instance.transform)
        {
            state.GetComponentInChildren<Flag>().capitalFlag.SetActive(false);
            if (state.name == "Massachusetts")
                GameManager.capitalState = state.GetComponent<State>();
        }

    }
}
