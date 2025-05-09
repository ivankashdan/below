using UnityEngine;

public class CameraCheck : MonoBehaviour
{
    public bool IsTargetOnScreenNotOccluded(GameObject target)
    {
        return IsObjectOnScreen(Camera.main, target) && IsObjectVisible(Camera.main, target);
    }


    bool IsObjectOnScreen(Camera cam, GameObject obj)
    {
        Vector3 viewPos = cam.WorldToViewportPoint(obj.transform.position);
        return viewPos.x >= 0 && viewPos.x <= 1 &&
               viewPos.y >= 0 && viewPos.y <= 1 &&
               viewPos.z > 0;
    }

    bool IsObjectVisible(Camera cam, GameObject obj)
    {
        Vector3 dirToObj = obj.transform.position - cam.transform.position;
        if (Physics.Raycast(cam.transform.position, dirToObj, out RaycastHit hit))
        {
            return hit.transform == obj.transform;
        }
        return false;
    }
}
