using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;
using System.Linq;

public class RegionClickHandler : MonoBehaviour
{
    public static RegionClickHandler Instance {  get; private set; }
    public Camera mainCamera;
    public List<GameObject> neighborStates = new List<GameObject>();
    // Eski renkleri saklamak için bir dictionary (hashmap) oluþturun
    public  Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();
    // Tween nesnelerini saklamak için bir Dictionary oluþturun
    public  Dictionary<GameObject, Tween> moveTweens = new Dictionary<GameObject, Tween>();
    public  GameObject currentState;
    public static State staticState;
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
        if (Input.GetMouseButtonDown(0)) // Sol fare tuþuna basýldýðýnda
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Hit edilen objeyi al
                GameObject hitObject   = hit.collider.gameObject;

                if (GameManager.Instance.IsAttackFinish)
                {
                    // Eðer obje bir bölge ise bilgilerini göster
                    State state = hitObject.GetComponent<State>();
                    if (state != null  && !GameManager.Instance.ÝsAttack && !GameManager.Instance.IsRegionPanelOpen)
                    {
                        ISelectable selectable = hitObject.GetComponents<MonoBehaviour>()
                                           .OfType<ISelectable>()
                                           .FirstOrDefault(selectable => ((MonoBehaviour)selectable).enabled);
                       
                        if (selectable != null)
                        {
                            currentState = hitObject;
                            staticState= hitObject.GetComponent<State>();
                        // Debug.Log("Bölge paneli açýldý: " + hitObject.name);
                        selectable.SellectState();
                        }
                        else
                        {
                            Debug.LogWarning(" hayda seleced bulunamadý");
                        }
                        

                     
                    }
                    else if (state !=null && GameManager.Instance.ÝsAttack)
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
                            Debug.LogWarning("selectible bulunamadý");
                        }
                    }
                }
            }
        }
    }
    public void CloseBtn_CloseAll()
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
            Debug.LogWarning($"{currentState.name} içerisinde aktif bir ISelectable bulunamadý.");
        }


    }

}
