using UnityEngine;
using UnityEngine.Timeline;

public class BestiaryOnCutsceneEnd : MonoBehaviour
{
    public TimelineAsset triggerCutscene;

    BestiaryEntry bestiaryEntry;
    CutsceneManager cutsceneManager;

    bool activated;


    private void Awake()
    {
        cutsceneManager = FindAnyObjectByType<CutsceneManager>();
        bestiaryEntry = GetComponent<BestiaryEntry>();
    }

    private void OnEnable()
    {
        cutsceneManager.CutsceneEnded += OnCutsceneEnd;
    }

    private void OnDisable()
    {
        cutsceneManager.CutsceneEnded -= OnCutsceneEnd;
    }

    void OnCutsceneEnd(TimelineAsset timelineAsset)
    {
        if (!activated)
        {
            if (timelineAsset == triggerCutscene)
            {
                BestiaryManager.AddEntry(bestiaryEntry.reference);
                activated = true;
            }
        }

       
    }

}