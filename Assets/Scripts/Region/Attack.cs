using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static Attack Instance;
    public TextMeshProUGUI attackingStateText;
    public Transform USA_Transform;
    float diceLowerLimit = 0.5f, diceMidLimit=.75f, diceUpperLimit=1.25f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject); // Use .gameObject to destroy the existing instance properly
        }
        else
        {
            Instance = this;
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
        // Print attacking and defending state
        Debug.Log("Saldýran: " + attackingStateText.text + " Savunan: " + defendingState );

        // Trim strings and convert to a common case (e.g., lower case) before comparison
        string attackingState = attackingStateText.text.Substring(6);

        GameObject attackingStateGameObject = FindChildByName(USA_Transform, attackingState);
        GameObject defendingStateGameObject = FindChildByName(USA_Transform, defendingState);
        float attackingStateTotalArmyPower, defendingStateTotalArmyPower;
        int diceCount;
        if (attackingStateGameObject != null && defendingStateGameObject != null)
        {
            attackingStateTotalArmyPower = attackingStateGameObject.GetComponent<State>().TotalArmyPower;
            defendingStateTotalArmyPower= defendingStateGameObject.GetComponent<State>().TotalArmyPower;
           diceCount= DiceCountCalcuation(attackingStateTotalArmyPower, defendingStateTotalArmyPower);
        }
        else
        {
            Debug.LogError("statelr bulunamadý");
        }
        

        // Further checks and logic for attacking

        // Check if the defending state is a neighbor
        if (Neighbor.Instance.AreNeighbors(attackingState, defendingState))
        {
            // No loss attack
            Debug.Log("Kayýpsýz saldýrý gerçekleþti.");


           



        }
        else
        {
            // Calculate random loss between 5% and 25%
            float loss = UnityEngine.Random.Range(5f, 25f);
          
            Debug.Log("Kayýp: % " + loss);
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
