using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GeneralManager:MonoBehaviour
{
    public static GeneralManager Instance { get; private set; }
    public static List<General> generals = new List<General>();
    // Her state'in hangi general'e atandýðýný tutan dictionary
    public static  Dictionary<State, General> stateGenerals = new Dictionary<State, General>();
    public static int GeneralIndex 
    { get; private set; }
  


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InýtGeneral();
            
        }
            
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        GameManager.Instance.OnGameDataLoaded += GameLoaded;
    }
    void GameLoaded()
    {
        
    }
    public static void SetGeneralIndex(int index)
    {
        if (index > generals.Count)
        {
            Debug.LogError(" index general den büyük olmaz");
        }
        else
        {
            GeneralIndex = index;
        }


    }

    private void InýtGeneral()
    {
        generals.Add(new General("John Paul Jones", 0, Specialty.Naval, GeneralRank.SecondLieutenant, 0.05f, 0.1f, 0.1f));
        generals.Add(new General("Nathaniel Greene", 0, Specialty.Land, GeneralRank.SecondLieutenant, 0.1f, 0.05f, 0.1f));
        generals.Add(new General("Henry Knox", 0, Specialty.Artillery, GeneralRank.SecondLieutenant, 0.05f, 0.05f, 0.15f));
    }


    // Bir State'e General atama iþlemi
    public static  void AssignGeneralToState(State state, General general)
    {
        // Eðer general baþka bir state'e atanmýþsa, önce o state'den kaldýrýlýr
        State previousState = stateGenerals.FirstOrDefault(x => x.Value == general).Key;
        if (previousState != null)
        {
            stateGenerals.Remove(previousState);
        //    Debug.Log($"General {general.Name} removed from {previousState.StateName}");
        }

        // Eðer state'e zaten bir general atanmýþsa, önce o general kaldýrýlýr
        if (stateGenerals.ContainsKey(state))
        {
            var previousGeneral = stateGenerals[state];
            stateGenerals.Remove(state);
         //   Debug.Log($"General {previousGeneral.Name} removed from {state.StateName}");
        }

        // Yeni general state'e atanýr
        stateGenerals[state] = general;
      //  Debug.Log($"General {general.Name} successfully assigned to {state.StateName}");
    }

    // Belirli bir state'in generalini geri alma iþlemi
    public General GetGeneralForState(State state)
    {
        if (stateGenerals.TryGetValue(state, out var general))
        {
            return general;
        }
        else
        {
            Debug.LogWarning($"{state.StateName} has no general assigned.");
            return null;
        }
    }

    // State'den general kaldýrma iþlemi
    public void RemoveGeneralFromState(State state)
    {
        if (stateGenerals.TryGetValue(state, out var general))
        {
            stateGenerals.Remove(state);  // General'i state'den kaldýr
            Debug.Log($"General {general.Name} removed from {state.StateName}");
        }
        else
        {
            Debug.Log($"{state.StateName} had no general assigned.");
        }
    }
    public enum GeneralRank
    {
        SecondLieutenant,  // Teðmen
        FirstLieutenant,   // Üsteðmen
        Captain,           // Yüzbaþý
        Major,             // Binbaþý
        LieutenantColonel, // Yarbay
        Colonel,           // Albay
        BrigadierGeneral,  // Tuðgeneral
        MajorGeneral,      // Tümgeneral
        LieutenantGeneral, // Korgeneral
        General,           // Orgeneral
        GeneralOfTheArmy   // Mareþal (Savaþ zamaný 5 yýldýzlý General)
    }
    [Serializable]
    public class General
    {
        public string Name;
        public float Experience ;
        public int ExperienceLimit;
        public int ExperineceIncraseValue;
        public Specialty Specialty ;
        public GeneralRank Rank ;
        public float LandHelpRate ;  // Kara yardým oraný
        public float NavalHelpRate ;  // Deniz yardým oraný
        public float IncraseHeplRate;
        public int WonCount ;
        public int LostCount ;

        public General(string name, float experience, Specialty specialty, GeneralRank rank, float landHelpRate, float navalHelpRate, float incraseHelpRate)
        {
            Name = name;
            Experience = experience;
            Specialty = specialty;
            Rank = rank;
            LandHelpRate = landHelpRate;
            NavalHelpRate = navalHelpRate;
            IncraseHeplRate= incraseHelpRate;
            WonCount = 0;
            LostCount = 0;
            ExperienceLimit = 10000;
            ExperineceIncraseValue = 5000;
        }

        public void WinBattle(int numberOfKill)
        {
            WonCount++;
            GainExperience(numberOfKill);  // Savaþ kazanýnca tecrübe artýþý
        }

        public void LoseBattle(int numberOfKill)
        {
            LostCount++;
            GainExperience(numberOfKill/2);  // Kaybedince daha az tecrübe artýþý
        }

        private void GainExperience(float amount)
        {
            if(Experience+amount > ExperienceLimit) 
            {
                float newAmount = (Experience + amount) - ExperienceLimit;
                if(Rank!=GeneralRank.GeneralOfTheArmy)
                {
                    Rank = Rank + 1;
                    ExperienceLimit += ExperineceIncraseValue;
                    Experience = newAmount;
                    NavalHelpRate += IncraseHeplRate;
                    LandHelpRate += IncraseHeplRate;

                }
               
                

            }else
            {
                Experience += amount;
            }
            

           

        }
    }
    public enum Specialty
    {
        Naval,
        Land,
        Artillery
    }

  
    

}
