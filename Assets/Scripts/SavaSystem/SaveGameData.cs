using System;
using System.Collections.Generic;
using System.Linq;
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
                    UnitArmyPower = stateComponent.UnitArmyPower,
                    TotalArmyPower = stateComponent.GetTotalArmyPower(),
                    stateType = stateComponent.stateType,
                    Morele = stateComponent.Morele,
                    Population = stateComponent.Population,
                    Resources = stateComponent.Resources
                };

                data.Add(stateData);
            }
        }

        var dataToSave = JsonUtility.ToJson(data);
        SaveSystem.instance.SaveData(dataToSave);
    }

    public void LoadGame()
    {
      
        string dataToLoad = SaveSystem.instance.LoadData();

        if (string.IsNullOrEmpty(dataToLoad) == false)
        {
            SaveData data = JsonUtility.FromJson<SaveData>(dataToLoad);
            foreach (var stateData in data.stateData)
            {
                Transform stateObjectTransform = Usa.Instance.GetComponentsInChildren<Transform>()
            .FirstOrDefault(child => child.GetComponent<State>()?.StateName == stateData.StateName);
                GameObject stateObject = stateObjectTransform.gameObject;
                if (stateObject == null)
                {
                    Debug.LogWarning($"{stateObject.name} game objecte yüklemenedi usa içinde böyle bir ad yok ");
                }

                State stateComponent = stateObject.GetComponent<State>();
                if (stateComponent != null)
                {
                    stateComponent.StateName = stateData.StateName;
                    stateComponent.ArmySize = stateData.ArmySize;
                    stateComponent.UnitArmyPower = stateData.UnitArmyPower;
                    stateComponent.TotalArmyPower = stateData.TotalArmyPower;
                    stateComponent.stateType = stateData.stateType;
                    stateComponent.Morele = stateData.Morele;
                    stateComponent.Population = stateData.Population;
                    stateComponent.Resources = stateData.Resources;
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
