using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using static GeneralManager;

public class SaveGameData : MonoBehaviour
{
    public static SaveGameData Instance { get;private set; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
   
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    



    public void SaveGame()
    {
        SaveData data = new SaveData();
        GameData gameData = new GameData();

        gameData.currentTime = GameDateManager.instance.GetCurrentDataString();
        foreach (var keyValuePair in GeneralManager.stateGenerals)
        {
            gameData.generalStatesList.Add(keyValuePair.Key);
            gameData.assignedGeneralList.Add(keyValuePair.Value);
        }data.gameData = gameData;
        foreach (Transform item in Usa.Instance.transform)
        {
            State stateComponent = item.gameObject.GetComponent<State>();
            if (stateComponent != null)
            {
                StateData stateData = new StateData
                {
                    StateName = stateComponent.StateName,
                    ArmySize = stateComponent.GetArmySize(),
                    TotalArmyPower = stateComponent.GetTotalArmyPower(),
                    stateType = stateComponent.stateType,
                    Morele = stateComponent.Morele,
                    Population = stateComponent.Population,
                    Resources = stateComponent.Resources,
                    LandArmySize = stateComponent.GetLandArmySize(),
                    NavalArmySize = stateComponent.GetNavalArmySize(),
                    UnitNavalArmyPower = stateComponent.UnitNavalArmyPower,
                    UnitLandArmyPower = stateComponent.UnitLandArmyPower,
                    ArmyBarrackSize = stateComponent.GetArmyBarrackSize(),
                    

                // Ticaret bilgilerini ekle
                importTrade = stateComponent.importTrade,
                    exportTrade = stateComponent.exportTrade
                };

                // Vergi bilgilerini ekle
                foreach (var tax in stateComponent.Taxes)
                {
                    stateData.Taxes.Add(tax);
                }
                foreach (var resource in stateComponent.resourceData)
                {
                    stateData.resourceDataList.Add(resource.Value);

                    //if (resource.Key == ResourceType.Water)
                    //{
                    //    Debug.LogWarning("Water mine count: " + resource.Value.mineCount);
                    //}
                }



                data.Add(stateData);
            }
        }

        var dataToSave = JsonUtility.ToJson(data);
        SaveSystem.instance.SaveData(dataToSave);
    }


    public void LoadGame()
    {
        string dataToLoad = SaveSystem.instance.LoadData();

        if (!string.IsNullOrEmpty(dataToLoad))
        {
            SaveData data = JsonUtility.FromJson<SaveData>(dataToLoad);
            GameDateManager.currentDate = GameDateManager.ConvertStringToDate(data.gameData.currentTime);
            GeneralManager.stateGenerals = data.gameData.generalStatesList
          .Select((state, index) => new { state, general = data.gameData.assignedGeneralList[index] })
          .ToDictionary(x => x.state, x => x.general);
            if (Usa.Instance == null)
                Debug.LogError(" usa instance is null");
            foreach (var stateData in data.stateData)
            {
                Transform stateObjectTransform = Usa.Instance.GetComponentsInChildren<Transform>(true).FirstOrDefault(child => child.GetComponent<State>()?.StateName == stateData.StateName);
                if (stateObjectTransform == null)
                {
                    Debug.LogWarning($"State '{stateData.StateName}' not found in the USA map.");
                    continue;
                }

                GameObject stateObject = stateObjectTransform.gameObject;
                State stateComponent = stateObject.GetComponent<State>();

                if (stateComponent != null)
                {
                    stateComponent.StateName = stateData.StateName;
                    stateComponent.ArmySize= stateData.ArmySize;               
                    stateComponent.TotalArmyPower = stateData.TotalArmyPower;
                    stateComponent.stateType = stateData.stateType;
                    stateComponent.Morele = stateData.Morele;
                    stateComponent.Population = stateData.Population;
                    stateComponent.Resources = stateData.Resources;
                    stateComponent.LandArmySize = stateData.LandArmySize;
                    stateComponent.NavalArmySize= stateData.NavalArmySize;
                    stateComponent.UnitNavalArmyPower= stateData.UnitNavalArmyPower;
                    stateComponent.UnitLandArmyPower= stateData.UnitLandArmyPower;
                    stateComponent.ArmyBarrackSize = stateData.ArmyBarrackSize;
                    stateComponent.resourceData = new Dictionary<ResourceType, ResourceData>(); // Yeniden baþlatma

                    // Ticaret verilerini yükle
                    stateComponent.importTrade = stateData.importTrade;
                    stateComponent.exportTrade = stateData.exportTrade;

                    // Vergi verilerini yükle
                    stateComponent.Taxes.Clear();
                    stateComponent.Taxes.AddRange(stateData.Taxes);
                    stateComponent.resourceData.Clear();
                    foreach (var res in stateData.resourceDataList)
                    {
                        stateComponent.resourceData[res.resourceType] = res;
                    }

                    // Renk deðiþimini yap
                    ChangeCollor.Instance.ChangeGameobjectColor(stateObject, stateData.stateType);
                }
            }
        }
    }


    [Serializable]
    public class SaveData
    {
        public List<StateData> stateData;
        public GameData gameData;  // Oyun genelindeki veriler için yeni yapý


        public SaveData()
        {
            stateData = new List<StateData>();
            gameData = new GameData();
        }

        public void Add(StateData stateData)
        {
            this.stateData.Add(stateData);
        }
    }
    
}

[Serializable]
public class StateData
{
    public string StateName;
    public int LandArmySize;
    public int NavalArmySize;
    public float UnitNavalArmyPower;
    public float UnitLandArmyPower;
    public int ArmyBarrackSize;
    public int ArmySize;
    public float TotalArmyPower;
    public StateType stateType;
    public float Morele;
    public int Population;
    public int Resources;
  


    // Ticaret verileri
    public Trade importTrade;
    public Trade exportTrade;

    // Vergi verileri
    public List<TaxData> Taxes;
    public List<ResourceData> resourceDataList;
    
    public StateData()
    {
        Taxes = new List<TaxData>();
        resourceDataList = new List<ResourceData>();

    }
}
[Serializable]
public class GameData
{
    public string currentTime;
    public List<State> generalStatesList;
    public List<General> assignedGeneralList;
    public GameData()
    {
       generalStatesList= new List<State> ();
        assignedGeneralList= new List<General>();
    }
 
}
