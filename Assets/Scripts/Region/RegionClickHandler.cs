using UnityEngine;

public class RegionClickHandler : MonoBehaviour
{
    public RegionManager regionManager;

    void OnMouseDown()
    {
        regionManager.ShowRegionInfo(gameObject.name);
    }
}
