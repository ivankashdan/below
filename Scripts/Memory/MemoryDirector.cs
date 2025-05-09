using UnityEngine;
using UnityEngine.Playables;

public class MemoryDirector : MonoBehaviour
{
    [HideInInspector] public PlayableDirector director;

    MemoryTextSelection textSelection;

    bool isTimelineOpen;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();

        textSelection = FindAnyObjectByType<MemoryTextSelection>();

    }

    private void OnEnable()
    {
        textSelection.SelectionChanged += OnSelectionChanged;
    }


    private void OnDisable()
    {
        textSelection.SelectionChanged -= OnSelectionChanged;
    }
    public void OpenTimeline(PlayableAsset playable)
    {
        isTimelineOpen = true;
        director.playableAsset = playable;
        director.time = director.initialTime;
        director.Evaluate(); 
    }

    public void CloseTimeline()
    {
        isTimelineOpen = false;
        director.Stop();
    }


    void OnSelectionChanged()
    {
        if (isTimelineOpen)
        {
            director.time = Mathf.Lerp((float)director.initialTime, (float)director.duration, textSelection.percentageThroughText);
            director.Evaluate(); // Update the timeline manually
        }
    }



}
