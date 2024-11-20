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
            bool isGeneralAssing = false;
            var stateAndGeneral = stateGenerals.FirstOrDefault(x => x.Key == attackingState);

            // FirstOrDefault kullan�ld���nda, e�er e�le�me yoksa KeyValuePair'in varsay�lan de�eri d�ner,
            // bu nedenle .Key'yi kontrol etmek gerekir
            if (!stateAndGeneral.Equals(default(KeyValuePair<State, General>)))
            {
                isGeneralAssing = true;
                generalIndex = generals[(int)stateAndGeneral.Value.Specialty].Name;
                playerGeneralSprite.sprite = GameDataSo.Instance.GeneralSprite[(int)stateAndGeneral.Value.Specialty];
                
            }
            else
            {
             //   Debug.LogWarning("Bu state i�in atanm�� bir general bulunamad�.");
            }
            playerWonCountText.text = Attack.Instance.numberOfDiceWonByThePlayer.ToString();
            enemyWonCountText.text= Attack.Instance.numberOfDiceWonByTheRival.ToString();
            warDate = GameDateManager.instance.GetCurrentDataString();
            if( Attack.Instance.numberOfDiceWonByThePlayer>Attack.Instance.numberOfDiceWonByTheRival)
            {
               
                warResultType = WarResultType.Victory;
                MissionsManager.AddTotalWin();
               
                if (generalIndex != "-")
                {
                    
                    generals[(int)stateAndGeneral.Value.Specialty].WinBattle((int)defendingState.loss);
                }
                SetRandomQuotes("victory",isGeneralAssing);
                RegionClickHandler.staticState.SetMorale(5);
                MessageManager.AddMessage("Samuel Thompson: We, the citizens of this state, thank our courageous leaders and brave soldiers for their unwavering commitment to our freedom." +
                    " This victory shows our strength and unity. Your leadership has inspired us," +
                    " and the sacrifices made by our forces ensure a brighter future. Together, we stand victorious! Our morale has increased by +5!");

            }
            else if (Attack.Instance.numberOfDiceWonByThePlayer < Attack.Instance.numberOfDiceWonByTheRival)
            {
                warResultType = WarResultType.Defeat;
                SetRandomQuotes("defeat", isGeneralAssing);
                if (generalIndex != "-")
                {
                  
                    generals[(int)stateAndGeneral.Value.Specialty].LoseBattle((int)defendingState.loss);
                }
                RegionClickHandler.staticState.SetMorale(-5);
                MessageManager.AddMessage("Reginald Bradford: As citizens of this state, we are disheartened by our recent defeat. This loss exposes the flaws in our leadership and the decisions made in the heat of battle." +
                    " We cannot continue to follow leaders who jeopardize our freedom and ignore the needs of the people. Our morale has dropped by -5, and we must demand better for the sake of our future!");
            }
            else
            {
                warResultType = WarResultType.Draw;
                SetRandomQuotes("draw", isGeneralAssing);
                if (generalIndex != "-")
                {
                  
                    generals[(int)stateAndGeneral.Value.Specialty].LoseBattle((int)defendingState.loss);
                }
               
            }
            SoundManager.instance.Play(warResultType.ToString());
            War war=new(generalIndex ,attackingState,defendingState,warResultType,warDate);

            WarHistory.generalIndexAndWarList.Push( war);

            enemyRegionNameText.text = defendingState.gameObject.name + "";
            enemyRegionDamageText.text = "(- " + (int)defendingState.loss + " )";
            enemyTotalArmyPowerText.text = (int)defendingState.GetArmySize() + "";
            if (defendingState.StateIcon != null)
            {
                //   Debug.Log($"{defendingState.StateIcon.name}");
                EnemyFlagSprite.sprite = defendingState.StateIcon;

            }
            else
                Debug.LogWarning("stae icon bulunamad�");



            playerRegionNameText.text = attackingState.gameObject.name + "";
            playerRegionDamageText.text = "(- " + (int)attackingState.loss + " )";
            playerTotalArmyPowerText.text = (int)attackingState.GetArmySize() + "";
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
        SoundManager.instance.Stop(warResultType.ToString());
    }
    void SetRandomQuotes(string resultType , bool isGeneralAssingned=false)
    {
        // Zafer i�in s�zler dizisi
        List<List<string>> victoryQuotes = quotesDictionary[resultType];

        // Rastgele bir index se�
        int randomIndex = Random.Range(0, victoryQuotes.Count); // 0 ile victoryQuotes.Count aras�nda bir index se�er

        // Se�ilen indexten s�zleri al
        string playerQuote = victoryQuotes[randomIndex][0]; // Oyuncu generalinin s�z�
        string enemyQuote = victoryQuotes[randomIndex][1]; // D��man generalinin s�z�
        if (!isGeneralAssingned)
        {
            playerQuote = "";
        }
     

        // Metin bile�enlerine atama
        playerGeneralText.text = playerQuote;
        enemyGeneralText.text = enemyQuote;

        // Log ile kontrol
       //Debug.Log("Player Quote: " + playerQuote);
       // Debug.Log("Enemy Quote: " + enemyQuote);
    }

}
