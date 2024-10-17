using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trade 
{
    

    public TradeType tradeType { get; private set; }
  
    public List<ResourceType> resourceTypes { get; private set; }
    
    public List<float>  contractPrices { get; private set; }
    public List<float> limit {  get; private set; }


    public Trade(TradeType tradeType, List<ResourceType> resourceTypes, List<float> contractPrices, List<float> limit)
    {
        this.tradeType = tradeType;
        this.resourceTypes = new List<ResourceType> { ResourceType.Water, ResourceType.Salt, ResourceType.Meat, ResourceType.Fruits, ResourceType.Vegetables, ResourceType.Wheat, ResourceType.Wood, ResourceType.Coal, ResourceType.Iron, ResourceType.Stone, ResourceType.Diamond } ;
        this.contractPrices = contractPrices;
        this.limit = limit;
    }

}
public enum TradeType
{
    Import,
    Export,
    
}
