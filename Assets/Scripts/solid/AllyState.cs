using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyState : MonoBehaviour, ISelectable
{
    
  
   
    public void SellectState()
    {
        //Debug.Log("ally state seçildi");

        // Eski rengi kaydet
        if (! RegionClickHandler.Instance.originalColors.ContainsKey(gameObject))
        {
            RegionClickHandler.Instance.originalColors[gameObject] = gameObject.GetComponent<Renderer>().material.color;
        }

        // Rengi yeþil yap
        
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        

        // Komþu bölgeleri bul ve iþle
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
                    // Eski animasyonlarý temizle
                    if (RegionClickHandler.Instance.moveTweens.ContainsKey(neighborStateGameobject))
                    {
                        RegionClickHandler.Instance.moveTweens[neighborStateGameobject].Kill(); // Önceki Tween'i öldür
                    }

                    // Hareket ve renk deðiþimi
                    Tween moveTween = neighborStateGameobject.transform.DOMoveY(neighborStateGameobject.transform.position.y + GameManager.Instance.moveAmount, GameManager.Instance.moveDuration)
                        .SetLoops(-1, LoopType.Yoyo)  // Sonsuz döngü (Yoyo hareketi)
                        .SetEase(Ease.InOutQuad);     // Ease türü (Kolaylaþtýrma)

                    // Tween nesnesini saklayýn
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
            kvp.Value.Kill(); // Animasyonu durdur ve kaldýr
        }
           RegionClickHandler.Instance.moveTweens.Clear(); // Tüm Tween nesnelerini temizle
    }

    // Eski renkleri geri yüklemek için bir fonksiyon
    public void RestoreOriginalColors()
    {
        foreach (var kvp in RegionClickHandler.Instance.originalColors)
        {
            kvp.Key.GetComponent<Renderer>().material.color = kvp.Value;
        }
        RegionClickHandler.Instance.originalColors.Clear(); // Tüm renkleri geri yükledikten sonra dictionary'i temizle
    }

    public void Attack2()
    {
        Debug.LogWarning("kendi bölgene saldýramassýn");
    }

    public void CloseAll()
    {
        FinishAttack();
    }


}
