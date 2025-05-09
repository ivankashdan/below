using UnityEngine;

public class OrbitTrigger : MonoBehaviour
{
    public OrbitsReference targetOrbit;

    OrbitsBehaviour orbitsBehaviour;

    private void Awake()
    {
        orbitsBehaviour = FindAnyObjectByType<OrbitsBehaviour>();
    }

    public void SetDistance()
    {
        orbitsBehaviour.StartOrbitTransition(targetOrbit.storedOrbits);
    }

    public void ReleaseDistance()
    {
        orbitsBehaviour.StartOrbitTransitionToDefault();
    }



}
