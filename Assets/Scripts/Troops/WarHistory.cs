using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarHistory : MonoBehaviour
{
    public static Stack<War> generalIndexAndWarList = new Stack< War>(); // general index
    public static int  maxWarHistoryCount=6;

}
public class War
{
    
   public string warDate;
    public string generalIndex;
    public State allyState, enemyState;
    public WarResultType warReslut; 

    public War(string generalIndex,State allyState, State enemyState, WarResultType warReslut, string wardate)
    {
        this.generalIndex = generalIndex;
        this.allyState = allyState;
        this.enemyState = enemyState;
        this.warReslut = warReslut;
        this.warDate = wardate;
        
    }
}
public enum WarResultType
{
    Victory,
    Draw,
    Defeat

}


