using Unity.Cinemachine;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitsReference : MonoBehaviour
{
    public bool storeOrbits;
    public Cinemachine3OrbitRig.Settings storedOrbits;
    public bool applyStoredOrbits;

    CinemachineOrbitalFollow orbitalFollow;
    CameraBehaviourManager cameraManager;

    private void Awake()
    {
        cameraManager = GetComponentInParent<CameraBehaviourManager>();
        orbitalFollow = cameraManager.followCamera.GetComponent<CinemachineOrbitalFollow>();
    }

    private void Update()
    {
        if (storeOrbits)
        {
            storeOrbits = false;
            StoreOrbitsInDefault();
        }

        if (applyStoredOrbits)
        {
            applyStoredOrbits = false;
            RestoreDefaultOrbits();
        }
    }
    void StoreOrbitsInDefault()
    {
        storedOrbits = orbitalFollow.Orbits;
    }

    void RestoreDefaultOrbits()
    {
        orbitalFollow.Orbits = storedOrbits;
    }
}
