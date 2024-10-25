using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarHistory : MonoBehaviour
{
    public static Stack<War> generalIndexAndWarList = new Stack< War>(); // general index
    public static int  maxWarHistoryCount=6;
    public static int GetWarCountByGeneral(string generalIndex)
    {
        return generalIndexAndWarList.Count(war => war.generalIndex == generalIndex);
    }
    public static int GetResultCountByGeneral(string generalIndex, WarResultType warResult)
    {
        return generalIndexAndWarList.Count(war => war.generalIndex == generalIndex && war.warReslut==warResult );
    }
    
}
[Serializable]
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
[Serializable]
public enum WarResultType
{
    Victory,
    Draw,
    Defeat

}


