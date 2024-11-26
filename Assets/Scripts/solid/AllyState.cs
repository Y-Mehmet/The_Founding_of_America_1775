using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyState : MonoBehaviour, ISelectable
{

    

    public void SellectState()
    {
       // Debug.Log("ally state se�ildi");

        // Eski rengi kaydet
        if (! RegionClickHandler.Instance.originalColors.ContainsKey(gameObject))
        {
            RegionClickHandler.Instance.originalColors[gameObject] = gameObject.GetComponent<Renderer>().material.color;
        }

        // Rengi ye�il yap
        
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        

        // Kom�u b�lgeleri bul ve i�le
        foreach (string neighborState in Neighbor.Instance.GetEnemyNeighbors(gameObject.name))
        {
            GameObject neighborStateGameobject = GameObject.Find(neighborState);
            if (neighborStateGameobject != null)
            {
                // Eski rengi kaydet
                if (!RegionClickHandler.Instance.originalColors.ContainsKey(neighborStateGameobject))
                {
                    RegionClickHandler.Instance.originalColors[neighborStateGameobject] = neighborStateGameobject.GetComponent<Renderer>().material.color;
                }

                if (neighborStateGameobject.GetComponent<State>().stateType == StateType.Ally)
                {
                    RegionClickHandler.Instance.neighborStates.Add(neighborStateGameobject);
                }
                else
                {
                    // Eski animasyonlar� temizle
                    if (RegionClickHandler.Instance.moveTweens.ContainsKey(neighborStateGameobject))
                    {
                        RegionClickHandler.Instance.moveTweens[neighborStateGameobject].Kill(); // �nceki Tween'i �ld�r
                    }

                    // Hareket ve renk de�i�imi
                    float startY = neighborStateGameobject.transform.position.y;
                    float targetY = startY + GameManager.Instance.moveAmount;

                    Tween moveTween = neighborStateGameobject.transform.DOMoveY(targetY, GameManager.Instance.moveDuration)
                        .SetLoops(-1, LoopType.Yoyo)
                        .SetEase(Ease.InOutQuad);

                    // Pozisyon de�erlerini kontrol etmek i�in log ekleyin
                  //  Debug.Log($"Start Y: {startY}, Target Y: {targetY}");

                    // Tween nesnesini saklay�n
                    RegionClickHandler.Instance.moveTweens[neighborStateGameobject] = moveTween;

                    neighborStateGameobject.GetComponent<Renderer>().material.color = Color.grey;
                    RegionClickHandler.Instance.neighborStates.Add(neighborStateGameobject);
                }
            }
        }

        //RegionManager.instance.ShowAllyRegionInfo(gameObject.name);
       
        if(GetComponent<State>().IsCapitalCity)
        {
           // Debug.LogWarning("capital city g�sterildi ");
            UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.CapitalStatePanel);
        }else
        {
           // Debug.LogWarning("ally stete g�sterildi");
            UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.AllyStatePanel);
        }
        GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();

    }
    

    public void FinishAttack()
    {
        GameManager.Instance.ChangeIsAttackValueFalse();
        //GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
        RestoreOriginalColors();
        StopAnimations();
    }

    public void StopAnimations()
    {
        foreach (var kvp in RegionClickHandler.Instance.moveTweens)
        {
            kvp.Value.Kill(); // Animasyonu durdur ve kald�r
        }
           RegionClickHandler.Instance.moveTweens.Clear(); // T�m Tween nesnelerini temizle
    }

    // Eski renkleri geri y�klemek i�in bir fonksiyon
    public void RestoreOriginalColors()
    {
        foreach (var kvp in RegionClickHandler.Instance.originalColors)
        {
            kvp.Key.transform.position = new Vector3(kvp.Key.transform.position.x, 33.35355f, kvp.Key.transform.position.z);
            kvp.Key.GetComponent<Renderer>().material.color = kvp.Value;
           
           

        }
        RegionClickHandler.Instance.originalColors.Clear(); // T�m renkleri geri y�kledikten sonra dictionary'i temizle
    }

    public void Attack2()
    {
        Debug.LogWarning("kendi b�lgene sald�ramass�n");
    }

    public void CloseAll()
    {
        FinishAttack();
        RegionClickHandler.staticState = null;
        RegionClickHandler.Instance.currentState = null;
        
    }


}
