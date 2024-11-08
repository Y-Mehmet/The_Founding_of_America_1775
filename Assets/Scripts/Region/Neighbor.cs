using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine; // Use Unity's logging system

class Neighbor
{
    // Stores the adjacency list of cities
    public Dictionary<string, List<string>> adjacencyList;
    public static Node washington = new Node("Washington");
    public static Node oregon = new Node("Oregon");
    public static Node idaho = new Node("Idaho");
    public static Node california = new Node("California");
    public static Node texas = new Node("Texas");
    public static Node newYork = new Node("New York");
    public static Node florida = new Node("Florida");
    public static Node georgia = new Node("Georgia");
    public static Node montana = new Node("Montana");
    public static Node northDakota = new Node("North Dakota");
    public static Node minnesota = new Node("Minnesota");
    public static Node wisconsin = new Node("Wisconsin");
    public static Node wyoming = new Node("Wyoming");
    public static Node southDakota = new Node("South Dakota");
    public static Node iowa = new Node("Iowa");
    public static Node illinois = new Node("Ýllinois");
    public static Node michigan = new Node("Michigan");
    public static Node massachusetts = new Node("Massachusetts");
    public static Node newJersey = new Node("New Jersey");
    public static Node pennsylvania = new Node("Pennsylvania");
    public static Node maryland = new Node("Maryland");
    public static Node ohio = new Node("Ohio");
    public static Node indiana = new Node("Indiana");
    public static Node virginia = new Node("Virginia");
    public static Node northCarolina = new Node("North Carolina");
    public static Node southCarolina = new Node("South Carolina");
    public static Node alabama = new Node("Alabama");
    public static Node mississippi = new Node("Mississippi");
    public static Node louisiana = new Node("Louisiana");
    public static Node newMexico = new Node("New Mexico");
    public static Node arizona = new Node("Arizona");
    public static Node nevada = new Node("Nevada");
    public static Node utah = new Node("Utah");
    public static Node colorado = new Node("Colorado");
    public static Node kansas = new Node("Kansas");
    public static Node nebraska = new Node("Nebraska");
    public static Node missouri = new Node("Missouri");
    public static Node kentucky = new Node("Kentucky");
    public static Node westVirginia = new Node("West Virginia");
    public static Node tennessee = new Node("Tennessee");
    public static Node arkansas = new Node("Arkansas");
    public static Node oklahoma = new Node("Oklahoma");
    public static Node maine = new Node("Maine");
    public static Node newHampshire = new Node("New Hampshire");
    public static Node vermont = new Node("Vermont");
    public static Node rhodeIsland = new Node("Rhode Island");
    public static Node delaware = new Node("Delaware");

    // Singleton instance
    private static Neighbor instance;
    

    // Private constructor to prevent external instantiation
    private Neighbor()
    {
        adjacencyList = new Dictionary<string, List<string>>();
        InitializeNeighbors2();
    }

