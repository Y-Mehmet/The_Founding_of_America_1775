using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    //public GameObject StatePanel;
    //public GameObject allyPanel;
    //public GameObject enemyPanel;
    //public GameObject neutralPanel;
    //public GameObject attackCanvas;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }
    public  void HideLastPanel()
    {
        GetComponent<HideLastPanelButton>().DoHidePanel();
    }
    public void HideAllPanel()
    {
        GetComponent<HideAllPanelButton>().DoHidePanel();
    }

}
