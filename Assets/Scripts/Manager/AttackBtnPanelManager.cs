using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackBtnPanelManager : MonoBehaviour
{
    public GameObject attacBtn, plunderBtn, retreatBtn;
    public TMP_Text enemyRegionNameText, enemyRegionDamageText, enemyTotalArmyPowerText, playerRegionNameText, playerRegionDamageText, playerTotalArmyPowerText, playerGeneralText,enemyGeneralText,playerWonCountText,enemyWonCountText;
    public Image EnemyFlagSprite, playerFlagSprite, playerGeneralSprite, enemyGeneralSprite;

    private void OnEnable()
    {
        ShowAttackResultBtn();
    }
    private void ShowAttackResultBtn()
    {
        State attackingState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastAttackingState).GetComponent<State>();
        State defendingState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastDefendingState).GetComponent<State>();
        if( attackingState != null && defendingState != null )
        {
            enemyRegionNameText.text = defendingState.gameObject.name + "";
            enemyRegionDamageText.text = "(- " + (int)defendingState.loss + " )";
            enemyTotalArmyPowerText.text = (int)defendingState.TotalArmyCalculator() + "";
            if (defendingState.StateIcon != null)
            {
                //   Debug.Log($"{defendingState.StateIcon.name}");
                EnemyFlagSprite.sprite = defendingState.StateIcon;
            }
            else
                Debug.LogWarning("stae icon bulunamadý");



            playerRegionNameText.text = attackingState.gameObject.name + "";
            playerRegionDamageText.text = "(- " + (int)attackingState.loss + " )";
            playerTotalArmyPowerText.text = (int)attackingState.TotalArmyCalculator() + "";
            playerFlagSprite.sprite = attackingState.StateIcon;

            if (attackingState.attackCanvasButtonPanelIndex == 3)
            {
                retreatBtn.SetActive(true);
            }
            else if (defendingState.attackCanvasButtonPanelIndex == 1)
            {
                attacBtn.SetActive(true);
                retreatBtn.SetActive(true);

            }
            else
            {
                attacBtn.SetActive(true);
                plunderBtn.SetActive(true);
            }
        }else
        {
            Debug.LogWarning(" state is null");
        }
        
    }
    private void OnDisable()
    {
        attacBtn.SetActive(false);
        retreatBtn.SetActive(false);
        plunderBtn.SetActive(false);
    }

}
