using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXInGameManager : MonoBehaviour
{
    public AudioMixer mixer;
    public string volumeString;
    float defaultVolume;

    private void Awake()
    {
        if (mixer.GetFloat(volumeString, out defaultVolume))
        {
            Debug.Log("Successfully accessed string");
        }
        else
        {
            Debug.LogError("Failed to access string");
        }
    }

    private void OnEnable()
    {
        MenuManager.MenuOpened += OnMenuOpened;
        MenuManager.MenuBaseDeactivated += OnMenuClosed;
    }

    private void OnDisable()
    {
        MenuManager.MenuOpened -= OnMenuOpened;
        MenuManager.MenuBaseDeactivated -= OnMenuClosed;
    }

    void OnMenuOpened(MenuManager.Menu menu)
    {
        if (mixer.SetFloat(volumeString, -80f))
        {
            Debug.Log("Successfully muted string");
        }
        else
        {
            Debug.LogError("Failed to mute string");
        }
    }

    void OnMenuClosed()
    {
        if (mixer.SetFloat(volumeString, defaultVolume))
        {
            Debug.Log("Successfully reset string");
        }
        else
        {
            Debug.LogError("Failed to reset string");
        }
    }

    private void OnDestroy()
    {
        mixer.SetFloat(volumeString, defaultVolume);
    }
}
