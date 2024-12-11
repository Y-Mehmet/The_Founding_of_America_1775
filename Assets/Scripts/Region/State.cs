using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GeneralManager;
using static GameManager;
using static USCongress;
using static StateResourceSO;
using Random = UnityEngine.Random;



[Serializable]
public class State : MonoBehaviour
{
    public bool IsCapitalCity = false;
    private Coroutine increaseArmySizeCoroutine;
    private Coroutine decreaseArmySizeCoroutine;
    private Coroutine resourceProductionCoroutine;
    private Coroutine moreleCoroutine;
    private Coroutine incrasePopulationCoroutine;
 
    public Action<float, State> OnMoreleChanged;
   
    public int HierarchicalIndex;
    public string StateName = "";
    public float ArmySize ;
    public int ArmyBarrackSize;
    public float LandArmySize;
    public float NavalArmySize;
    public float UnitLandArmyPower;
    public float UnitNavalArmyPower;
    public float UnitArmyPower ;
    public float TotalArmyPower;
    public StateType stateType;
    public float Morele ;
    public Sprite StateIcon;// falg
    public int Population;
    public int populationAddedValue;
    public int Resources; // fav resources 
    public float loss;
    public int attackCanvasButtonPanelIndex = 1;
 
    public List<TaxData> Taxes = new List<TaxData>();

    public Dictionary<ResourceType, ResourceData> resourceData = new Dictionary<ResourceType, ResourceData>();
    public float resoruceAddedValue;
    public Trade importTrade;
    public Trade exportTrade;
    public List<Trade> tradeLists = new List<Trade>();
    
    public  Dictionary<ResourceType, float> plunderedResources = new Dictionary<ResourceType, float>();

    int firstArmySize = 100; // state el de�itirdikten sonraki army size 
    public float MoraleMultiplier = 0.01f; // Moralin asker art���na etkisi
  
    public float PopulationMultiplier = 0.001f; // N�fusun asker art���na etkisi
    private float populationGrowthRateMultiplier=0.00002f; // population growth rate multiplier

   
    private void Start()
    {
        if(GameManager.Instance!= null)
        {
            GameManager.Instance.OnGameDataLoaded += GameDataLoaded;
            if(stateType== StateType.Ally)
            {
                SubsucribeAction();
            }
           
           
        }
        else
        {
            Debug.LogError("gamanenager is null");
        }

    }
    

    private void GameDataLoaded()
    {
        TotalArmyPower = GetTotalArmyPower();
       // ArmyBarrackSize = (int)(Population * GameManager.ArmyBarrackRatio);
        FindISelectibleComponentAndDisable();
        switch (stateType)
        {
            case (StateType.Ally):
                gameObject.GetComponent<AllyState>().enabled= true;
                if (IsCapitalCity)
                {

                    transform.GetComponentInChildren<Flag>().capitalFlag.SetActive(true);
                   // Debug.LogError(" gamedata loadeda catiptal satate flag is true " + this.name);
                }
                else
                {
                    transform.GetComponentInChildren<Flag>().capitalFlag.SetActive(false);
                }
                break;
            case (StateType.Enemy):
                gameObject.GetComponent<EnemyState>().enabled = true;
                break;
            case (StateType.Neutral):
                gameObject.GetComponent<NaturalState>().enabled = true;
                break;
        }
        GameManager.Instance.OnAttackStarted += HandleAttackStarted;
        GameManager.Instance.OnAttackStopped += HandleAttackStopped;
     
        HandleAttackStopped();
       


    }


    public void ReduceEnemyMorale(int decraseValue)
    {
        Morele += decraseValue;
        Morele = Morele < 0 ? 0 : Morele;
        if (Morele <= 10 && !isWarDeclared)
        {
            isWarDeclared = true;
            StartCoroutine(DeclereWar());
        }
    }

    IEnumerator DeclereWar()
    {
        MessageManager.AddMessage("The " + name + " State has raised arms against you! Prepare to defend your lands and honor.");

        while (Morele <= 10)
        {
            int random = Random.Range(0, 10);

            if (random == 5 && GameManager.Instance.IsAttackFinish)
            {
                // isRegionPanel'in false olmas�n� bekle
                while (GameManager.Instance.IsRegionPanelOpen == true) // isRegionPanel true oldu�u s�rece bekle
                {
                    yield return null; // Her frame'de kontrol� tekrar yap
                }

                // Sava�� ba�lat ve coroutine�i sonland�r
               if( !AllyStateList.Contains(this))
                {
                    GameManager.Instance.ChangeIsAttackValueTrue();
                    UIManager.Instance.GetComponent<HideAllPanelButton>().DoHidePanel();
                    RegionClickHandler.Instance.currentState = null;
                    RegionClickHandler.staticState = null;
                  //  Debug.LogWarning("random say� 5 sava� ilan� " + random);

                    Attack.Instance.Attacking(gameObject.name);
                    SoundManager.instance.Play("ChurchBell");
                    SoundManager.instance.Stop("Theme");
                }

                // Coroutine'den ��kmak i�in isWarDeclared'i false yap�yoruz
                isWarDeclared = false;
                yield break;
            }

            yield return new WaitForSeconds(gameDayTime);
        }
        // E�er Morele 10'dan yukar� ��karsa sava� durumu resetlenir
        isWarDeclared = false;
    }



