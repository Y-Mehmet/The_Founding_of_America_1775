using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[Serializable]
public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
    public static List<State> AllyStateList = new List<State>();
    public Action<State, bool > onAllyStateChanged;
    public static int tottalPopulation = 0;
    public bool ÝsAttack= false;
    public bool IsRegionPanelOpen = false;
    public bool IsAttackFinish  = true;
    public bool IsSpy= false;
    public static bool isWarDeclared = false;

    public float attackFinishDurtion = 7.0f;
    //animasyon için 
    public float moveAmount = 135.33f;    // Y ekseninde hareket edilecek mesafe
    public float moveDuration = 1.5f; // Hareket süresi

    public static float gameDayTime = 6.0f;
    public static  float  neigbordTradeTime= 3 ;
    public static float  nonNeigbordTradeTime= 9 ;
    public static float ArrmyRatio = 0.05f; // nufusun yüzde kaçý asker olark baþlayacak 
    public static float ArmyBarrackRatio = 0.2f; // kýþla oraný

    public int thresholdForSuccesfulEspionage = 3;
    public int spyCostModPurchases = 10;//number of spy cost mod purchases

    
    public bool isGamePause = false;
    public bool ÝsGameOver= false;
    public bool IsFirstSave = true;
    public const int MAX_POPULATION = 20000;

   


    public List<State> allStates = new List<State>();
    public event Action OnAttackStarted;
    public event Action OnAttackStopped;
    public Action OnGameDataLoaded;
    public Action OnCloseBtnClicked;
    public static State capitalState;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
           

        }
        else
        {
            Instance = this;
         
        }
    }
    private void Start()
    {
        OnGameDataLoaded += GameLoaded;
       
        //onAllyStateChanged += ChangeStateType;
      
       
    }
    private void OnDisable()
    {
        OnGameDataLoaded -= GameLoaded;
       
        //onAllyStateChanged -= ChangeStateType;

    }
    void GameLoaded()
    {
      
        AllyStateList.Clear();
        AllyStateList = GetStatesByType(StateType.Ally);
        StartCoroutine(UnloadAdditiveScene("LoadScene"));
    }
    public IEnumerator UnloadAdditiveScene(string sceneName)
    {
        yield return new WaitForSeconds(1);
        // Sahnenin yüklü olup olmadýðýný kontrol et
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene to unload is not loaded: " + sceneName);
        }
    }
    public  void ChangeCapitalCity()
    {
        
        if (AllyStateList.Count > 0)
        {
            AllyStateList[0].IsCapitalCity = true;
            AllyStateList[0].transform.GetComponentInChildren<Flag>().capitalFlag.SetActive(true);
         //   Debug.LogError(" change capital da  catiptal satate flag is true " + AllyStateList[0].name);
            capitalState = AllyStateList[0];

        }
        else
        {
            
            
           
            ÝsGameOver = true;


        }
    }
  

    public static void TotalPopulationManager( int addedValue)
    {
        tottalPopulation += addedValue;
    }


    public List<State> GetStatesByType(StateType stateType)
    {
      
        List<State> states = new List<State>();
        foreach (State state in allStates)
        {
         
            if (state.stateType == stateType)
            {
                states.Add(state);
            }
        }
      
        return states;
    }
    // kullanýlmýyor***********
    public void OnAllyStateChanged()
    {
        AllyStateList.Clear();
        foreach (State state in allStates)
        {
            if (state.stateType == StateType.Ally)
            {
                allStates.Add(state);
            }
        }
        
    }
    public void ChangeIsAttackValueTrue()
    {
        ÝsAttack = true;
        isGamePause = true;
        OnAttackStarted?.Invoke();

    }
    public void ChangeIsAttackValueFalse()
    {

        ÝsAttack = false;
        isGamePause = false;
        OnAttackStopped?.Invoke(); // Olayý tetikleyin
        capitalState.transform.GetComponentInChildren<Flag>().capitalFlag.SetActive(true);
       // Debug.LogError("attaack fnish  capital state flag is true " + capitalState.name);




    }
    public void ChanngeIsRegionPanelOpenValueTrue()
    {
        IsRegionPanelOpen = true;
        UIManager.ShowPanelClicked?.Invoke();
    }
    public void ChanngeIsRegionPanelOpenValueFalse()
    {
        IsRegionPanelOpen = false;
        UIManager.HideAllPanelClicked?.Invoke();
    }
    public void ChangeIsSpyValueTrue()
    {
        IsSpy = true;
    }
    public void ChangeAttackFinisValueFalse()
    {
        IsAttackFinish = false;
    }
    public void ChangeAttackFinisValueTrue()
    {
        IsAttackFinish = true;
    }
    public void GameOver()
    {
       StartCoroutine(OpenGameOverPanel()); 
        
    }
    //kullanýlmýyor
    IEnumerator OpenGameOverPanel()
    {
        yield return new WaitForSeconds(0.5f);

        UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.GameOverPanel);
        GameManager.Instance.IsRegionPanelOpen = true;
    }



}
[Serializable]
public enum ActType
{
    Population,
    Social,
    National,
    Emancipation,
    Labor,
    None

}
