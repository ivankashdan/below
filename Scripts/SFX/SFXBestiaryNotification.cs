using System.Collections;
using UnityEngine;

public class SFXBestiaryNotification : OneShotTrack
{
    bool active = false;

    private void Start()
    {
        StartCoroutine(OpeningDelayCoroutine());
    }

    private void OnEnable()
    {   
        BestiaryManager.BestiaryEntryAdded += OnBestiaryEntryAdded;
    }

    private void OnDisable()
    {
        BestiaryManager.BestiaryEntryAdded -= OnBestiaryEntryAdded;
    }

    void OnBestiaryEntryAdded(BestiaryScriptableObject reference)
    {
        if (active)
        {
            PlayTrack();
        }
    }

    IEnumerator OpeningDelayCoroutine()
    {
        yield return new WaitForSeconds(1f);
        active = true;
    }
 
}
