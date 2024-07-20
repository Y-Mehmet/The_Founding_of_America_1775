using System;
using System.Collections.Generic;

class Neighbor
{
    // Þehirlerin komþuluk iliþkilerini tutan bitiþiklik listesi
    private Dictionary<string, List<string>> adjacencyList;

    public Neighbor()
    {
        adjacencyList = new Dictionary<string, List<string>>();
    }

    // Yeni bir þehir ekler
    public void AddCity(string city)
    {
        if (!adjacencyList.ContainsKey(city))
        {
            adjacencyList[city] = new List<string>();
        }
    }

    // Þehirler arasýndaki komþuluk iliþkisini ekler
    public void AddNeighbor(string city1, string city2)
    {
        if (adjacencyList.ContainsKey(city1) && adjacencyList.ContainsKey(city2))
        {
            adjacencyList[city1].Add(city2);
            adjacencyList[city2].Add(city1);
        }
    }

    // Ýki þehrin komþu olup olmadýðýný kontrol eder
    public bool AreNeighbors(string city1, string city2)
    {
        if (adjacencyList.ContainsKey(city1))
        {
            return adjacencyList[city1].Contains(city2);
        }
        return false;
    }
}

class Program
{
    static void Main()
    {
        Neighbor game = new Neighbor();

        // Þehirleri ekle
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
            game.AddCity(city);
        }

        // Komþuluk iliþkilerini ekle
        game.AddNeighbor("Washington", "Oregon");
        game.AddNeighbor("Washington", "Idaho");

        game.AddNeighbor("Oregon", "Washington");
        game.AddNeighbor("Oregon", "Idaho");
        game.AddNeighbor("Oregon", "California");
        game.AddNeighbor("Oregon", "Nevada");

        game.AddNeighbor("Idaho", "Washington");
        game.AddNeighbor("Idaho", "Oregon");
        game.AddNeighbor("Idaho", "Nevada");
        game.AddNeighbor("Idaho", "Utah");
        game.AddNeighbor("Idaho", "Wyoming");
        game.AddNeighbor("Idaho", "Montana");

        game.AddNeighbor("Montana", "Idaho");
        game.AddNeighbor("Montana", "Wyoming");
        game.AddNeighbor("Montana", "North Dakota");
        game.AddNeighbor("Montana", "South Dakota");

        game.AddNeighbor("North Dakota", "Montana");
        game.AddNeighbor("North Dakota", "South Dakota");
        game.AddNeighbor("North Dakota", "Minnesota");

        game.AddNeighbor("Minnesota", "North Dakota");
        game.AddNeighbor("Minnesota", "South Dakota");
        game.AddNeighbor("Minnesota", "Iowa");
        game.AddNeighbor("Minnesota", "Wisconsin");

        game.AddNeighbor("Wisconsin", "Minnesota");
        game.AddNeighbor("Wisconsin", "Iowa");
        game.AddNeighbor("Wisconsin", "Illinois");

        game.AddNeighbor("Wyoming", "Montana");
        game.AddNeighbor("Wyoming", "South Dakota");
        game.AddNeighbor("Wyoming", "Nebraska");
        game.AddNeighbor("Wyoming", "Colorado");
        game.AddNeighbor("Wyoming", "Utah");
        game.AddNeighbor("Wyoming", "Idaho");

        game.AddNeighbor("South Dakota", "North Dakota");
        game.AddNeighbor("South Dakota", "Minnesota");
        game.AddNeighbor("South Dakota", "Iowa");
        game.AddNeighbor("South Dakota", "Nebraska");
        game.AddNeighbor("South Dakota", "Wyoming");
        game.AddNeighbor("South Dakota", "Montana");

        game.AddNeighbor("Iowa", "Minnesota");
        game.AddNeighbor("Iowa", "Wisconsin");
        game.AddNeighbor("Iowa", "Illinois");
        game.AddNeighbor("Iowa", "Missouri");
        game.AddNeighbor("Iowa", "Nebraska");
        game.AddNeighbor("Iowa", "South Dakota");

        game.AddNeighbor("Illinois", "Wisconsin");
        game.AddNeighbor("Illinois", "Iowa");
        game.AddNeighbor("Illinois", "Missouri");
        game.AddNeighbor("Illinois", "Kentucky");
        game.AddNeighbor("Illinois", "Indiana");

        game.AddNeighbor("Michigan", "Wisconsin");
        game.AddNeighbor("Michigan", "Indiana");
        game.AddNeighbor("Michigan", "Ohio");

