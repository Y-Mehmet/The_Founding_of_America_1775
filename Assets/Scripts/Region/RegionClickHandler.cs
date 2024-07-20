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
                Debug.Log("Hit: " + hitObject.name);

                // E�er obje bir b�lge ise bilgilerini g�ster
                State state = hitObject.GetComponent<State>();
                if (state != null && RegionManager.instance!= null)
                {
                    RegionManager.instance.ShowRegionInfo(hitObject.name);
                }
            }
        }
    }
}
