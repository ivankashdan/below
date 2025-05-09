using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class TunnelManualYRecenter : MonoBehaviour
{

    [Header("Y Recenter Transition")]
    public float yRecenterDuration = 0.5f;
    public AnimationCurve yRecenterCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    Coroutine yRecenterCoroutine;

    CameraBehaviourManager cameraManager;
    CinemachineOrbitalFollow orbitalFollow;
    private void Awake()
    {
        cameraManager = GetComponentInParent<CameraBehaviourManager>();
        orbitalFollow = cameraManager.followCamera.GetComponentInParent<CinemachineOrbitalFollow>();
    }

    public void StartYRecenterTransition()
    {
        if (yRecenterCoroutine != null) StopCoroutine(yRecenterCoroutine);
        yRecenterCoroutine = StartCoroutine(TransitionYRecenterCoroutine());
    }

    IEnumerator TransitionYRecenterCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < yRecenterDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = yRecenterCurve.Evaluate(elapsedTime / yRecenterDuration);

            orbitalFollow.VerticalAxis.Value = Mathf.Lerp(orbitalFollow.VerticalAxis.Value, orbitalFollow.VerticalAxis.Center, t);

            yield return null;
        }

        Debug.Log("Y recentered");
    }


}
