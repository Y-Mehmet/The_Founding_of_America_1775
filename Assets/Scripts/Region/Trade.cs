using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade 
{
    

    public TradeType tradeType { get; private set; }
  
    public List<ResourceType> resourceTypes { get; private set; }
    
    public List<float>  contractPrices { get; private set; }


    public Trade(TradeType tradeType, List<ResourceType> resourceTypes, List<float> contractPrices)
    {
        this.tradeType = tradeType;
        this.resourceTypes = resourceTypes;
        this.contractPrices= contractPrices;
       
    }

}
public enum TradeType
{
    Import,
    Export,
    
}
