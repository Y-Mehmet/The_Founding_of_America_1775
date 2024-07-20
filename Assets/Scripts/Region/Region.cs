using System.Collections.Generic;

public class Region
{
    public string Name;
    public float Happiness;
    public int Population;
    public int FoodStock;

    public Region(string name, float happiness, int population, int foodStock)
    {
        Name = name;
        Happiness = happiness;
        Population = population;
        FoodStock = foodStock;
    }
}
