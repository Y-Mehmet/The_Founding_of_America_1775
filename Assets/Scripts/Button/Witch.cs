using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Witch : MonoBehaviour
{
    
    public TMP_InputField inputField;

    TextMeshProUGUI stateName;

    float random_min_range =0.3f;
    float random_max_range = 1.0f;
    int witchPower = 30; // her bir cadý ne kadar asker öldürecek 
    public void SendWitch()
    {
        Transform USA_Transform= Usa.Instance.gameObject.transform;
        stateName = RegionManager.instance.e_regionNameText;
        string state= stateName.text;

        GameObject enemyState = FindChildByName(USA_Transform, state);
        int witchCount;
        if(int.TryParse(inputField.text, out witchCount))
        {
           float  loss = witchCount * witchPower;
            float newLoss = Random.RandomRange(random_min_range, random_max_range)*loss;
            enemyState.GetComponent<State>().ReduceArmySize(newLoss);
            Debug.LogWarning($" {state} 'inde orduda baþlatýlan cadý avýnda  {newLoss} tane cadý elegeçirildi");
        }


    }
    GameObject FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                return child.gameObject;
            }

            GameObject found = FindChildByName(child, name);
            if (found != null)
            {
                return found;
            }
        }
        return null;
    }
}
