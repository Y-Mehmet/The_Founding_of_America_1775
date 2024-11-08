using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usa : MonoBehaviour
{
    public static Usa Instance ;
    private void Awake()
    {
        if( Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            
        }
       

        foreach (Transform state in transform)
        {
            if (state.GetComponent<State>() != null)
            {

                // LoadStateIcon(state.gameObject);
                int index= state.GetSiblingIndex();
                state.GetComponent<State>().HierarchicalIndex = index;
                LoadStateIconWhitSO(state.gameObject,index);
                



            }
               
            else
            {
                Debug.LogWarning("state componenti   içermiyor");
            }
            
                

        }
    }
    void LoadStateIcon(GameObject state)
    {
        
        string spritePath = "StateIcon/" + state.gameObject.name;
        Sprite sprite = Resources.Load<Sprite>(spritePath);
        if (sprite != null)
        {
            
            state.GetComponent<State>().StateIcon = sprite;


            //Debug.Log("burada resimler eklendi " + spritePath);
        }
        else
        {
            Debug.Log("burada resimler eklenemedi!!! sprite yolu " + spritePath);

        }
    }
    void LoadStateIconWhitSO(GameObject state, int index)
    {
        Sprite sprite = StateFlagSpritesSO.Instance.flagSpriteLists[index];

        if (sprite != null)
        {

            state.GetComponent<State>().StateIcon = sprite;


            //Debug.Log("burada resimler eklendi " + spritePath);
        }
        else
        {
            Debug.Log("burada resimler eklenemedi!!! sprite yolu " + index+state.name);

        }
    }
    public State FindStateByName(string gameObjectName)
    {
        // Adý normalize ederek Ýngilizce karakterlere çevir
        gameObjectName = NormalizeString(gameObjectName);

        foreach (Transform item in transform)
        {
            string normalizedItemName = NormalizeString(item.name); // Türkçe karakterlerden arýndýr
            if (normalizedItemName == gameObjectName)
            {
                return item.GetComponent<State>();
            }
        }
        Debug.LogWarning("Yanlýþ state adý girildi: " + gameObjectName);
        return null;
    }

    // Türkçe karakterleri Ýngilizce karþýlýklarýna çeviren fonksiyon
    private string NormalizeString(string input)
    {
        return input

            .Replace("Ý", "I");
            
    }
}