    public void SubsucribeAction()
    {
        USCongress.OnRepealActChange += OnRepealChanged;
        USCongress.OnEnactActChange += OnEnactChanged;
        USCongress.OnEnactActChange?.Invoke(currentAct);
    }
    public void DeSubcucribeAction()
    {
        USCongress.OnRepealActChange -= OnRepealChanged;
        USCongress.OnEnactActChange -= OnEnactChanged;
        currentAct = ActType.None;
        PopulationStabilityAct = false;
        MoralAddedValue = 0;
        ConsumptionAddedValue = 100;
        UnitArmyPowerAddedValue = 0;
        ProductionAddedValue = 100;
        PopulationAddedValue = 100;
}

    public int GetMorale()
    {
        return (int)Morele;
    }
    public   void SetMorale(int morale)
    {
        if (moreleCoroutine != null)
        {
            StopCoroutine(moreleCoroutine);
            moreleCoroutine = null;
        }
        Morele += morale;
        Morele = Morele > 100 ? 100 : Morele;
        Morele = Morele < 0 ? 0 : Morele;
        if (moreleCoroutine == null)
        {
            moreleCoroutine = StartCoroutine(ChangeMorale());
        }
           

    }
    public int GetArmyBarrackSize()
    {
        return (int)ArmyBarrackSize;
    }
    public int GetSoliderQuota()
    {
        int quota= GetArmyBarrackSize() - GetArmySize();
        return quota > 0 ? quota : 0;

    }

    public int GetNavalArmySize()
    {
        return (int)NavalArmySize;
    }
    public int GetLandArmySize()
    {
        return (int) LandArmySize;
    }
    public int GetTotalArmyPower()
    {
        General general;
        float generalLandHelpRate = 0;
        float generalNavalHelpRate = 0;
        if (stateGenerals.TryGetValue(this, out general))
        {
            generalLandHelpRate = general.LandHelpRate;
            generalNavalHelpRate = general.NavalHelpRate;

        }
        

        TotalArmyPower = (LandArmySize * GetUnitLandRate() +NavalArmySize*GetUnitNavalRate()) /100*(Morele<50?50:Morele);
       // Debug.LogWarning($"{LandArmySize} ");
        return TotalArmyPower<0?0:(int) TotalArmyPower;
    }
    public int GetArmySize()
    {
        ArmySize = (LandArmySize ) + (NavalArmySize);
        return ArmySize<0? 0:(int)ArmySize;
    }
    public float  GetUnitLandRate()
    {
        General general;
        float generalLandHelpRate = 0;
      if (stateGenerals.TryGetValue(this, out general))
        {
            generalLandHelpRate = general.LandHelpRate;
        }
        float totalRate = UnitLandArmyPower + generalLandHelpRate+UnitArmyPowerAddedValue;
        return float.Parse(String.Format("{0:0.00}", totalRate)); ;
    }
    public float GetUnitNavalRate()
    {
        General general;
        float generalNavalRate = 0;
        if (stateGenerals.TryGetValue(this, out general))
        {
            generalNavalRate = general.NavalHelpRate;
        }
        float totalRate= UnitNavalArmyPower + generalNavalRate+ UnitArmyPowerAddedValue; 
        return float.Parse(String.Format("{0:0.00}", totalRate)); 
    }
    public void DeployTroops( int land, int naval)
    {
        Debug.Log(" deploy troop ");
        LandArmySize -= land;
        NavalArmySize -= naval;
        
       
    }
    public void ReinForceTroops(int land, int naval)
    {
        Debug.Log(" reinforce troop ");
        int reinforceTroop = land + naval;
        int loss = GetSoliderQuota() - reinforceTroop;
       
        if (loss<0)
        {
            land += land / reinforceTroop * loss;
            naval += naval / reinforceTroop * loss;
            MessageManager.AddMessage("Eager to bolster " + name +
                ", reinforcements were dispatched from  origin state "+
    " Yet, overcrowded barracks left no space for the incoming soldiers. With nowhere to go and patience wearing thin," +
    " many of the stranded forces abandoned their posts, refusing to endure such mismanagement." +
    " This failure has cost the state both manpower and morale, as disillusioned soldiers desert in frustration.");
            SetMorale(-5);

            // T�rk�e kar��l�k:
            // " " + destinationState + "'i g��lendirmek i�in " + originState + " 'den takviye birlikler g�nderildi." +
            // " Ancak kalabal�k k��lalar, gelen askerler i�in yer b�rakmad�." +
            // " Gidecek bir yer bulamayan ve sabr� t�kenen bir�ok asker bu beceriksizli�e daha fazla tahamm�l edemeyip firar etti." +
            // " Bu ba�ar�s�zl�k, devleti hem insan g�c� hem de moral a��s�ndan a��r bir zarara u�ratt�."

        }
        LandArmySize += land;
        NavalArmySize += naval;

    }
  
