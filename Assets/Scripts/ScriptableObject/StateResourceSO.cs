using BaseAssets;

using UnityEngine;
[CreateAssetMenu(menuName = "StateResourceSO", fileName ="newStateResourceSO")]
public class StateResourceSO : SingletonScriptableObject<StateResourceSO>
{
    // bir madenin üretim hýzý 
    public static int GoldproductionRate = 5;
    public static int WaterproductionRate = 340;
    public static int SaltproductionRate = 40;
    public static int MeatproductionRate = 30;
    public static int FruitsproductionRate = 25;
    public static int VegetablesproductionRate =45 ;
    public static int WheatproductionRate = 25;
    public static int WoodproductionRate = 20;
    public static int CoalproductionRate = 50;
    public static int IronproductionRate = 50;
    public static int StoneproductionRate = 50;
    public static int DimondproductionRate = 1;  

    // 1 insanýn günlük kaynak tüketim miktarý
    public static float WaterConsumptionRate = 0.6f;
    public static float SaltConsumptionRate = 0.07f;
    public static float MeatConsumptionRate = 0.05f;
    public static float FruitConsumptionRate = 0.04f;
    public static float VegetablesConsumptionRate =0.08f;
    public static float WheatConsumptionRate = 0.07f;

    // 1k için günlük doüum oraný 
    public static float BaseBirthRate = 1;

    // 1K için günlük ölüm oraný
    public static float BaseDeathRate = 1;

    // 1k asker için 30 günlük idame maliyeti
    public static float SoliderCost = 300;
  

}
