using System.Collections.Generic;

public class Region
{
    public string Name { get; }
    public float Morale { get; }
    public int Population { get; }
    public int Resources { get; }
    public float UnitArmyPower { get; set; } // Yeni �zellik
    public int ArmySize { get; set; }        // Yeni �zellik

    public Region(string name, float morale, int population, int resources)
    {
        Name = name;
        Morale = morale;
        Population = population;
        Resources = resources;
        UnitArmyPower = 0;  // Ba�lang�� de�eri
        ArmySize = 0;       // Ba�lang�� de�eri
    }
}
