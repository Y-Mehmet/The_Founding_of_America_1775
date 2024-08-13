using BaseAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "StateResourceSO", fileName ="newStateResourceSO")]
public class StateResourceSO : SingletonScriptableObject<StateResourceSO>
{

    public int GoldproductionRate = 5;
    public int WaterproductionRate = 340;
    public int SaltproductionRate = 4;
    public int MeatproductionRate = 30;
    public int FruitsproductionRate = 25;
    public int VegetablesproductionRate =45 ;
    public int WheatproductionRate = 25;
    public int WoodproductionRate = 20;
    public int CoalproductionRate = 5;
    public int IronproductionRate = 5;
    public int StoneproductionRate = 5;
    public int DimondproductionRate = 1;
  

}
