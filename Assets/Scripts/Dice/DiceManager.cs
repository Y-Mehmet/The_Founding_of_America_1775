using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance;


    public List<GameObject> activeRivalDiceLists = new List<GameObject>();
    public List<GameObject> activePlayerDiceLists = new List<GameObject>();

    public List<GameObject> activeRivalDiceSortedLists = new List<GameObject>();
    public List<GameObject> activePlayerDiceSortedLists = new List<GameObject>();

    public List<int> activeRivalDiceValueSortedLists= new List<int>();
    public List<int> activePlayerDiceValueSortedLists = new List<int>();

    private float attackDuration=5.0f;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    Vector3 offset = new Vector3(0, 0, 10);
   
   

   public void DiceActiveted(int rivalDiceCount)
    {
       
        for(int i = 0; i < rivalDiceCount; i++)
        {
            GameObject dice= DiceSpawnner.Instance.rivalDiceLists[i].gameObject;
            dice.SetActive(true);
            dice.GetComponent<DiceRollerDO>().RollDice();
            activeRivalDiceLists.Add(dice);

        }
        for (int i = 0; i < 3; i++)
        {
            GameObject dice = DiceSpawnner.Instance.playerDiceLists[i].gameObject;
            dice.SetActive(true);
            dice.GetComponent<DiceRollerDO>().RollDice();
            activePlayerDiceLists.Add(dice);

        }
        SortDiceList();

    }
    public void StartDiceDisActivated()
    {
        StartCoroutine(DiceDisactiveted());
    }
    
    public IEnumerator DiceDisactiveted()
    {
       // Debug.LogWarning(" dis aktivated 5 sn sonra baþlayacak");
        yield return new WaitForSeconds(attackDuration);
       // Debug.LogWarning(" dis aktivated baþladý");
        activePlayerDiceLists.Clear();
        activeRivalDiceLists.Clear();
        for (int i = 0; i < 4; i++)
        {
            GameObject dice = DiceSpawnner.Instance.rivalDiceLists[i].gameObject;
            dice.transform.position= dice.transform.parent.transform.position+ (offset*i);
            dice.SetActive(false);
            

        }
        for (int i = 0; i < 3; i++)
        {
            GameObject dice = DiceSpawnner.Instance.playerDiceLists[i].gameObject;
            dice.transform.position = dice.transform.parent.transform.position + (offset * i);
            dice.SetActive(false);
            

        }

    }
    public void  SortDiceList()
    {
        // player ve rival zarlarý listelere eklendi 
        foreach (var dice in activeRivalDiceLists)
        {
            activeRivalDiceValueSortedLists.Add(dice.gameObject.GetComponent<DiceRollerDO>().DiceValue);
        }

        foreach (var dice in activePlayerDiceLists)
        {
            activePlayerDiceValueSortedLists.Add(dice.gameObject.GetComponent<DiceRollerDO>().DiceValue);
        }
        // player ve rivall zarlarý sýralandý

        activeRivalDiceValueSortedLists.Sort();
        activePlayerDiceValueSortedLists.Sort();

        // sýralý listedeki sýraya göre zarlar yeniden listelendi
        for( int i=0; i< activeRivalDiceLists.Count; i++)
        {
            for( int j=0;j< activeRivalDiceLists.Count; j++)
            {
                if (activeRivalDiceLists[j].GetComponent<DiceRollerDO>().DiceValue == activeRivalDiceValueSortedLists[i])
                {
                    activeRivalDiceSortedLists.Add(activeRivalDiceLists[j]);
                }
            }
        }

        for (int i = 0; i < activePlayerDiceLists.Count; i++)
        {
            for (int j = 0; j < activePlayerDiceLists.Count; j++)
            {
                if (activePlayerDiceLists[j].GetComponent<DiceRollerDO>().DiceValue == activePlayerDiceValueSortedLists[i])
                {
                    activePlayerDiceSortedLists.Add(activePlayerDiceLists[j]);
                }
            }
        }
    }
    
}
