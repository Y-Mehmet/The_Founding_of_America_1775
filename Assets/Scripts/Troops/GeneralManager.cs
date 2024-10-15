using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneralManager:MonoBehaviour
{
    public static GeneralManager Instance { get; private set; }
    public static List<General> generals = new List<General>();
    // Her state'in hangi general'e atand���n� tutan dictionary
    public static  Dictionary<State, General> stateGenerals = new Dictionary<State, General>();
    public static int GeneralIndex 
    { get; private set; }
    public static void SetGeneralIndex(int index)
    {
        if(index>generals.Count)
        {
            Debug.LogError(" index general den b�y�k olmaz");
        }
        else
        {
            GeneralIndex = index;
        }
        
        
    }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            In�tGeneral();
        }
            
        else
            Destroy(gameObject);
    }

    private void In�tGeneral()
    {
        
        generals.Add(new General("John Paul Jones", 0, Specialty.Naval, GeneralRank.SecondLieutenant, 0.05f, 0.1f,0.1f));
        generals.Add(new General("Nathaniel Greene", 0, Specialty.Land, GeneralRank.SecondLieutenant, 0.1f, 0.05f, 0.1f));
        generals.Add(new General("Henry Knox", 0, Specialty.Artillery, GeneralRank.SecondLieutenant, 0.05f, 0.05f,0.15f));
        
        
    }

    // Bir State'e General atama i�lemi
    public static  void AssignGeneralToState(State state, General general)
    {
        // E�er general ba�ka bir state'e atanm��sa, �nce o state'den kald�r�l�r
        State previousState = stateGenerals.FirstOrDefault(x => x.Value == general).Key;
        if (previousState != null)
        {
            stateGenerals.Remove(previousState);
            Debug.Log($"General {general.Name} removed from {previousState.StateName}");
        }

        // E�er state'e zaten bir general atanm��sa, �nce o general kald�r�l�r
        if (stateGenerals.ContainsKey(state))
        {
            var previousGeneral = stateGenerals[state];
            stateGenerals.Remove(state);
            Debug.Log($"General {previousGeneral.Name} removed from {state.StateName}");
        }

        // Yeni general state'e atan�r
        stateGenerals[state] = general;
        Debug.Log($"General {general.Name} successfully assigned to {state.StateName}");
    }

    // Belirli bir state'in generalini geri alma i�lemi
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

    // State'den general kald�rma i�lemi
    public void RemoveGeneralFromState(State state)
    {
        if (stateGenerals.TryGetValue(state, out var general))
        {
            stateGenerals.Remove(state);  // General'i state'den kald�r
            Debug.Log($"General {general.Name} removed from {state.StateName}");
        }
        else
        {
            Debug.LogWarning($"{state.StateName} had no general assigned.");
        }
    }
    public enum GeneralRank
    {
        SecondLieutenant,  // Te�men
        FirstLieutenant,   // �ste�men
        Captain,           // Y�zba��
        Major,             // Binba��
        LieutenantColonel, // Yarbay
        Colonel,           // Albay
        BrigadierGeneral,  // Tu�general
        MajorGeneral,      // T�mgeneral
        LieutenantGeneral, // Korgeneral
        General,           // Orgeneral
        GeneralOfTheArmy   // Mare�al (Sava� zaman� 5 y�ld�zl� General)
    }

    public class General
    {
        public string Name { get; private set; }
        public float Experience { get; private set; }
        public int ExperienceLimit { get; private set; }
        public int ExperineceIncraseValue { get; private set; }
        public Specialty Specialty { get; private set; }
        public GeneralRank Rank { get; private set; }
        public float LandHelpRate { get; private set; }  // Kara yard�m oran�
        public float NavalHelpRate { get; private set; }  // Deniz yard�m oran�
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
            GainExperience(numberOfKill);  // Sava� kazan�nca tecr�be art���
        }

        public void LoseBattle(int numberOfKill)
        {
            LostCount++;
            GainExperience(numberOfKill/2);  // Kaybedince daha az tecr�be art���
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
