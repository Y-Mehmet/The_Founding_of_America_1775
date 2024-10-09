using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour, ISelectable
{
    public List<GameObject> neighborStates = new List<GameObject>();



    public void Attack2()
    {
        Debug.Log("Sava��lacak b�lge se�ildi: " + gameObject.name);
        Attack.Instance.Attacking(gameObject.name);

        // Eski rengi geri y�kle
        FinishAttack();
    }

    public void SellectState()
    {
      //  Debug.LogWarning("enemy state se�ildi");






        // RegionManager.instance.ShowEnemyRegionInfo(gameObject.name);
        
        GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();
        UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.EnemyStatePanel);
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






    public void CloseAll()
    {
        FinishAttack();
    }
}
