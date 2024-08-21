using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    public static MineManager instance { get; set; }
    public Dictionary<ResourceType, MineMatarials> MineRequiredResList = new Dictionary<ResourceType, MineMatarials> ();
    public Dictionary<ResourceType, string> ResMineNameList = new Dictionary<ResourceType, string> ();
    public ResourceType curentResource;
    public Action<ResourceType> OnResourceChanged;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }
    private void Start()
    {
        SetCurrentResource(ResourceType.Gold);
    }
    public void SetCurrentResource(ResourceType resourceType)
    {
        curentResource = resourceType;
        OnResourceChanged?.Invoke(resourceType);
    }
    public string  GetMineName()
    {
        string mineName = ResMineNameList[curentResource];
        return mineName;
    }
    public List<int> GetReqResValue()
    {
        List<int> valueList= new List<int> ();
        valueList= MineRequiredResList[curentResource].RequiredResValueList;
        
        return valueList;

    }
    public List<ResourceType> GetReqResType()
    {
        List<ResourceType> resTypeList = new List<ResourceType>();
        resTypeList = MineRequiredResList[curentResource].RequiredResTypeList;
       
        return resTypeList;

    }



}
 public class MineMatarials
{
  public List<ResourceType> RequiredResTypeList = new List<ResourceType>();
  public List<int> RequiredResValueList = new List<int>();

    public MineMatarials(List<ResourceType> requiredResTypeList, List<int> requiredResValueList)
    {
        RequiredResTypeList = requiredResTypeList;
        RequiredResValueList = requiredResValueList;
    }
}