        game.AddNeighbor("New York", "Pennsylvania");
        game.AddNeighbor("New York", "New Jersey");
        game.AddNeighbor("New York", "Vermont");
        game.AddNeighbor("New York", "Massachusetts");
        game.AddNeighbor("New York", "Connecticut");

        game.AddNeighbor("Vermont", "New York");
        game.AddNeighbor("Vermont", "New Hampshire");
        game.AddNeighbor("Vermont", "Massachusetts");

        game.AddNeighbor("New Hampshire", "Vermont");
        game.AddNeighbor("New Hampshire", "Maine");
        game.AddNeighbor("New Hampshire", "Massachusetts");

        game.AddNeighbor("Maine", "New Hampshire");

        game.AddNeighbor("Massachusetts", "New York");
        game.AddNeighbor("Massachusetts", "Vermont");
        game.AddNeighbor("Massachusetts", "New Hampshire");
        game.AddNeighbor("Massachusetts", "Connecticut");
        game.AddNeighbor("Massachusetts", "Rhode Island");

        game.AddNeighbor("New Jersey", "New York");
        game.AddNeighbor("New Jersey", "Pennsylvania");

        game.AddNeighbor("Pennsylvania", "New York");
        game.AddNeighbor("Pennsylvania", "New Jersey");
        game.AddNeighbor("Pennsylvania", "Delaware");
        game.AddNeighbor("Pennsylvania", "Maryland");
        game.AddNeighbor("Pennsylvania", "West Virginia");
        game.AddNeighbor("Pennsylvania", "Ohio");

        game.AddNeighbor("Maryland", "Pennsylvania");
        game.AddNeighbor("Maryland", "Delaware");
        game.AddNeighbor("Maryland", "Virginia");
        game.AddNeighbor("Maryland", "West Virginia");

        game.AddNeighbor("Ohio", "Pennsylvania");
        game.AddNeighbor("Ohio", "West Virginia");
        game.AddNeighbor("Ohio", "Kentucky");
        game.AddNeighbor("Ohio", "Indiana");
        game.AddNeighbor("Ohio", "Michigan");

        game.AddNeighbor("Indiana", "Michigan");
        game.AddNeighbor("Indiana", "Ohio");
        game.AddNeighbor("Indiana", "Kentucky");
        game.AddNeighbor("Indiana", "Illinois");

        game.AddNeighbor("Virginia", "Maryland");
        game.AddNeighbor("Virginia", "West Virginia");
        game.AddNeighbor("Virginia", "Kentucky");
        game.AddNeighbor("Virginia", "Tennessee");
        game.AddNeighbor("Virginia", "North Carolina");

        game.AddNeighbor("North Carolina", "Virginia");
        game.AddNeighbor("North Carolina", "Tennessee");
        game.AddNeighbor("North Carolina", "South Carolina");
        game.AddNeighbor("North Carolina", "Georgia");

        game.AddNeighbor("South Carolina", "North Carolina");
        game.AddNeighbor("South Carolina", "Georgia");

        game.AddNeighbor("Georgia", "North Carolina");
        game.AddNeighbor("Georgia", "South Carolina");
        game.AddNeighbor("Georgia", "Tennessee");
        game.AddNeighbor("Georgia", "Alabama");
        game.AddNeighbor("Georgia", "Florida");

        game.AddNeighbor("Florida", "Georgia");
        game.AddNeighbor("Florida", "Alabama");

        game.AddNeighbor("Alabama", "Tennessee");
        game.AddNeighbor("Alabama", "Georgia");
        game.AddNeighbor("Alabama", "Florida");
        game.AddNeighbor("Alabama", "Mississippi");

        game.AddNeighbor("Mississippi", "Tennessee");
        game.AddNeighbor("Mississippi", "Alabama");
        game.AddNeighbor("Mississippi", "Louisiana");
        game.AddNeighbor("Mississippi", "Arkansas");

        game.AddNeighbor("Louisiana", "Arkansas");
        game.AddNeighbor("Louisiana", "Mississippi");
        game.AddNeighbor("Louisiana", "Texas");

        game.AddNeighbor("Texas", "Oklahoma");
        game.AddNeighbor("Texas", "Arkansas");
        game.AddNeighbor("Texas", "Louisiana");
        game.AddNeighbor("Texas", "New Mexico");

        game.AddNeighbor("New Mexico", "Texas");
        game.AddNeighbor("New Mexico", "Oklahoma");
        game.AddNeighbor("New Mexico", "Colorado");
        game.AddNeighbor("New Mexico", "Arizona");

