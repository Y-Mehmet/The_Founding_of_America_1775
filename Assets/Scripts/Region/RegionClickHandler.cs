using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;

public class RegionClickHandler : MonoBehaviour
{
    public static RegionClickHandler Instance {  get; private set; }

    public Camera mainCamera;

    public List<GameObject> neighborStates = new List<GameObject>();

    // Eski renkleri saklamak i�in bir dictionary (hashmap) olu�turun
    public  Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();
    // Tween nesnelerini saklamak i�in bir Dictionary olu�turun
    public  Dictionary<GameObject, Tween> moveTweens = new Dictionary<GameObject, Tween>();
    private GameObject currentState;

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
                    if (state != null && RegionManager.instance != null && !GameManager.Instance.�sAttack && !GameManager.Instance.IsRegionPanelOpen)
                    {
                        ISelectable selectable = hitObject.GetComponent<ISelectable>();
                        if (selectable != null)
                        {
                            currentState = hitObject;
                         Debug.Log("B�lge paneli a��ld�: " + hitObject.name);
                        selectable.SellectState();
                        }
                        

                     
                    }
                    else if (state != null && RegionManager.instance != null && GameManager.Instance.�sAttack)
                    {
                        
                        ISelectable selectable = hitObject.GetComponent<ISelectable>();
                        if (selectable != null)
                        {
                          
                            selectable.Attack2();
                        }
                    }
                }
            }
        }
    }
    public void CloseBtn_CloseAll()
    {
        ISelectable selectable = currentState.GetComponent<ISelectable>();
        if(selectable != null && ((MonoBehaviour)selectable).enabled)
        {
            selectable.CloseAll();
        }
        else
        {
            Debug.LogWarning($"{currentState} selecteble i�ermiyor ");
        }
    }

}
