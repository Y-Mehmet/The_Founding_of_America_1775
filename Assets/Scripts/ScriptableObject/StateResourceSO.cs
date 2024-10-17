using BaseAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "StateResourceSO", fileName ="newStateResourceSO")]
public class StateResourceSO : SingletonScriptableObject<StateResourceSO>
{
    // bir madenin �retim h�z� 
    public int GoldproductionRate = 50;
    public int WaterproductionRate = 3400;//6500 7200
    public int SaltproductionRate = 40;//70
    public int MeatproductionRate = 300;//500
    public int FruitsproductionRate = 250;//450
    public int VegetablesproductionRate =450 ;//800
    public int WheatproductionRate = 250;//700
    public int WoodproductionRate = 200;
    public int CoalproductionRate = 50;
    public int IronproductionRate = 50;
    public int StoneproductionRate = 50;
    public int DimondproductionRate = 1;  

    // 1K insan�n g�nl�k kaynak t�ketim miktar�
    public static float WaterConsumptionRate = 0.0650f;
    public static float SaltConsumptionRate = 0.007f;
    public static float MeatConsumptionRate = 0.0050f;
    public static float FruitConsumptionRate = 0.0045f;
    public static float VegetablesConsumptionRate =0.0080f;
    public static float WheatConsumptionRate = 0.0070f;

    // 1k i�in g�nl�k do�um oran� 
    public float BaseBirthRate = 1;

    // 1K i�in g�nl�k �l�m oran�
    public float BaseDeathRate = 1;

    // 1k asker i�in 30 g�nl�k idame maliyeti
    public float SoliderCost = 300;
  

}
