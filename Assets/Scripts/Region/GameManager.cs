using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);

        }else
        {
            Instance = this;
        }
    }
    public bool �sAttack= false;
    public bool IsRegionPanelOpen = false;
    public float attackFinidhDuration = 7.0f;

    public void ChangeIsAttackValueTrue()
    {
        �sAttack = true;
    }
    public void ChangeIsAttackValueFalse()
    {
        �sAttack = false;
    }
    public void ChanngeIsRegionPanelOpenValueTrue()
    {
        IsRegionPanelOpen = true;
    }
    public void ChanngeIsRegionPanelOpenValueFalse()
    {
        IsRegionPanelOpen = false;
    }
}
