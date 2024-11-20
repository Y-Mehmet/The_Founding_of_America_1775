using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    public static MissionsManager instance { get; private set; }

    public static  List<Mission> TroppyMissions { get; private set; } = new List<Mission>();
    public static List<Mission> EconomyMissions { get; private set; } = new List<Mission>();
    public static List<Mission> MiscellaneousMissions { get; private set; } = new List<Mission>();

    public static int TroppyMissonsIndex = 0, EconomyMissionsIndex=0, MiscellaneousMissionsIndex=0, CompletedMissionCount=0, ClaimedMissionCount=0;
    public static Action OnComplate, OnClaim;
 
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }else
        {
            Destroy(instance);
        }
        TroppyMissions.Add(new Mission("Warrior", // Görev Adý
         "Win  battle to prove your strength.", // Görev Açýklamasý
         rewardGold: new List<int> { 100, 1000, 2000, 5000 }, // Ödül olarak verilen altýn
         missionCount: new List<int> { 1, 10, 50, 150 }

         ));
            TroppyMissions.Add( new Mission(
        "Conqueror", // Görev Adý
        "Conquer a new state to expand your territory.", // Görev Açýklamasý
        rewardGold: new List<int> { 200, 1500, 3000, 6000 }, // Ödül listesi
        missionCount: new List<int> { 1, 5, 15, 45 }, // Fethedilmesi gereken yerleþim sayýsý
        isChanged: true
    ));
        TroppyMissions.Add(new Mission(
    "Great Army", // Görev Adý
    "Recruit new soldiers to strengthen your army.", // Görev Açýklamasý
    rewardGold: new List<int> { 250, 2000, 3500, 7000 }, // Ödül listesi
    missionCount: new List<int> { 1000, 10000, 50000, 150000 } // Alýnmasý gereken asker sayýsý

));
        TroppyMissions.Add(new Mission(
    "Loot Hunter", // Görev Adý
    "Earn gold by plundering enemy territories.", // Görev Açýklamasý
    rewardGold: new List<int> { 300, 2500, 4000, 8000 }, // Ödül listesi
    missionCount: new List<int> { 25000, 100000, 250000, 500000 } // Plunder ederek kazanýlmasý gereken altýn miktarý
));
        TroppyMissions.Add(new Mission(
      "Military Leaders",
      "Level up your generals to improve their skills and leadership.",
      rewardGold: new List<int> { 500, 3500, 5000, 10000 },
      missionCount: new List<int> { 3, 9, 15, 30 }
  ));
        /// economy 

        EconomyMissions.Add(new Mission(
   "Import Specialists",
   "Purchase goods worth the specified gold amount to improve your trade balance.",
   rewardGold: new List<int> { 50, 500, 1500, 3000 }, // Gold rewarded for completing the mission
    missionCount: new List<int> { 1000, 10000, 50000, 150000 } // Gold value of import targets
));

        EconomyMissions.Add(new Mission(
      "Export Pioneers",
      "Sell goods worth the specified gold amount to strengthen your trade network.",
      rewardGold: new List<int> { 100, 1000, 2000, 5000 }, // Ödül olarak verilen altýn
       missionCount: new List<int> { 1000, 10000, 50000, 150000 }  // Altýn deðerinde ihracat hedefleri
   ));
        EconomyMissions.Add(new Mission(
   "Tax Sovereign",
   "Collect the specified amount of gold in taxes to prove your dominance in governance.",
    rewardGold: new List<int> { 300, 2500, 4000, 8000 }, // Gold rewarded for completing the mission
   missionCount: new List<int> { 25000, 100000, 250000, 500000 }  // Tax collection targets in gold
));
        EconomyMissions.Add(new Mission(
   "Mine Innovator",
   "Upgrade your mines to boost resource production and secure your economy.",
   rewardGold: new List<int> { 400, 2750, 4500, 9000 }, // Gold rewarded for completing the mission
   missionCount: new List<int> { 5, 15, 50, 100 } // Number of mines to upgrade
));

        EconomyMissions.Add(new Mission(
   "Wheel Master",
   "Spin the Wheel of Fortune to test your luck and claim rewards.",
    rewardGold: new List<int> { 500, 3500, 5000, 10000 }, // Gold rewarded for completing the mission
   missionCount: new List<int> { 1, 10, 50, 100 } // Number of wheel spins required
));
        /// general mission
        MiscellaneousMissions.Add(new Mission(
       "Covert Operative",
       "Complete the specified number of successful espionage missions to gather vital intelligence.",
       rewardGold: new List<int> { 500, 1000, 3000, 5000 }, // Gold rewarded for completing the mission
       missionCount: new List<int> { 5, 15, 50, 100 } // Number of successful espionage missions
    ));
        MiscellaneousMissions.Add(new Mission(
   "Witch Hunter",
   "Spread witch rumors and turn the specified number of enemy soldiers into witches.",
   rewardGold: new List<int> { 1000, 2000, 4000, 8000 }, // Gold rewarded for completing the mission
   missionCount: new List<int> { 2000, 10000, 25000, 50000 } // Number of soldiers to be turned into witches
));
        MiscellaneousMissions.Add(new Mission(
   "Good Neighbor States",
   "Send gifts to enemy states and spend the specified amount of gold to improve bilateral relations.",
   rewardGold: new List<int> { 2000, 4000, 8000, 20000 }, // Gold rewarded for completing the mission
   missionCount: new List<int> { 10000, 15000, 25000, 100000 } // Amount of gold to be spent on gifts
));
        MiscellaneousMissions.Add(new Mission(
   "The Great Population",
   "Achieve a population of the specified size to secure dominance in economy and military strength.",
   rewardGold: new List<int> { 4000, 8000, 20000, 50000 }, // Gold rewarded for completing the mission
   missionCount: new List<int> { 50000, 100000, 200000, 500000 } // Population target
));
        MiscellaneousMissions.Add(new Mission(
   "Questmaster",
   "Complete the specified number of missions to prove your dedication and efficiency.",
   rewardGold: new List<int> { 8000, 20000, 50000, 75000 }, // Gold rewarded for completing the mission
   missionCount: new List<int> {  25, 35, 45,59 } // Number of missions to be completed
));
    }

    private void ApplyRewards(Mission mission)
    {
        // Görevin ödüllerini oyuncuya uygula
        Debug.Log($"Görev tamamlandý: {mission.MissionName}");
        Debug.Log($"Altýn kazandýnýz: {mission.RewardGold}");
        
    }
   //  troopy 
    public static void AddTotalWin()
    {
        CheckTroppyMission(0, 1);
    }

    public static void AddTotalAnnex()
    {
        CheckTroppyMission(1, 1);
    }

    public static void AddTotalSolider(int count)
    {
        CheckTroppyMission(2, count);
    }

    public static void AddTotalPlunderGold(int count)
    {
        CheckTroppyMission(3, count);
    }

    public static void AddTotalGeneralRank(int count)
    {
        CheckTroppyMission(4, count);
    }
    // economy
    public static void AddTotalImportGold(int count)
    {
        CheckEconomyMission(0, count);
    }
    public static void AddTotalExportGold(int count)
    {
        CheckEconomyMission(1, count);
    }
    public static void AddTotalTaxGold(int count)
    {
        CheckEconomyMission(2, -count);
    }
    public static void AddTotalMineUpgrade(int count)
    {
        CheckEconomyMission(3, count);
    }
    public static void AddTotalSpin(int count)
    {
        CheckEconomyMission( 4, count);
    }
    public static void AddTotalSucsessIntel(int count)
    {
        CheckMiscellaneousMission(0, count);
    }
    public static void AddTotalWichtSolider(int count)
    {
        CheckMiscellaneousMission(1, count);
    }
    public static void AddTotalGiftCost(int count)
    {
        CheckMiscellaneousMission(2, count);
    }
    public static void AddTotalPopulation(int count)
    {
        MiscellaneousMissions[3].ComplatedMissionCount = 0;
        CheckMiscellaneousMission(3, count);
    }
    public static void AddTotalCpmlatedMission(int count)
    {

        CheckMiscellaneousMission(4, count);
    }
    // Yeni metod: Diðer kategoriler için de kullanýlabilir.
    static void CheckMiscellaneousMission(  int index, int count = 1)
    {
        MiscellaneousMissions[index].ComplatedMissionCount += count;

        if (!MiscellaneousMissions[index].IsCompleted && MiscellaneousMissions[index].ComplatedMissionCount >= MiscellaneousMissions[index].MissionCount[MiscellaneousMissions[index].MissinonIndex] && index== MiscellaneousMissionsIndex )
        {
            
            MiscellaneousMissions[index].IsCompleted = true;
            AddTotalCpmlatedMission(1);
            CompletedMissionCount++; // toplam tamamlanan görev
            if (MiscellaneousMissions[index].MissionCount.Count < MiscellaneousMissions[index].MissinonIndex)
            {
                MiscellaneousMissions[index].MissinonIndex++;
            }
        }
    }
    static void CheckTroppyMission( int index, int count = 1)
    {
        TroppyMissions[index].ComplatedMissionCount += count;

        if (!TroppyMissions[index].IsCompleted && TroppyMissions[index].ComplatedMissionCount >= TroppyMissions[index].MissionCount[TroppyMissions[index].MissinonIndex] && index== TroppyMissonsIndex)
        {

            TroppyMissions[index].IsCompleted = true;
            AddTotalCpmlatedMission(1);
            CompletedMissionCount++; // toplam tamamlanan görev
            if (TroppyMissions[index].MissionCount.Count < TroppyMissions[index].MissinonIndex)
            {
                TroppyMissions[index].MissinonIndex++;
            }
        }
    }
    static void CheckEconomyMission( int index, int count = 1)
    {
        EconomyMissions[index].ComplatedMissionCount += count;

        if (!EconomyMissions[index].IsCompleted && EconomyMissions[index].ComplatedMissionCount >= EconomyMissions[index].MissionCount[EconomyMissions[index].MissinonIndex] && index== EconomyMissionsIndex)
        {

            EconomyMissions[index].IsCompleted = true;
            AddTotalCpmlatedMission(1);
            CompletedMissionCount++; // toplam tamamlanan görev
            if (EconomyMissions[index].MissionCount.Count < EconomyMissions[index].MissinonIndex)
            {
                EconomyMissions[index].MissinonIndex++;
            }
        }
    }




}
[Serializable]
public  class Mission
{
    public string MissionName;
    public string Description;
    public bool IsCompleted;
    public List<int> RewardGold;
    public List<int>  MissionCount;
    public int ComplatedMissionCount;
    public int MissinonIndex; 
   public bool IsChanged;
    public Action OnComplete;
    public Mission(string name, string description, List<int> rewardGold, List<int> missionCount,   bool isChanged = false)
    {
        MissionName = name;
        Description = description;
        RewardGold = rewardGold;
        MissionCount = missionCount;

        ComplatedMissionCount = 0;
        MissinonIndex = 0;
        IsChanged = isChanged;
        IsCompleted = false;
       
    }

    public  void CompleteMission()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
         
        }
    }
}
[Serializable]
public enum MissionType
{
    Troopy,
    Economy,
    Miscellaneous

}
