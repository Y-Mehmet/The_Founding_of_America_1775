using BaseAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "StateResourceSO", fileName ="newStateResourceSO")]
public class StateResourceSO : SingletonScriptableObject<StateResourceSO>
{
    // bir madenin �retim h�z� 
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

    // 1K insan�n g�nl�k kaynak t�ketim miktar�
    public float WaterConsumptionRate = 65;

    // 1k i�in g�nl�k do�um oran� 
    public float BaseBirthRate = 1;

    // 1K i�in g�nl�k �l�m oran�
    public float BaseDeathRate = 1;

    // 1k asker i�in 30 g�nl�k idame maliyeti
    public float SoliderCost = 300;
  

}
