using System.Collections;
using TMPro;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class DebugHotkeys : MonoBehaviour
{
    public TMP_Text uiText;

    public AudioMixer audioMixer;

    public string masterVolume;

    const float volumeMin = -80f;
    const float volumeMax = 20f;

    bool yAxisInverted;

    CinemachineOrbitalFollow orbitalFollow;

    private void Awake()
    {
        CameraBehaviourManager cameraManager = FindAnyObjectByType<CameraBehaviourManager>();
        orbitalFollow = cameraManager.followCamera.GetComponent<CinemachineOrbitalFollow>();

        UpdateText();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F11))
        //{
        //    uiText.gameObject.SetActive(!uiText.gameObject.activeSelf);
        //}
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            IncrementVolume(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
        {
            IncrementVolume(+1);
        }
        //else if (Input.GetKeyDown(KeyCode.I))
        //{
        //    InvertYAxis();
        //}

    }

    void InvertYAxis()
    {
        orbitalFollow.VerticalAxis.Range = new Vector2(-orbitalFollow.VerticalAxis.Range.x, -orbitalFollow.VerticalAxis.Range.y);
        yAxisInverted = !yAxisInverted;

        UpdateText();
    }

    void IncrementVolume(float value)
    {
        audioMixer.GetFloat(masterVolume, out float currentVolume);
        float newVolume = Mathf.Clamp(currentVolume + value, volumeMin, volumeMax);
        audioMixer.SetFloat(masterVolume, newVolume);

        StopAllCoroutines();
        StartCoroutine(DelayedClearRoutine());

        UpdateText();

        Debug.Log($"Volume changed: {newVolume}");
    }

    void UpdateText()
    {
        audioMixer.GetFloat(masterVolume, out float currentVolume);

        uiText.text = 
            //$"Change volume: - +\n" +
            $"Current volume: {(int)currentVolume}\n\n";
            
            //+
            //$"Invert Y: I" +
            //$"Y inverted: {yAxisInverted.ToString()}";
    }

    IEnumerator DelayedClearRoutine()
    {
        uiText.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(2f);

        uiText.gameObject.SetActive(false);

    }

}