    public int GetCurrentResValue(ResourceType resourceType)
    {
        return (int) resourceData[resourceType].currentAmount;
    }
    private float GetTaxSatisfactionRate()
    {
        float taxSatisfactionRate = 0;
        foreach (var tax in Taxes)
        {
            if(tax.taxType!= TaxType.DirectTax || tax.taxType != TaxType.ValueAddedTax)
            {
                float result = tax.currentRate - tax.toleranceLimit;

                if (result > 0)
                {
                    // Exponential etki: Fark�n karesi veya ba�ka bir �s

                    taxSatisfactionRate -= Mathf.Pow(result, 1.25f); // Fark�n karesi (result^2)

                }
                else
                {
                    taxSatisfactionRate += Mathf.Pow(-result, 0.5f); // 
                }
            }
        }
        return taxSatisfactionRate * 0.005f;
    }
    private float GetResourceFactionRate()
    {

        return resoruceAddedValue * 0.05f;
    }
  
    public int GetGoldResValue()
    {
       
        if (resourceData.ContainsKey(ResourceType.Gold))
        {
           
            return (int)resourceData[ResourceType.Gold].currentAmount <= 0 ? 0 : (int)resourceData[ResourceType.Gold].currentAmount;
        }
        else
        {
            return 0; // E�er 'Diamond' anahtar� yoksa, 0 d�nd�r.
        }
    }
    public int GetGemResValue()
    {
        if (resourceData.ContainsKey(ResourceType.Diamond))
        {
            return (int)resourceData[ResourceType.Diamond].currentAmount <= 0 ? 0 : (int)resourceData[ResourceType.Diamond].currentAmount;
        }
        else
        {
            return 0; // E�er 'Diamond' anahtar� yoksa, 0 d�nd�r.
        }
    }

    public void IncreaseArmyBarrackSize(int barrackValue)
    {
        ArmyBarrackSize += barrackValue;
     
    }
    public void IncreaseNavalArmySize( int navalArmyValue)
    {
        NavalArmySize+= navalArmyValue;
        MissionsManager.AddTotalSolider(navalArmyValue);
    }
    public void IncreaseLandArmySize(int landArmyValue)
    {
        LandArmySize += landArmyValue;
        MissionsManager.AddTotalSolider(landArmyValue);
    }

    private void HandleAttackStarted()
    {
        if (increaseArmySizeCoroutine != null)
        {
            StopCoroutine(increaseArmySizeCoroutine);
            increaseArmySizeCoroutine = null;
        }
        if (decreaseArmySizeCoroutine != null)
        {
            StopCoroutine(decreaseArmySizeCoroutine);
            decreaseArmySizeCoroutine = null;
        }

            if (resourceProductionCoroutine != null)
        {
            StopCoroutine(resourceProductionCoroutine);
            resourceProductionCoroutine = null;
        }
        if (moreleCoroutine != null)
        {
            StopCoroutine(moreleCoroutine);
            moreleCoroutine = null;
        }
        StopIncrasePopulation();


    }

    void StopIncrasePopulation()
    {
        if (incrasePopulationCoroutine != null)
        {
            StopCoroutine(incrasePopulationCoroutine);

            incrasePopulationCoroutine = null;
            populationAddedValue = 0;

        }
    }
    void StartIncrasePopulatoin()
    {
      
        if (incrasePopulationCoroutine == null)
        {
           incrasePopulationCoroutine= StartCoroutine(IncrasePopulationOverTime());

          

        }
    }
    void OnEnactChanged(ActType actType)
    {
        switch(((int)actType))
        {
            case 0:
                StopIncrasePopulation();
                USCongress.PopulationStabilityAct = true;
                populationAddedValue = 0;
                MoralAddedValue = -1f;
                break;
            case 1:
                MoralAddedValue = .1f;
                ConsumptionAddedValue = 110;
                break;
            case 2:
                UnitArmyPowerAddedValue = 0.1f;
                ProductionAddedValue = 90;
                break;
            case 3:
                MoralAddedValue = .1f;
                ProductionAddedValue = 90;
                break;
                case 4:
                ProductionAddedValue =110;
                PopulationAddedValue = 110;
                break;

            default:
                break;
        }

    }
    void OnRepealChanged(ActType actType)
    {
        switch (((int)actType))
        {
            case 0:
                StartIncrasePopulatoin();
                USCongress.PopulationStabilityAct = false;
                MoralAddedValue = 0;
                break;
            case 1:
                MoralAddedValue = 0;
                ConsumptionAddedValue = 100;
                break;
            case 2:
                UnitArmyPowerAddedValue = 0;
                ProductionAddedValue = 100;
                break;
            case 3:
                MoralAddedValue = 0;
                ProductionAddedValue = 100;
                break;
            case 4:
                ProductionAddedValue = 100;
                PopulationAddedValue = 100;
                break;
                
            default:
                break;
        }
    }

