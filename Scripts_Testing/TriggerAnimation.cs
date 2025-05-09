using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{

    public Animator animator;
    public string trigger;
    bool triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.gameObject.tag == "Player")
        {
            animator.SetTrigger(trigger);
            //animator.ResetTrigger(trigger);
            triggered = true;

        }
    }



}
