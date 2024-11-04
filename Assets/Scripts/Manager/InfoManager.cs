using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public static InfoManager Instance;
    public static Dictionary<PanelID, string> HelpInfoLists = new Dictionary<PanelID, string>();
    public static Queue<PanelID> LastActivePanel= new Queue<PanelID>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        HelpInfoLists.Add(PanelID.EspionagePanel, "Our spies have been deployed with a Probability of Intel Success," +
            " reflecting the chance our agents will gather key intelligence. Their Chance of Avoiding Capture. If captured," +
            " relations with enemyState will fall by 50 points; relations below 10 may allow them to declare war." +
            " Successful espionage reveals: (1) the enemy’s army size, (2) diplomatic relations between states, and (3) the state's main resource (the asset we can plunder besides gold)");
        HelpInfoLists.Add(PanelID.WitchPanel, "The Witch Rumor Spread indicates the extent to which rumors of witchcraft have permeated the enemy army. The second factor, Witchcraft Tales Credibility, " +
            "reflects how much the enemy believes these rumors. The more convincing the tales, the higher the number of soldiers accused and removed due to witchcraft suspicions.");
        HelpInfoLists.Add(PanelID.TroopyPanel, "Total Army Power is the combined strength of all troops, calculated by multiplying Army Size with Unit Army Power. " +
            "Unit Army Power is influenced by the morale of the state and the experience level of the general. With a skilled general and high morale among the people, no land will remain unconquered.");
        HelpInfoLists.Add(PanelID.CitizenPanel, "Here, you can boost public morale by implementing measures that positively impact citizens' lives. Choose from a range of actions to increase happiness and unity among the people.");
        HelpInfoLists.Add(PanelID.PruductMinePanel, "The population and army consume six vital resources: water, salt, meat, fruits, vegetables, and wheat. If any of these resources run out, a famine is declared. To meet demand, " +
            "missing items are automatically purchased using the state’s gold. When state funds are depleted, the central bank’s reserves are used. If those also run dry, the population, unable to endure famine, will revolt.");
        HelpInfoLists.Add(PanelID.MineUpgradePanel, "There are two ways to increase the number of mines. " +
            "The first is by spending three required resources along with gold to add a mine. Alternatively, you can choose the easier route by using gems alone to increase the mine count.");
        HelpInfoLists.Add(PanelID.TaxPanel, "Income tax is collected from the daily production of gold mines at the set tax rate, generating revenue based on the rate applied. Value-added tax is collected from exports," +
            " while plunder tax is applied during raids. The stamp tax revenue increases with the state's population. Exercise caution when setting tax rates; if citizens start questioning where their money is going," +
            " unrest may follow. As Ibn Khaldun said, 'A state that imposes high taxes will face decline, while one with lower taxes will prosper.'");
        HelpInfoLists.Add(PanelID.BuyPanel, "The maximum quantity for purchasing resources is influenced by two factors: first, the amount of gold or gems the purchasing state has; second, the trade limit and the current available amount of the resource in the selling state." +
            " If there are insufficient resources available, the trade limit becomes irrelevant. If you want to receive products more quickly, you can purchase from neighboring states.");
        HelpInfoLists.Add(PanelID.SellPanel, "The maximum quantity for selling resources is determined by two values: first, the amount available from the selling state; second, the purchasing state's gold value and trade limit." +
            " If the buying state does not have enough gold, the trade limit becomes irrelevant.");
        HelpInfoLists.Add(PanelID.SellAllPanel, "You can sell all resources except gold at a quarter of their original price, allowing you to quickly clear out excess supplies." +
            " However, be aware that this action may not sit well with your citizens, leading to unrest as they may question the stability of their provisions.");
        HelpInfoLists.Add(PanelID.GeneralPanel, "Generals are indispensable leaders who enhance the power of your army. In the complex nature of warfare, they guide success with their experience and tactical knowledge. Each general can only be assigned to one state's army. " +
            "In the 18th century, wars were a part of daily life, and a general's leadership boosts the morale of the army, facilitating your conquests. With a good general, no territory is unattainable.");
        HelpInfoLists.Add(PanelID.GeneralInfoPanel, "If a battle results in victory, the general earns experience points equal to the number of enemies defeated. In the case of a draw or defeat, they gain only half the experience points based on the enemies they have eliminated. " +
            "Once the experience points are full, the general is honored with a new rank, providing an Incremental Effect boost to army power per unit.");
    }
    public static void AddQueque(PanelID panelID)
    {
        if(LastActivePanel.Count>0)
        {
            LastActivePanel.Dequeue();
        }
       LastActivePanel.Enqueue(panelID);

    }
}

