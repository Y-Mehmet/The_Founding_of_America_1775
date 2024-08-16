using UnityEngine;
using System.Collections.Generic;

public class ListChildName : MonoBehaviour
{
    public Transform parentTransform; // Adlarýný almak istediðimiz parent transform

    void Start()
    {
        List<string> childNames = new List<string>();

        // Parent transform'un tüm çocuklarýný döngüye al
        foreach (Transform child in parentTransform)
        {
            // Çocuðun adýný listeye ekle
            childNames.Add(child.name);
        }
        string satets = "";

        // Listedeki isimleri ekrana yazdýr (Debug için)
        foreach (string name in childNames)
        {
           // Debug.Log(name);
            satets += " "+name;
        }
        print(satets);
    }
}
