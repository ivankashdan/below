using UnityEngine;

public class LeafletTriggers : MonoBehaviour
{
    public Collider leafletCollider;

    //GestureState gestureState;
    AnimationBehaviour animationBehaviour;

    TutorialMessages tutorialMessages;
    bool activated;

    private void Awake()
    {
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        //gestureState = FindAnyObjectByType<GestureState>();
        tutorialMessages = FindAnyObjectByType<TutorialMessages>();

        leafletCollider.enabled = false;
    }

    private void OnEnable()
    {
        //gestureState.GestureStarted += OnGestureStarted;
        //animationBehaviour.AnimationStarted += OnAnimationStarted;
             
        tutorialMessages.TutorialMessageCleared += OnTutorialMessageCleared;
    }

    private void OnDisable()
    {
        //gestureState.GestureStarted -= OnGestureStarted;
        //animationBehaviour.AnimationStarted -= OnAnimationStarted;
        tutorialMessages.TutorialMessageCleared -= OnTutorialMessageCleared;
    }

    void OnTutorialMessageCleared(TutorialMessages.Prompt prompt)
    {
        if (!activated && prompt == TutorialMessages.Prompt.gesture)
        {
            if (TryGetComponent<BestiaryEntry>(out var entry))
            {
                BestiaryManager.AddEntry(entry.reference);
                leafletCollider.enabled = true;
                activated = true;
            }
        }
    }

    //void OnAnimationStarted(AnimationBehaviour.Animation animation)
    //{
    //    if (!activated)
    //    {
    //        if (animation == AnimationBehaviour.Animation.Gesture)
    //        {
    //            if (tutorialMessages.IsPromptListening(TutorialMessages.Prompt.gesture))
    //            {
    //                if (TryGetComponent<BestiaryEntry>(out var entry))
    //                {
    //                    BestiaryManager.AddEntry(entry.reference);
    //                    leafletCollider.enabled = true;
    //                    activated = true;
    //                }
    //            }
    //        }
    //    }
    //}
    //void OnGestureStarted()
    //{
    //    if (tutorialMessages.IsPromptListening(TutorialMessages.Prompt.gesture))
    //    {
    //        if (TryGetComponent<BestiaryEntry>(out var entry))
    //        {
    //            BestiaryManager.AddEntry(entry.reference);
    //            leafletCollider.enabled = true;
    //            activated = true;
    //        }
    //    }
    //}



    //void ColliderRefresh()
    //{
    //    leafletCollider.enabled = false;
    //    leafletCollider.enabled = true;
    //}

}
