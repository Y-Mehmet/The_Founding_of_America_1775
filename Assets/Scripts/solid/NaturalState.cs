using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalState : MonoBehaviour, ISelectable
{
    public List<GameObject> neighborStates = new List<GameObject>();

    // Eski renkleri saklamak i�in bir dictionary (hashmap) olu�turun
    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();
    // Tween nesnelerini saklamak i�in bir Dictionary olu�turun
    private Dictionary<GameObject, Tween> moveTweens = new Dictionary<GameObject, Tween>();
    public void Attack2()
    {
        Debug.Log("Sava��lacak b�lge se�ildi: " + gameObject.name);
        Attack.Instance.Attacking(gameObject.name);

        // Eski rengi geri y�kle
        FinishAttack();
    }

    public void SellectState()
    {
        Debug.LogWarning("natual state se�ildi");






       // RegionManager.instance.ShowNaturalRegionInfo(gameObject.name);
        GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();
        UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.NaturalStatePanel);
    }
    public void FinishAttack()
    {
        GameManager.Instance.ChangeIsAttackValueFalse();
       // GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
       
    }

  

    public void CloseAll()
    {
        FinishAttack();
    }
}
