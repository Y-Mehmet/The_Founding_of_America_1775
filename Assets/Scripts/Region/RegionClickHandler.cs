using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Linq;
using System;

public class RegionClickHandler : MonoBehaviour
{
    public static RegionClickHandler Instance {  get; private set; }
    public Camera mainCamera;
    public List<GameObject> neighborStates = new List<GameObject>();
    // Eski renkleri saklamak i�in bir dictionary (hashmap) olu�turun
    public  Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();
    // Tween nesnelerini saklamak i�in bir Dictionary olu�turun
    public  Dictionary<GameObject, Tween> moveTweens = new Dictionary<GameObject, Tween>();
    public  GameObject currentState;
    public static State staticState;
    public static State statsState;
    public Action onStatsStateChanged;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tu�una bas�ld���nda
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Hit edilen objeyi al
                GameObject hitObject   = hit.collider.gameObject;

                if (GameManager.Instance.IsAttackFinish)
                {
                    // E�er obje bir b�lge ise bilgilerini g�ster
                    State state = hitObject.GetComponent<State>();
                    if (state != null  && !GameManager.Instance.�sAttack && !GameManager.Instance.IsRegionPanelOpen)
                    {
                        ISelectable selectable = hitObject.GetComponents<MonoBehaviour>()
                                           .OfType<ISelectable>()
                                           .FirstOrDefault(selectable => ((MonoBehaviour)selectable).enabled);
                       
                        if (selectable != null)
                        {
                            currentState = hitObject;
                            staticState= hitObject.GetComponent<State>();
                        // Debug.Log("B�lge paneli a��ld�: " + hitObject.name);
                        selectable.SellectState();
                        }
                        else
                        {
                            Debug.LogWarning(" hayda seleced bulunamad�");
                        }
                        

                     
                    }
                    else if (state !=null && GameManager.Instance.�sAttack)
                    {

                        ISelectable selectable = hitObject.GetComponents<MonoBehaviour>()
                                           .OfType<ISelectable>()
                                           .FirstOrDefault(selectable => ((MonoBehaviour)selectable).enabled);
                        if (selectable != null)
                        {
                          
                            selectable.Attack2();
                        }
                        else
                        {
                            Debug.LogWarning("selectible bulunamad�");
                        }
                    }
                }
            }
        }
    }
    public void CloseBtn_CloseAll()
    {
      if(currentState!= null)
        {
            ISelectable activeSelectable = currentState.GetComponents<MonoBehaviour>()
                                         .OfType<ISelectable>()
                                         .FirstOrDefault(selectable => ((MonoBehaviour)selectable).enabled);

            if (activeSelectable != null)
            {

                activeSelectable.CloseAll();

            }
            else
            {
                Debug.LogWarning($"{currentState.name} i�erisinde aktif bir ISelectable bulunamad�.");
            }
        }


    }

}
