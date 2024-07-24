using UnityEngine;

public class RegionClickHandler : MonoBehaviour
{
    //public RegionManager regionManager;

    //void OnMouseDown()
    //{
    //    regionManager.ShowRegionInfo(gameObject.name);
    //}
    public Camera mainCamera; // Ana kameray� referans olarak alaca��z

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
                

                // E�er obje bir b�lge ise bilgilerini g�ster
                State state = hitObject.GetComponent<State>();
                if (state != null && RegionManager.instance!= null && !GameManager.Instance.�sAttack && !GameManager.Instance.IsRegionPanelOpen)
                {
                    Debug.Log(" b�lge paneli a��ld� " + hitObject.name);
                    RegionManager.instance.ShowRegionInfo(hitObject.name);
                    GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();
                }
                else if (state != null && RegionManager.instance != null && GameManager.Instance.�sAttack)
                {
                    Debug.Log("sava��lacak b�lge se�ildi " + hitObject.name);
                    Attack.Instance.Attacking(hitObject.name);
                    GameManager.Instance.ChangeIsAttackValueFalse();
                    GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
                }
            }
        }
    }
}
