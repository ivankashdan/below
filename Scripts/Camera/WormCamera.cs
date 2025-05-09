using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;


[RequireComponent(typeof(CinemachineCamera))]
public class WormCamera : MonoBehaviour
{
    public bool active;
    
    CinemachineCamera cam;
    PlayerState playerState;

    private void Awake()
    {
        cam = GetComponent<CinemachineCamera>();
        playerState = FindAnyObjectByType<PlayerState>();   
    }

    private void Update()
    {
        if (active)
        { 
            if (playerState.IsInState(PlayerState.State.ground))
            {
                ActivateCam(false);
            }
        }
    }

    public void ActivateCam(bool prioritise)
    {
        cam.Priority = prioritise ? 100 : 0;
        active = prioritise;
    }



    



}
