using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject StatePanel;
    public GameObject allyPanel;
    public GameObject enemyPanel;
    public GameObject neutralPanel;
    public GameObject attackCanvas;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }
    public void ShowPanel(State state)
    {
       // StatePanel.SetActive(true);
        switch (state.stateType)
        {
            case StateType.Ally:
                allyPanel.SetActive(true);
                enemyPanel.SetActive(false);
                neutralPanel.SetActive(false);
                break;
            case StateType.Enemy:
                allyPanel.SetActive(false);
                enemyPanel.SetActive(true);
                neutralPanel.SetActive(false);
                break;
            case StateType.Neutral:
                allyPanel.SetActive(false);
                enemyPanel.SetActive(false);
                neutralPanel.SetActive(true);
                break;
        }
    }
    public void  ShowAttackCanvas()
    {
        attackCanvas.SetActive(true);
        AttackCanvas.Instance.ShowAttackCanvasInfo(  );
        
    }
}
