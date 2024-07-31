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
    public bool ÝsAttack= false;
    public bool IsRegionPanelOpen = false;
    public float attackFinidhDuration = 5.0f;

    public void ChangeIsAttackValueTrue()
    {
        ÝsAttack = true;
    }
    public void ChangeIsAttackValueFalse()
    {
        ÝsAttack = false;
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
