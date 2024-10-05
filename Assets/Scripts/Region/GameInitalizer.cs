using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameInitalizer : MonoBehaviour
{
    public Dictionary<string, Region> regions = new Dictionary<string, Region>();
    public List<Trade> tradeLists = new List<Trade>();

    public int goldRate, waterRate, saltRate, meatRate, fruitsRate, vegetablesRate, wheatRate, woodRate, coalRate, ironRate, stoneRate, diamondRate;
    public string[] largeStates, mediumStates, smallStates;
    Dictionary<string, List<ResourceType>> statesImportTradeResTypeList;
    Dictionary<string, List<ResourceType>> statesExportTradeResTypeList;
    Dictionary<string, List<float>> statesImportTradeContratPriceList = new Dictionary<string, List<float>>();
    Dictionary<string, List<float>> statesExportTradeContratPriceList = new Dictionary<string, List<float>>();
    Dictionary<string, List<float>> statesImporTradeLimitList = new Dictionary<string, List<float>>();
    Dictionary<string, List<float>> statesExportTradLimitList = new Dictionary<string, List<float>>();

    private void Start()
    {

        // Initialize the Neighbor class with city data
        goldRate = StateResourceSO.Instance.GoldproductionRate;
        waterRate = StateResourceSO.Instance.WaterproductionRate;
        saltRate = StateResourceSO.Instance.SaltproductionRate;
        meatRate = StateResourceSO.Instance.MeatproductionRate;
        fruitsRate = StateResourceSO.Instance.FruitsproductionRate;
        vegetablesRate = StateResourceSO.Instance.VegetablesproductionRate;
        wheatRate = StateResourceSO.Instance.WheatproductionRate;
        woodRate = StateResourceSO.Instance.WoodproductionRate;
        coalRate = StateResourceSO.Instance.CoalproductionRate;
        ironRate = StateResourceSO.Instance.IronproductionRate;
        stoneRate = StateResourceSO.Instance.StoneproductionRate;
        diamondRate = StateResourceSO.Instance.DimondproductionRate;

        regions = new Dictionary<string, Region>();

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
        { "largeStates", new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone, ResourceType.Meat, ResourceType.Water, ResourceType.Diamond, ResourceType.Fruits, ResourceType.Salt, ResourceType.Vegetables, ResourceType.Wheat } }
    };

        statesExportTradeResTypeList = new Dictionary<string, List<ResourceType>>
    {
        { "smallStates", new List<ResourceType> { ResourceType.Fruits, ResourceType.Vegetables, ResourceType.Water } },
        { "mediumStates", new List<ResourceType> { ResourceType.Fruits, ResourceType.Vegetables, ResourceType.Meat, ResourceType.Wheat, ResourceType.Salt } },
        { "largeStates", new List<ResourceType> { ResourceType.Diamond, ResourceType.Wheat, ResourceType.Meat, ResourceType.Water, ResourceType.Iron, ResourceType.Stone, ResourceType.Coal, ResourceType.Wood } }
    };

        // İthalat kontrat fiyatlarını dinamik çarpanlarla hesapla
        statesImportTradeContratPriceList = new Dictionary<string, List<float>>
    {
        { "smallStates", new List<float>
            {
                GameEconomy.Instance.GetGoldValue(ResourceType.Wood) * smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Coal) * smallStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Iron) * smallStateImportMultiplier
            }
        },
        { "mediumStates", new List<float>
            {
                GameEconomy.Instance.GetGoldValue(ResourceType.Wood) * mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Coal) * mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Iron) * mediumStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Stone) * mediumStateImportMultiplier
            }
        },
        { "largeStates", new List<float>
            {
                GameEconomy.Instance.GetGoldValue(ResourceType.Coal) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Iron) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Stone) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Meat) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Water) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Diamond) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Fruits) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Salt) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Vegetables) * largeStateImportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Wheat) * largeStateImportMultiplier
            }
        }
    };

        // İhracat kontrat fiyatlarını dinamik çarpanlarla hesapla
        statesExportTradeContratPriceList = new Dictionary<string, List<float>>
    {
        { "smallStates", new List<float>
            {
                GameEconomy.Instance.GetGoldValue(ResourceType.Fruits) * smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Vegetables) * smallStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Water) * smallStateExportMultiplier
            }
        },
        { "mediumStates", new List<float>
            {
                GameEconomy.Instance.GetGoldValue(ResourceType.Fruits) * mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Vegetables) * mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Meat) * mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Wheat) * mediumStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Salt) * mediumStateExportMultiplier
            }
        },
        { "largeStates", new List<float>
            {
                GameEconomy.Instance.GetGoldValue(ResourceType.Diamond) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Wheat) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Meat) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Water) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Iron) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Stone) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Coal) * largeStateExportMultiplier,
                GameEconomy.Instance.GetGoldValue(ResourceType.Wood) * largeStateExportMultiplier
            }
        }
    };
        // İthalat ve ihracat limitlerini 10 olarak ayarla
        statesImporTradeLimitList = new Dictionary<string, List<float>>
    {
        { "smallStates", new List<float> { 10, 10, 10 } },
        { "mediumStates", new List<float> { 10, 10, 10, 10 } },
        { "largeStates", new List<float> { 100, 100, 10, 100, 100, 100, 100, 100, 100, 100 } }
    };

        statesExportTradLimitList = new Dictionary<string, List<float>>
    {
        { "smallStates", new List<float> { 10, 10, 10 } },
        { "mediumStates", new List<float> { 10, 10, 10, 10, 10 } },
        { "largeStates", new List<float> { 100, 100, 100, 100, 100, 100, 100, 100 } }
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
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone },  new List<int>{1000,2000,3000}));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Water, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 1000, 1000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Salt, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 2000, 3000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Meat, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 2000, 3000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Fruits, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 2000, 3000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Vegetables, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 2000, 3000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Wheat, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 2000, 3000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Wood, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 2000, 3000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Coal, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 2000, 3000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Iron, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 2000, 3000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Stone, new MineMatarials
       (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 2000, 3000 }));

        MineManager.instance.MineRequiredResList.Add(ResourceType.Diamond, new MineMatarials
     (new List<ResourceType> { ResourceType.Coal, ResourceType.Iron, ResourceType.Stone }, new List<int> { 1000, 2000, 3000 }));



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
       
        var largeStateTemplate = new Region("LargeState", 100.0f, 20000, 1, new Dictionary<ResourceType, ResourceData> {
    { ResourceType.Gold, new ResourceData { currentAmount = 1500, mineCount = 5, productionRate = goldRate }},
    { ResourceType.Water, new ResourceData { currentAmount = 4000, mineCount = 4, productionRate = waterRate }},
    { ResourceType.Salt, new ResourceData { currentAmount = 2000, mineCount = 4, productionRate = saltRate }},
    { ResourceType.Meat, new ResourceData { currentAmount = 3000, mineCount = 4, productionRate = meatRate }},
    { ResourceType.Fruits, new ResourceData { currentAmount = 2500, mineCount = 4, productionRate = fruitsRate }},
    { ResourceType.Vegetables, new ResourceData { currentAmount = 2400, mineCount = 4, productionRate = vegetablesRate }},
    { ResourceType.Wheat, new ResourceData { currentAmount = 2500, mineCount = 5, productionRate = wheatRate }},
    { ResourceType.Wood, new ResourceData { currentAmount = 2600, mineCount = 5, productionRate = woodRate }},
    { ResourceType.Coal, new ResourceData { currentAmount = 2800, mineCount = 4, productionRate = coalRate }},
    { ResourceType.Iron, new ResourceData { currentAmount = 3000, mineCount = 5, productionRate = ironRate }},
    { ResourceType.Stone, new ResourceData { currentAmount = 2500, mineCount = 4, productionRate = stoneRate }},
    { ResourceType.Diamond, new ResourceData { currentAmount = 10, mineCount = 1, productionRate = diamondRate }}
});
        var mediumStateTemplate = new Region("MediumState", 90.0f, 15000, 2, new Dictionary<ResourceType, ResourceData> {
    { ResourceType.Gold, new ResourceData { currentAmount = 1200, mineCount = 4, productionRate = goldRate }},
    { ResourceType.Water, new ResourceData { currentAmount = 3500, mineCount = 3, productionRate = waterRate }},
    { ResourceType.Salt, new ResourceData { currentAmount = 1800, mineCount = 3, productionRate = saltRate }},
    { ResourceType.Meat, new ResourceData { currentAmount = 2500, mineCount = 3, productionRate = meatRate }},
    { ResourceType.Fruits, new ResourceData { currentAmount = 2200, mineCount = 3, productionRate = fruitsRate }},
    { ResourceType.Vegetables, new ResourceData { currentAmount = 2100, mineCount = 3, productionRate = vegetablesRate }},
    { ResourceType.Wheat, new ResourceData { currentAmount = 2300, mineCount = 4, productionRate = wheatRate }},
    { ResourceType.Wood, new ResourceData { currentAmount = 2400, mineCount = 4, productionRate = woodRate }},
    { ResourceType.Coal, new ResourceData { currentAmount = 2600, mineCount = 3, productionRate = coalRate }},
    { ResourceType.Iron, new ResourceData { currentAmount = 2800, mineCount = 4, productionRate = ironRate }},
    { ResourceType.Stone, new ResourceData { currentAmount = 2000, mineCount = 3, productionRate = stoneRate }},
    { ResourceType.Diamond, new ResourceData { currentAmount = 2, mineCount = 0, productionRate = diamondRate }}
});
        var smallStateTemplate = new Region("SmallState", 80.0f, 10000, 3, new Dictionary<ResourceType, ResourceData> {
    { ResourceType.Gold, new ResourceData { currentAmount = 1000, mineCount = 3, productionRate = goldRate }},
    { ResourceType.Water, new ResourceData { currentAmount = 3000, mineCount = 2, productionRate = waterRate }},
    { ResourceType.Salt, new ResourceData { currentAmount = 1600, mineCount = 2, productionRate = saltRate }},
    { ResourceType.Meat, new ResourceData { currentAmount = 2000, mineCount = 2, productionRate = meatRate }},
    { ResourceType.Fruits, new ResourceData { currentAmount = 2000, mineCount = 2, productionRate = fruitsRate }},
    { ResourceType.Vegetables, new ResourceData { currentAmount = 1900, mineCount = 2, productionRate = vegetablesRate }},
    { ResourceType.Wheat, new ResourceData { currentAmount = 2000, mineCount = 3, productionRate = wheatRate }},
    { ResourceType.Wood, new ResourceData { currentAmount = 2100, mineCount = 3, productionRate = woodRate }},
    { ResourceType.Coal, new ResourceData { currentAmount = 2200, mineCount = 2, productionRate = coalRate }},
    { ResourceType.Iron, new ResourceData { currentAmount = 2400, mineCount = 3, productionRate = ironRate }},
    { ResourceType.Stone, new ResourceData { currentAmount = 1900, mineCount = 2, productionRate = stoneRate }},
    { ResourceType.Diamond, new ResourceData { currentAmount = 0, mineCount = 0, productionRate = diamondRate }}
});

      
       

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

       

        foreach (var state in largeStates)
        {
            regions[state] = new Region(state, largeStateTemplate.Morale, largeStateTemplate.Population, largeStateTemplate.Resources, new Dictionary<ResourceType, ResourceData>(largeStateTemplate.Resources));
                    
        }

        foreach (var state in mediumStates)
        {
            regions[state] = new Region(state, mediumStateTemplate.Morale, mediumStateTemplate.Population, mediumStateTemplate.Resources, new Dictionary<ResourceType, ResourceData>(mediumStateTemplate.Resources));
        }

        foreach (var state in smallStates)
        {
            regions[state] = new Region(state, smallStateTemplate.Morale, smallStateTemplate.Population, smallStateTemplate.Resources, new Dictionary<ResourceType, ResourceData>(smallStateTemplate.Resources));
        }


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
                if (regions.ContainsKey(state.name))
                {
                    Region region = regions[state.name];
                    if (largeStates.Contains(state.name))
                    {// large region
                        s.UnitArmyPower = (float)(0.75 + rand.NextDouble() * 0.25); //ort 0.875
                        s.resourceData = new Dictionary<ResourceType, ResourceData>
                        {   { ResourceType.Gold, new ResourceData { currentAmount = 1500, mineCount = 5, productionRate = goldRate }},
                            { ResourceType.Water, new ResourceData { currentAmount = 4000, mineCount = 4, productionRate = waterRate }},
                            { ResourceType.Salt, new ResourceData { currentAmount = 2000, mineCount = 4, productionRate = saltRate }},
                            { ResourceType.Meat, new ResourceData { currentAmount = 3000, mineCount = 4, productionRate = meatRate }},
                            { ResourceType.Fruits, new ResourceData { currentAmount = 2500, mineCount = 4, productionRate = fruitsRate }},
                            { ResourceType.Vegetables, new ResourceData { currentAmount = 2400, mineCount = 4, productionRate = vegetablesRate }},
                            { ResourceType.Wheat, new ResourceData { currentAmount = 2500, mineCount = 5, productionRate = wheatRate }},
                            { ResourceType.Wood, new ResourceData { currentAmount = 2600, mineCount = 5, productionRate = woodRate }},
                            { ResourceType.Coal, new ResourceData { currentAmount = 2800, mineCount = 4, productionRate = coalRate }},
                            { ResourceType.Iron, new ResourceData { currentAmount = 3000, mineCount = 5, productionRate = ironRate }},
                            { ResourceType.Stone, new ResourceData { currentAmount = 2500, mineCount = 4, productionRate = stoneRate }},
                            { ResourceType.Diamond, new ResourceData { currentAmount = 10, mineCount = 1, productionRate = diamondRate }}};
                        // Import Trade - largeStates için
                        s.importTrade = new Trade(
                            TradeType.Import,
                            statesImportTradeResTypeList["largeStates"],                // İthalat için 'largeStates' kaynakları
                            statesImportTradeContratPriceList["largeStates"],           // İthalat fiyatları
                            statesImporTradeLimitList["largeStates"]                    // İthalat limitleri
                        );

                        // Export Trade - largeStates için
                        s.exportTrade = new Trade(
                            TradeType.Export,
                            statesExportTradeResTypeList["largeStates"],                // İhracat için 'largeStates' kaynakları
                            statesExportTradeContratPriceList["largeStates"],           // İhracat fiyatları
                            statesExportTradLimitList["largeStates"]                    // İhracat limitleri
                        );

                        TaxData StampTax = new TaxData
                        {
                            taxType = TaxType.StampTax,
                            currentRate = 0,
                            toleranceLimit = 0,
                            taxIncome = 0 ,
                            unitTaxIncome = 20
                        };
                        TaxData IncomeTax = new TaxData
                        {
                            taxType = TaxType.IncomeTax,
                            currentRate = 0,
                            toleranceLimit = 10,
                            taxIncome = 0,
                            unitTaxIncome = 20
                        };
                        TaxData DirectTax = new TaxData
                        {
                            taxType = TaxType.DirectTax,
                            currentRate = 0,
                            toleranceLimit = 10,
                            taxIncome = 0,
                            unitTaxIncome = 20
                        };
                        TaxData ValueAddedTax = new TaxData
                        {
                            taxType = TaxType.ValueAddedTax,
                            currentRate = 0,
                            toleranceLimit = 7,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        s.Taxes.Add(StampTax);
                        s.Taxes.Add(IncomeTax);
                        s.Taxes.Add(DirectTax);
                        s.Taxes.Add(ValueAddedTax);

                    }
                   
                    else if (smallStates.Contains(region.Name))
                    {
                        s.UnitArmyPower = (float)(0.35 + rand.NextDouble() * 0.05);
                        s.resourceData = new Dictionary<ResourceType, ResourceData>
                     {
                            { ResourceType.Gold, new ResourceData { currentAmount = 1000, mineCount = 3, productionRate = goldRate }},
                            { ResourceType.Water, new ResourceData { currentAmount = 3000, mineCount = 2, productionRate = waterRate }},
                            { ResourceType.Salt, new ResourceData { currentAmount = 1600, mineCount = 2, productionRate = saltRate }},
                            { ResourceType.Meat, new ResourceData { currentAmount = 2000, mineCount = 2, productionRate = meatRate }},
                            { ResourceType.Fruits, new ResourceData { currentAmount = 2000, mineCount = 2, productionRate = fruitsRate }},
                            { ResourceType.Vegetables, new ResourceData { currentAmount = 1900, mineCount = 2, productionRate = vegetablesRate }},
                            { ResourceType.Wheat, new ResourceData { currentAmount = 2000, mineCount = 3, productionRate = wheatRate }},
                            { ResourceType.Wood, new ResourceData { currentAmount = 2100, mineCount = 3, productionRate = woodRate }},
                            { ResourceType.Coal, new ResourceData { currentAmount = 2200, mineCount = 2, productionRate = coalRate }},
                            { ResourceType.Iron, new ResourceData { currentAmount = 2400, mineCount = 3, productionRate = ironRate }},
                            { ResourceType.Stone, new ResourceData { currentAmount = 1900, mineCount = 2, productionRate = stoneRate }},
                            { ResourceType.Diamond, new ResourceData { currentAmount = 0, mineCount = 0, productionRate = diamondRate }} };
                        // Import Trade - smallStates için
                        s.importTrade = new Trade(
                            TradeType.Import,
                            statesImportTradeResTypeList["smallStates"],                // İthalat için 'smallStates' kaynakları
                            statesImportTradeContratPriceList["smallStates"],           // İthalat fiyatları
                            statesImporTradeLimitList["smallStates"]                    // İthalat limitleri
                        );

                        // Export Trade - smallStates için
                        s.exportTrade = new Trade(
                            TradeType.Export,
                            statesExportTradeResTypeList["smallStates"],                // İhracat için 'smallStates' kaynakları
                            statesExportTradeContratPriceList["smallStates"],           // İhracat fiyatları
                            statesExportTradLimitList["smallStates"]                    // İhracat limitleri
                        );
                        TaxData StampTax = new TaxData
                        {
                            taxType = TaxType.StampTax,
                            currentRate = 0,
                            toleranceLimit = 7,
                            taxIncome = 0,
                            unitTaxIncome = 20
                        };
                        TaxData IncomeTax = new TaxData
                        {
                            taxType = TaxType.IncomeTax,
                            currentRate = 0,
                            toleranceLimit = 25,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        TaxData DirectTax = new TaxData
                        {
                            taxType = TaxType.DirectTax,
                            currentRate = 0,
                            toleranceLimit = 30,
                            taxIncome = 10,
                            unitTaxIncome = 20
                        };
                        TaxData ValueAddedTax = new TaxData
                        {
                            taxType = TaxType.ValueAddedTax,
                            currentRate = 0,
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
                        s.UnitArmyPower = (float)(0.75 + rand.NextDouble() * 0.15);
                        s.resourceData = new Dictionary<ResourceType, ResourceData>
                        {
                            { ResourceType.Gold, new ResourceData { currentAmount = 1200, mineCount = 4, productionRate = goldRate }},
                            { ResourceType.Water, new ResourceData { currentAmount = 3500, mineCount = 3, productionRate = waterRate }},
                            { ResourceType.Salt, new ResourceData { currentAmount = 1800, mineCount = 3, productionRate = saltRate }},
                            { ResourceType.Meat, new ResourceData { currentAmount = 2500, mineCount = 3, productionRate = meatRate }},
                            { ResourceType.Fruits, new ResourceData { currentAmount = 2200, mineCount = 3, productionRate = fruitsRate }},
                            { ResourceType.Vegetables, new ResourceData { currentAmount = 2100, mineCount = 3, productionRate = vegetablesRate }},
                            { ResourceType.Wheat, new ResourceData { currentAmount = 2300, mineCount = 4, productionRate = wheatRate }},
                            { ResourceType.Wood, new ResourceData { currentAmount = 2400, mineCount = 4, productionRate = woodRate }},
                            { ResourceType.Coal, new ResourceData { currentAmount = 2600, mineCount = 3, productionRate = coalRate }},
                            { ResourceType.Iron, new ResourceData { currentAmount = 2800, mineCount = 4, productionRate = ironRate }},
                            { ResourceType.Stone, new ResourceData { currentAmount = 2000, mineCount = 3, productionRate = stoneRate }},
                            { ResourceType.Diamond, new ResourceData { currentAmount = 2, mineCount = 0, productionRate = diamondRate }}};
                        // Import Trade - mediumStates için
                        s.importTrade = new Trade(
                            TradeType.Import,
                            statesImportTradeResTypeList["mediumStates"],               // İthalat için 'mediumStates' kaynakları
                            statesImportTradeContratPriceList["mediumStates"],          // İthalat fiyatları
                            statesImporTradeLimitList["mediumStates"]                   // İthalat limitleri
                        );

                        // Export Trade - mediumStates için
                        s.exportTrade = new Trade(
                            TradeType.Export,
                            statesExportTradeResTypeList["mediumStates"],               // İhracat için 'mediumStates' kaynakları
                            statesExportTradeContratPriceList["mediumStates"],          // İhracat fiyatları
                            statesExportTradLimitList["mediumStates"]                   // İhracat limitleri
                        );
                        TaxData StampTax = new TaxData
                        {
                            taxType = TaxType.StampTax,
                            currentRate = 0,
                            toleranceLimit = 3,
                            taxIncome = 0,
                            unitTaxIncome=20
                        };
                        TaxData IncomeTax = new TaxData
                        {
                            taxType = TaxType.IncomeTax,
                            currentRate = 0,
                            toleranceLimit = 12,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        TaxData DirectTax = new TaxData
                        {
                            taxType = TaxType.DirectTax,
                            currentRate = 0,
                            toleranceLimit = 20,
                            taxIncome = 0,
                            unitTaxIncome = 20
                        };
                        TaxData ValueAddedTax = new TaxData
                        {
                            taxType = TaxType.ValueAddedTax,
                            currentRate = 0,
                            toleranceLimit = 18,
                            taxIncome = 0,
                            unitTaxIncome = 0
                        };
                        s.Taxes.Add(StampTax);
                        s.Taxes.Add(IncomeTax);
                        s.Taxes.Add(DirectTax);
                        s.Taxes.Add(ValueAddedTax);
                    }
                 
                   
                    s.StateName = region.Name;
                    s.ArmySize = (int)(region.Population * 0.25); // Nüfusun %25'i
                    s.Morele = region.Morale;
                    s.Resources = region.Resources;
                    s.Population = region.Population;
                   
                }
                else
                {
                    Debug.LogWarning(" state bulunamadı ");
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
