using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SaveGameData : MonoBehaviour
{






    public void SaveGame()
    {
        SaveData data = new SaveData();
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

                    if (resource.Key == ResourceType.Water)
                    {
                        Debug.LogWarning("Water mine count: " + resource.Value.mineCount);
                    }
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
            foreach (var stateData in data.stateData)
            {
                Transform stateObjectTransform = Usa.Instance.GetComponentsInChildren<Transform>()
                    .FirstOrDefault(child => child.GetComponent<State>()?.StateName == stateData.StateName);
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

        public SaveData()
        {
            stateData = new List<StateData>();
        }

        public void Add(StateData stateData)
        {
            this.stateData.Add(stateData);
        }
    }
}

