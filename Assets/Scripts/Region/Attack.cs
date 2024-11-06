using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using System.Net;

using UnityEditor;
public class Attack : MonoBehaviour
{
    public static Attack Instance;
    public string attackingStateText;
    Sprite StateIcon;
   
    public Transform USA_Transform;
    float diceLowerLimit = 0.5f, diceMidLimit=0.75f, diceUpperLimit=1.0f;
    public  int diceCount;

    public float attackDuration = 2.0f;
    float moveForWarDuration = 3.0f;
    public string lastAttackingState ,  lastDefendingState;
    float oneAttackDuration = 0.5f;
    public float numberOfDiceWonByThePlayer;
    public float numberOfDiceWonByTheRival;
    public float numberOfDrew;



    private void Awake()
    {
        if (Instance == null)
        {
            Destroy(Instance);
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject); 
        }
    }
    private void OnEnable()
    {
     numberOfDiceWonByThePlayer = 0;
     numberOfDiceWonByTheRival = 0;
     numberOfDrew = 0;

}
public IEnumerator AttackingCoroutine(string defendingState, bool isFirst= false)
    {

        if (RegionClickHandler.Instance.currentState == null)
        {
            RegionClickHandler.staticState = GameManager.AllyStateList.OrderBy(state => state.GetTotalArmyPower()).FirstOrDefault();
            RegionClickHandler.Instance.currentState = RegionClickHandler.staticState.gameObject;

            attackingStateText = RegionClickHandler.staticState.name;
            //   Debug.LogError(" attacikn state " + attackingStateText);
            List<Node> path = PathFindDeneme.PathInstance.GetPath(defendingState, attackingStateText);
            foreach (Node node in path)
            {

                Usa.Instance.FindStateByName(node.Name).transform.GetComponentInChildren<Flag>().flagList[0].SetActive(true);

            }
            Usa.Instance.FindStateByName(path[0].Name).transform.GetComponentInChildren<Flag>().flagList[1].SetActive(true);

            for (int i = 1; i < path.Count; i++)
            {
                yield return new WaitForSeconds(1.0f);
                Usa.Instance.FindStateByName(path[i - 1].Name).transform.GetComponentInChildren<Flag>().flagList[2].SetActive(true);
                Usa.Instance.FindStateByName(path[i].Name).transform.GetComponentInChildren<Flag>().flagList[1].SetActive(true);
            }
            yield return new WaitForSeconds(1.0f);
            foreach (Node node in path)
            {

                Usa.Instance.FindStateByName(node.Name).transform.GetComponentInChildren<Flag>().flagList[0].SetActive(false);
                Usa.Instance.FindStateByName(node.Name).transform.GetComponentInChildren<Flag>().flagList[1].SetActive(false);
                Usa.Instance.FindStateByName(node.Name).transform.GetComponentInChildren<Flag>().flagList[2].SetActive(false);

            }
        }
        else if (isFirst)
        {



            attackingStateText = RegionClickHandler.staticState.name;
            //   Debug.LogError(" attacikn state " + attackingStateText);
            List<Node> path = PathFindDeneme.PathInstance.GetPath(attackingStateText, defendingState);
            if (path != null && path.Count > 0)
            {
                if (path.Count > 2)
                {

                    int loss = UnityEngine.Random.Range(5, 25);
                    //  Debug.Log("Kayýp miktarý  % " + loss + " kayýpdan önce total amry: " + RegionClickHandler.staticState.GetArmySize());
                    RegionClickHandler.staticState.GetComponent<State>().ReduceArmySize(loss);
                    //  Debug.Log("kayýptan sonra " + RegionClickHandler.staticState.GetArmySize());
                    MessageManager.AddMessage("Ambushed on enemy soil, we suffered a grievous blow and lost " + loss + "% of our men.");
                }
                foreach (Node node in path)
                {

                    Usa.Instance.FindStateByName(node.Name).transform.GetComponentInChildren<Flag>().flagList[0].SetActive(true);

                }
                Usa.Instance.FindStateByName(path[0].Name).transform.GetComponentInChildren<Flag>().flagList[1].SetActive(true);

                for (int i = 1; i < path.Count; i++)
                {
                    yield return new WaitForSeconds(1.0f);
                    Usa.Instance.FindStateByName(path[i - 1].Name).transform.GetComponentInChildren<Flag>().flagList[2].SetActive(true);
                    Usa.Instance.FindStateByName(path[i].Name).transform.GetComponentInChildren<Flag>().flagList[1].SetActive(true);
                }
                yield return new WaitForSeconds(1.0f);
                foreach (Node node in path)
                {

                    Usa.Instance.FindStateByName(node.Name).transform.GetComponentInChildren<Flag>().flagList[0].SetActive(false);
                    Usa.Instance.FindStateByName(node.Name).transform.GetComponentInChildren<Flag>().flagList[1].SetActive(false);
                    Usa.Instance.FindStateByName(node.Name).transform.GetComponentInChildren<Flag>().flagList[2].SetActive(false);

                }
            }
        }
        else
        {
            attackingStateText = RegionClickHandler.staticState.name;
        }


        //   Debug.LogWarning("attakicn state " + attackingStateText);       
        lastDefendingState = defendingState;
        lastAttackingState = attackingStateText;
        yield return null;
        

        DiceManager2.Instance.StartDiceDisActivated(GameManager.Instance.attackFinishDurtion);
        // Print attacking and defending state
      //  Debug.Log("Saldýran: " + attackingStateText + " Savunan: " + defendingState);

        // Trim strings and convert to a common case (e.g., lower case) before comparison
         string attackingState = attackingStateText;


        GameObject attackingStateGameObject = FindChildByName(USA_Transform, attackingState);
        GameObject defendingStateGameObject = FindChildByName(USA_Transform, defendingState);
        float attackingStateTotalArmyPower, defendingStateTotalArmyPower;

        // Further checks and logic for attacking

      
        
            // No loss attack
           // Debug.Log("Kayýpsýz saldýrý gerçekleþti.");
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
            StartCoroutine(WarCalculator(defendingStateGameObject, attackingStateGameObject));
        
        
    }
    IEnumerator WarCalculator( GameObject defendingStateGameObject,GameObject attackingStateGameObject )
    {
        DiceManager2.Instance.DiceActiveted(diceCount);
        numberOfDiceWonByThePlayer = 0;
        numberOfDiceWonByTheRival = 0;
        numberOfDrew = 0;

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
                    yield return new WaitForSeconds(oneAttackDuration);
                    
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
                    yield return new WaitForSeconds(oneAttackDuration);

                    diceComponent.DiceMoveForFight(target, moveForWarDuration );
                }
                else
                {
                    Debug.LogWarning("Dice2 bileþeni bulunamadý: ");
                }
            }
            else
            {
                yield return new WaitForSeconds(oneAttackDuration);
                numberOfDrew++;
                GameObject target = DiceManager2.Instance.activePlayerDiceSortedLists[i]; ;
               
               StartCoroutine(DiceManager2.Instance.activeRivalDiceSortedLists[i].GetComponent<Dice2>().StartDiceShackForDrawWar(target, moveForWarDuration));
                
            }
        }
     //   Debug.Log($" player {numberOfDiceWonByThePlayer} drew {numberOfDrew} rival {numberOfDiceWonByTheRival}");
        defendingStateGameObject.GetComponent<State>().LostWar((float)((numberOfDiceWonByThePlayer + numberOfDrew) / DiceManager2.Instance.activeRivalDiceLists.Count));
        attackingStateGameObject.GetComponent<State>().LostWar((float)((numberOfDiceWonByTheRival + numberOfDrew) / DiceManager2.Instance.activePlayerDiceLists.Count));
        StopCoroutine("WarCalculator");
        StopCoroutine("AttackingCoroutine");

    }
    public void Attacking(string defendingState, bool isFirst=false)
    {

        GameManager.Instance.IsAttackFinish = false;
       
        if(isFirst)
        {
            Usa.Instance.FindStateByName(defendingState).ReduceEnemyMorale(-75);
            MessageManager.AddMessage($"After the battle, your relations with {defendingState} have dropped .");
        }
       
       // Debug.LogWarning(defendingState);
        StartCoroutine(AttackingCoroutine(defendingState, isFirst));
    }

    public void AttackAgain()
    {
        GameManager.Instance.IsAttackFinish = false;
        
        GameManager.Instance.ChangeIsAttackValueFalse();
        //GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
        Attacking(lastDefendingState);
    }
    public void  AttackFinished()
    {
        GameManager.Instance.IsAttackFinish = true;
        GameManager.Instance.isGamePause = false;
    }

   public GameObject FindChildByName(Transform parent, string name)
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

        if((attackingStateTotalArmyPower*diceLowerLimit)>defendingStateTotalArmyPower)
        {
           // Debug.LogWarning($" attack state army {attackingStateTotalArmyPower} def army {defendingStateTotalArmyPower}");
            return 1;
        }
        else if ((attackingStateTotalArmyPower * diceMidLimit) > defendingStateTotalArmyPower)
        {
            return 2;
        }
        else if ((attackingStateTotalArmyPower * diceUpperLimit) > defendingStateTotalArmyPower)
        {
            return 3;
        }
        else
        return 4;
    }
   

}
