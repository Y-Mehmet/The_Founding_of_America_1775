using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyState : MonoBehaviour, ISelectable
{
    
  
   
    public void SellectState()
    {
        Debug.Log("ally state se�ildi");

        // Eski rengi kaydet
        if (! RegionClickHandler.Instance.originalColors.ContainsKey(gameObject))
        {
            RegionClickHandler.Instance.originalColors[gameObject] = gameObject.GetComponent<Renderer>().material.color;
        }

        // Rengi ye�il yap
        Color oldGloryBlue;
        UnityEngine.ColorUtility.TryParseHtmlString("#002147", out oldGloryBlue);
        gameObject.GetComponent<Renderer>().material.color = Color.green;

        // Kom�u b�lgeleri bul ve i�le
        foreach (string neighborState in Neighbor.Instance.GetNeighbors(gameObject.name))
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
                    Tween moveTween = neighborStateGameobject.transform.DOMoveY(neighborStateGameobject.transform.position.y + GameManager.Instance.moveAmount, GameManager.Instance.moveDuration)
                        .SetLoops(-1, LoopType.Yoyo)  // Sonsuz d�ng� (Yoyo hareketi)
                        .SetEase(Ease.InOutQuad);     // Ease t�r� (Kolayla�t�rma)

                    // Tween nesnesini saklay�n
                    RegionClickHandler.Instance.moveTweens[neighborStateGameobject] = moveTween;

                    neighborStateGameobject.GetComponent<Renderer>().material.color = Color.grey;
                    RegionClickHandler.Instance.neighborStates.Add(neighborStateGameobject);
                }
            }
        }

        RegionManager.instance.ShowAllyRegionInfo(gameObject.name);
        GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();
        GameManager.Instance.UpdateStatePanel(gameObject.GetComponent<State>());
    }
    

    public void FinishAttack()
    {
        GameManager.Instance.ChangeIsAttackValueFalse();
        GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
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
    }
}
