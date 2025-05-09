using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

[RequireComponent(typeof(NpcStateMachine))]
[RequireComponent(typeof(NpcSight))]
[RequireComponent(typeof(NpcSpeed))]
[RequireComponent(typeof(NpcGizmo))]
public abstract class NpcBase : MonoBehaviour
{
    protected NavMeshAgent agent;

    protected NpcStateMachine stateMachine;
    protected NpcSight sight;
    protected NpcSpeed speed;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        stateMachine = GetComponent<NpcStateMachine>();
        sight = GetComponent<NpcSight>();
        speed = GetComponent<NpcSpeed>(); 

    }
    
  

}