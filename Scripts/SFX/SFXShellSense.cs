using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class SFXShellSense : MonoBehaviour
{
    public AudioMixer audioMixer;

    public float fadeDuration = 2f;

    string SpaceVolume = "SpaceVolume";
    string SpaceHighPass = "SpaceHighPass";
    string SpaceLowPass = "SpaceLowPass";

    float volumeStart = 0;
    float highPassStart = 10;
    float lowPassStart = 22000;

    float volumeEnd = -40f;
    float highPassEnd = 1000;
    float lowPassEnd = 2000;

    AudioSource audioSource;
    ShellSense shellSense;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        shellSense = FindAnyObjectByType<ShellSense>();
    }

    private void OnEnable()
    {
        shellSense.SenseActivated += OnShellSenseActivated;
        shellSense.SenseDeactivated += OnShellSenseDeactivated;
    }

    private void OnDisable()
    {
        shellSense.SenseActivated -= OnShellSenseActivated;
        shellSense.SenseDeactivated -= OnShellSenseDeactivated;
    }

    void OnShellSenseActivated()
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(fadeDuration));
    }

    void OnShellSenseDeactivated()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    private IEnumerator FadeInCoroutine(float duration)
    {
        SetInitialMixerValues(volumeStart, highPassStart, lowPassStart);

        audioSource.volume = 0f;
        audioSource.Play();

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;

            InterpolateMixerParameters(time / duration, volumeStart, volumeEnd, highPassStart, highPassEnd, lowPassStart, lowPassEnd);
            audioSource.volume = Mathf.Lerp(0f, 1f, time / duration);

            yield return null;
        }

        SetFinalMixerValues(volumeEnd, highPassEnd, lowPassEnd);
        audioSource.volume = 1f;
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = audioSource.volume;
        float startVolumeMixer, startHighPass, startLowPass;
        audioMixer.GetFloat(SpaceVolume, out startVolumeMixer);
        audioMixer.GetFloat(SpaceHighPass, out startHighPass);
        audioMixer.GetFloat(SpaceLowPass, out startLowPass);

        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;

            InterpolateMixerParameters(time / duration, startVolumeMixer, volumeStart, startHighPass, highPassStart, startLowPass, lowPassStart);
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);

            yield return null;
        }

        SetFinalMixerValues(volumeStart, highPassStart, lowPassStart);
        audioSource.volume = 0f;
        audioSource.Stop();
    }

    private void SetInitialMixerValues(float volume, float highPass, float lowPass)
    {
        audioMixer.SetFloat(SpaceVolume, volume);
        audioMixer.SetFloat(SpaceHighPass, highPass);
        audioMixer.SetFloat(SpaceLowPass, lowPass);
    }

    private void SetFinalMixerValues(float volume, float highPass, float lowPass)
    {
        audioMixer.SetFloat(SpaceVolume, volume);
        audioMixer.SetFloat(SpaceHighPass, highPass);
        audioMixer.SetFloat(SpaceLowPass, lowPass);
    }

    private void InterpolateMixerParameters(float t, float volumeStart, float volumeEnd, float highPassStart, float highPassEnd, float lowPassStart, float lowPassEnd)
    {
        audioMixer.SetFloat(SpaceVolume, Mathf.Lerp(volumeStart, volumeEnd, t));
        audioMixer.SetFloat(SpaceHighPass, Mathf.Lerp(highPassStart, highPassEnd, t));
        audioMixer.SetFloat(SpaceLowPass, Mathf.Lerp(lowPassStart, lowPassEnd, t));
    }
}
