using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Neighbor;
public class PathFindDeneme : MonoBehaviour
{
    public static PathFindDeneme PathInstance;
    
    Pathfinding pathfinding = new Pathfinding();
    // Singleton örneði üzerinden Node'lara eriþim saðlýyoruz
    Neighbor instance = Instance; // Singleton örneði çaðrýlýyor

    private void Awake()
    {
        if (PathInstance != null)
        {
            Destroy(gameObject);
        }else
        {
            PathInstance = this;
        }
    }
    public  List<Node> GetPath(string Start , string Target)
    {
       
        return pathfinding.FindPath(GetNodeByName(Start), GetNodeByName(Target));
    }
}

