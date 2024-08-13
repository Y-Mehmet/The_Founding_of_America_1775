using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
public class AttackCanvas : MonoBehaviour
{
  public static AttackCanvas Instance { get; private set; }

    [Header(" enemy region panel panel ")]
    public TextMeshProUGUI enemyRegionNameText;
    public Sprite EnemyFlagSprite;
    public TextMeshProUGUI enemyRegionDamageText;
    public Sprite EnemyGeneralSprite;

    [Header(" player region panel panel ")]
    public TextMeshProUGUI playerRegionNameText;
    public Sprite playerFlagSprite;
    public TextMeshProUGUI playerRegionDamageText;
    public Image playerGeneralSprite;
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
        playerRegionNameText.text = attackingState.gameObject.name + "";
        playerRegionDamageText.text = "(- " +(int) attackingState.loss + " )";

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
