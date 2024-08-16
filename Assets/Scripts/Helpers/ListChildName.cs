using UnityEngine;
using System.Collections.Generic;

public class ListChildName : MonoBehaviour
{
    public Transform parentTransform; // Adlar�n� almak istedi�imiz parent transform

    void Start()
    {
        List<string> childNames = new List<string>();

        // Parent transform'un t�m �ocuklar�n� d�ng�ye al
        foreach (Transform child in parentTransform)
        {
            // �ocu�un ad�n� listeye ekle
            childNames.Add(child.name);
        }
        string satets = "";

        // Listedeki isimleri ekrana yazd�r (Debug i�in)
        foreach (string name in childNames)
        {
           // Debug.Log(name);
            satets += " "+name;
        }
        print(satets);
    }
}