    private void HandleAttackStopped()
    {
        HandleAttackStarted();
        if (increaseArmySizeCoroutine == null && stateType!=StateType.Ally)
            increaseArmySizeCoroutine = StartCoroutine(IncreaseArmySizeOverTime());

        if (resourceProductionCoroutine == null)
        {
            resourceProductionCoroutine = StartCoroutine(ResourceProduction());
        }else
        {
             Debug.LogWarning(" resource null degil ");
        }
            
       

        if (moreleCoroutine == null)
            moreleCoroutine = StartCoroutine(ChangeMorale());
        if (incrasePopulationCoroutine == null )
        {
            if(stateType== StateType.Ally)
            {
                if(!PopulationStabilityAct)
                    incrasePopulationCoroutine = StartCoroutine(IncrasePopulationOverTime());

            }else
            incrasePopulationCoroutine = StartCoroutine(IncrasePopulationOverTime());          
        }
       
    }

    private IEnumerator ChangeMorale()
    {
        float addedValue = 0;
        while (!GameManager.Instance.�sGameOver && !GameManager.Instance.isGamePause && GameManager.Instance.IsAttackFinish && stateType== StateType.Ally)
        {
           addedValue= GetTaxSatisfactionRate()+GetResourceFactionRate()+ MoralAddedValue;
                        
                Morele += addedValue;
                Morele = Mathf.Clamp(Morele, 0, 100);
                State state = gameObject.GetComponent<State>();
                OnMoreleChanged?.Invoke(Morele, state);
            if(Morele<=10)
            {
                int rand = UnityEngine.Random.Range(0, 10);
                if(rand==9 && GameManager.Instance.IsAttackFinish)
                {
                    LostState();
                    UIManager.Instance.HideAllPanel();
                    MessageManager.AddMessage("With dwindling resources and shattered morale, the spirit of the people fades." +
    " Once-loyal citizens abandon their allegiance as despair takes root. Cries for change grow louder" +
    " and discontent sweeps across " + name + " like a wildfire. In the final hour, the people seize their fate," +
    " toppling the weakened state that failed them in their hour of need. Rebellion has consumed " + name + ".");
                    UIManager.Instance.GetComponent<ShowPanelButton>().DoShowPanelWhitId(PanelID.MessagePanel);
                    MessageManager.unreadMessageCount = 0;
                    MessageManager.OnAddMessage?.Invoke(0);

                    // T�rk�e kar��l�k:
                    // "Kaynaklar t�kendik�e ve moraller parampar�a olduk�a, halk�n ruhu zay�fl�yor. Bir zamanlar sad�k olan vatanda�lar, umutsuzluk k�k salarken ba�l�l�klar�n� terk eder." +
                    // " De�i�im talepleri giderek y�kseliyor ve huzursuzluk " + name + " boyunca bir yang�n gibi yay�l�yor." +
                    // " Son saatlerde halk, kendi kaderlerini ele ge�irerek onlar� ihtiya� an�nda yaln�z b�rakan zay�f d��m�� devleti devral�yor." +
                    // " �syan, " + name + "'i t�ketti."

                }

            }

            yield return new WaitForSeconds(gameDayTime);
        }
    }
    private IEnumerator IncreaseArmySizeOverTime()
    {
        while (!GameManager.Instance.�sGameOver && !GameManager.Instance.isGamePause && GameManager.Instance.IsAttackFinish)
        {
            float armyIncreasePerSecond = Morele * MoraleMultiplier * Population * PopulationMultiplier;
            LandArmySize += armyIncreasePerSecond;
            NavalArmySize+= armyIncreasePerSecond;
            //TotalArmyPower = ArmySize * UnitArmyPower;
            yield return new WaitForSeconds(GameManager.gameDayTime);
        }
    }
    private IEnumerator ReduceArmySizeOverTime()
    {
        while (!GameManager.Instance.�sGameOver && !GameManager.Instance.isGamePause && GameManager.Instance.IsAttackFinish&& GetArmyBarrackSize() < GetArmySize())
        {
            float armyIncreasePerSecond = (100-Morele) * MoraleMultiplier * Population * PopulationMultiplier;
            LandArmySize -= armyIncreasePerSecond;
            NavalArmySize -= armyIncreasePerSecond;
            //TotalArmyPower = ArmySize * UnitArmyPower;
            yield return new WaitForSeconds(GameManager.gameDayTime);
        }
    }
    private IEnumerator IncrasePopulationOverTime()
    {
       
        int populationIncreasePerSecond = 0;
        int populationDecrasePerSecond = 0;

        while (!GameManager.Instance.�sGameOver && !GameManager.Instance.isGamePause && GameManager.Instance.IsAttackFinish)
        {
            populationIncreasePerSecond = (int)( Morele * Population * populationGrowthRateMultiplier/100*PopulationAddedValue);
            
             populationDecrasePerSecond = (int)((100 - Morele) * Population * populationGrowthRateMultiplier);
             populationAddedValue= populationIncreasePerSecond-populationDecrasePerSecond;

            Population += populationAddedValue;
            if (Population >= MAX_POPULATION)
            {
                Population = MAX_POPULATION; PopulationAddedValue = 0;
            }
               

            if(stateType==StateType.Ally)
            TotalPopulationManager(populationAddedValue);
            yield return new WaitForSeconds(GameManager.gameDayTime);
        }
    }
   

