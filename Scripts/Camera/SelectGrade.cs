using Unity.Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class SelectGrade : MonoBehaviour
{
    public static SelectGrade Instance { get; private set; }

    public Volume defaultVolume;
    public Volume cabinetVolume;
    public Volume senseVolume;


    Coroutine defaultCoroutine;
    Coroutine cabinetCoroutine;
    Coroutine senseCoroutine;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SelectVolume(Volume volume, float duration)
    {
        if (volume == defaultVolume)
        {
            FadeGrade(defaultVolume, 1f, duration);
            FadeGrade(cabinetVolume, 0f, duration);
            FadeGrade(senseVolume, 0f, duration);
        }
        else if (volume == cabinetVolume)
        {
            FadeGrade(defaultVolume, 0f, duration);
            FadeGrade(cabinetVolume, 1f, duration);
            FadeGrade(senseVolume, 0f, duration);
        }
        else if (volume == senseVolume)
        {

            FadeGrade(defaultVolume, 0f, duration);
            FadeGrade(cabinetVolume, 0f, duration);
            FadeGrade(senseVolume, 1f, duration);
        }
    }
    void FadeGrade(Volume volume, float targetValue, float duration)
    {
        if (volume == defaultVolume)
        {
            if (defaultCoroutine != null) StopCoroutine(defaultCoroutine);
            StartCoroutine(FadeCoroutine(defaultVolume, targetValue, duration));
        }
        else if (volume == cabinetVolume)
        {
            if (cabinetCoroutine != null) StopCoroutine(cabinetCoroutine);
            StartCoroutine(FadeCoroutine(cabinetVolume, targetValue, duration));
        }
        else if (volume == senseVolume)
        {
            if (senseCoroutine != null) StopCoroutine(senseCoroutine);
            StartCoroutine(FadeCoroutine(senseVolume, targetValue, duration));
        }

    }

    IEnumerator FadeCoroutine(Volume volume,float targetValue, float duration)
    {

        float startValue = volume.weight;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            volume.weight = Mathf.Lerp(startValue, targetValue, t);
            yield return null;
        }

        volume.weight = targetValue; // Ensure the final value is set
    }




}
