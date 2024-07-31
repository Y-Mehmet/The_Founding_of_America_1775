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

    public float attackDuration = 2.0f;
    float moveForWarDuration = 3.0f;
    public string lastAttackingState ,  lastDefendingState;



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

    public IEnumerator AttackingCoroutine(string defendingState)
    {
        lastDefendingState = defendingState;
        yield return null;
        GameManager.Instance.IsAttackFinish = false;

        DiceManager2.Instance.StartDiceDisActivated(GameManager.Instance.attackFinishDurtion);
        // Print attacking and defending state
        Debug.Log("Saldýran: " + attackingStateText.text + " Savunan: " + defendingState);

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
            Debug.Log("Kayýpsýz saldýrý gerçekleþti.");
            if (attackingStateGameObject != null && defendingStateGameObject != null)
            {
                attackingStateTotalArmyPower = attackingStateGameObject.GetComponent<State>().TotalArmyCalculator();
                defendingStateTotalArmyPower = defendingStateGameObject.GetComponent<State>().TotalArmyCalculator();
                diceCount = DiceCountCalcuation(attackingStateTotalArmyPower, defendingStateTotalArmyPower);
            }
            else
            {
                Debug.LogError("eyaletler bulunamadý");
            }
            WarCalculator(defendingStateGameObject, attackingStateGameObject);
        }
        else
        {
            // Calculate random loss between 5% and 25%
            float loss = UnityEngine.Random.Range(5f, 25f);
            Debug.Log("Kayýp: % " + loss);
            attackingStateGameObject.GetComponent<State>().ReduceArmySize(loss);

            if (attackingStateGameObject != null && defendingStateGameObject != null)
            {
                attackingStateTotalArmyPower = attackingStateGameObject.GetComponent<State>().TotalArmyCalculator();
                defendingStateTotalArmyPower = defendingStateGameObject.GetComponent<State>().TotalArmyCalculator();
                diceCount = DiceCountCalcuation(attackingStateTotalArmyPower, defendingStateTotalArmyPower);
            }
            else
            {
                Debug.LogError("statelr bulunamadý");
            }
            WarCalculator(defendingStateGameObject, attackingStateGameObject);
        }
    }
    void WarCalculator( GameObject defendingStateGameObject,GameObject attackingStateGameObject )
    {
        DiceManager2.Instance.DiceActiveted(diceCount);
        int numberOfDiceWonByThePlayer = 0;
        int numberOfDiceWonByTheRival = 0;
        int numberOfDrew = 0;

        for (int i = 0; i < DiceManager2.Instance.activePlayerDiceLists.Count; i++)
        {
            if (i >= DiceManager2.Instance.activeRivalDiceLists.Count)
                break;
            if (DiceManager2.Instance.activeRivalDiceValueSortedLists[i] < DiceManager2.Instance.activePlayerDiceValueSortedLists[i])
            {
                numberOfDiceWonByThePlayer++;

                GameObject target = DiceManager2.Instance.activeRivalDiceSortedLists[i];
             
                if (DiceManager2.Instance.activePlayerDiceSortedLists[i].TryGetComponent<Dice2>(out Dice2 diceComponent))
                {
                    
                    diceComponent.DiceMoveForFight(target, moveForWarDuration);
                }
                else
                {
                    Debug.LogWarning("Dice2 bileþeni bulunamadý: " );
                }
            }
            else if (DiceManager2.Instance.activeRivalDiceValueSortedLists[i] > DiceManager2.Instance.activePlayerDiceValueSortedLists[i])
            {
                numberOfDiceWonByTheRival++;
                GameObject target = DiceManager2.Instance.activePlayerDiceSortedLists[i];
              

                if (DiceManager2.Instance.activeRivalDiceSortedLists[i].TryGetComponent<Dice2>(out Dice2 diceComponent))
                {
                   
                    diceComponent.DiceMoveForFight(target, moveForWarDuration );
                }
                else
                {
                    Debug.LogWarning("Dice2 bileþeni bulunamadý: ");
                }
            }
            else
            {
                numberOfDrew++;
                GameObject target = DiceManager2.Instance.activeRivalDiceSortedLists[i]; ;
               
                DiceManager2.Instance.activeRivalDiceSortedLists[i].GetComponent<Dice2>().DiceMoveForFight(target, moveForWarDuration);
                GameObject target2 = DiceManager2.Instance.activePlayerDiceSortedLists[i]; 

                DiceManager2.Instance.activePlayerDiceSortedLists[i].GetComponent<Dice2>().DiceMoveForFight(target2, moveForWarDuration);
            }
        }

        defendingStateGameObject.GetComponent<State>().LostWar(((numberOfDiceWonByThePlayer + numberOfDrew) / DiceManager2.Instance.activeRivalDiceLists.Count));
        attackingStateGameObject.GetComponent<State>().LostWar(((numberOfDiceWonByTheRival + numberOfDrew) / DiceManager2.Instance.activePlayerDiceLists.Count));
    }
    public void Attacking(string defendingState)
    {
        StartCoroutine(AttackingCoroutine(defendingState));
    }

    public void AttackAgain()
    {
        GameManager.Instance.IsAttackFinish = false;
        GameManager.Instance.ChangeIsAttackValueFalse();
        GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
        Attacking(lastDefendingState);
    }
    public void  AttackFinished()
    {
        GameManager.Instance.IsAttackFinish = true;
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