    private IEnumerator ResourceProduction()
    {
      //  Debug.Log("res proeodact �al��t�");
       
        while (!GameManager.Instance.�sGameOver && !GameManager.Instance.isGamePause && GameManager.Instance.IsAttackFinish )
        {
            resoruceAddedValue = 0;
            foreach (var item in resourceData)
            {
                float productionAmount = item.Value.mineCount * item.Value.productionRate;
                float moraleEffect = (101 - Morele) / 100;
                productionAmount *= (1 - moraleEffect * 0.1f);

                if (item.Key== ResourceType.Gold && stateType== StateType.Ally)
                {
                    foreach (var item1 in Taxes)
                    {
                        if(item1.taxType==TaxType.IncomeTax)
                        {
                            float tax = (productionAmount / 100)* item1.currentRate*ProductionAddedValue/100;
                          
                            item1.taxIncome= tax;
                            ResourceManager.Instance.ChargeTax(ResourceType.Gold, tax);
                            productionAmount -= tax;

                        }
                        else if(item1.taxType== TaxType.StampTax)
                        {
                            float tax =  item1.currentRate*Population*item1.unitTaxIncome;
                            
                           ResourceManager.Instance.ChargeTax(ResourceType.Gold, tax);
                          
                        }
                        
                    }
                    
                }
                //if(item.Key== ResourceType.Gold && IsCapitalCity)
                // Debug.LogWarning($"{item.Key}  �retim  {productionAmount} rate {item.Value.productionRate } ");

                float consumption = (item.Value.consumptionAmount / 100 * ConsumptionAddedValue *( Population+GetArmySize()));
                item.Value.surplus = productionAmount - consumption;
               
                item.Value.currentAmount += productionAmount;
              
                item.Value.currentAmount -= consumption;

                if(stateType== StateType.Ally)
                {
                    if (item.Key != ResourceType.Gold)
                    {
                        if (item.Value.currentAmount < 0)
                        {
                            if ((int)item.Key > 0 && (int)item.Key < 7)
                                resoruceAddedValue += (item.Value.surplus / item.Value.productionRate);
                            int goldValue = ((int)(Mathf.CeilToInt(GameEconomy.Instance.GetGoldValue(item.Value.resourceType, -1 * item.Value.currentAmount)) * 1.1f));
                            if( goldValue<=0)
                            {
                                Debug.LogError(" gold valye " + goldValue);
                            }
                            if (GetGoldResValue() >= goldValue)
                            {
                               
                                GoldSpend(goldValue);
                            }
                            else if (ResourceManager.Instance.GetResourceAmount(ResourceType.Gold) > goldValue)
                            {
                                ResourceManager.Instance.ReduceResource(ResourceType.Gold, goldValue);
                            }
                            else
                            {
                                LostState();
                                MessageManager.AddMessage("As famine strikes the land, citizens first trade their precious gold for food," +
                                    " clinging to survival. When their gold reserves run dry, they turn to the state�s vaults in a final bid for sustenance. But as the last gold piece vanishes," +
                                    " so does hope. With empty bellies and empty hands, the people rise in desperate revolt, forsaking their allegiance." + name +
                                    ", weakened by hunger, is lost in the shadow of rebellion.");
                            }


                            item.Value.currentAmount = 0;
                        }
                    }
                }
                else
                {
                    if (item.Value.currentAmount < 0)
                    {
                      //  Debug.Log($"state name {name} mine count bir arrt� {item.Value.mineCount + 1} res type {item.Key}");
                        item.Value.mineCount++;
                    }
                }
                

            }




            yield return new WaitForSeconds(GameManager.gameDayTime);
        }
    }

    //kullan�lm�yor 
    //public void IncraseMetod()
    //{
    //    if (increaseArmySizeCoroutine == null)
    //        increaseArmySizeCoroutine = StartCoroutine(IncreaseArmySizeOverTime());

