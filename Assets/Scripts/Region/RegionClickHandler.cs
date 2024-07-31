using UnityEngine;

public class RegionClickHandler : MonoBehaviour
{
    //public RegionManager regionManager;

    //void OnMouseDown()
    //{
    //    regionManager.ShowRegionInfo(gameObject.name);
    //}
    public Camera mainCamera; // Ana kamerayý referans olarak alacaðýz

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
                

                if ( GameManager.Instance.IsAttackFinish)
                {
                    // Eðer obje bir bölge ise bilgilerini göster
                    State state = hitObject.GetComponent<State>();
                    if (state != null && RegionManager.instance != null && !GameManager.Instance.ÝsAttack && !GameManager.Instance.IsRegionPanelOpen)
                    {
                        Debug.Log(" bölge paneli açýldý " + hitObject.name);
                        RegionManager.instance.ShowRegionInfo(hitObject.name);
                        GameManager.Instance.ChanngeIsRegionPanelOpenValueTrue();
                    }
                    else if (state != null && RegionManager.instance != null && GameManager.Instance.ÝsAttack)
                    {
                        Debug.Log("savaþýlacak bölge seçildi " + hitObject.name);
                        Attack.Instance.Attacking(hitObject.name);
                        GameManager.Instance.ChangeIsAttackValueFalse();
                        GameManager.Instance.ChanngeIsRegionPanelOpenValueFalse();
                    }
                }
                
            }
        }
    }
}
