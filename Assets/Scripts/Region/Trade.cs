using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade 
{
    

    public TradeType tradeType { get; private set; }
  
    public List<ResourceType> resourceTypes { get; private set; }
    
    public List<float>  contractPrices { get; private set; }
    public List<int> limits { get; private set; }

    public Trade(TradeType tradeType, List<ResourceType> resourceTypes, List<float> contractPrices, List<int> limits)
    {
        this.tradeType = tradeType;
        this.resourceTypes = resourceTypes;
        this.contractPrices= contractPrices;
        this.limits = limits;
    }

}
public enum TradeType
{
    Import,
    Export,
    
}
