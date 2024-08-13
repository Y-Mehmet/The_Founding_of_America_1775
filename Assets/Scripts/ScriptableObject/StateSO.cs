
using BaseAssets;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newStateSO", menuName ="StateSo")]
public class StateSO : SingletonScriptableObject<StateSO> 
{
    public string StateName;
    public float ArmySize;
    public float UnitArmyPower;
    public float TotalArmyPower;
    public Sprite StateImage;
    public StateType StateType;

    public void CalculateTotalArmyPower()
    {
        TotalArmyPower = UnitArmyPower * ArmySize;
    }


    public List<StateData> StateDatas= new List<StateData>();
   
    public StateData GetStateData(string stateName)
    {
        foreach (var state in StateDatas)
        { 
        if( state.StateName== StateName)
            {
                return state;
            }
        
                
        }
        return null;
    }
    public void LoadStateSpriteAndAddToStateData(string spritePath, string stateName )
    {
        Sprite stateSprite= Resources.Load<Sprite>(spritePath);
        if(stateSprite!= null)
        {
            StateData newState = new StateData {  StateName = stateName , };
        }
    }
    

    

    //// Yeni metod: Resources'tan sprite yükleyip listeye eklemek
    //public void LoadSpriteAndAddToCardData(string resourcePath, CardType cardType, int cardValue)
    //{
    //    Sprite sprite = Resources.Load<Sprite>(resourcePath);
    //    if (sprite != null)
    //    {
    //        CardData newCard = new CardData { CardType = cardType, CardValue = cardValue, CardImage = sprite };
    //        CardDatas.Add(newCard);
    //        Debug.Log("Sprite baþarýyla yüklendi ve listeye eklendi.");
    //    }
    //    else
    //    {
    //        Debug.LogError("Sprite yüklenemedi: " + resourcePath);
    //    }
    //}



}
