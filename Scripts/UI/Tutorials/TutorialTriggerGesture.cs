using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TutorialTriggerGesture : MonoBehaviour
{
    TutorialMessages tutorialMessages;
    //PlayerState playerState;
    //AnimationBehaviour animationBehaviour;

    void Awake()
    {
        tutorialMessages = FindAnyObjectByType<TutorialMessages>();
        //playerState = FindAnyObjectByType<PlayerState>();
        //animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
    }

    private void OnEnable()
    {
        //animationBehaviour.AnimationStarted += Pm
        //tutorialMessages.TutorialMessageCleared += OnTutorialMessageCleared;
    }

    private void OnDisable()
    {
        //tutorialMessages.TutorialMessageCleared += OnTutorialMessageCleared;
    }


  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialMessages.SetPromptActiveIfInactive(TutorialMessages.Prompt.gesture);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tutorialMessages.SetPromptInactiveIfActive(TutorialMessages.Prompt.gesture);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        if (playerState.IsInState(PlayerState.State.ground))
    //        {
    //            tutorialMessages.SetPromptActiveIfInactive(TutorialMessages.Prompt.gesture);
    //        }
    //        else
    //        {
    //            tutorialMessages.SetPromptInactiveIfActive(TutorialMessages.Prompt.gesture);
    //        }
    //    }
    //}



    //void OnAnimationStarted(AnimationBehaviour.Animation animation)
    //{
    //    if (animation == AnimationBehaviour.Animation.Gesture)
    //    {

    //    }
    //}


    //void OnTutorialMessageCleared(TutorialMessages.Prompt prompt)
    //{
    //    if (prompt == TutorialMessages.Prompt.gesture)
    //    {
    //        PlayerState playerState = FindAnyObjectByType<PlayerState>();
    //        if (playerState.IsInState(PlayerState.State.gesturing) == false) 
    //        {
    //            tutorialMessages.SetPromptActive (TutorialMessages.Prompt.gesture);
    //        } 
    //    }
    //}
}