    //    if (resourceProductionCoroutine == null)
    //        resourceProductionCoroutine = StartCoroutine(ResourceProduction());
    //}
    public float TotalArmyCalculator()
    {
       
        return GetTotalArmyPower();
    }
    public void  ReduceArmySize(float loss)
    {
        int armySize = GetArmySize();
        float landArmyRatio = (float)LandArmySize / armySize;
        

        // Her bir orduya da��t�lacak kay�p say�s�n� hesapla
        int landArmyCasualties = (int)(loss * landArmyRatio);
        int navalArmyCasualties = (int)loss - landArmyCasualties;
        LandArmySize -= landArmyCasualties;
        NavalArmySize -= navalArmyCasualties;
        armySize =(int)  Mathf.Clamp(GetArmySize(), 0, 9999999999999);
        if(armySize <= 20)
        {
            
            switch (stateType)
            {
                case (StateType.Ally):

                     LostState();
                    attackCanvasButtonPanelIndex = 3;
                    
                    break;
                case (StateType.Enemy):
                    
                    attackCanvasButtonPanelIndex = 2;
                    break;
                case (StateType.Neutral):
                    RelaseState();
                    attackCanvasButtonPanelIndex = 2;
                    break;
            }

            
            

        }
        else
        {
            attackCanvasButtonPanelIndex = 1;

        }

       

    }
    void ChangeResourceAfterOccupy()
    {
        resourceData.Clear();
        Population = 7500;

        Population += Random.Range(-500, 1001);

        // ResourceData s�zl���n� olu�tur ve mevcut mineCount de�erine rastgele ekle
        resourceData = new Dictionary<ResourceType, ResourceData>
        {
            { ResourceType.Gold, new ResourceData {resourceType=ResourceType.Gold, currentAmount = 1500, mineCount = 10 + Random.Range(-1, 3), productionRate = GoldproductionRate }},
            { ResourceType.Water, new ResourceData {resourceType=ResourceType.Water, currentAmount = 40000, mineCount = 11 + Random.Range(-1, 3), productionRate = WaterproductionRate, consumptionAmount = WaterConsumptionRate }},
            { ResourceType.Salt, new ResourceData {resourceType=ResourceType.Salt, currentAmount = 2000, mineCount = 15 + Random.Range(-1, 3), productionRate = SaltproductionRate, consumptionAmount = SaltConsumptionRate }},
            { ResourceType.Meat, new ResourceData {resourceType=ResourceType.Meat, currentAmount = 3000, mineCount = 12 + Random.Range(-1, 3), productionRate = MeatproductionRate, consumptionAmount = MeatConsumptionRate }},
            { ResourceType.Fruits, new ResourceData {resourceType=ResourceType.Fruits, currentAmount = 2500, mineCount = 12 + Random.Range(-1, 3), productionRate = FruitsproductionRate, consumptionAmount = FruitConsumptionRate }},
            { ResourceType.Vegetables, new ResourceData {resourceType=ResourceType.Vegetables, currentAmount = 2400, mineCount = 11 + Random.Range(-1, 3), productionRate = VegetablesproductionRate, consumptionAmount = VegetablesConsumptionRate }},
            { ResourceType.Wheat, new ResourceData {resourceType=ResourceType.Wheat, currentAmount = 2500, mineCount = 21 + Random.Range(-1, 3), productionRate = WheatproductionRate, consumptionAmount = WheatConsumptionRate }},
            { ResourceType.Wood, new ResourceData {resourceType=ResourceType.Wood, currentAmount = 2600, mineCount = 11 + Random.Range(-1, 3), productionRate = WoodproductionRate }},
            { ResourceType.Coal, new ResourceData {resourceType=ResourceType.Coal, currentAmount = 2800, mineCount = 10 + Random.Range(-1, 3), productionRate = CoalproductionRate }},
            { ResourceType.Iron, new ResourceData {resourceType=ResourceType.Iron, currentAmount = 3000, mineCount = 10 + Random.Range(-1, 3), productionRate = IronproductionRate }},
            { ResourceType.Stone, new ResourceData {resourceType=ResourceType.Stone, currentAmount = 2500, mineCount = 10 + Random.Range(-1, 3), productionRate = StoneproductionRate }},
            { ResourceType.Diamond, new ResourceData {resourceType=ResourceType.Diamond, currentAmount = 0, mineCount = 0 , productionRate = DimondproductionRate }}
        };
    }
    public void LostWar(float lossRate)
    {
        loss = lossRate * GetArmySize();
     //   Debug.LogWarning($"loss: {loss} armysize {ArmySize}  name {gameObject.name} loss rate {lossRate}");
        ReduceArmySize(loss);
        
    }
    public  void OccupyState()
    {
        if (stateType == StateType.Enemy)
        {
            
            //Debug.LogWarning("state i�gal edildi");
            LandArmySize = firstArmySize;
            NavalArmySize = firstArmySize;
            ArmyBarrackSize= firstArmySize*2;
            stateType = StateType.Ally;
            ChangeCollor.Instance.ChangeGameobjectColor(gameObject, stateType);
            FindISelectibleComponentAndDisable();

            AllyState allyState = gameObject.GetComponent<AllyState>();
            if (allyState != null)
            {
            //    Debug.LogWarning($"ally stete {allyState.name} bulundu ve eneblesi actif edildi");
                allyState.enabled = true;
                ChangeResourceAfterOccupy();
                TotalPopulationManager(Population);
            }
            else
            {
             //   Debug.LogWarning("ally state bulunamad� ");
                gameObject.AddComponent<AllyState>();
            }
            Morele = 50;
            SubsucribeAction();
            MissionsManager.AddTotalAnnex();
        }
        else
            Debug.LogWarning("elege�irmeye �al��t���n satte enmey de�il ");
        HandleAttackStopped();
        
    }
  
