using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class FocalZoom : MonoBehaviour
{
    public float transitionDuration = 0.5f;
    public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    float defaultFOV = 40f;

    CameraBehaviourManager cameraBehaviourManager;
    CinemachineCamera virtualCamera;
    private void Awake()
    {
        cameraBehaviourManager = GetComponentInParent<CameraBehaviourManager>();
        virtualCamera = cameraBehaviourManager.followCamera as CinemachineCamera;
    }

    public void SetFOV(float targetValue)
    {
        StopAllCoroutines();
        StartCoroutine(FocalZoomTransitionCoroutine(targetValue));
    }

    public void SetFOVToDefault()
    {
        StopAllCoroutines();
        StartCoroutine(FocalZoomTransitionCoroutine(defaultFOV));
    }

    IEnumerator FocalZoomTransitionCoroutine(float targetValue)
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = transitionCurve.Evaluate(elapsedTime / transitionDuration);

            virtualCamera.Lens.FieldOfView = Mathf.Lerp(virtualCamera.Lens.FieldOfView, targetValue, t);

            yield return null;
        }

        //Debug.Log("Y recentered");
    }




}
