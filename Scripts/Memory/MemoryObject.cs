using Unity.Cinemachine;
//using HighlightPlus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using HighlightPlus;

public class MemoryObject : MonoBehaviour, IInteractable
{
    public Sprite sprite;
    public CinemachineVirtualCameraBase virtualCamera;
    public float fadeDuration = 1f;

    bool memoryOpen;
    bool faded;

    ParticleSystem particles;
    Light pointLight;
    MemoryMenu memoryMenu;
    HighlightEffect highlightEffect;


    protected virtual void Awake()
    {
        memoryMenu = FindAnyObjectByType<MemoryMenu>();

        particles = GetComponentInChildren<ParticleSystem>();
        pointLight = GetComponentInChildren<Light>();
        highlightEffect = GetComponentInChildren<HighlightEffect>();
    }

    protected virtual void Start()
    {
        //gameObject.tag = "Memory";

    }

    public bool IsInteractable() =>!memoryMenu.IsMemoryOpen();
    public int GetInteractPriority() => 5;

    public void Interact()
    {
        memoryMenu.ViewMemory(virtualCamera, sprite);
        //virtualCamera.Priority = 10;

        if (particles != null)
        {
            particles.Stop(); 
            particles.Clear();
        }
        if (highlightEffect != null)
        {
            //highlightEffect.highlighted = false;
            highlightEffect.glow = 0f;
            if (!faded) StartInnerGlowFadeOut();
        }
        if (pointLight != null)
        {
            pointLight.enabled = false;
        }
       

    }

    public void StartInnerGlowFadeOut()
    {
        // Start the fade-out coroutine
        StartCoroutine(FadeOutInnerGlow());
    }

    IEnumerator FadeOutInnerGlow()
    {
        float initialInnerGlowIntensity = highlightEffect.innerGlow;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newIntensity = Mathf.Lerp(initialInnerGlowIntensity, 0f, elapsedTime / fadeDuration);
            highlightEffect.innerGlow = newIntensity;
            yield return null;
        }
        highlightEffect.innerGlow = 0f;
        faded = true;

    }

  
}
