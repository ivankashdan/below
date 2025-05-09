using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Animator))]
public class AnimationBehaviour : MonoBehaviour
{
    public event Action<Animation> AnimationStarted;
    public event Action<Animation> AnimationCompleted;

    //public bool hiding;
    //bool lastHidingCheck;


    public float layerTransition = 1f;

    Vector3 lastPosition;
    float speed;

    [HideInInspector] public Animator animator;
    Transform playerObject;
    ScaleControl scaleControl;

    Coroutine armLayerTransitionCoroutine;
    Coroutine tailLayerTransitionCoroutine;

    int lBase = 0;
    int lArms = 1;
    int lTailShell = 2;

    string aWalk = "Walk";
    string aJump = "Jump";
    string aFall = "Fall";
    string aGlide = "Glide";
    string aGesture = "Gesture";
    string aHide = "Hide";
    string aUnhide = "Unhide";
    string aEat = "Eat";

    string fMoveSpeed = "moveSpeed";
    string tGround = "ground";
    string tHop = "hop";
    string tFall = "fall";
    string tGlide = "glide";
    string tGesture = "gesture";
    string tHide = "hide";
    string tUnhide = "unhide";
    string tEat = "eat";

    public enum Animation
    {
        Walk,
        Jump,
        Fall,
        Glide,
        Gesture,
        Hide,
        Unhide,
        Eat
    }

    Dictionary<Animation, string> triggerStringDictionary;
    Dictionary<Animation, string> animationStringDictionary;

    //HashSet<Animation> animationPlayingHashSet = new HashSet<Animation>();

    void InitAnimationDictionary()
    {
        animationStringDictionary = new Dictionary<Animation, string>()
        {
            {Animation.Walk, aWalk},
            {Animation.Jump, aJump},
            {Animation.Fall, aFall },
            {Animation.Glide, aGlide},
            {Animation.Gesture, aGesture},
            {Animation.Hide, aHide },
            {Animation.Unhide, aUnhide },
            {Animation.Eat, aEat }
        };
    }

    void InitTriggerDictionary()
    {
        triggerStringDictionary = new Dictionary<Animation, string>()
        {
            {Animation.Walk, tGround},
            {Animation.Jump, tHop},
            {Animation.Fall, tFall },
            {Animation.Glide, tGlide},
            {Animation.Gesture, tGesture},
            {Animation.Hide, tHide },
            {Animation.Unhide, tUnhide },
            {Animation.Eat, tEat }
        };
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        scaleControl = FindAnyObjectByType<ScaleControl>();
        playerObject = GameObject.FindWithTag("Player").transform;

        InitAnimationDictionary();
        InitTriggerDictionary();


        animator.Rebind();
        animator.Update(0);
    }

    public void DeSyncAnimatorMoveSpeed()
    {
        animator.SetFloat(fMoveSpeed, 0);
    }
    public void SyncAnimatorMoveSpeed()
    {
        float distance = Vector3.Distance(playerObject.position, lastPosition);
        speed = distance / Time.unscaledDeltaTime;
        lastPosition = playerObject.position;
        float gfxScaledSpeed = speed / scaleControl.GetStageGFXScale(); 
        animator.SetFloat(fMoveSpeed, gfxScaledSpeed);
    }

    public void ArmLayerOverride(bool value)
    {
        StartLayerTransitionCoroutine(lArms, ref armLayerTransitionCoroutine, value);
    }

    public void TailShellLayerOverride(bool value)
    {
        StartLayerTransitionCoroutine(lTailShell, ref tailLayerTransitionCoroutine, value);
    }


    public void TriggerAnimation(Animation animation)
    {
        if (triggerStringDictionary.TryGetValue(animation, out string triggerString))
        {
            animator.SetTrigger(triggerString);

            AnimationStarted?.Invoke(animation);

        }
        else throw new System.Exception("trigger string could not be found");
    }

    public void ResetAnimationTrigger(Animation animation)
    {
        if (triggerStringDictionary.TryGetValue(animation, out string triggerString))
        {
            animator.ResetTrigger(triggerString);
        }
        else throw new System.Exception("trigger string could not be found");

    }

    public bool IsAnimationPlaying(Animation animation)
    {
        if (animationStringDictionary.TryGetValue(animation, out string animationString))
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.IsName(animationString);
        }
        else throw new System.Exception("animation string could not be found");
    }

    public bool IsAnimationFinished(Animation animation)
    {
        if (animationStringDictionary.TryGetValue(animation, out string animationString))
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0); // Assuming layer 0
            return stateInfo.IsName(animationString) && stateInfo.normalizedTime >= 1.0f;
        }
        else throw new System.Exception("animation string could not be found");
    }

 

    void StartLayerTransitionCoroutine(int layer, ref Coroutine coroutine, bool value)
    {
        if (coroutine != null) StopCoroutine(coroutine);
        coroutine = StartCoroutine(LayerTransitionCoroutine(layer, value ? 1f : 0f, layerTransition));
    }

    IEnumerator LayerTransitionCoroutine(int layerIndex, float targetWeight, float duration)
    {
        float startWeight = animator.GetLayerWeight(layerIndex);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newWeight = Mathf.Lerp(startWeight, targetWeight, elapsed / duration);
            animator.SetLayerWeight(layerIndex, newWeight);
            yield return null;
        }

        animator.SetLayerWeight(layerIndex, targetWeight);
    }



    //void Update()
    //{
    //    TimelineHidingControl();
    //}



    ////get rid of these last two
    //void TimelineHidingControl()
    //{
    //    if (lastHidingCheck != hiding)
    //    {
    //        TimelineTriggerHiding(hiding);
    //    }
    //    lastHidingCheck = hiding;
    //}


    //void TimelineTriggerHiding(bool hide) //redundant?
    //{
    //    if (hide)
    //    {
    //        animator.SetTrigger("hide");
    //        animator.ResetTrigger("unhide");
    //    }
    //    else
    //    {
    //        animator.SetTrigger("unhide");
    //        animator.ResetTrigger("hide");
    //    }

    //}


    //void OnAnimationStarted(Animation animation)
    //{
    //    animationPlayingHashSet.Add(animation);
    //}

    //void AutoResetTriggers()
    //{
    //    if (animationPlayingHashSet.Count > 0)
    //    {
    //        foreach (var animation in animationPlayingHashSet)
    //        {
    //            if (!IsAnimationPlaying(animation))
    //            {
    //                AnimationCompleted?.Invoke(animation);
    //                ResetAnimationTrigger(animation);
    //                animationPlayingHashSet.Remove(animation);
    //                return;
    //            }
    //        }
    //    }
    //}



    //public void TriggerHideAnimation()
    //{
    //    if (GameState.Instance.IsPlayerInControl() && playerState.IsInState(PlayerState.State.ground) && !IsAnimationPlaying("Hide"))
    //    {
    //        animator.SetTrigger("hide");
    //        animator.ResetTrigger("unhide");
    //    }
    //}

    //public void TriggerUnhideAnimation()
    //{
    //    if (GameState.Instance.IsPlayerInControl() && IsAnimationFinished("Hide"))
    //    {
    //        animator.SetTrigger("unhide");
    //        animator.ResetTrigger("hide");
    //    }
    //}



    //public void TriggerEatAnimation()
    //{
    //    animator.SetTrigger("eat");

    //}







}
