using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SFXEnding : MusicTrack
{
    //public TimelineAsset triggerCutscene;
    //CutsceneManager cutsceneManager;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    cutsceneManager = FindAnyObjectByType<CutsceneManager>();

    //}

    //private void OnEnable()
    //{
    //    cutsceneManager.CutsceneEnded += OnCutsceneEnded;
    //    //cutsceneManager.playableDirector.stopped += OnTimelineStopped;
    //}

    //private void OnDisable()
    //{
    //    cutsceneManager.CutsceneEnded -= OnCutsceneEnded;
    //    //cutsceneManager.playableDirector.stopped -= OnTimelineStopped;
    //}

    //void OnCutsceneEnded(TimelineAsset timelineAsset)
    //{
    //    if (timelineAsset == triggerCutscene)
    //    {
    //        PlayTrack();
    //    }
    //}



    public void ActivateEnding()
    {
        audioCrossfade.defaultTrack = this;
        PlayTrack();
    }

    //void OnTimelineStopped(PlayableDirector playableDirector)
    //{

    //}


}
