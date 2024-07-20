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
                Debug.Log("Hit: " + hitObject.name);

                // Eðer obje bir bölge ise bilgilerini göster
                State state = hitObject.GetComponent<State>();
                if (state != null && RegionManager.instance!= null)
                {
                    RegionManager.instance.ShowRegionInfo(hitObject.name);
                }
            }
        }
    }
}
