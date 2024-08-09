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

                LoadStateIcon(state.gameObject);




            }
               
            else
            {
                Debug.LogWarning("state  içermiyor");
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
}
