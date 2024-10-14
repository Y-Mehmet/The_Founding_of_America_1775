using BaseAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="gameDataSO",menuName ="newGameDataSO")]
public class GameDataSo : SingletonScriptableObject<GameDataSo>
{
    public List<Sprite> GeneralSprite;
    public List<Sprite> GeneralTypeIconSprite;


}
