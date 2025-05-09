using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteInteraction : MonoBehaviour
{
    public GameObject gfx;
    public Animator animator;

    AnemoneBehaviour anemoneBehaviour;
    EelBehaviour thisEelBehaviour;
    ShellCheck shellCheck;
    //HidePhysics hidePhysics;

    Coroutine biteCoroutine;

    float biteResetTime = 2f;

    private void Start()
    {
        shellCheck = FindAnyObjectByType<ShellCheck>();
        anemoneBehaviour = FindAnyObjectByType<AnemoneBehaviour>();
        thisEelBehaviour = GetComponentInParent<EelBehaviour>();
    }

    private void OnTriggerStay(Collider other) //update so it looks at shell instead, activating anemone if present, breaking it if none
    {
        if (other.transform.tag.Contains("Player"))
            //&& FindAnyObjectByType<ScaleControl>().GetStage() <= 5) //need to update this so it checks shell if the eel remembers where player was
        {
            if (biteCoroutine == null)
            {
                biteCoroutine = StartCoroutine(ResetBite());

                //bool stunned = false;

                //if (other.transform.tag == "Shell") Debug.Log("registered shell"); //
                if (shellCheck.IsAShellEquipped())
                {
                    //if (anemoneBehaviour.IsThereAnemoneOfCharge(true))
                    //{
                        anemoneBehaviour.ReleaseCharge();
                        thisEelBehaviour.Stunned();
                        //stunned = true;
                    //}
                }

                //if (!hidePhysics.IsHidden())
                //{
                //    if (!stunned)
                //    {
                //        shellBehaviour.TryDamagePlayer();

                //        if (thisEelBehaviour.IsEelStunned() == false)
                //        {
                //            animator.ResetTrigger("bite");
                //            animator.SetTrigger("bite");
                //        }
                //    }
                //}
            }
        }
    }

    IEnumerator ResetBite()
    {
        yield return new WaitForSeconds(biteResetTime);
        biteCoroutine = null;
    }

}
