using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCollor : MonoBehaviour
{
    public static ChangeCollor Instance { get; private set; }

    Color oldGloryRed;
    Color oldGloryBlue;
    Color oldGloryWhite;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        UnityEngine.ColorUtility.TryParseHtmlString("#B22234", out oldGloryRed);
        UnityEngine.ColorUtility.TryParseHtmlString("#002147", out oldGloryBlue);
        oldGloryWhite = Color.white;
    }

   

    public void ChangeGameobjectColor(GameObject obj, StateType stateType)
    {
        Color colorToApply = GetColorByEnum(stateType);
        obj.GetComponent<Renderer>().material.color = colorToApply;
    }

    private Color GetColorByEnum(StateType stateType)
    {
        switch (stateType)
        {
            case StateType.Enemy:
                return oldGloryRed;
            case StateType.Ally:
                return oldGloryBlue;
            case StateType.Neutral:
                return oldGloryWhite;
            default:
                return oldGloryRed; 
        }
    }
}


