using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Net;
public class Attack : MonoBehaviour
{
    public static Attack Instance;
    public TextMeshProUGUI attackingStateText;
    public Transform USA_Transform;
    float diceLowerLimit = 0.5f, diceMidLimit=.75f, diceUpperLimit=1.25f;
    public  int diceCount;

    public float attackDuration = 1.0f;
   

    private void Awake()
    {
        if (Instance == null)
        {
            Destroy(Instance);
            Instance = this;
            
        }
        else
        {
            Destroy(Instance.gameObject); // Use .gameObject to destroy the existing instance properly
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If needed, update things here
    }

    // Method to handle the attack
    public void Attacking(string defendingState)
    {
        DiceManager2.Instance.StartDiceDisActivated();
        // Print attacking and defending state
        Debug.Log("Sald�ran: " + attackingStateText.text + " Savunan: " + defendingState );

        // Trim strings and convert to a common case (e.g., lower case) before comparison
        string attackingState = attackingStateText.text.Substring(6);

        GameObject attackingStateGameObject = FindChildByName(USA_Transform, attackingState);
        GameObject defendingStateGameObject = FindChildByName(USA_Transform, defendingState);
        float attackingStateTotalArmyPower, defendingStateTotalArmyPower;
        
        
        

        // Further checks and logic for attacking

        // Check if the defending state is a neighbor
        if (Neighbor.Instance.AreNeighbors(attackingState, defendingState))
        {
            // No loss attack
            Debug.Log("Kay�ps�z sald�r� ger�ekle�ti.");
            if (attackingStateGameObject != null && defendingStateGameObject != null)
            {
                attackingStateTotalArmyPower = attackingStateGameObject.GetComponent<State>().TotalArmyCalculator();
                defendingStateTotalArmyPower = defendingStateGameObject.GetComponent<State>().TotalArmyCalculator();
                diceCount = DiceCountCalcuation(attackingStateTotalArmyPower, defendingStateTotalArmyPower);
            }
            else
            {
                Debug.LogError("statelr bulunamad�");
            }
            // DiceSpawnner.Instance.SpawnDice(diceCount);
            DiceManager2.Instance.DiceActiveted(diceCount);
            int numberOfDiceWonByThePlayer = 0;
            int numberOfDiceWonByTheRival = 0;
            int numberOfDrew = 0;
            for (int i=0; i< DiceManager2.Instance.activeRivalDiceValueSortedLists.Count; i++)
            {
               
                if (DiceManager2.Instance.activeRivalDiceValueSortedLists[i] < DiceManager2.Instance.activePlayerDiceValueSortedLists[i])
                {
                    numberOfDiceWonByThePlayer++;
                    Vector3 target = DiceManager2.Instance.activeRivalDiceSortedLists[i].gameObject.transform.position;


                    // Zar�n h�zlanarak �arpmas�n� sa�lamak i�in Ease.InQuad kullan�l�yor
                    DiceManager2.Instance.activeRivalDiceSortedLists[i].GetComponent<Dice2>().
                    DiceMoveForFight(target, attackDuration);



                }
                else if (DiceManager2.Instance.activeRivalDiceValueSortedLists[i] > DiceManager2.Instance.activePlayerDiceValueSortedLists[i])
                {
                    numberOfDiceWonByTheRival++;

                    Vector3 target = DiceManager2.Instance.activeRivalDiceSortedLists[i].gameObject.transform.position;


                    // Zar�n h�zlanarak �arpmas�n� sa�lamak i�in Ease.InQuad kullan�l�yor
                    DiceManager2.Instance.activeRivalDiceSortedLists[i].GetComponent<Dice2>().
                    DiceMoveForFight(target, attackDuration);

                }
                else
                {
                    numberOfDrew++;

                    Vector3 target = DiceManager2.Instance.activeRivalDiceSortedLists[i].gameObject.transform.position;


                    // Zar�n h�zlanarak �arpmas�n� sa�lamak i�in Ease.InQuad kullan�l�yor
                    DiceManager2.Instance.activeRivalDiceSortedLists[i].GetComponent<Dice2>().
                    DiceMoveForFight(target, attackDuration);


                }

            }
            defendingStateGameObject.GetComponent<State>().LostWar(((numberOfDiceWonByThePlayer + numberOfDrew) / 3));
            attackingStateGameObject.GetComponent<State>().LostWar(((numberOfDiceWonByTheRival + numberOfDrew) / 3));
           // DiceManager2.Instance.DiceDisactiveted();







        }
        else
        {
            // Calculate random loss between 5% and 25%
            float loss = UnityEngine.Random.Range(5f, 25f);
          
            Debug.Log("Kay�p: % " + loss);
            attackingStateGameObject.GetComponent<State>().ReduceArmySize(loss);
           
            if (attackingStateGameObject != null && defendingStateGameObject != null)
            {
                attackingStateTotalArmyPower = attackingStateGameObject.GetComponent<State>().TotalArmyCalculator();
                defendingStateTotalArmyPower = defendingStateGameObject.GetComponent<State>().TotalArmyCalculator();
                diceCount = DiceCountCalcuation(attackingStateTotalArmyPower, defendingStateTotalArmyPower);
            }
            else
            {
                Debug.LogError("statelr bulunamad�");
            }
            // DiceSpawnner.Instance.SpawnDice(diceCount);
            DiceManager2.Instance.DiceActiveted(diceCount);

            int numberOfDiceWonByThePlayer = 0;
            int numberOfDiceWonByTheRival = 0;
            int numberOfDrew = 0;
            for (int i = 0; i < DiceManager2.Instance.activeRivalDiceValueSortedLists.Count; i++)
            {

                if (DiceManager2.Instance.activeRivalDiceValueSortedLists[i] < DiceManager2.Instance.activePlayerDiceValueSortedLists[i])
                {
                    numberOfDiceWonByThePlayer++;
                }
                else if (DiceManager2.Instance.activeRivalDiceValueSortedLists[i] > DiceManager2.Instance.activePlayerDiceValueSortedLists[i])
                {
                    numberOfDiceWonByTheRival++;
                }
                else
                {
                    numberOfDrew++;
                }

            }
            defendingStateGameObject.GetComponent<State>().LostWar(((numberOfDiceWonByThePlayer + numberOfDrew) / DiceManager2.Instance.activeRivalDiceLists.Count));
           
            attackingStateGameObject.GetComponent<State>().LostWar(((numberOfDiceWonByTheRival + numberOfDrew) / DiceManager2.Instance.activePlayerDiceLists.Count));

           


        }
    }
   
    GameObject FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }

            GameObject found = FindChildByName(child, name);
            if (found != null)
            {
                return found;
            }
        }
        return null;
    }
    int  DiceCountCalcuation(float attackingStateTotalArmyPower,float defendingStateTotalArmyPower)
    {

        if((attackingStateTotalArmyPower*diceLowerLimit)<defendingStateTotalArmyPower)
        {
            return 1;
        }
        else if ((attackingStateTotalArmyPower * diceMidLimit) < defendingStateTotalArmyPower)
        {
            return 2;
        }
        else if ((attackingStateTotalArmyPower * diceUpperLimit) < defendingStateTotalArmyPower)
        {
            return 3;
        }
        else
        return 4;
    }

}
