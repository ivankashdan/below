using UnityEngine;

[RequireComponent (typeof(Collider))]
public class Tunnel : MonoBehaviour
{
    TunnelManager tunnelManager;
    private void Awake()
    {
        tunnelManager = FindAnyObjectByType<TunnelManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            tunnelManager.RegisterTunnel(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            tunnelManager.UnregisterTunnel(this);
        }
    }
}
