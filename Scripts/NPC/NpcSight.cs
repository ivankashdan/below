using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class NpcSight : MonoBehaviour
{
    [SerializeField] public float sightRadius = 20f;

    [HideInInspector] public SphereCollider sightCollider;
    [HideInInspector] public Transform playerObject;

    void Awake()
    {
        sightCollider = GetComponent<SphereCollider>();
        playerObject = GameObject.FindWithTag("Player").transform;
    }



    public bool IsPlayerVisible()
    {
        return Vector3.Distance(playerObject.position, transform.position) <= sightRadius;
    }

    public bool IsPlayerWithinDistance(float distance)
    {
        return Vector3.Distance(playerObject.position, transform.position) <= distance;
    }


}
