using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;

public class RegionClickHandler : MonoBehaviour
{
    public Camera mainCamera; // Ana kamerayý referans olarak alacaðýz
    public float moveAmount = 0.33f;    // Y ekseninde hareket edilecek mesafe
    public float moveDuration = 0.5f; // Hareket süresi
    public List<GameObject> neighborStates = new List<GameObject>();

    // Eski renkleri saklamak için bir dictionary (hashmap) oluþturun
    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();
    // Tween nesnelerini saklamak için bir Dictionary oluþturun
    private Dictionary<GameObject, Tween> moveTweens = new Dictionary<GameObject, Tween>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tuþuna basýldýðýnda
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Hit edilen objeyi al
                GameObject hitObject = hit.collider.gameObject;

                if (GameManager.Instance.IsAttackFinish)
                {
                    // Eðer obje bir bölge ise bilgilerini göster
                    State state = hitObject.GetComponent<State>();
                    if (state != null && RegionManager.instance != null && !GameManager.Instance.ÝsAttack && !GameManager.Instance.IsRegionPanelOpen)
                    {
                        Debug.Log("Bölge paneli açýldý: " + hitObject.name);

                        // Eski rengi kaydet
                        if (!originalColors.ContainsKey(hitObject))
                        {
                            originalColors[hitObject] = hitObject.GetComponent<Renderer>().material.color;
                        }

                        // Rengi yeþil yap
                        Color oldGloryBlue;
                        UnityEngine.ColorUtility.TryParseHtmlString("#002147", out oldGloryBlue);
                        hitObject.GetComponent<Renderer>().material.color = Color.green;

                        // Komþu bölgeleri bul ve iþle
                        foreach (string neighborState in Neighbor.Instance.GetNeighbors(hitObject.name))
                        {
                            GameObject neighborStateGameobject = GameObject.Find(neighborState);
                            if (neighborStateGameobject != null)
                            {
                                // Eski rengi kaydet
                                if (!originalColors.ContainsKey(neighborStateGameobject))
                                {
                                    originalColors[neighborStateGameobject] = neighborStateGameobject.GetComponent<Renderer>().material.color;
                                }

                                if (neighborStateGameobject.GetComponent<State>().stateType == StateType.Ally)
                                {
                                    neighborStates.Add(neighborStateGameobject);
                                }
                                else
                                {
                                    // Eski animasyonlarý temizle
                                    if (moveTweens.ContainsKey(neighborStateGameobject))
                                    {
                                        moveTweens[neighborStateGameobject].Kill(); // Önceki Tween'i öldür
                                    }

                                    // Hareket ve renk deðiþimi
                                    Tween moveTween = neighborStateGameobject.transform.DOMoveY(neighborStateGameobject.transform.position.y + moveAmount, moveDuration)
                                        .SetLoops(-1, LoopType.Yoyo)  // Sonsuz döngü (Yoyo hareketi)
                                        .SetEase(Ease.InOutQuad);     // Ease türü (Kolaylaþtýrma)

                                    // Tween nesnesini saklayýn
                                    moveTweens[neighborStateGameobject] = moveTween;

                                    neighborStateGameobject.GetComponent<Renderer>().material.color = Color.grey;
                                    neighborStates.Add(neighborStateGameobject);
                                }
                            }
                        }

                        RegionManager.instance.ShowRegionInfo(hitObject.name);
                        GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();
                        GameManager.Instance.UpdateStatePanel(hitObject.GetComponent<State>());
                    }
                    else if (state != null && RegionManager.instance != null && GameManager.Instance.ÝsAttack)
                    {
                        Debug.Log("Savaþýlacak bölge seçildi: " + hitObject.name);
                        Attack.Instance.Attacking(hitObject.name);

                        // Eski rengi geri yükle
                        FinishAttack();
                    }
                }
            }
        }
    }

    public void FinishAttack()
    {
        GameManager.Instance.ChangeIsAttackValueFalse();
        GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
        RestoreOriginalColors();
        StopAnimations();
    }

    public void StopAnimations()
    {
        foreach (var kvp in moveTweens)
        {
            kvp.Value.Kill(); // Animasyonu durdur ve kaldýr
        }
        moveTweens.Clear(); // Tüm Tween nesnelerini temizle
    }

    // Eski renkleri geri yüklemek için bir fonksiyon
    public void RestoreOriginalColors()
    {
        foreach (var kvp in originalColors)
        {
            kvp.Key.GetComponent<Renderer>().material.color = kvp.Value;
        }
        originalColors.Clear(); // Tüm renkleri geri yükledikten sonra dictionary'i temizle
    }
}
