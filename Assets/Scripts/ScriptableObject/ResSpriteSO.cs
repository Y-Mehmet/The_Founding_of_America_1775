using BaseAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newResSpriteSO", menuName ="ResSpriteSO")]
public class ResSpriteSO: SingletonScriptableObject<ResSpriteSO>
{
    [Header("Gold,\r\n    Water,\r\n    Salt,\r\n    Meat,\r\n    Fruits,\r\n    Vegetables,\r\n    Wheat,\r\n    Wood,\r\n    Coal,\r\n    Iron,\r\n    Stone,\r\n    Diamond")]
   public List<Sprite> resIcon;
}