    // Static method to get the Singleton instance
    public static Neighbor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Neighbor();
            }
            return instance;
        }
    }
    public static Node GetNodeByName(string name)
    {
        // Boþluklarý kaldýr ve ilk harfi küçük yaparak formatla (newHamspire gibi).
        string formattedName = FormatToCamelCase(name);

        FieldInfo field = typeof(Neighbor).GetField(formattedName, BindingFlags.Static | BindingFlags.Public);
        return field != null ? (Node)field.GetValue(null) : null;
    }

    private static string FormatToCamelCase(string input)
    {
        // Boþluklarý kaldýrýp her kelimenin ilk harfini büyük yap
        string[] words = input.Split(' ');
        string result = "";

        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > 0)
            {
                if (i == 0)
                {
                    // Ýlk kelimenin tamamýný küçük yaparken Ýngilizce kurallara göre (örn: "new")
                    result += words[i].ToLowerInvariant();
                }
                else
                {
                    // Diðer kelimelerin ilk harfi büyük, geri kalaný küçük olsun (örn: "Hamspire")
                    result += char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }
        }

        return result;
    }



    // Adds a new city to the adjacency list
    public void AddCity(string city)
    {
        if (!adjacencyList.ContainsKey(city))
        {
            adjacencyList[city] = new List<string>();
        }
    }

    // Adds a neighbor relationship between two cities
    public void AddNeighbor(string city1, string city2)
    {
        if (adjacencyList.ContainsKey(city1) && adjacencyList.ContainsKey(city2))
        {
            adjacencyList[city1].Add(city2);
            adjacencyList[city2].Add(city1);
        }
    }

    // Checks if two cities are neighbors
    public bool AreNeighbors(string city1, string city2)
    {
        foreach (var item in adjacencyList)
        {
            if (item.Key.Equals(city1))
            {
                if(item.Value.Contains(city2))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Retrieves the neighbors of a city
    public List<string> GetNeighbors(string city)
    {
        //if (!adjacencyList.ContainsKey(city))
        //{
        //    Debug.LogWarning($"The given key '{city}' was not present in the dictionary.");
           
        //    return new List<string>();
        //}

        List<string> neighborLists = new List<string>();
        foreach (var item in adjacencyList[city])
        {
           // Debug.Log("Komþular yazdý: " + item);
            neighborLists.Add(item);
        }
        return neighborLists;
    }
    private void InitializeNeighbors2()
    {
        // Komþuluk iliþkilerini ekle
        washington.Neighbors.Add(oregon);
        washington.Neighbors.Add(idaho);

        oregon.Neighbors.Add(washington);
        oregon.Neighbors.Add(idaho);
        oregon.Neighbors.Add(california);
        oregon.Neighbors.Add(nevada);

        idaho.Neighbors.Add(washington);
        idaho.Neighbors.Add(oregon);
        idaho.Neighbors.Add(nevada);
        idaho.Neighbors.Add(utah);
        idaho.Neighbors.Add(wyoming);
        idaho.Neighbors.Add(montana);

        montana.Neighbors.Add(idaho);
        montana.Neighbors.Add(wyoming);
        montana.Neighbors.Add(northDakota);
        montana.Neighbors.Add(southDakota);

        northDakota.Neighbors.Add(montana);
        northDakota.Neighbors.Add(southDakota);
        northDakota.Neighbors.Add(minnesota);

        minnesota.Neighbors.Add(northDakota);
        minnesota.Neighbors.Add(southDakota);
        minnesota.Neighbors.Add(iowa);
        minnesota.Neighbors.Add(wisconsin);

        wisconsin.Neighbors.Add(minnesota);
        wisconsin.Neighbors.Add(iowa);
        wisconsin.Neighbors.Add(illinois);

        wyoming.Neighbors.Add(montana);
        wyoming.Neighbors.Add(southDakota);
        wyoming.Neighbors.Add(nebraska);
        wyoming.Neighbors.Add(colorado);
        wyoming.Neighbors.Add(utah);
        wyoming.Neighbors.Add(idaho);

        southDakota.Neighbors.Add(northDakota);
        southDakota.Neighbors.Add(minnesota);
        southDakota.Neighbors.Add(iowa);
        southDakota.Neighbors.Add(nebraska);
        southDakota.Neighbors.Add(wyoming);
        southDakota.Neighbors.Add(montana);

        iowa.Neighbors.Add(minnesota);
        iowa.Neighbors.Add(wisconsin);
        iowa.Neighbors.Add(illinois);
        iowa.Neighbors.Add(missouri);
        iowa.Neighbors.Add(nebraska);
        iowa.Neighbors.Add(southDakota);

        illinois.Neighbors.Add(wisconsin);
        illinois.Neighbors.Add(iowa);
        illinois.Neighbors.Add(missouri);
        illinois.Neighbors.Add(kentucky);
        illinois.Neighbors.Add(indiana);

        michigan.Neighbors.Add(wisconsin);
        michigan.Neighbors.Add(indiana);
        michigan.Neighbors.Add(ohio);

        newYork.Neighbors.Add(pennsylvania);
        newYork.Neighbors.Add(newJersey);
        newYork.Neighbors.Add(vermont);
        newYork.Neighbors.Add(massachusetts);


        vermont.Neighbors.Add(newYork);
        vermont.Neighbors.Add(newHampshire);
        vermont.Neighbors.Add(massachusetts);

        newHampshire.Neighbors.Add(vermont);
        newHampshire.Neighbors.Add(maine);
        newHampshire.Neighbors.Add(massachusetts);

        maine.Neighbors.Add(newHampshire);

        massachusetts.Neighbors.Add(newYork);
        massachusetts.Neighbors.Add(vermont);
        massachusetts.Neighbors.Add(newHampshire);

        massachusetts.Neighbors.Add(rhodeIsland);

        newJersey.Neighbors.Add(newYork);
        newJersey.Neighbors.Add(pennsylvania);

        pennsylvania.Neighbors.Add(newYork);
        pennsylvania.Neighbors.Add(newJersey);
        pennsylvania.Neighbors.Add(delaware);
        pennsylvania.Neighbors.Add(maryland);
        pennsylvania.Neighbors.Add(westVirginia);
        pennsylvania.Neighbors.Add(ohio);

        maryland.Neighbors.Add(pennsylvania);
        maryland.Neighbors.Add(delaware);
        maryland.Neighbors.Add(virginia);
        maryland.Neighbors.Add(westVirginia);

        ohio.Neighbors.Add(pennsylvania);
        ohio.Neighbors.Add(westVirginia);
        ohio.Neighbors.Add(kentucky);
        ohio.Neighbors.Add(indiana);
        ohio.Neighbors.Add(michigan);

        indiana.Neighbors.Add(michigan);
        indiana.Neighbors.Add(ohio);
        indiana.Neighbors.Add(kentucky);
        indiana.Neighbors.Add(illinois);

        virginia.Neighbors.Add(maryland);
        virginia.Neighbors.Add(westVirginia);
        virginia.Neighbors.Add(kentucky);
        virginia.Neighbors.Add(tennessee);
        virginia.Neighbors.Add(northCarolina);

        northCarolina.Neighbors.Add(virginia);
        northCarolina.Neighbors.Add(tennessee);
        northCarolina.Neighbors.Add(southCarolina);
        northCarolina.Neighbors.Add(georgia);

        southCarolina.Neighbors.Add(northCarolina);
        southCarolina.Neighbors.Add(georgia);

        georgia.Neighbors.Add(northCarolina);
        georgia.Neighbors.Add(southCarolina);
        georgia.Neighbors.Add(tennessee);
        georgia.Neighbors.Add(alabama);
        georgia.Neighbors.Add(florida);

        florida.Neighbors.Add(georgia);
        florida.Neighbors.Add(alabama);

        alabama.Neighbors.Add(tennessee);
        alabama.Neighbors.Add(georgia);
        alabama.Neighbors.Add(florida);
        alabama.Neighbors.Add(mississippi);

        mississippi.Neighbors.Add(tennessee);
        mississippi.Neighbors.Add(alabama);
        mississippi.Neighbors.Add(louisiana);
        mississippi.Neighbors.Add(arkansas);

        louisiana.Neighbors.Add(arkansas);
        louisiana.Neighbors.Add(mississippi);
        louisiana.Neighbors.Add(texas);

        texas.Neighbors.Add(oklahoma);
        texas.Neighbors.Add(arkansas);
        texas.Neighbors.Add(louisiana);
        texas.Neighbors.Add(newMexico);

        newMexico.Neighbors.Add(texas);
        newMexico.Neighbors.Add(oklahoma);
        newMexico.Neighbors.Add(colorado);
        newMexico.Neighbors.Add(arizona);

        arizona.Neighbors.Add(california);
        arizona.Neighbors.Add(nevada);
        arizona.Neighbors.Add(utah);
        arizona.Neighbors.Add(colorado);
        arizona.Neighbors.Add(newMexico);

        california.Neighbors.Add(oregon);
        california.Neighbors.Add(nevada);
        california.Neighbors.Add(arizona);

        nevada.Neighbors.Add(oregon);
        nevada.Neighbors.Add(idaho);
        nevada.Neighbors.Add(utah);
        nevada.Neighbors.Add(arizona);
        nevada.Neighbors.Add(california);

        utah.Neighbors.Add(idaho);
        utah.Neighbors.Add(wyoming);
        utah.Neighbors.Add(colorado);
        utah.Neighbors.Add(arizona);
        utah.Neighbors.Add(nevada);

        colorado.Neighbors.Add(wyoming);
        colorado.Neighbors.Add(nebraska);
        colorado.Neighbors.Add(kansas);
        colorado.Neighbors.Add(oklahoma);
        colorado.Neighbors.Add(newMexico);
        colorado.Neighbors.Add(utah);

        kansas.Neighbors.Add(nebraska);
        kansas.Neighbors.Add(missouri);
        kansas.Neighbors.Add(oklahoma);
        kansas.Neighbors.Add(colorado);

        nebraska.Neighbors.Add(southDakota);
        nebraska.Neighbors.Add(iowa);
        nebraska.Neighbors.Add(missouri);
        nebraska.Neighbors.Add(kansas);
        nebraska.Neighbors.Add(colorado);
        nebraska.Neighbors.Add(wyoming);

        missouri.Neighbors.Add(iowa);
        missouri.Neighbors.Add(nebraska);
        missouri.Neighbors.Add(kansas);
        missouri.Neighbors.Add(oklahoma);
        missouri.Neighbors.Add(arkansas);
        missouri.Neighbors.Add(tennessee);
        missouri.Neighbors.Add(kentucky);
        missouri.Neighbors.Add(illinois);

        kentucky.Neighbors.Add(illinois);
        kentucky.Neighbors.Add(indiana);
        kentucky.Neighbors.Add(ohio);
        kentucky.Neighbors.Add(westVirginia);
        kentucky.Neighbors.Add(virginia);
        kentucky.Neighbors.Add(tennessee);
        kentucky.Neighbors.Add(missouri);

        westVirginia.Neighbors.Add(ohio);
        westVirginia.Neighbors.Add(pennsylvania);
        westVirginia.Neighbors.Add(maryland);
        westVirginia.Neighbors.Add(virginia);
        westVirginia.Neighbors.Add(kentucky);

        tennessee.Neighbors.Add(kentucky);
        tennessee.Neighbors.Add(virginia);
        tennessee.Neighbors.Add(northCarolina);
        tennessee.Neighbors.Add(georgia);
        tennessee.Neighbors.Add(alabama);
        tennessee.Neighbors.Add(mississippi);
        tennessee.Neighbors.Add(arkansas);
        tennessee.Neighbors.Add(missouri);

        arkansas.Neighbors.Add(missouri);
        arkansas.Neighbors.Add(tennessee);
        arkansas.Neighbors.Add(mississippi);
        arkansas.Neighbors.Add(louisiana);
        arkansas.Neighbors.Add(texas);
        arkansas.Neighbors.Add(oklahoma);

        oklahoma.Neighbors.Add(colorado);
        oklahoma.Neighbors.Add(kansas);
        oklahoma.Neighbors.Add(missouri);
        oklahoma.Neighbors.Add(arkansas);
        oklahoma.Neighbors.Add(texas);
        oklahoma.Neighbors.Add(newMexico);
    }
}





