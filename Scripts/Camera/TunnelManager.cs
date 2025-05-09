using System;
using System.Collections.Generic;
using UnityEngine;

public class TunnelManager : MonoBehaviour
{

    public event Action PlayerEnteredTunnel;
    public event Action PlayerExitedTunnel;

    HashSet<Tunnel> activeTunnels = new HashSet<Tunnel>();

    public bool IsPlayerInTunnel => activeTunnels.Count > 0;

    public void RegisterTunnel(Tunnel tunnel)
    {
        if (activeTunnels.Count == 0)
        {
            PlayerEnteredTunnel?.Invoke();
        }

        activeTunnels.Add(tunnel);
        Debug.Log($"Active tunnel count: {activeTunnels.Count}");
    }

    public void UnregisterTunnel(Tunnel tunnel)
    {
        activeTunnels.Remove(tunnel);

        if (activeTunnels.Count == 0)
        {
            PlayerExitedTunnel?.Invoke();
        }

        Debug.Log($"Active tunnel count: {activeTunnels.Count}");
    }
}
