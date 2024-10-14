using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static GeneralManager;

public class AttackBtnPanelManager : MonoBehaviour
{
    public GameObject attacBtn, plunderBtn, retreatBtn;
    public TMP_Text enemyRegionNameText, enemyRegionDamageText, enemyTotalArmyPowerText, playerRegionNameText, playerRegionDamageText, playerTotalArmyPowerText, playerGeneralText,enemyGeneralText,playerWonCountText,enemyWonCountText;
    public Image EnemyFlagSprite, playerFlagSprite, playerGeneralSprite, enemyGeneralSprite;
    string generalIndex ;
    string warDate;
    WarResultType warResultType;
    
    private void OnEnable()
    {
        generalIndex = "-";
        ShowAttackResultBtn();
    }
    private void ShowAttackResultBtn()
    {
        State attackingState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastAttackingState).GetComponent<State>();
        State defendingState = Attack.Instance.FindChildByName(Usa.Instance.transform, Attack.Instance.lastDefendingState).GetComponent<State>();
        if( attackingState != null && defendingState != null )
        {
            var stateAndGeneral = GeneralManager.stateGenerals.FirstOrDefault(x => x.Key == attackingState);

            // FirstOrDefault kullanýldýðýnda, eðer eþleþme yoksa KeyValuePair'in varsayýlan deðeri döner,
            // bu nedenle .Key'yi kontrol etmek gerekir
            if (!stateAndGeneral.Equals(default(KeyValuePair<State, General>)))
            {
                generalIndex = generals[(int)stateAndGeneral.Value.Specialty].Name;
            }
            else
            {
                Debug.LogWarning("Bu state için atanmýþ bir general bulunamadý.");
            }
            warDate = GameDateManager.instance.GetCurrentDataString();
            if( Attack.Instance.numberOfDiceWonByThePlayer>Attack.Instance.numberOfDiceWonByTheRival)
            {
                warResultType = WarResultType.Victory;
            }else if (Attack.Instance.numberOfDiceWonByThePlayer < Attack.Instance.numberOfDiceWonByTheRival)
            {
                warResultType = WarResultType.Defeat;
            }else
            {
                warResultType = WarResultType.Draw;
            }
            War war=new(generalIndex ,attackingState,defendingState,warResultType,warDate);

            WarHistory.generalIndexAndWarList.Push( war);

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
