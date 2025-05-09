using UnityEngine;

public class NearCameraManager : MonoBehaviour
{
    public void DisableNearCameraFade()
    {
        SetCameraBypass(true);
    }

    public void EnableNearCameraFade()
    {
        SetCameraBypass(false);
    }

    void SetCameraBypass(bool value)
    {
        HermieNearCameraFade[] nearCameraFades = FindObjectsByType<HermieNearCameraFade>(FindObjectsSortMode.None);

        foreach (var fade in nearCameraFades)
        {
            fade.bypass = value;
        }
    }
}
