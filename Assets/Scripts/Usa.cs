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
                Debug.LogWarning("state componenti   i�ermiyor");
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
        // Ad� normalize ederek �ngilizce karakterlere �evir
        gameObjectName = NormalizeString(gameObjectName);

        foreach (Transform item in transform)
        {
            string normalizedItemName = NormalizeString(item.name); // T�rk�e karakterlerden ar�nd�r
            if (normalizedItemName == gameObjectName)
            {
                return item.GetComponent<State>();
            }
        }
        Debug.LogWarning("Yanl�� state ad� girildi: " + gameObjectName);
        return null;
    }

    // T�rk�e karakterleri �ngilizce kar��l�klar�na �eviren fonksiyon
    private string NormalizeString(string input)
    {
        return input

            .Replace("�", "I");
            
    }
}
