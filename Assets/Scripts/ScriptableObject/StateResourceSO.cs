using BaseAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "StateResourceSO", fileName ="newStateResourceSO")]
public class StateResourceSO : SingletonScriptableObject<StateResourceSO>
{
    // bir madenin üretim hýzý 
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

    // 1K insanýn günlük kaynak tüketim miktarý
    public static float WaterConsumptionRate = 0.0650f;
    public static float SaltConsumptionRate = 0.007f;
    public static float MeatConsumptionRate = 0.0050f;
    public static float FruitConsumptionRate = 0.0045f;
    public static float VegetablesConsumptionRate =0.0080f;
    public static float WheatConsumptionRate = 0.0070f;

    // 1k için günlük doüum oraný 
    public float BaseBirthRate = 1;

    // 1K için günlük ölüm oraný
    public float BaseDeathRate = 1;

    // 1k asker için 30 günlük idame maliyeti
    public float SoliderCost = 300;
  

}
