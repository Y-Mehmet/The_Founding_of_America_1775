using BaseAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newStateFlagSpritesSO", menuName ="StateSpritesSo")]
public class StateFlagSpritesSO : SingletonScriptableObject<StateFlagSpritesSO>
{
    public List<Sprite> flagSpriteLists;
}
