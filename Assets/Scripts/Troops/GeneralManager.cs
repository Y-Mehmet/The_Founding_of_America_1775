using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralManager:MonoBehaviour
{
    public static GeneralManager Instance { get; private set; }
    public static List<General> generals = new List<General>();
    // Her state'in hangi general'e atandýðýný tutan dictionary
    private Dictionary<State, General> stateGenerals = new Dictionary<State, General>();


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

    private void InýtGeneral()
    {
        
        generals.Add(new General("John Paul Jones", 0, Specialty.Naval, GeneralRank.SecondLieutenant, 0.05f, 0.1f,0.1f));
        generals.Add(new General("Nathaniel Greene", 0, Specialty.Land, GeneralRank.SecondLieutenant, 0.1f, 0.05f, 0.1f));
        generals.Add(new General("Henry Knox", 0, Specialty.Artillery, GeneralRank.SecondLieutenant, 0.05f, 0.05f,0.15f));
    }

    // Bir State'e General atama iþlemi
    public void AssignGeneralToState(State state, General general)
    {
        // Eðer general baþka bir state'e atanmýþsa, önce o state'den kaldýrýlýr
        State previousState = stateGenerals.FirstOrDefault(x => x.Value == general).Key;
        if (previousState != null)
        {
            stateGenerals.Remove(previousState);
            Debug.Log($"General {general.Name} removed from {previousState.StateName}");
        }

        // Eðer state'e zaten bir general atanmýþsa, önce o general kaldýrýlýr
        if (stateGenerals.ContainsKey(state))
        {
            var previousGeneral = stateGenerals[state];
            stateGenerals.Remove(state);
            Debug.Log($"General {previousGeneral.Name} removed from {state.StateName}");
        }

        // Yeni general state'e atanýr
        stateGenerals[state] = general;
        Debug.Log($"General {general.Name} successfully assigned to {state.StateName}");
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
            Debug.LogWarning($"{state.StateName} had no general assigned.");
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

    public class General
    {
        public string Name { get; private set; }
        public float Experience { get; private set; }
        public int ExperienceLimit { get; private set; }
        public int ExperineceIncraseValue { get; private set; }
        public Specialty Specialty { get; private set; }
        public GeneralRank Rank { get; private set; }
        public float LandHelpRate { get; private set; }  // Kara yardým oraný
        public float NavalHelpRate { get; private set; }  // Deniz yardým oraný
        public float IncraseHeplRate { get; private set; }
        public int WonCount { get; private set; }
        public int LostCount { get; private set; }

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