    public void LostState()
    {
     
        GeneralManager.Instance.RemoveGeneralFromState(this);
        AllyStateList.Remove(this);
        GameManager.Instance.onAllyStateChanged?.Invoke(this, false);

        LandArmySize = Population/4;
        NavalArmySize = Population / 4;
        stateType = StateType.Enemy;
        ChangeCollor.Instance.ChangeGameobjectColor(gameObject, stateType);

        FindISelectibleComponentAndDisable();

       EnemyState enemyState= gameObject.GetComponent<EnemyState>();
        if(enemyState != null)
        {
            TotalPopulationManager(-1 * Population);
            enemyState.enabled = true;
            DeSubcucribeAction();
        }
        else
        {
            Debug.LogWarning("enmey state bulunamad� ");
            gameObject.AddComponent<EnemyState>();
        }
        HandleAttackStopped();
        if( IsCapitalCity)
        {
            transform.GetComponentInChildren<Flag>().capitalFlag.SetActive(false);
            IsCapitalCity = false;
            GameManager.Instance.ChangeCapitalCity();
        }
        
    }
  void RelaseState()
    {
      //  Debug.LogWarning("state �zg�rleitirildi edildi");
        ArmySize = firstArmySize;
        stateType = StateType.Neutral;
        ChangeCollor.Instance.ChangeGameobjectColor(gameObject, stateType);


        FindISelectibleComponentAndDisable();
        gameObject.AddComponent<NaturalState>();
        NaturalState naturalState = gameObject.GetComponent<NaturalState>();
        if (naturalState != null)
        {
            naturalState.enabled = true;
        }
        else
        {
            Debug.LogWarning("natural state bulunamad� ");
            gameObject.AddComponent<NaturalState>();
        }
        HandleAttackStopped();
    }
    private void FindISelectibleComponentAndDisable()
    {
        // ISelectable aray�z�n� implemente eden t�m bile�enleri bul
        MonoBehaviour[] components = gameObject.GetComponents<MonoBehaviour>();

        foreach (var component in components)
        {
            if (component is ISelectable)
            {
                
                component.enabled = false;

            }
        }
    }
    public Dictionary<ResourceType, float>  PlunderResource()
    {
        plunderedResources = new Dictionary<ResourceType, float>();
    plunderedResources.Clear();


       
        ResourceType resourceType = (ResourceType)Resources;

        //Debug.LogWarning($"g�ncel alt�n ilk  durmu plunderliste uzunlu�u {plunderedResources.Count} {resourceData[ResourceType.Gold].currentAmount} " + name);
        float gold = GetGoldResValue();
        gold = gold > 5000 ? 5000 : gold;
        plunderedResources.Add(ResourceType.Gold, gold);
           
            plunderedResources.Add(resourceType, resourceData[resourceType].currentAmount/4);
          
        //   Debug.LogWarning($"g�ncel alt�n durmu {resourceData[ResourceType.Gold].currentAmount} "+ name);
       

        // E�er ba�ka kaynaklar da ya�malanacaksa buraya ekleyebilirsiniz

        return plunderedResources;
    }
    public Dictionary<ResourceType, float> GetPlundData()
    {
        return plunderedResources;
    }

public void GoldSpend(int value)
    {
       
        resourceData[ResourceType.Gold].currentAmount -= value;

    }
public void GemSpend(int value)
    {
        
        resourceData[ResourceType.Diamond].currentAmount -= value;
    }
    
    public void AddResource(Dictionary<ResourceType, float> plunderedResources)
{
        
    foreach (var resource in plunderedResources)
    {
        if (resourceData.ContainsKey(resource.Key))
        {
                if(resource.Key== ResourceType.Gold && resource.Value>0)
                {
                    foreach (var item1 in Taxes)
                    {
                        if (item1.taxType == TaxType.DirectTax)// victory 
                        {
                            float tax = (resource.Value / 100) * item1.currentRate;
                            Debug.LogWarning("plunder tax: "+tax);
                            int moraleAddedValue = ((int)( item1.toleranceLimit-item1.currentRate));
                            if ( moraleAddedValue<0)
                            {
                                if (Random.Range(0, 6) > 4)
                                    MessageManager.AddMessage($"Benedict Arnold, Our soldiers fought fiercely, risking life and limb, to seize that loot�and now you take a 5% tax from it?" +
                                    $" This is a slap in the face to those who paid with blood for our victories. We deserve better! Morale has dropped by {moraleAddedValue} points.");
                                SetMorale(-moraleAddedValue);
                            }
                            item1.taxIncome = tax;
                            ResourceManager.Instance.ChargeTax(ResourceType.Gold, tax);
                            resourceData[ResourceType.Gold].currentAmount -= tax;
                                
                        }
                    }
                    
                }
                
                resourceData[resource.Key].currentAmount += resource.Value;
        }
        else
        {
                Debug.LogWarning("u try new resoruce type  ");
            }
    }
}

