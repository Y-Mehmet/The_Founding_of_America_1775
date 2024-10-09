using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class Witch : MonoBehaviour
{
    
    public TMP_InputField inputField;
    public TMP_Text witchCostTxt;

    TextMeshProUGUI stateName;

    float random_min_range =0.3f;
    float random_max_range = 1.0f;
    int oneWitchCost = 100;
    int witchPower = 30; // her bir cadý ne kadar asker öldürecek 
    int witchCost;

    Color originalwitchCostTextColor;
    private void Start()
    {
        inputField.onValueChanged.AddListener(InputFieldOnValueCahnged);
        originalwitchCostTextColor= witchCostTxt.color;
    }
    void InputFieldOnValueCahnged(string input)
    {
        int witchCount;
        if(int.TryParse(input, out witchCount))
        {
           
             witchCost = witchCount * oneWitchCost;
           

            int gold = ResourceManager.Instance.GetResourceAmount(ResourceType.Gold);
            if(gold >= witchCost) {
                witchCostTxt.color = originalwitchCostTextColor;
              
            }
            else
            {
                witchCostTxt.color = Color.red;
               
            }
            witchCostTxt.text = witchCost.ToString();
        }
        else
        {
            witchCostTxt.text = "0";
           // Debug.LogWarning("witch input fiekd deðeri sayý deðil");
        }
        

    }
    public void SendWitch()
    {
        int gold = ResourceManager.Instance.GetResourceAmount(ResourceType.Gold);
        if (gold > witchCost)
        {
            ResourceManager.Instance.ReduceResource(ResourceType.Gold, witchCost);
            Transform USA_Transform = Usa.Instance.gameObject.transform;
            stateName.text = RegionClickHandler.Instance.currentState.name.ToString();
            string state = stateName.text;

            GameObject enemyState = FindChildByName(USA_Transform, state);
            int witchCount;
            if (int.TryParse(inputField.text, out witchCount))
            {
                float loss = witchCount * witchPower;
                float newLoss = Random.RandomRange(random_min_range, random_max_range) * loss;
                Debug.LogWarning($" {state}'inde orduda baþlatýlan cadý avýnda  {newLoss} tane cadý elegeçirildi  avdan önceki asker sayýsý {enemyState.GetComponent<State>().ArmySize}");
                enemyState.GetComponent<State>().ReduceArmySize(newLoss);
                Debug.LogWarning($"avdan sonra asker sayýsý {enemyState.GetComponent<State>().ArmySize}");
            }
            ClearInputField();


        }
        else
            Debug.Log("witch gönderilemedi paran yok");

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
    void ClearInputField()
    {
        inputField.text = "0";
    }
    
}
