using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class AttackCanvas : MonoBehaviour
{
  public static AttackCanvas Instance { get; private set; }

    [Header(" enemy region panel panel ")]
    public TextMeshProUGUI enemyRegionNameText;
    public TextMeshProUGUI enemyRegionDamageText;
    public Image EnemyGeneralSprite;
    public TextMeshProUGUI enemyTotalArmyPowerText;
    public Image EnemyFlagSprite;

    [Header(" player region panel panel ")]
    public TextMeshProUGUI playerRegionNameText;
    public Image playerFlagSprite;
    public TextMeshProUGUI playerRegionDamageText;
    public Image playerGeneralSprite;
    public TextMeshProUGUI playerTotalArmyPowerText;

    [Header("savaþ sonucuna göre açýlacak buton panelleri")]
    public GameObject attackPanelIndex1;
    public GameObject decisionPanelIndex2;
    public GameObject retreatePanelIndex3;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


    }
    public void ShowAttackCanvasInfo()
    {
        State attackingState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastAttackingState).GetComponent<State>();
        State defendingState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastDefendingState).GetComponent<State>();
       
        enemyRegionNameText.text = defendingState.gameObject.name + "";
        enemyRegionDamageText.text = "(- "+(int)defendingState.loss+" )";
        enemyTotalArmyPowerText.text= (int)defendingState.TotalArmyCalculator()+"";
        if (defendingState.StateIcon != null)
        {
         //   Debug.Log($"{defendingState.StateIcon.name}");
            EnemyFlagSprite.sprite = defendingState.StateIcon;
        }
        else
            Debug.LogWarning("stae icon bulunamadý");
        


        playerRegionNameText.text = attackingState.gameObject.name + "";
        playerRegionDamageText.text = "(- " +(int) attackingState.loss + " )";
        playerTotalArmyPowerText.text = (int)attackingState.TotalArmyCalculator()+"";
        playerFlagSprite.sprite = attackingState.StateIcon;

        attackPanelIndex1.gameObject.SetActive(false);
        decisionPanelIndex2.gameObject.SetActive(false);
        retreatePanelIndex3.gameObject.SetActive(false);


        if(attackingState.attackCanvasButtonPanelIndex==3)
        {
            retreatePanelIndex3.gameObject.SetActive(true);
        }
        else if(defendingState.attackCanvasButtonPanelIndex==2)
        {
            decisionPanelIndex2.gameObject.SetActive(true);
        }else
        {
            attackPanelIndex1.gameObject.SetActive(true);
        }
      



    }

}
