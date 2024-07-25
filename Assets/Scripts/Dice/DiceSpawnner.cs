using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawnner : MonoBehaviour
{

    public Transform rivalDicePoint, playerDicePoint;
    public GameObject dicePrefab;
    public static DiceSpawnner Instance;
    public List<GameObject> rivalDiceLists = new List<GameObject>();
    public List<GameObject> playerDiceLists = new List<GameObject>();


    private Vector3  offset = new Vector3(0, 0, 10);

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
       
    }
    private void Start()
    {
        SpawnDice();
    }
    public  void SpawnDice()
    {
        for (int i = 0; i < 4; i++)
        {
          GameObject rivalDice=  Instantiate(dicePrefab, rivalDicePoint);
            rivalDice.transform.position = rivalDicePoint.position+ i*offset;
            rivalDice.SetActive(false);
            rivalDiceLists.Add(rivalDice);
           
            
        }
        for(int i = 0;i < 3;i++) {
            GameObject playerDice = Instantiate(dicePrefab, playerDicePoint);
            playerDice.transform.position = playerDicePoint.position + i *offset;    
            playerDice.GetComponent<DiceRollerDO>().IsPlayerDice = true;
            playerDice.SetActive(false);
            playerDiceLists.Add(playerDice);
           
        }
    }
}
