using System.Collections.Generic;

public class Region
{
    public string Name { get; }
    public float Morale { get; }
    public int Population { get; }
    public int Resources { get; }
    public float UnitArmyPower { get; set; } // Yeni özellik
    public int ArmySize { get; set; }        // Yeni özellik
    public new Dictionary<ResourceType, ResourceData> ResourceDatas {get; set;}
    public Region(string name, float morale, int population, int resources, Dictionary<ResourceType, ResourceData> resourceDatas)
    {
        Name = name;
        Morale = morale;
        Population = population;
        Resources = resources;
        UnitArmyPower = 0;  // Baþlangýç deðeri
        ArmySize = 0;       // Baþlangýç deðeri
        ResourceDatas = resourceDatas;
    }
}
