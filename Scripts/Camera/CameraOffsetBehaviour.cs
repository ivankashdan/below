using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraOffsetBehaviour : MonoBehaviour
{

    public float transitionDuration = 0.5f;

    float baseOffsetY;

    CinemachineCameraOffset cameraOffset;

    private void Awake()
    {
        CameraBehaviourManager cameraManager = GetComponentInParent<CameraBehaviourManager>();
        cameraOffset = cameraManager.followCamera.GetComponent<CinemachineCameraOffset>();

        baseOffsetY = cameraOffset.Offset.y;
    }

    public void StartCameraOffsetTransition(float yValue)
    {

        StopAllCoroutines();
        StartCoroutine(TransitionCameraOffsetCoroutine(yValue));
    }

    public void StartCameraOffsetTransitionToBase()
    {
        StartCameraOffsetTransition(baseOffsetY);
    }

    IEnumerator TransitionCameraOffsetCoroutine(float yValue)
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / transitionDuration;
            //float t = screenYCurve.Evaluate(elapsedTime / screenYDuration);

            float lerpedComposerValue = Mathf.Lerp(cameraOffset.Offset.y, yValue, t);
            cameraOffset.Offset.y = lerpedComposerValue;

            yield return null;
        }

        Debug.Log("Camera Y Offset Updated: " + yValue);
    }

}
