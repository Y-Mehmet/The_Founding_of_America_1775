using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static StateResourceSO;
using static Neighbor;
public class GameInitalizer : MonoBehaviour
{
    
    public static GameInitalizer Instance;


    public int goldRate, waterRate, saltRate, meatRate, fruitsRate, vegetablesRate, wheatRate, woodRate, coalRate, ironRate, stoneRate, diamondRate;
    public string[] largeStates, mediumStates, smallStates;
    Dictionary<string, List<ResourceType>> statesImportTradeResTypeList;
    Dictionary<string, List<ResourceType>> statesExportTradeResTypeList;
    Dictionary<string, List<float>> statesImportTradeContratPriceList = new Dictionary<string, List<float>>();
    Dictionary<string, List<float>> statesExportTradeContratPriceList = new Dictionary<string, List<float>>();
    Dictionary<string, List<float>> statesImporTradeLimitList = new Dictionary<string, List<float>>();
    Dictionary<string, List<float>> statesExportTradLimitList = new Dictionary<string, List<float>>();
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {


        InitGame();

    }
    public void InitGame()
    {
        // Initialize the Neighbor class with city data
        goldRate = GoldproductionRate;
        waterRate = WaterproductionRate;
        saltRate = SaltproductionRate;
        meatRate = MeatproductionRate;
        fruitsRate = FruitsproductionRate;
        vegetablesRate = VegetablesproductionRate;
        wheatRate = WheatproductionRate;
        woodRate = WoodproductionRate;
        coalRate = CoalproductionRate;
        ironRate = IronproductionRate;
        stoneRate = StoneproductionRate;
        diamondRate = DimondproductionRate;




        Neighbor game = Neighbor.Instance;
        InitializeCities();
        StateSize();
        InitializedStateDataValue();
        InitializeNeighbors();
      
        SetMineRequaredValue();
        SetMineName();
    }
    void SetTradeData()
    {
        // İthalat ve ihracat çarpanlarını belirle
        float smallStateImportMultiplier = 0.95f;
        float mediumStateImportMultiplier = 0.90f;
        float largeStateImportMultiplier = 0.85f;

        float smallStateExportMultiplier = 1.05f;
        float mediumStateExportMultiplier = 1.10f;
        float largeStateExportMultiplier = 1.15f;

        // İthalat ve ihracat için kaynak listelerini oluştur
        statesImportTradeResTypeList = new Dictionary<string, List<ResourceType>>
    {
        { "smallStates", new List<ResourceType> { ResourceType.Wood, ResourceType.Coal, ResourceType.Iron } },
        { "mediumStates", new List<ResourceType> { ResourceType.Wood, ResourceType.Coal, ResourceType.Iron, ResourceType.Stone } },
        { "largeStates", new List<ResourceType> { (ResourceType)1,(ResourceType)2, (ResourceType)3, (ResourceType)4, (ResourceType)5, (ResourceType)6, (ResourceType)7, (ResourceType)8, (ResourceType)9, (ResourceType)10, (ResourceType)11 } }
    };

        statesExportTradeResTypeList = new Dictionary<string, List<ResourceType>>
    {
        { "smallStates", new List<ResourceType> { ResourceType.Water , ResourceType.Fruits, ResourceType.Vegetables} },
        { "mediumStates", new List<ResourceType> {   ResourceType.Salt,ResourceType.Meat,ResourceType.Fruits, ResourceType.Vegetables, ResourceType.Wheat } },
        { "largeStates", new List<ResourceType> { (ResourceType)2, (ResourceType)3, (ResourceType)4, (ResourceType)5, (ResourceType)6, (ResourceType)7, (ResourceType)8, (ResourceType)9, (ResourceType)10, (ResourceType)11} }
    };

        // İthalat kontrat fiyatlarını dinamik çarpanlarla hesapla
        statesImportTradeContratPriceList = new Dictionary<string, List<float>>
    {
        { "smallStates", new List<float>
            {
              
  
                GameEconomy.Instance.GetGoldValue((ResourceType)1)*smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)2)*smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)3)*smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)4)*smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)5)*smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)6)*smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)7)*smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)8)*smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)9)*smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)10)*smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)11)*smallStateImportMultiplier,


            }
        },
        { "mediumStates", new List<float>
        {  
                
                GameEconomy.Instance.GetGoldValue((ResourceType)1)*mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)2)*mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)3)*mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)4)*mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)5)*mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)6)*mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)7)*mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)8)*mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)9)*mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)10)*mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)11)*mediumStateImportMultiplier,


        }
        },
        { "largeStates", new List<float>
            {
                GameEconomy.Instance.GetGoldValue((ResourceType)1) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)2) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)3) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)4) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)5) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)6) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)7) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)8) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)9) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)10) *largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)11) *largeStateImportMultiplier,
            }
        }
    };

        // İhracat kontrat fiyatlarını dinamik çarpanlarla hesapla
        statesExportTradeContratPriceList = new Dictionary<string, List<float>>
    {
        { "smallStates", new List<float>
            {
               
                  GameEconomy.Instance.GetGoldValue((ResourceType)1) * 
               
                
                GameEconomy.Instance.GetGoldValue((ResourceType)1)*smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)2)*smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)3)*smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)4)*smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)5)*smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)6)*smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)7)*smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)8)*smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)9)*smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)10)*smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)11)*smallStateExportMultiplier,


            }
        },
        { "mediumStates", new List<float>
            {
                
             
                
                GameEconomy.Instance.GetGoldValue((ResourceType)1)* mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)2)* mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)3)* mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)4)* mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)5)* mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)6)* mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)7)* mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)8)* mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)9)* mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)10)* mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)11)* mediumStateExportMultiplier,

            }
        },
        { "largeStates", new List<float>
            {

                GameEconomy.Instance.GetGoldValue((ResourceType)1) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)2) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)3) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)4) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)5) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)6) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)7) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)8) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)9) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)10) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue((ResourceType)11) * largeStateExportMultiplier,
               
            }
        }
    };
        // İthalat ve ihracat limitlerini 10 olarak ayarla
        statesImporTradeLimitList = new Dictionary<string, List<float>>
    {
        { "smallStates",  new List<float> { 65000, 54000, 0, 0, 0, 0, 80070, 70830, 17800, 17050 ,100} },
        { "mediumStates", new List<float> { 100000, 80550, 50580, 40470, 13040, 40530, 5430, 73000, 40057, 52400,200} },
        { "largeStates", new List<float> { 150000, 403360, 70370, 70300, 90730, 30900, 30000, 0, 0, 0,0} },
    };

        statesExportTradLimitList = new Dictionary<string, List<float>>
    {
        { "smallStates", new List<float> { 0, 0, 0, 20000, 20000, 20000, 0, 0, 0, 0 ,0} },
        { "mediumStates", new List<float> { 150000, 25000, 20500, 25000, 20500, 50000, 15000, 35000, 100000, 40000 ,0} },
        { "largeStates", new List<float>{ 0, 0, 0, 0, 0, 0, 0, 250000, 65000, 200000 ,50} },
    };
    }

    void SetMineName()
    {
        MineManager.instance.ResMineNameList.Clear();
        MineManager.instance.ResMineNameList.Add(ResourceType.Gold, "Gold Mine");
        MineManager.instance.ResMineNameList.Add(ResourceType.Water, "Mineral Water Factory");
        MineManager.instance.ResMineNameList.Add(ResourceType.Salt, "Salt Mine");
        MineManager.instance.ResMineNameList.Add(ResourceType.Meat, "Cattle Farm");
        MineManager.instance.ResMineNameList.Add(ResourceType.Fruits, "Garden");
        MineManager.instance.ResMineNameList.Add(ResourceType.Vegetables, "Greenhouse");
        MineManager.instance.ResMineNameList.Add(ResourceType.Wheat,"Wheat Field");
        MineManager.instance.ResMineNameList.Add(ResourceType.Wood,"Forest");
        MineManager.instance.ResMineNameList.Add(ResourceType.Coal,"Coal Mine");
        MineManager.instance.ResMineNameList.Add(ResourceType.Iron, "Iron Mine");
        MineManager.instance.ResMineNameList.Add(ResourceType.Stone,"Quarry");
        MineManager.instance.ResMineNameList.Add(ResourceType.Diamond, " Gem Mine");


    }
    void SetMineRequaredValue()
    {
        MineManager.instance.MineRequiredResList.Clear();
        MineManager.instance.MineRequiredResList.Add(ResourceType.Gold, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone },  new List<int>{22000,22000,11000}));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Water, new MineMatarials
       (new List<ResourceType> { ResourceType.Wood, ResourceType.Iron, ResourceType.Stone }, new List<int> { 17000, 17000, 8500 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Salt, new MineMatarials
       (new List<ResourceType> { ResourceType.Wood, ResourceType.Iron, ResourceType.Stone }, new List<int> { 16000, 16000, 8000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Meat, new MineMatarials
       (new List<ResourceType> { ResourceType.Water, ResourceType.Wood, ResourceType.Stone }, new List<int> {  7500,15000, 15000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Fruits, new MineMatarials
       (new List<ResourceType> { ResourceType.Water, ResourceType.Wood, ResourceType.Stone }, new List<int> { 8000,16000, 16000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Vegetables, new MineMatarials
       (new List<ResourceType> { ResourceType.Water, ResourceType.Wood, ResourceType.Stone }, new List<int> { 8500,17000, 17000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Wheat, new MineMatarials
       (new List<ResourceType> { ResourceType.Water, ResourceType.Wood, ResourceType.Stone }, new List<int> { 8000,16000, 16000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Wood, new MineMatarials
       (new List<ResourceType> { ResourceType.Water, ResourceType.Iron, ResourceType.Stone }, new List<int> { 11000, 22000, 22000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Coal, new MineMatarials
       (new List<ResourceType> { ResourceType.Wood, ResourceType.Iron, ResourceType.Stone }, new List<int> { 15000, 15000, 7500 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Iron, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Wood, ResourceType.Stone }, new List<int> { 12000, 12000, 6000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Stone, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Wood }, new List<int> { 20000, 20000, 10000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Diamond, new MineMatarials
     (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 48000, 48000, 24000 }));



    }
    private void InitializeCities()
    {
        string[] cities = {
            "Washington", "Oregon", "Idaho", "Montana", "North Dakota", "Minnesota", "Wisconsin",
            "Wyoming", "South Dakota", "Iowa", "Illinois", "Michigan", "New York", "Vermont",
            "New Hampshire", "Maine", "Massachusetts", "New Jersey", "Pennsylvania", "Maryland",
            "Ohio", "Indiana", "Virginia", "North Carolina", "South Carolina", "Georgia",
            "Florida", "Alabama", "Mississippi", "Louisiana", "Texas", "New Mexico", "Arizona",
            "California", "Nevada", "Utah", "Colorado", "Kansas", "Nebraska", "Missouri",
            "Kentucky", "West Virginia", "Tennessee", "Arkansas", "Oklahoma"
        };

        foreach (string city in cities)
        {
            Neighbor.Instance.AddCity(city);
           
        }

    }
    private void StateSize()
    {

        largeStates = new[] { "California", "Texas", "New York", "Florida", "Washington", "Georgia" };
         mediumStates = new[] { 
            "Oregon", "Idaho", "Montana", "North Dakota", "Minnesota", "Wisconsin",
            "Wyoming", "South Dakota", "Iowa", "Illinois", "Michigan",
              "Massachusetts", "New Jersey", "Pennsylvania", "Maryland",
            "Ohio", "Indiana", "Virginia", "North Carolina", "South Carolina",
           "Alabama", "Mississippi", "Louisiana", "New Mexico", "Arizona",
             "Nevada", "Utah", "Colorado", "Kansas", "Nebraska", "Missouri",
            "Kentucky", "West Virginia", "Tennessee", "Arkansas", "Oklahoma" };
         smallStates = new[] { "Maine", "New Hampshire", "Vermont", "Rhode Island", "Delaware" };
         

         }


    private void InitializedStateDataValue()
    {
       




        System.Random rand = new System.Random();

        SetTradeData();
        foreach (Transform state in Usa.Instance.transform)
        {
            if (state.GetComponent<State>() != null)
            {
                    State s = state.GetComponent<State>();
                    if(s.name== "Massachusetts")
                    {
                        s.IsCapitalCity= true;
                    GameManager.TotalPopulationManager(10000);
                    }
                

                    if (largeStates.Contains(state.name))
                    {// large region

                    s.StateName = state.name;
                    s.Population = 15000;
                    s.LandArmySize = s.Population * GameManager.ArrmyRatio; // Nüfusun %25'i
                    s.NavalArmySize = s.Population * GameManager.ArrmyRatio; // Nüfusun %25'i
                    s.ArmyBarrackSize= (int)(s.Population* GameManager.ArrmyRatio*2);
                    s.Morele = 100;
                    s.Resources = 11;
                  
                    s.UnitLandArmyPower = (float)(0.75 + rand.NextDouble() * 0.25); //ort 0.875
                        s.UnitNavalArmyPower = (float)(0.75 + rand.NextDouble() * 0.25); //ort 0.875
                        s.resourceData = new Dictionary<ResourceType, ResourceData>
                        {
                            { ResourceType.Gold, new ResourceData {resourceType=ResourceType.Gold, currentAmount = 15000, mineCount = 40, productionRate = goldRate }}, // Gold'a tüketim eklenmedi
                            { ResourceType.Water, new ResourceData {resourceType=ResourceType.Water, currentAmount = 40000, mineCount = 44, productionRate = waterRate, consumptionAmount = WaterConsumptionRate }},
                            { ResourceType.Salt, new ResourceData {resourceType=ResourceType.Salt, currentAmount = 20000, mineCount = 50, productionRate = saltRate, consumptionAmount = SaltConsumptionRate }},
                            { ResourceType.Meat, new ResourceData {resourceType=ResourceType.Meat, currentAmount = 30000, mineCount = 48, productionRate = meatRate, consumptionAmount = MeatConsumptionRate }},
                            { ResourceType.Fruits, new ResourceData {resourceType=ResourceType.Fruits, currentAmount = 25000, mineCount = 44, productionRate = fruitsRate, consumptionAmount = FruitConsumptionRate }},
                            { ResourceType.Vegetables, new ResourceData {resourceType=ResourceType.Vegetables, currentAmount = 24000, mineCount = 40, productionRate = vegetablesRate, consumptionAmount = VegetablesConsumptionRate }},
                            { ResourceType.Wheat, new ResourceData {resourceType=ResourceType.Wheat, currentAmount = 25000, mineCount = 50, productionRate = wheatRate, consumptionAmount = WheatConsumptionRate }},
                            { ResourceType.Wood, new ResourceData {resourceType=ResourceType.Wood, currentAmount = 2600, mineCount = 44, productionRate = woodRate }}, // Wood'a tüketim eklenmedi
                            { ResourceType.Coal, new ResourceData {resourceType=ResourceType.Coal, currentAmount = 2800, mineCount = 40, productionRate = coalRate }}, // Coal'a tüketim eklenmedi
                            { ResourceType.Iron, new ResourceData {resourceType=ResourceType.Iron, currentAmount = 3000, mineCount = 40, productionRate = ironRate }}, // Iron'a tüketim eklenmedi
                            { ResourceType.Stone, new ResourceData {resourceType=ResourceType.Stone, currentAmount = 2500, mineCount = 40, productionRate = stoneRate }}, // Stone'a tüketim eklenmedi
                            { ResourceType.Diamond, new ResourceData {resourceType=ResourceType.Diamond, currentAmount = 10, mineCount = 1, productionRate = diamondRate }} // Diamond'a tüketim eklenmedi
                        };
                    // Import Trade - largeStates için
                    // Örneğin, importTrade ve exportTrade nesnelerini her state için kopyalayarak oluştur:
                         s.tradeLists.Insert(0,new Trade(
                         TradeType.Import,
                         new List<ResourceType>(statesImportTradeResTypeList["largeStates"]),
                         new List<float>(statesImportTradeContratPriceList["largeStates"]),
                         new List<float>(statesImporTradeLimitList["largeStates"])
                        ));


                    s.tradeLists.Insert(1,( new Trade(
                            TradeType.Export,
                              new List<ResourceType>(statesImportTradeResTypeList["largeStates"]),
                            new List<float>(statesExportTradeContratPriceList["largeStates"]),
                            new List<float>(statesExportTradLimitList["largeStates"])
                        )));


                        TaxData StampTax = new TaxData
                        {
                            taxType = TaxType.StampTax,
                            currentRate = 5,
                            toleranceLimit = 5,
                            taxIncome = 0,
                            unitTaxIncome = 0.0005f
                        };
                        TaxData IncomeTax = new TaxData
                        {
                            taxType = TaxType.IncomeTax,
                            currentRate = 10,
                            toleranceLimit = 10,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        TaxData DirectTax = new TaxData
                        {
                            taxType = TaxType.DirectTax,
                            currentRate = 10,
                            toleranceLimit = 10,
                            taxIncome = 0,
                            unitTaxIncome = 20
                        };
                        TaxData ValueAddedTax = new TaxData
                        {
                            taxType = TaxType.ValueAddedTax,
                            currentRate = 7,
                            toleranceLimit = 7,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        s.Taxes.Add(StampTax);
                        s.Taxes.Add(IncomeTax);
                        s.Taxes.Add(DirectTax);
                        s.Taxes.Add(ValueAddedTax);


                    }

                    else if (smallStates.Contains(state.name))
                    {
                    s.StateName = state.name;
                    s.Population = 5000;
                    s.LandArmySize = s.Population * GameManager.ArrmyRatio; // Nüfusun %25'i
                    s.NavalArmySize = s.Population * GameManager.ArrmyRatio; // Nüfusun %25'i
                    s.ArmyBarrackSize = (int)(s.Population * GameManager.ArrmyRatio * 2);
                    s.Morele = 90;
                    s.Resources = 10;
                    s.UnitLandArmyPower = (float)(0.35 + rand.NextDouble() * 0.05);
                        s.UnitNavalArmyPower = (float)(0.35 + rand.NextDouble() * 0.05);
                    s.resourceData = new Dictionary<ResourceType, ResourceData>
                        {
                            { ResourceType.Gold, new ResourceData {resourceType=ResourceType.Gold, currentAmount = 1500, mineCount = 10, productionRate = goldRate }}, // Gold'a tüketim eklenmedi
                            { ResourceType.Water, new ResourceData {resourceType=ResourceType.Water, currentAmount = 40000, mineCount = 11, productionRate = waterRate, consumptionAmount = WaterConsumptionRate }},
                            { ResourceType.Salt, new ResourceData {resourceType=ResourceType.Salt, currentAmount = 20000, mineCount = 15, productionRate = saltRate, consumptionAmount = SaltConsumptionRate }},
                            { ResourceType.Meat, new ResourceData {resourceType=ResourceType.Meat, currentAmount = 30000, mineCount = 12, productionRate = meatRate, consumptionAmount = MeatConsumptionRate }},
                            { ResourceType.Fruits, new ResourceData {resourceType=ResourceType.Fruits, currentAmount = 25000, mineCount = 12, productionRate = fruitsRate, consumptionAmount = FruitConsumptionRate }},
                            { ResourceType.Vegetables, new ResourceData {resourceType=ResourceType.Vegetables, currentAmount = 24000, mineCount = 11, productionRate = vegetablesRate, consumptionAmount = VegetablesConsumptionRate }},
                            { ResourceType.Wheat, new ResourceData {resourceType=ResourceType.Wheat, currentAmount = 25000, mineCount = 21, productionRate = wheatRate, consumptionAmount = WheatConsumptionRate }},
                            { ResourceType.Wood, new ResourceData {resourceType=ResourceType.Wood, currentAmount = 2600, mineCount = 11, productionRate = woodRate }}, // Wood'a tüketim eklenmedi
                            { ResourceType.Coal, new ResourceData {resourceType=ResourceType.Coal, currentAmount = 2800, mineCount = 10, productionRate = coalRate }}, // Coal'a tüketim eklenmedi
                            { ResourceType.Iron, new ResourceData {resourceType=ResourceType.Iron, currentAmount = 3000, mineCount = 10, productionRate = ironRate }}, // Iron'a tüketim eklenmedi
                            { ResourceType.Stone, new ResourceData {resourceType=ResourceType.Stone, currentAmount = 2500, mineCount = 10, productionRate = stoneRate }}, // Stone'a tüketim eklenmedi
                            { ResourceType.Diamond, new ResourceData {resourceType=ResourceType.Diamond, currentAmount = 10, mineCount = 0, productionRate = diamondRate }} // Diamond'a tüketim eklenmedi
                        };

                    s.tradeLists.Insert(0,new Trade(
                            TradeType.Import,
                             new List<ResourceType>(statesImportTradeResTypeList["largeStates"]),
                            new List<float>(statesImportTradeContratPriceList["smallStates"]),
                            new List<float>(statesImporTradeLimitList["smallStates"])                   // İthalat limitleri
                        )) ;


                    s.tradeLists.Insert(1, new Trade(
                            TradeType.Export,
                              new List<ResourceType>(statesImportTradeResTypeList["largeStates"]),
                            new List<float>(statesExportTradeContratPriceList["smallStates"]),
                            new List<float>(statesExportTradLimitList["smallStates"])
                        // İhracat limitleri
                        )) ;
                        TaxData StampTax = new TaxData
                        {
                            taxType = TaxType.StampTax,
                            currentRate = 10,
                            toleranceLimit = 10,
                            taxIncome = 0,
                            unitTaxIncome = 0.0005f
                        };
                        TaxData IncomeTax = new TaxData
                        {
                            taxType = TaxType.IncomeTax,
                            currentRate = 25,
                            toleranceLimit = 25,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        TaxData DirectTax = new TaxData
                        {
                            taxType = TaxType.DirectTax,
                            currentRate = 30,
                            toleranceLimit = 30,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        TaxData ValueAddedTax = new TaxData
                        {
                            taxType = TaxType.ValueAddedTax,
                            currentRate = 18,
                            toleranceLimit = 18,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        s.Taxes.Add(StampTax);
                        s.Taxes.Add(IncomeTax);
                        s.Taxes.Add(DirectTax);
                        s.Taxes.Add(ValueAddedTax);
                    }
                    else
                    {// midle rigion
                    s.StateName = state.name;
                    s.Population = 10000;
                    s.LandArmySize = s.Population * GameManager.ArrmyRatio; // Nüfusun %25'i
                    s.NavalArmySize = s.Population * GameManager.ArrmyRatio; // Nüfusun %25'i
                    s.ArmyBarrackSize = (int)(s.Population * GameManager.ArrmyRatio * 2);
                    s.Morele = 95;
                    s.Resources = 1;
                    s.UnitLandArmyPower = (float)(0.75 + rand.NextDouble() * 0.15);
                        s.UnitNavalArmyPower = (float)(0.75 + rand.NextDouble() * 0.15);
                    s.resourceData = new Dictionary<ResourceType, ResourceData>
                        {
                            { ResourceType.Gold, new ResourceData {resourceType=ResourceType.Gold, currentAmount = 1500, mineCount = 20, productionRate = goldRate }}, // Gold'a tüketim eklenmedi
                            { ResourceType.Water, new ResourceData {resourceType=ResourceType.Water, currentAmount = 40000, mineCount = 22, productionRate = waterRate, consumptionAmount = WaterConsumptionRate }},
                            { ResourceType.Salt, new ResourceData {resourceType=ResourceType.Salt, currentAmount = 20000, mineCount = 30, productionRate = saltRate, consumptionAmount = SaltConsumptionRate }},
                            { ResourceType.Meat, new ResourceData {resourceType=ResourceType.Meat, currentAmount = 30000, mineCount = 24, productionRate = meatRate, consumptionAmount = MeatConsumptionRate }},
                            { ResourceType.Fruits, new ResourceData {resourceType=ResourceType.Fruits, currentAmount = 25000, mineCount = 22, productionRate = fruitsRate, consumptionAmount = FruitConsumptionRate }},
                            { ResourceType.Vegetables, new ResourceData {resourceType=ResourceType.Vegetables, currentAmount = 24000, mineCount = 20, productionRate = vegetablesRate, consumptionAmount = VegetablesConsumptionRate }},
                            { ResourceType.Wheat, new ResourceData {resourceType=ResourceType.Wheat, currentAmount = 25000, mineCount = 40, productionRate = wheatRate, consumptionAmount = WheatConsumptionRate }},
                            { ResourceType.Wood, new ResourceData {resourceType=ResourceType.Wood, currentAmount = 2600, mineCount = 22, productionRate = woodRate }}, // Wood'a tüketim eklenmedi
                            { ResourceType.Coal, new ResourceData {resourceType=ResourceType.Coal, currentAmount = 2800, mineCount = 20, productionRate = coalRate }}, // Coal'a tüketim eklenmedi
                            { ResourceType.Iron, new ResourceData {resourceType=ResourceType.Iron, currentAmount = 3000, mineCount = 20, productionRate = ironRate }}, // Iron'a tüketim eklenmedi
                            { ResourceType.Stone, new ResourceData {resourceType=ResourceType.Stone, currentAmount = 2500, mineCount = 20, productionRate = stoneRate }}, // Stone'a tüketim eklenmedi
                            { ResourceType.Diamond, new ResourceData {resourceType=ResourceType.Diamond, currentAmount = 0, mineCount = 0, productionRate = diamondRate }} // Diamond'a tüketim eklenmedi
                        };
                    // Import Trade - mediumStates için
                    s.tradeLists.Insert(0, new Trade(
                            TradeType.Import,
                             new List<ResourceType>(statesImportTradeResTypeList["largeStates"]),
                            new List<float>(statesImportTradeContratPriceList["mediumStates"]),
                            new List<float>(statesImporTradeLimitList["mediumStates"])                 // İthalat limitleri
                        ));

                    // Export Trade - mediumStates için
                    s.tradeLists.Insert(1, new Trade(
                            TradeType.Export,
                             new List<ResourceType>(statesImportTradeResTypeList["largeStates"]),
                            new List<float>(statesExportTradeContratPriceList["mediumStates"]),
                            new List<float>(statesExportTradLimitList["mediumStates"])                 // İhracat limitleri
                        ));
                        TaxData StampTax = new TaxData
                        {
                            taxType = TaxType.StampTax,
                            currentRate = 7,
                            toleranceLimit = 7,
                            taxIncome = 0,
                            unitTaxIncome = 0.0005f
                        };
                        TaxData IncomeTax = new TaxData
                        {
                            taxType = TaxType.IncomeTax,
                            currentRate = 12,
                            toleranceLimit = 12,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        TaxData DirectTax = new TaxData
                        {
                            taxType = TaxType.DirectTax,
                            currentRate = 20,
                            toleranceLimit = 20,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        TaxData ValueAddedTax = new TaxData
                        {
                            taxType = TaxType.ValueAddedTax,
                            currentRate = 18,
                            toleranceLimit = 18,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        s.Taxes.Add(StampTax);
                        s.Taxes.Add(IncomeTax);
                        s.Taxes.Add(DirectTax);
                        s.Taxes.Add(ValueAddedTax);
                    }


                   
                

            }
        }
        
     }
   
    private void InitializeNeighbors()
    {
        // Komşuluk ilişkilerini ekle
        Neighbor.Instance.AddNeighbor("Washington", "Oregon");
       
        Neighbor.Instance.AddNeighbor("Washington", "Idaho");
        

        Neighbor.Instance.AddNeighbor("Oregon", "Washington");
        Neighbor.Instance.AddNeighbor("Oregon", "Idaho");
        Neighbor.Instance.AddNeighbor("Oregon", "California");
        Neighbor.Instance.AddNeighbor("Oregon", "Nevada");

        Neighbor.Instance.AddNeighbor("Idaho", "Washington");
        Neighbor.Instance.AddNeighbor("Idaho", "Oregon");
        Neighbor.Instance.AddNeighbor("Idaho", "Nevada");
        Neighbor.Instance.AddNeighbor("Idaho", "Utah");
        Neighbor.Instance.AddNeighbor("Idaho", "Wyoming");
        Neighbor.Instance.AddNeighbor("Idaho", "Montana");

        Neighbor.Instance.AddNeighbor("Montana", "Idaho");
        Neighbor.Instance.AddNeighbor("Montana", "Wyoming");
        Neighbor.Instance.AddNeighbor("Montana", "North Dakota");
        Neighbor.Instance.AddNeighbor("Montana", "South Dakota");

        Neighbor.Instance.AddNeighbor("North Dakota", "Montana");
        Neighbor.Instance.AddNeighbor("North Dakota", "South Dakota");
        Neighbor.Instance.AddNeighbor("North Dakota", "Minnesota");

        Neighbor.Instance.AddNeighbor("Minnesota", "North Dakota");
        Neighbor.Instance.AddNeighbor("Minnesota", "South Dakota");
        Neighbor.Instance.AddNeighbor("Minnesota", "Iowa");
        Neighbor.Instance.AddNeighbor("Minnesota", "Wisconsin");

        Neighbor.Instance.AddNeighbor("Wisconsin", "Minnesota");
        Neighbor.Instance.AddNeighbor("Wisconsin", "Iowa");
        Neighbor.Instance.AddNeighbor("Wisconsin", "Illinois");

        Neighbor.Instance.AddNeighbor("Wyoming", "Montana");
        Neighbor.Instance.AddNeighbor("Wyoming", "South Dakota");
        Neighbor.Instance.AddNeighbor("Wyoming", "Nebraska");
        Neighbor.Instance.AddNeighbor("Wyoming", "Colorado");
        Neighbor.Instance.AddNeighbor("Wyoming", "Utah");
        Neighbor.Instance.AddNeighbor("Wyoming", "Idaho");

        Neighbor.Instance.AddNeighbor("South Dakota", "North Dakota");
        Neighbor.Instance.AddNeighbor("South Dakota", "Minnesota");
        Neighbor.Instance.AddNeighbor("South Dakota", "Iowa");
        Neighbor.Instance.AddNeighbor("South Dakota", "Nebraska");
        Neighbor.Instance.AddNeighbor("South Dakota", "Wyoming");
        Neighbor.Instance.AddNeighbor("South Dakota", "Montana");

        Neighbor.Instance.AddNeighbor("Iowa", "Minnesota");
        Neighbor.Instance.AddNeighbor("Iowa", "Wisconsin");
        Neighbor.Instance.AddNeighbor("Iowa", "Illinois");
        Neighbor.Instance.AddNeighbor("Iowa", "Missouri");
        Neighbor.Instance.AddNeighbor("Iowa", "Nebraska");
        Neighbor.Instance.AddNeighbor("Iowa", "South Dakota");

        Neighbor.Instance.AddNeighbor("Illinois", "Wisconsin");
        Neighbor.Instance.AddNeighbor("Illinois", "Iowa");
        Neighbor.Instance.AddNeighbor("Illinois", "Missouri");
        Neighbor.Instance.AddNeighbor("Illinois", "Kentucky");
        Neighbor.Instance.AddNeighbor("Illinois", "Indiana");

        Neighbor.Instance.AddNeighbor("Michigan", "Wisconsin");
        Neighbor.Instance.AddNeighbor("Michigan", "Indiana");
        Neighbor.Instance.AddNeighbor("Michigan", "Ohio");

        Neighbor.Instance.AddNeighbor("New York", "Pennsylvania");
        Neighbor.Instance.AddNeighbor("New York", "New Jersey");
        Neighbor.Instance.AddNeighbor("New York", "Vermont");
        Neighbor.Instance.AddNeighbor("New York", "Massachusetts");
        Neighbor.Instance.AddNeighbor("New York", "Connecticut");

        Neighbor.Instance.AddNeighbor("Vermont", "New York");
        Neighbor.Instance.AddNeighbor("Vermont", "New Hampshire");
        Neighbor.Instance.AddNeighbor("Vermont", "Massachusetts");

        Neighbor.Instance.AddNeighbor("New Hampshire", "Vermont");
        Neighbor.Instance.AddNeighbor("New Hampshire", "Maine");
        Neighbor.Instance.AddNeighbor("New Hampshire", "Massachusetts");

        Neighbor.Instance.AddNeighbor("Maine", "New Hampshire");

        Neighbor.Instance.AddNeighbor("Massachusetts", "New York");
        Neighbor.Instance.AddNeighbor("Massachusetts", "Vermont");
        Neighbor.Instance.AddNeighbor("Massachusetts", "New Hampshire");
        Neighbor.Instance.AddNeighbor("Massachusetts", "Connecticut");
        Neighbor.Instance.AddNeighbor("Massachusetts", "Rhode Island");

        Neighbor.Instance.AddNeighbor("New Jersey", "New York");
        Neighbor.Instance.AddNeighbor("New Jersey", "Pennsylvania");

        Neighbor.Instance.AddNeighbor("Pennsylvania", "New York");
        Neighbor.Instance.AddNeighbor("Pennsylvania", "New Jersey");
        Neighbor.Instance.AddNeighbor("Pennsylvania", "Delaware");
        Neighbor.Instance.AddNeighbor("Pennsylvania", "Maryland");
        Neighbor.Instance.AddNeighbor("Pennsylvania", "West Virginia");
        Neighbor.Instance.AddNeighbor("Pennsylvania", "Ohio");

        Neighbor.Instance.AddNeighbor("Maryland", "Pennsylvania");
        Neighbor.Instance.AddNeighbor("Maryland", "Delaware");
        Neighbor.Instance.AddNeighbor("Maryland", "Virginia");
        Neighbor.Instance.AddNeighbor("Maryland", "West Virginia");

        Neighbor.Instance.AddNeighbor("Ohio", "Pennsylvania");
        Neighbor.Instance.AddNeighbor("Ohio", "West Virginia");
        Neighbor.Instance.AddNeighbor("Ohio", "Kentucky");
        Neighbor.Instance.AddNeighbor("Ohio", "Indiana");
        Neighbor.Instance.AddNeighbor("Ohio", "Michigan");

        Neighbor.Instance.AddNeighbor("Indiana", "Michigan");
        Neighbor.Instance.AddNeighbor("Indiana", "Ohio");
        Neighbor.Instance.AddNeighbor("Indiana", "Kentucky");
        Neighbor.Instance.AddNeighbor("Indiana", "Illinois");

        Neighbor.Instance.AddNeighbor("Virginia", "Maryland");
        Neighbor.Instance.AddNeighbor("Virginia", "West Virginia");
        Neighbor.Instance.AddNeighbor("Virginia", "Kentucky");
        Neighbor.Instance.AddNeighbor("Virginia", "Tennessee");
        Neighbor.Instance.AddNeighbor("Virginia", "North Carolina");

        Neighbor.Instance.AddNeighbor("North Carolina", "Virginia");
        Neighbor.Instance.AddNeighbor("North Carolina", "Tennessee");
        Neighbor.Instance.AddNeighbor("North Carolina", "South Carolina");
        Neighbor.Instance.AddNeighbor("North Carolina", "Georgia");

        Neighbor.Instance.AddNeighbor("South Carolina", "North Carolina");
        Neighbor.Instance.AddNeighbor("South Carolina", "Georgia");

        Neighbor.Instance.AddNeighbor("Georgia", "North Carolina");
        Neighbor.Instance.AddNeighbor("Georgia", "South Carolina");
        Neighbor.Instance.AddNeighbor("Georgia", "Tennessee");
        Neighbor.Instance.AddNeighbor("Georgia", "Alabama");
        Neighbor.Instance.AddNeighbor("Georgia", "Florida");

        Neighbor.Instance.AddNeighbor("Florida", "Georgia");
        Neighbor.Instance.AddNeighbor("Florida", "Alabama");

        Neighbor.Instance.AddNeighbor("Alabama", "Tennessee");
        Neighbor.Instance.AddNeighbor("Alabama", "Georgia");
        Neighbor.Instance.AddNeighbor("Alabama", "Florida");
        Neighbor.Instance.AddNeighbor("Alabama", "Mississippi");

        Neighbor.Instance.AddNeighbor("Mississippi", "Tennessee");
        Neighbor.Instance.AddNeighbor("Mississippi", "Alabama");
        Neighbor.Instance.AddNeighbor("Mississippi", "Louisiana");
        Neighbor.Instance.AddNeighbor("Mississippi", "Arkansas");

        Neighbor.Instance.AddNeighbor("Louisiana", "Arkansas");
        Neighbor.Instance.AddNeighbor("Louisiana", "Mississippi");
        Neighbor.Instance.AddNeighbor("Louisiana", "Texas");

        Neighbor.Instance.AddNeighbor("Texas", "Oklahoma");
        Neighbor.Instance.AddNeighbor("Texas", "Arkansas");
        Neighbor.Instance.AddNeighbor("Texas", "Louisiana");
        Neighbor.Instance.AddNeighbor("Texas", "New Mexico");

        Neighbor.Instance.AddNeighbor("New Mexico", "Texas");
        Neighbor.Instance.AddNeighbor("New Mexico", "Oklahoma");
        Neighbor.Instance.AddNeighbor("New Mexico", "Colorado");
        Neighbor.Instance.AddNeighbor("New Mexico", "Arizona");

        Neighbor.Instance.AddNeighbor("Arizona", "California");
        Neighbor.Instance.AddNeighbor("Arizona", "Nevada");
        Neighbor.Instance.AddNeighbor("Arizona", "Utah");
        Neighbor.Instance.AddNeighbor("Arizona", "Colorado");
        Neighbor.Instance.AddNeighbor("Arizona", "New Mexico");

        Neighbor.Instance.AddNeighbor("California", "Oregon");
        Neighbor.Instance.AddNeighbor("California", "Nevada");
        Neighbor.Instance.AddNeighbor("California", "Arizona");

        Neighbor.Instance.AddNeighbor("Nevada", "Oregon");
        Neighbor.Instance.AddNeighbor("Nevada", "Idaho");
        Neighbor.Instance.AddNeighbor("Nevada", "Utah");
        Neighbor.Instance.AddNeighbor("Nevada", "Arizona");
        Neighbor.Instance.AddNeighbor("Nevada", "California");

        Neighbor.Instance.AddNeighbor("Utah", "Idaho");
        Neighbor.Instance.AddNeighbor("Utah", "Wyoming");
        Neighbor.Instance.AddNeighbor("Utah", "Colorado");
        Neighbor.Instance.AddNeighbor("Utah", "Arizona");
        Neighbor.Instance.AddNeighbor("Utah", "Nevada");

        Neighbor.Instance.AddNeighbor("Colorado", "Wyoming");
        Neighbor.Instance.AddNeighbor("Colorado", "Nebraska");
        Neighbor.Instance.AddNeighbor("Colorado", "Kansas");
        Neighbor.Instance.AddNeighbor("Colorado", "Oklahoma");
        Neighbor.Instance.AddNeighbor("Colorado", "New Mexico");
        Neighbor.Instance.AddNeighbor("Colorado", "Utah");

        Neighbor.Instance.AddNeighbor("Kansas", "Nebraska");
        Neighbor.Instance.AddNeighbor("Kansas", "Missouri");
        Neighbor.Instance.AddNeighbor("Kansas", "Oklahoma");
        Neighbor.Instance.AddNeighbor("Kansas", "Colorado");

        Neighbor.Instance.AddNeighbor("Nebraska", "South Dakota");
        Neighbor.Instance.AddNeighbor("Nebraska", "Iowa");
        Neighbor.Instance.AddNeighbor("Nebraska", "Missouri");
        Neighbor.Instance.AddNeighbor("Nebraska", "Kansas");
        Neighbor.Instance.AddNeighbor("Nebraska", "Colorado");
        Neighbor.Instance.AddNeighbor("Nebraska", "Wyoming");

        Neighbor.Instance.AddNeighbor("Missouri", "Iowa");
        Neighbor.Instance.AddNeighbor("Missouri", "Nebraska");
        Neighbor.Instance.AddNeighbor("Missouri", "Kansas");
        Neighbor.Instance.AddNeighbor("Missouri", "Oklahoma");
        Neighbor.Instance.AddNeighbor("Missouri", "Arkansas");
        Neighbor.Instance.AddNeighbor("Missouri", "Tennessee");
        Neighbor.Instance.AddNeighbor("Missouri", "Kentucky");
        Neighbor.Instance.AddNeighbor("Missouri", "Illinois");

        Neighbor.Instance.AddNeighbor("Kentucky", "Illinois");
        Neighbor.Instance.AddNeighbor("Kentucky", "Indiana");
        Neighbor.Instance.AddNeighbor("Kentucky", "Ohio");
        Neighbor.Instance.AddNeighbor("Kentucky", "West Virginia");
        Neighbor.Instance.AddNeighbor("Kentucky", "Virginia");
        Neighbor.Instance.AddNeighbor("Kentucky", "Tennessee");
        Neighbor.Instance.AddNeighbor("Kentucky", "Missouri");

        Neighbor.Instance.AddNeighbor("West Virginia", "Ohio");
        Neighbor.Instance.AddNeighbor("West Virginia", "Pennsylvania");
        Neighbor.Instance.AddNeighbor("West Virginia", "Maryland");
        Neighbor.Instance.AddNeighbor("West Virginia", "Virginia");
        Neighbor.Instance.AddNeighbor("West Virginia", "Kentucky");

        Neighbor.Instance.AddNeighbor("Tennessee", "Kentucky");
        Neighbor.Instance.AddNeighbor("Tennessee", "Virginia");
        Neighbor.Instance.AddNeighbor("Tennessee", "North Carolina");
        Neighbor.Instance.AddNeighbor("Tennessee", "Georgia");
        Neighbor.Instance.AddNeighbor("Tennessee", "Alabama");
        Neighbor.Instance.AddNeighbor("Tennessee", "Mississippi");
        Neighbor.Instance.AddNeighbor("Tennessee", "Arkansas");
        Neighbor.Instance.AddNeighbor("Tennessee", "Missouri");

        Neighbor.Instance.AddNeighbor("Arkansas", "Missouri");
        Neighbor.Instance.AddNeighbor("Arkansas", "Tennessee");
        Neighbor.Instance.AddNeighbor("Arkansas", "Mississippi");
        Neighbor.Instance.AddNeighbor("Arkansas", "Louisiana");
        Neighbor.Instance.AddNeighbor("Arkansas", "Texas");
        Neighbor.Instance.AddNeighbor("Arkansas", "Oklahoma");

        Neighbor.Instance.AddNeighbor("Oklahoma", "Colorado");
        Neighbor.Instance.AddNeighbor("Oklahoma", "Kansas");
        Neighbor.Instance.AddNeighbor("Oklahoma", "Missouri");
        Neighbor.Instance.AddNeighbor("Oklahoma", "Arkansas");
        Neighbor.Instance.AddNeighbor("Oklahoma", "Texas");
        Neighbor.Instance.AddNeighbor("Oklahoma", "New Mexico");

       
    }


  
}
public class Node
{
    public string Name;
    public List<Node> Neighbors = new List<Node>();
    public Node(string name)
    {
        Name = name;
    }
}
public class Pathfinding
{
    public List<Node> FindPath(Node start, Node target)
    {
        List<Node> openSet = new List<Node>(); // A*'da ziyaret edilecek düğümler
        HashSet<Node> closedSet = new HashSet<Node>(); // Ziyaret edilen düğümler
        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>(); // Hangi düğümden geldiğini tutmak için
        Dictionary<Node, float> gScore = new Dictionary<Node, float>(); // Başlangıçtan her düğüme olan en kısa mesafe
        Dictionary<Node, float> fScore = new Dictionary<Node, float>(); // Başlangıçtan hedefe tahmin edilen mesafe

        openSet.Add(start);
        gScore[start] = 0;
        fScore[start] = HeuristicCostEstimate(start, target); // Başlangıçtan hedefe tahmin edilen mesafe

        while (openSet.Count > 0)
        {
            Node current = GetLowestFScoreNode(openSet, fScore);

            // Hedef düğüme ulaşıldıysa, geri döner
            if (current == target)
                return ReconstructPath(cameFrom, current);

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Node neighbor in current.Neighbors)
            {
                if (closedSet.Contains(neighbor))
                    continue; // Ziyaret edildi, atla

                float tentativeGScore = gScore[current] + 1; // Varsayılan mesafe her komşuya 1

                if (!openSet.Contains(neighbor))
                    openSet.Add(neighbor); // Komşu daha önce ziyaret edilmemişse ekle

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    // GScore güncelle
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + HeuristicCostEstimate(neighbor, target);
                }
            }
        }

        Debug.LogError(" yol bulunamadı");
        return null; // Yol bulunamadı
    }

    private float HeuristicCostEstimate(Node a, Node b)
    {
        // Basit bir tahmin, mesafeyi veya benzeri bir şey kullanabilirsin
        return 1; // Tüm komşular için eşit mesafe
    }

    private Node GetLowestFScoreNode(List<Node> openSet, Dictionary<Node, float> fScore)
    {
        Node lowestNode = openSet[0];
        foreach (Node node in openSet)
        {
            if (fScore.TryGetValue(node, out float score))
            {
                if (score < fScore[lowestNode])
                    lowestNode = node;
            }
        }
        return lowestNode;
    }

    private List<Node> ReconstructPath(Dictionary<Node, Node> cameFrom, Node current)
    {
        List<Node> totalPath = new List<Node> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Add(current);
        }
        totalPath.Reverse(); // Başlangıçtan hedefe doğru
        return totalPath;
    }
}
