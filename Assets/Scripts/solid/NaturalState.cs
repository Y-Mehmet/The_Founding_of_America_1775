using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalState : MonoBehaviour, ISelectable
{
    public List<GameObject> neighborStates = new List<GameObject>();

    // Eski renkleri saklamak için bir dictionary (hashmap) oluþturun
    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();
    // Tween nesnelerini saklamak için bir Dictionary oluþturun
    private Dictionary<GameObject, Tween> moveTweens = new Dictionary<GameObject, Tween>();
    public void Attack2()
    {
        Debug.Log("Savaþýlacak bölge seçildi: " + gameObject.name);
        Attack.Instance.Attacking(gameObject.name);

        // Eski rengi geri yükle
        FinishAttack();
    }

    public void SellectState()
    {
        Debug.LogWarning("natual state seçildi");






        RegionManager.instance.ShowRegionInfo(gameObject.name);
        GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();
        GameManager.Instance.UpdateStatePanel(gameObject.GetComponent<State>());
    }
    public void FinishAttack()
    {
        GameManager.Instance.ChangeIsAttackValueFalse();
        GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
       
    }

  

    public void CloseAll()
    {
        FinishAttack();
    }
}
