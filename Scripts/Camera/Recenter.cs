using UnityEngine;

public class Recenter : MonoBehaviour
{
    public RecenterManager.Axis axis = RecenterManager.Axis.xy;

    public Transform target;
    public Vector3 direction;

    public float delayedStart = 0f;
    public float turnSpeed = 0.5f;

    public bool cutoff = true;
    public float cutoffTime = 2f;

    public bool suspendControl = false;



}
