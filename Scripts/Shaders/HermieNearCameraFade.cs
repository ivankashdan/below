using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class HermieNearCameraFade : MonoBehaviour
{
    public Material material; 

    public float fadeDistance = 2f;
    public float fadeSpeed = 3f;
    public float fadeValue = 0.08f;
    public float currentDistance = 0.5f;
    float defaultValue = 1f;

    bool visible;

    [HideInInspector] public bool bypass;

    Coroutine fadeCoroutine;

    Transform playerTransform;
    Transform cameraTransform;

    private void Awake()
    {
        PlayerManager playerManager = FindAnyObjectByType<PlayerManager>();
        playerTransform = playerManager.playerObject.transform;
        cameraTransform = Camera.main.transform;

        if (material == null)
        {   
            if (GetComponentInChildren<MeshRenderer>() != null)
            {
                material = GetComponentInChildren<MeshRenderer>().material;
            }
            else if (GetComponentInChildren<SkinnedMeshRenderer>() != null)
            {
                material = GetComponentInChildren<SkinnedMeshRenderer>().material;
            }
            else
            {
                Debug.LogError("No MeshRenderer or SkinnedMeshRenderer found on object: " + gameObject.name);
                return;
            }

        }
        
        defaultValue = material.GetFloat("_Transparency");

        //meshRenderer = GetComponent<SkinnedMeshRenderer>();
    }


    private void Update()
    {
        if (!bypass)
        {
            if (fadeCoroutine == null)
            {
                currentDistance = Vector3.Distance(playerTransform.position, cameraTransform.position);

                //float transparencyValue = 1;

                if (currentDistance < fadeDistance && visible)
                {
                    fadeCoroutine = StartCoroutine(FadeCoroutine(fadeValue));

                    //transparencyValue = Mathf.InverseLerp(fadeMinDistance, fadeStartDistance, distance);
                }
                else if (currentDistance >= fadeDistance && visible == false)
                {
                    fadeCoroutine = StartCoroutine(FadeCoroutine(defaultValue));
                }

            }
        }
        else 
        {
            if (fadeCoroutine != null)
            {
                StopAllCoroutines();
                fadeCoroutine = null;
            }

            if (material.GetFloat("_Transparency") != defaultValue)
            {
                material.SetFloat("_Transparency", defaultValue);
            }
        }
    }


    IEnumerator FadeCoroutine(float targetValue)
    {
        while (material.GetFloat("_Transparency") != targetValue)
        {
            yield return new WaitForEndOfFrame();
            float currentTransparency = material.GetFloat("_Transparency");

            float newValue = Mathf.MoveTowards(currentTransparency, targetValue, fadeSpeed * Time.deltaTime);

            material.SetFloat("_Transparency", newValue);
        }

        if (targetValue == fadeValue) visible = false;
        else visible = true;

        fadeCoroutine = null;
    }

    private void OnDestroy()
    {
        material.SetFloat("_Transparency", defaultValue);

    }

}
