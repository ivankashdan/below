using NUnit.Framework.Constraints;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class ScreenYBehaviour : MonoBehaviour
{
    [Header("Screen Y Transition")]
    public float screenYDuration = 0.5f;
    public AnimationCurve screenYCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    float composerDefault;
    float modifierTopDefault;
    float modifierBottomDefault;

    CameraBehaviourManager cameraManager;
    CinemachineRotationComposer rotationComposer;
    CinemachineFreeLookModifier freeLookModifier;
    CinemachineFreeLookModifier.CompositionModifier compositionModifier;


    private void Awake()
    {
        cameraManager = GetComponentInParent<CameraBehaviourManager>();
        rotationComposer = cameraManager.followCamera.GetComponent<CinemachineRotationComposer>();
        freeLookModifier = cameraManager.followCamera.GetComponent<CinemachineFreeLookModifier>();
        compositionModifier = GetCompositionModifier();

        composerDefault = rotationComposer.Composition.ScreenPosition.y;
        modifierTopDefault = compositionModifier.Composition.Top.ScreenPosition.y;
        modifierBottomDefault = compositionModifier.Composition.Bottom.ScreenPosition.y;
    }

    public void StartScreenYTransition(float composerValue, float modTop, float modBottom)
    {
        StopAllCoroutines();
        StartCoroutine(TransitionScreenYCoroutine(composerValue, modTop, modBottom));
    }

    public void StartScreenYTransition(float composerValue)
    {
        StopAllCoroutines();
        StartCoroutine(TransitionScreenYCoroutine(composerValue, modifierTopDefault, modifierBottomDefault));
    }

    public void StartScreenYTransitionToBase()
    {
        StartScreenYTransition(composerDefault, modifierTopDefault, modifierBottomDefault);
    }

    IEnumerator TransitionScreenYCoroutine(float composerValue, float modTop, float modBottom)
    {
        float elapsedTime = 0f;
        while (elapsedTime < screenYDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = screenYCurve.Evaluate(elapsedTime / screenYDuration);

            float lerpedComposerValue = Mathf.Lerp(rotationComposer.Composition.ScreenPosition.y, composerValue, t);
            rotationComposer.Composition.ScreenPosition.y = lerpedComposerValue;

            float lerpedModTop = Mathf.Lerp(compositionModifier.Composition.Top.ScreenPosition.y, modTop, t);
            compositionModifier.Composition.Top.ScreenPosition.y = lerpedModTop;

            float lerpedModBottom = Mathf.Lerp(compositionModifier.Composition.Bottom.ScreenPosition.y, modBottom, t);
            compositionModifier.Composition.Bottom.ScreenPosition.y = lerpedModBottom;

            yield return null;
        }

        Debug.Log("Screen Y Updated: " + composerValue);
    }

    CinemachineFreeLookModifier.CompositionModifier GetCompositionModifier()
    {
        var modifiers = freeLookModifier.Modifiers;

        foreach (var modifier in modifiers)
        {
            if (modifier is CinemachineFreeLookModifier.CompositionModifier compositionModifier)
            {
                return compositionModifier;
            }
        }
        throw new System.Exception("Composition modifier not found");

    }
    float GetModifierScreenY()
    {
        var modifiers = freeLookModifier.Modifiers;

        foreach (var modifier in modifiers)
        {
            if (modifier is CinemachineFreeLookModifier.CompositionModifier compositionModifier)
            {
                return compositionModifier.Composition.Top.ScreenPosition.y;
            }
        }
        throw new System.Exception("Composition modifier not found");
    }

}