        game.AddNeighbor("Arizona", "California");
        game.AddNeighbor("Arizona", "Nevada");
        game.AddNeighbor("Arizona", "Utah");
        game.AddNeighbor("Arizona", "Colorado");
        game.AddNeighbor("Arizona", "New Mexico");

        game.AddNeighbor("California", "Oregon");
        game.AddNeighbor("California", "Nevada");
        game.AddNeighbor("California", "Arizona");

        game.AddNeighbor("Nevada", "Oregon");
        game.AddNeighbor("Nevada", "Idaho");
        game.AddNeighbor("Nevada", "Utah");
        game.AddNeighbor("Nevada", "Arizona");
        game.AddNeighbor("Nevada", "California");

        game.AddNeighbor("Utah", "Idaho");
        game.AddNeighbor("Utah", "Wyoming");
        game.AddNeighbor("Utah", "Colorado");
        game.AddNeighbor("Utah", "Arizona");
        game.AddNeighbor("Utah", "Nevada");

        game.AddNeighbor("Colorado", "Wyoming");
        game.AddNeighbor("Colorado", "Nebraska");
        game.AddNeighbor("Colorado", "Kansas");
        game.AddNeighbor("Colorado", "Oklahoma");
        game.AddNeighbor("Colorado", "New Mexico");
        game.AddNeighbor("Colorado", "Utah");

        game.AddNeighbor("Kansas", "Nebraska");
        game.AddNeighbor("Kansas", "Missouri");
        game.AddNeighbor("Kansas", "Oklahoma");
        game.AddNeighbor("Kansas", "Colorado");

        game.AddNeighbor("Nebraska", "South Dakota");
        game.AddNeighbor("Nebraska", "Iowa");
        game.AddNeighbor("Nebraska", "Missouri");
        game.AddNeighbor("Nebraska", "Kansas");
        game.AddNeighbor("Nebraska", "Colorado");
        game.AddNeighbor("Nebraska", "Wyoming");

        game.AddNeighbor("Missouri", "Iowa");
        game.AddNeighbor("Missouri", "Nebraska");
        game.AddNeighbor("Missouri", "Kansas");
        game.AddNeighbor("Missouri", "Oklahoma");
        game.AddNeighbor("Missouri", "Arkansas");
        game.AddNeighbor("Missouri", "Tennessee");
        game.AddNeighbor("Missouri", "Kentucky");
        game.AddNeighbor("Missouri", "Illinois");

        game.AddNeighbor("Kentucky", "Illinois");
        game.AddNeighbor("Kentucky", "Indiana");
        game.AddNeighbor("Kentucky", "Ohio");
        game.AddNeighbor("Kentucky", "West Virginia");
        game.AddNeighbor("Kentucky", "Virginia");
        game.AddNeighbor("Kentucky", "Tennessee");
        game.AddNeighbor("Kentucky", "Missouri");

        game.AddNeighbor("West Virginia", "Ohio");
        game.AddNeighbor("West Virginia", "Pennsylvania");
        game.AddNeighbor("West Virginia", "Maryland");
        game.AddNeighbor("West Virginia", "Virginia");
        game.AddNeighbor("West Virginia", "Kentucky");

        game.AddNeighbor("Tennessee", "Kentucky");
        game.AddNeighbor("Tennessee", "Virginia");
        game.AddNeighbor("Tennessee", "North Carolina");
        game.AddNeighbor("Tennessee", "Georgia");
        game.AddNeighbor("Tennessee", "Alabama");
        game.AddNeighbor("Tennessee", "Mississippi");
        game.AddNeighbor("Tennessee", "Arkansas");
        game.AddNeighbor("Tennessee", "Missouri");

        game.AddNeighbor("Arkansas", "Missouri");
        game.AddNeighbor("Arkansas", "Tennessee");
        game.AddNeighbor("Arkansas", "Mississippi");
        game.AddNeighbor("Arkansas", "Louisiana");
        game.AddNeighbor("Arkansas", "Texas");
        game.AddNeighbor("Arkansas", "Oklahoma");

        game.AddNeighbor("Oklahoma", "Colorado");
        game.AddNeighbor("Oklahoma", "Kansas");
        game.AddNeighbor("Oklahoma", "Missouri");
        game.AddNeighbor("Oklahoma", "Arkansas");
        game.AddNeighbor("Oklahoma", "Texas");
        game.AddNeighbor("Oklahoma", "New Mexico");

        // Komþuluk kontrolü
        Console.WriteLine(game.AreNeighbors("California", "Oregon")); // True
        Console.WriteLine(game.AreNeighbors("California", "Texas")); // False
        Console.WriteLine(game.AreNeighbors("Texas", "New Mexico")); // True
    }
}
