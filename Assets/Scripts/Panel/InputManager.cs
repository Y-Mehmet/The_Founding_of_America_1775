using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private EventSystem eventSystem;
    private GraphicRaycaster raycaster;

    private void Start()
    {
        eventSystem = EventSystem.current;

        // Canvas üzerinde GraphicRaycaster bileþenini al
        raycaster = GetComponent<GraphicRaycaster>();

        // Null kontrolü ekleyin
        if (eventSystem == null)
        {
            Debug.LogError("EventSystem bileþeni bulunamadý!");
        }

        if (raycaster == null)
        {
            Debug.LogError("GraphicRaycaster bileþeni bulunamadý!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Vector2 touchPosition;

            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
            }
            else
            {
                touchPosition = Input.mousePosition;
            }

            // Null kontrolleri ekleyin
            if (eventSystem == null || raycaster == null)
            {
                return; // Bileþenlerden biri null ise iþlemi yapmayýn
            }

            PointerEventData pointerEventData = new PointerEventData(eventSystem)
            {
                position = touchPosition
            };

            var results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            // Aktif Canvas'daki panel dýþýnda bir yere dokunulduðunu kontrol et
            bool isTouchOnPanel = false;
            foreach (var result in results)
            {
                // Panelin üzerinde bir raycast sonucu olup olmadýðýný kontrol et
                if (result.gameObject.GetComponent<RectTransform>() != null)
                {
                    isTouchOnPanel = true;
                    break;
                }
            }

            // Eðer dokunulan yer herhangi bir panel deðilse, aktif paneli kapat
            if (!isTouchOnPanel)
            {
                PanelManager.Instance.HideLastPanel();
            }
        }
    }
}
