using BaseAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameIconSO",menuName ="newGameIconSO")]
public class GameIconSO : SingletonScriptableObject<GameIconSO>
{
    public List<Sprite>  HappinesIcon;
}
