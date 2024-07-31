using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager2 : MonoBehaviour
{
    public static DiceManager2 Instance;


    public List<GameObject> activeRivalDiceLists = new List<GameObject>();
    public List<GameObject> activePlayerDiceLists = new List<GameObject>();

    public List<GameObject> activeRivalDiceSortedLists = new List<GameObject>();
    public List<GameObject> activePlayerDiceSortedLists = new List<GameObject>();

    public List<int> activeRivalDiceValueSortedLists = new List<int>();
    public List<int> activePlayerDiceValueSortedLists = new List<int>();

    private float attackFinishDuration ;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

       
    }
    Vector3 offset = new Vector3(0, 0, 10);

    private void Start()
    {
        
        attackFinishDuration = GameManager.Instance.attackFinidhDuration;
    }

    public void DiceActiveted(int rivalDiceCount)
    {

        for (int i = 0; i < rivalDiceCount; i++)
        {
            GameObject dice = DiceSpawnner2.Instance.rivalDiceLists[i].gameObject;
            dice.SetActive(true);
             dice.GetComponent<DiceRollerDO2>().RollDice();
            dice.GetComponent<Dice2>().DiceRoll();
            activeRivalDiceLists.Add(dice);

        }
        for (int i = 0; i < 3; i++)
        {
            GameObject dice = DiceSpawnner2.Instance.playerDiceLists[i].gameObject;
            dice.SetActive(true);
            dice.GetComponent<DiceRollerDO2>().RollDice();
            dice.GetComponent<Dice2>().DiceRoll();
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
        yield return new WaitForSeconds(attackFinishDuration);
        // Debug.LogWarning(" dis aktivated baþladý");
        activePlayerDiceLists.Clear();
        activeRivalDiceLists.Clear();
        for (int i = 0; i < 4; i++)
        {
            GameObject dice = DiceSpawnner2.Instance.rivalDiceLists[i].gameObject;
            dice.transform.position = dice.transform.parent.transform.position + (offset * i);
            dice.SetActive(false);


        }
        for (int i = 0; i < 3; i++)
        {
            GameObject dice = DiceSpawnner2.Instance.playerDiceLists[i].gameObject;
            dice.transform.position = dice.transform.parent.transform.position + (offset * i);
            dice.SetActive(false);


        }

    }
    public void SortDiceList()
    {
        // player ve rival zarlarý listelere eklendi 
        foreach (var dice in activeRivalDiceLists)
        {
            activeRivalDiceValueSortedLists.Add(dice.gameObject.GetComponent<DiceRollerDO2>().DiceValue);
        }

        foreach (var dice in activePlayerDiceLists)
        {
            activePlayerDiceValueSortedLists.Add(dice.gameObject.GetComponent<DiceRollerDO2>().DiceValue);
        }
        // player ve rivall zarlarý sýralandý

        activeRivalDiceValueSortedLists.Sort();
        activePlayerDiceValueSortedLists.Sort();

        // sýralý listedeki sýraya göre zarlar yeniden listelendi
        for (int i = 0; i < activeRivalDiceLists.Count; i++)
        {
            for (int j = 0; j < activeRivalDiceLists.Count; j++)
            {
                if (activeRivalDiceLists[j].GetComponent<DiceRollerDO2>().DiceValue == activeRivalDiceValueSortedLists[i])
                {
                    activeRivalDiceSortedLists.Add(activeRivalDiceLists[j]);
                }
            }
        }

        for (int i = 0; i < activePlayerDiceLists.Count; i++)
        {
            for (int j = 0; j < activePlayerDiceLists.Count; j++)
            {
                if (activePlayerDiceLists[j].GetComponent<DiceRollerDO2>().DiceValue == activePlayerDiceValueSortedLists[i])
                {
                    activePlayerDiceSortedLists.Add(activePlayerDiceLists[j]);
                }
            }
        }
    }

}