    public void RemoveResource(Dictionary<ResourceType, float> plunderedResources)
    {

        foreach (var resource in plunderedResources)
        {
            if (resourceData.ContainsKey(resource.Key))
            {
               

                resourceData[resource.Key].currentAmount = 0;
            }
            else
            {
                Debug.LogWarning("u try new resoruce type  ");
            }
        }
    }
    public void SellResource(ResourceType resType, float quantity, float earing, bool isAllyState=false)
    {
        
            if (resourceData.ContainsKey(resType))
            {
            tradeLists[1].limit[(int)resType - 1] -= quantity;

          //  Debug.LogWarning(" limit: " + exportTrade.limit[(int)resType - 1]);
           // Debug.Log($"{gameObject.name} {resType} kayna�� {quantity} �nceki miktar {resourceData[resType].currentAmount}");
            // E�er kaynak zaten mevcutsa, miktar� g�ncelleyebilirsiniz
            resourceData[resType].currentAmount -= quantity;
          //  Debug.Log($"{gameObject.name} {resType} kayna�� {quantity} sonraki  miktar {resourceData[resType].currentAmount}");
            foreach (var item in Taxes)
            {
                if(item.taxType== TaxType.ValueAddedTax && !isAllyState)
                {
                    int moralAddedValue = ((int)(item.toleranceLimit - item.currentRate));
                    if( moralAddedValue<0)
                    {
                        SetMorale(-moralAddedValue);
                        if (Random.Range(0, 6) > 4)
                            MessageManager.AddMessage($"Samuel Cohen: We are the hardworking souls who toil from dawn until dusk, facing every risk just to make a living in this land. Yet," +
                            $" we are being suffocated by these heavy taxes! The government takes a significant portion of our earnings; it is nothing but exploitation of our labor! We can no longer turn a profit; we have to work harder each day just to survive. " +
                            $"This burden being placed upon us not only diminishes our morale but also insults the sacrifices we made for our independence. Our morale has dropped by {moralAddedValue} points because of this!");
                    }
                    float tax = (earing / 100) * item.currentRate;
                    ResourceManager.Instance.ChargeTax(ResourceType.Gold, tax);
                    earing -= tax;
                }
            }
            resourceData[ResourceType.Gold].currentAmount += earing;
            }
            else
            {
                Debug.LogWarning("u try new resoruce type  ");
            }
       


    }
    public void BuyyResource(ResourceType resType, float quantity, float spending, float deliveryTime = 0)
    {
       // Debug.LogWarning("buy " + gameObject.name);
        if (resourceData.ContainsKey(resType))
        {

            resourceData[ResourceType.Gold].currentAmount -= spending;
            resourceData[resType].currentAmount += quantity;
            EventManager.Instance.ProductReceived();
            //   Debug.Log($"Coroutine ba�lat�l�yor: {resType}, Miktar: {quantity}, Teslimat S�resi: {deliveryTime}");
            //StartCoroutine(BuyResource(resType, quantity, deliveryTime));
             //   Debug.Log($"Coroutine ba�lat�l�yor: {resType}, Miktar: {quantity}, Teslimat S�resi: {deliveryTime}");

        }
        else
        {
            Debug.LogWarning("Yeni bir kaynak t�r� denediniz.");
        }
    }
    // oldddd
    IEnumerator BuyResource(ResourceType resType, float quantity, float deliveryTime = 0)
    {
        //Debug.Log($"Coroutine ba�lad�: {resType}, Teslimat S�resi: {deliveryTime}");
        yield return new WaitForSeconds(deliveryTime);
        //Debug.LogWarning($"{resType} �r�n� {this.name}'a ula�t�");
        resourceData[resType].currentAmount += quantity;
        EventManager.Instance.ProductReceived();
        //Debug.Log($"Coroutine bitti: {resType}");
    }
   

    public void InstantlyResource(ResourceType resType, float quantity, float spending)
    {

        if (resourceData.ContainsKey(resType))
        {

            resourceData[resType].currentAmount += quantity;
            resourceData[ResourceType.Diamond].currentAmount -= spending;
        }
        else
        {
            Debug.LogWarning("u try new resoruce type  ");
        }

    }
    public Trade GetTrade( int  index, ResourceType currentResType)
    {
       // Debug.LogWarning($"state name {gameObject.name} exporttrade value {exportTrade.resourceTypes[0]} {exportTrade.resourceTypes[1]} ");
        if (index == 0 )
        {
            
                if (tradeLists[0].limit[(int)currentResType - 1] > 0)
                {
                    return tradeLists[0];
                }
             
        }
        else
        {
            if (index == 1)
            {
                
                    if (tradeLists[1].limit[(int)currentResType-1] > 0)
                    {
                      // Debug.LogWarning($"e�le�me  tamam  {currentResType}  limit {exportTrade.limit[(int)currentResType - 1]}");

                        return tradeLists[1];
                    }
                
            }

        }
       // Debug.Log("wxport ya da import type e�le�mesi olmad� ******************"+ index);
        return null;
    }
    
}
public enum StateType
{
    Ally,
    Enemy,
    Neutral
}

public enum TaxType
{
    IncomeTax,
    ValueAddedTax,
    DirectTax, //  victory
    StampTax
}


[Serializable]
public class TaxData
{
    public TaxType taxType;
    public float currentRate; // Mevcut vergi oran�
    public float toleranceLimit; // Tolerans e�i�i
    public float taxIncome; // Bu verginin getirdi�i gelir
    public float unitTaxIncome;
}
public class ArmyData
{
    public int BarrrackSize;
    public int NavalArmySize;
    public int LandArmySize;
   // public General general;
    public float unitNavalArmyPower;
    public float unitLandArmyPower;
    public int armyMorale;
    public int totalArmyPower;

    
}