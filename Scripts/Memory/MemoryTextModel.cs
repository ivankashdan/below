using System.Collections;
using UnityEngine;
using HighlightPlus;

public class MemoryTextModel : MonoBehaviour, IInteractable
{
    //public static event Action<MemoryTextModel> MemoryOpened;

    public MemoryScriptableObject reference;

    public float fadeDuration = 1f;
    bool faded;

    MemoryReworkedLogic memoryLogic;
    HighlightEffect highlightEffect;
    ParticleSystem[] particles;

    protected virtual void Awake()
    {
        memoryLogic = FindAnyObjectByType<MemoryReworkedLogic>();
        particles = GetComponentsInChildren<ParticleSystem>();
        //highlightEffect = GetComponentInChildren<HighlightEffect>();
    }

    public virtual void Interact()
    {
        memoryLogic.OpenMemory(this);

        PointOfInterest.Discover(true, gameObject);

        //FadeEffects();
    }

    public virtual void CloseMemory() { }
    public bool IsInteractable() => true;
    public int GetInteractPriority() => 4;
    void FadeEffects()
    {
        if (particles != null)
        {
            foreach (var particle in particles)
            {
                particle.Stop();
                //particle.Clear();
            }
        }
        //if (highlightEffect != null)
        //{
        //    highlightEffect.glow = 0f;

        //    if (!faded)
        //    {
        //        faded = true;
        //        StartCoroutine(FadeOutInnerGlow());
        //    }
        //}
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
    }

}