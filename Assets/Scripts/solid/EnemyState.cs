using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour, ISelectable
{
    public List<GameObject> neighborStates = new List<GameObject>();

  
    
    
    public void Attack2()
    {
        Debug.Log("Savaþýlacak bölge seçildi: " + gameObject.name);
        Attack.Instance.Attacking(gameObject.name);

        // Eski rengi geri yükle
        FinishAttack();
    }

    public void SellectState()
    {
        Debug.LogWarning("enemy state seçildi");

       

        
       

        RegionManager.instance.ShowEnemyRegionInfo(gameObject.name);
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






    public void CloseAll()
    {
        FinishAttack();
    }
}
