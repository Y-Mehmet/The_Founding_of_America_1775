using System;
using System.Collections.Generic;
using UnityEngine; // Use Unity's logging system

class Neighbor
{
    // Stores the adjacency list of cities
    public Dictionary<string, List<string>> adjacencyList;

    // Singleton instance
    private static Neighbor instance;

    // Private constructor to prevent external instantiation
    private Neighbor()
    {
        adjacencyList = new Dictionary<string, List<string>>();
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
            Debug.Log("Komþular yazdý: " + item);
            neighborLists.Add(item);
        }
        return neighborLists;
    }
}





