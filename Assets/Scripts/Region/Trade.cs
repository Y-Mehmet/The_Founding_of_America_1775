using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Trade 
{


    public TradeType tradeType;  // Serileþtirilebilmesi için private kaldýrýldý
    public List<ResourceType> resourceTypes;
    public List<float> contractPrices;
    public List<float> limit;


    public Trade(TradeType tradeType, List<ResourceType> resourceTypes, List<float> contractPrices, List<float> limit)
    {
        this.tradeType = tradeType;
        this.resourceTypes = resourceTypes;
        this.contractPrices = contractPrices;
        this.limit = limit;
    }
   

}
[Serializable]
public enum TradeType
{
    Import,
    Export,
    
}
