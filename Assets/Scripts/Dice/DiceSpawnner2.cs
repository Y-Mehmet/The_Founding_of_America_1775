using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawnner2 : MonoBehaviour
{

    public Transform rivalDicePoint, playerDicePoint,rivalDiceTarget,playerDiceTarget;
    public GameObject dicePrefab;
    public static DiceSpawnner2 Instance;
    public List<GameObject> rivalDiceLists = new List<GameObject>();
    public List<GameObject> playerDiceLists = new List<GameObject>();
    public Material rivalDiceMatarial, playerDiceMatarial;
    public float firstScale = 0.035f;


    private Vector3 offset = new Vector3(20, 0, 0);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }
    private void Start()
    {
        SpawnDice();
    }
    public void SpawnDice()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject rivalDice = Instantiate(dicePrefab, rivalDicePoint);
            rivalDice.transform.position = rivalDicePoint.position + i * offset;
            rivalDice.GetComponent<Dice2>().target = rivalDiceTarget.position+ i* offset;
            rivalDice.GetComponent<Renderer>().material= rivalDiceMatarial;
            rivalDice.transform.localScale = Vector3.one * firstScale;
            rivalDice.SetActive(false);
            rivalDiceLists.Add(rivalDice);


        }
        for (int i = 0; i < 3; i++)
        {
            GameObject playerDice = Instantiate(dicePrefab, playerDicePoint);
            playerDice.transform.position = playerDicePoint.position + i * offset;
            playerDice.GetComponent<DiceRollerDO2>().IsPlayerDice = true;
            playerDice.GetComponent<Dice2>().target = playerDiceTarget.position + i * offset; ;
            playerDice.GetComponent<Renderer>().material = playerDiceMatarial;
            playerDice.transform.localScale = Vector3.one * firstScale;
            playerDice.SetActive(false);
            playerDiceLists.Add(playerDice);

        }
    }
}
