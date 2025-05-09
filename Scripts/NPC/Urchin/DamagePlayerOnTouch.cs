using NewFeatures;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DamagePlayerOnTouch : MonoBehaviour
{
    Locomotion playerLocomotion;
    PlayerState playerState;

    float launchPower = 1.5f;
    float upwardMultiplier = 0.1f;

    bool coolingDown;
    float coolDown = 0.2f;
    float timer;

    private void Awake()
    {
        playerLocomotion = FindAnyObjectByType<Locomotion>();
        playerState = FindAnyObjectByType<PlayerState>();
    }

    private void Update()
    {
        if (coolingDown)
        {
            timer += Time.deltaTime;
            if (timer > coolDown)
            {
                timer = 0;
                coolingDown = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && coolingDown == false)
        {
            playerState.SetState(PlayerState.State.falling);

            Vector3 directionToPlayer = other.transform.position - transform.position;
            Vector3 directionNoY = new Vector3(directionToPlayer.x, 0, directionToPlayer.z);
            Vector3 launchDirection = directionNoY.normalized + Vector3.up * upwardMultiplier; // Add upward component

            playerLocomotion.SetMomentum(launchDirection * launchPower);

            coolingDown = true;
            Debug.Log("Urchin collided with player");

        }
    }

    private void OnTriggerEnter(Collider other)
    {

        
    }

    

}
