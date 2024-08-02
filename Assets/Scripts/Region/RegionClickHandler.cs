using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections.Generic;

public class RegionClickHandler : MonoBehaviour
{
    public Camera mainCamera; // Ana kameray� referans olarak alaca��z
    public float moveAmount = 0.33f;    // Y ekseninde hareket edilecek mesafe
    public float moveDuration = 0.5f; // Hareket s�resi
    public List<GameObject> neighborStates = new List<GameObject>();

    // Eski renkleri saklamak i�in bir dictionary (hashmap) olu�turun
    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();
    // Tween nesnelerini saklamak i�in bir Dictionary olu�turun
    private Dictionary<GameObject, Tween> moveTweens = new Dictionary<GameObject, Tween>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tu�una bas�ld���nda
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Hit edilen objeyi al
                GameObject hitObject = hit.collider.gameObject;

                if (GameManager.Instance.IsAttackFinish)
                {
                    // E�er obje bir b�lge ise bilgilerini g�ster
                    State state = hitObject.GetComponent<State>();
                    if (state != null && RegionManager.instance != null && !GameManager.Instance.�sAttack && !GameManager.Instance.IsRegionPanelOpen)
                    {
                        Debug.Log("B�lge paneli a��ld�: " + hitObject.name);

                        // Eski rengi kaydet
                        if (!originalColors.ContainsKey(hitObject))
                        {
                            originalColors[hitObject] = hitObject.GetComponent<Renderer>().material.color;
                        }

                        // Rengi ye�il yap
                        Color oldGloryBlue;
                        UnityEngine.ColorUtility.TryParseHtmlString("#002147", out oldGloryBlue);
                        hitObject.GetComponent<Renderer>().material.color = Color.green;

                        // Kom�u b�lgeleri bul ve i�le
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
                                    // Eski animasyonlar� temizle
                                    if (moveTweens.ContainsKey(neighborStateGameobject))
                                    {
                                        moveTweens[neighborStateGameobject].Kill(); // �nceki Tween'i �ld�r
                                    }

                                    // Hareket ve renk de�i�imi
                                    Tween moveTween = neighborStateGameobject.transform.DOMoveY(neighborStateGameobject.transform.position.y + moveAmount, moveDuration)
                                        .SetLoops(-1, LoopType.Yoyo)  // Sonsuz d�ng� (Yoyo hareketi)
                                        .SetEase(Ease.InOutQuad);     // Ease t�r� (Kolayla�t�rma)

                                    // Tween nesnesini saklay�n
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
                    else if (state != null && RegionManager.instance != null && GameManager.Instance.�sAttack)
                    {
                        Debug.Log("Sava��lacak b�lge se�ildi: " + hitObject.name);
                        Attack.Instance.Attacking(hitObject.name);

                        // Eski rengi geri y�kle
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
            kvp.Value.Kill(); // Animasyonu durdur ve kald�r
        }
        moveTweens.Clear(); // T�m Tween nesnelerini temizle
    }

    // Eski renkleri geri y�klemek i�in bir fonksiyon
    public void RestoreOriginalColors()
    {
        foreach (var kvp in originalColors)
        {
            kvp.Key.GetComponent<Renderer>().material.color = kvp.Value;
        }
        originalColors.Clear(); // T�m renkleri geri y�kledikten sonra dictionary'i temizle
    }
}
