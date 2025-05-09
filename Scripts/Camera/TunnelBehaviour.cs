using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(TunnelManager))]
public class TunnelBehaviour : MonoBehaviour
{
    public OrbitsReference closeOrbit;
    public OrbitsReference normalOrbit;

    OrbitsBehaviour orbitsBehaviour;
    TunnelManualYRecenter yRecenterBehaviour;
    TunnelManager tunnelManager;
    

    //TunnelOrbits tunnelOrbits;
    //DefaultOrbits defaultOrbits;

    bool bypass = true;

    private void Awake()
    {
        yRecenterBehaviour = FindAnyObjectByType<TunnelManualYRecenter>();
        orbitsBehaviour = FindAnyObjectByType<OrbitsBehaviour>();

        tunnelManager = GetComponent<TunnelManager>();

        //tunnelOrbits = GetComponentInChildren<TunnelOrbits>();
        //defaultOrbits = GetComponentInChildren<DefaultOrbits>();

        StartCoroutine(BypassedStartCoroutine());
    }

    

    IEnumerator BypassedStartCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        bypass = false;
        if (tunnelManager.IsPlayerInTunnel)
        {
            StartTransition(closeOrbit);
        }
        else
        {
            StartTransition(normalOrbit);
        }
    }

    private void OnEnable()
    {
        tunnelManager.PlayerEnteredTunnel += OnPlayerEnteredTunnel;
        tunnelManager.PlayerExitedTunnel += OnPlayerExitedTunnel;
    }

    private void OnDisable()
    {

        tunnelManager.PlayerEnteredTunnel -= OnPlayerEnteredTunnel;
        tunnelManager.PlayerExitedTunnel -= OnPlayerExitedTunnel;
    }

    void OnPlayerEnteredTunnel()
    {
        if (!bypass)
        {
            StartTransition(closeOrbit);
        }
        //orbitsBehaviour.StopOrbitsCoroutine();
        //orbitsBehaviour.StartOrbitTransition(tunnelOrbits.storedOrbits);
        //yRecenterBehaviour.StartYRecenterTransition();
    }
    void OnPlayerExitedTunnel()
    {
        if (!bypass)
        {
            StartTransition(normalOrbit);
        }
        // orbitsBehaviour.StopOrbitsCoroutine();
        //orbitsBehaviour.StartOrbitTransition(defaultOrbits.storedOrbits);
        ////UpdateOrbitsBasedOnScale();
        //yRecenterBehaviour.StartYRecenterTransition();
    }

    void StartTransition(OrbitsReference reference)
    {
        orbitsBehaviour.StartOrbitTransition(reference.storedOrbits);

        orbitsBehaviour.defaultOrbit = reference;

        yRecenterBehaviour.StartYRecenterTransition();
        
        //if (reference == orbitsBehaviour.defaultOrbit)
        //{
        //    Debug.Log("Set Orbits To Default called");
        //}
        //else if (reference == tunnelOrbits) 
        //{
        //    Debug.Log("Set Orbits To Tunnel called");
        //}
    }


    //void SetOrbitsToTunnel()
    //{
    //    //orbitsBehaviour.StartOrbitTransition(Orbit.Top, tunnelTop);
    //    //orbitsBehaviour.StartOrbitTransition(Orbit.Center, tunnelCenter);
    //    //orbitsBehaviour.StartOrbitTransition(Orbit.Bottom, tunnelBottom);
    //}



}
