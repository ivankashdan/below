using UnityEngine;

public class TestAnimationTrigger : MonoBehaviour
{

    Animator animator;
    public string trigger;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetTrigger(trigger);
        }
    }

}
