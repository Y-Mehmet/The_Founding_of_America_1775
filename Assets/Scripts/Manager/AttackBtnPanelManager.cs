using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using static GeneralManager;
using static GeneralText;
using Random = UnityEngine.Random;

public class AttackBtnPanelManager : MonoBehaviour
{
    public GameObject attacBtn, plunderBtn, retreatBtn,annexBtn;
    public TMP_Text enemyRegionNameText, enemyRegionDamageText, enemyTotalArmyPowerText, playerRegionNameText,
        playerRegionDamageText, playerTotalArmyPowerText, playerGeneralText, enemyGeneralText, playerWonCountText, enemyWonCountText;
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
            var stateAndGeneral = stateGenerals.FirstOrDefault(x => x.Key == attackingState);

            // FirstOrDefault kullanýldýðýnda, eðer eþleþme yoksa KeyValuePair'in varsayýlan deðeri döner,
            // bu nedenle .Key'yi kontrol etmek gerekir
            if (!stateAndGeneral.Equals(default(KeyValuePair<State, General>)))
            {
                generalIndex = generals[(int)stateAndGeneral.Value.Specialty].Name;
                playerGeneralSprite.sprite = GameDataSo.Instance.GeneralSprite[(int)stateAndGeneral.Value.Specialty];
                
            }
            else
            {
                Debug.LogWarning("Bu state için atanmýþ bir general bulunamadý.");
            }
            playerWonCountText.text = Attack.Instance.numberOfDiceWonByThePlayer.ToString();
            enemyWonCountText.text= Attack.Instance.numberOfDiceWonByTheRival.ToString();
            warDate = GameDateManager.instance.GetCurrentDataString();
            if( Attack.Instance.numberOfDiceWonByThePlayer>Attack.Instance.numberOfDiceWonByTheRival)
            {
                warResultType = WarResultType.Victory;
                SetRandomQuotes("victory");
                if (generalIndex != "-")
                {
                    SetRandomQuotes("victory");
                    generals[(int)stateAndGeneral.Value.Specialty].WinBattle((int)defendingState.loss);
                }
               
            }
            else if (Attack.Instance.numberOfDiceWonByThePlayer < Attack.Instance.numberOfDiceWonByTheRival)
            {
                warResultType = WarResultType.Defeat;
             
                if (generalIndex != "-")
                {
                    SetRandomQuotes("defeat");
                    generals[(int)stateAndGeneral.Value.Specialty].LoseBattle((int)defendingState.loss);
                }
            }
            else
            {
                warResultType = WarResultType.Draw;
                SetRandomQuotes("draw");
                if (generalIndex != "-")
                {
                    SetRandomQuotes("draw");
                    generals[(int)stateAndGeneral.Value.Specialty].LoseBattle((int)defendingState.loss);
                }
               
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
                annexBtn.SetActive(true);
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
        annexBtn.SetActive(false);
    }
    void SetRandomQuotes(string resultType)
    {
        // Zafer için sözler dizisi
        List<List<string>> victoryQuotes = quotesDictionary[resultType];

        // Rastgele bir index seç
        int randomIndex = Random.Range(0, victoryQuotes.Count); // 0 ile victoryQuotes.Count arasýnda bir index seçer

        // Seçilen indexten sözleri al
        string playerQuote = victoryQuotes[randomIndex][0]; // Oyuncu generalinin sözü
        string enemyQuote = victoryQuotes[randomIndex][1]; // Düþman generalinin sözü

        // Metin bileþenlerine atama
        playerGeneralText.text = playerQuote;
        enemyGeneralText.text = enemyQuote;

        // Log ile kontrol
        Debug.Log("Player Quote: " + playerQuote);
        Debug.Log("Enemy Quote: " + enemyQuote);
    }

}
