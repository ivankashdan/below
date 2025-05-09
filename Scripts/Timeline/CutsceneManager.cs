using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneManager : MonoBehaviour
{
    public event Action<TimelineAsset> CutsceneStarted;
    public event Action<TimelineAsset> CutsceneEnded;

    
    //public CinemachineVirtualCameraBase easeBlendCamera;
    //public CinemachineVirtualCameraBase cutBlendCamera;

    CinemachineBlendDefinition defaultBlend;

    //CinemachineVirtualCameraBase currentCamera;

    CinemachineBrain cinemachineBrain;
    [HideInInspector] public PlayableDirector playableDirector;
    PlayerState playerState;
    AnimationBehaviour animationBehaviour;

    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();

        playerState = FindAnyObjectByType<PlayerState>();
        animationBehaviour = FindAnyObjectByType<AnimationBehaviour>();
        cinemachineBrain = FindAnyObjectByType<CinemachineBrain>();

        //easeBlendCamera.Priority = 0;
        //cutBlendCamera.Priority = 0;

        defaultBlend = cinemachineBrain.DefaultBlend;
    }




    private void OnEnable()
    {
        playableDirector.stopped += OnPlayableStopped;
    }

    private void OnDisable()
    {
        playableDirector.stopped -= OnPlayableStopped;
    }

    public bool IsCutscenePlaying() => playableDirector.state == PlayState.Playing;

    //public void PlayCutsceneWithCutBlend(TimelineAsset timelineAsset)
    //{
    //    BlendOverrideCut();
    //    PlayCutscene(timelineAsset);
    //}

    public void PlayCutscene(TimelineAsset timelineAsset)
    {
        animationBehaviour.animator.StopPlayback();
        //animationBehaviour.animator.Update(0);

        //easeBlendCamera.Priority = 100;
        //currentCamera = camera;
        playerState.SetState(PlayerState.State.none);
        playableDirector.Play(timelineAsset);
        CutsceneStarted?.Invoke(timelineAsset);

    }

    public void PlayAnimation(TimelineAsset timelineAsset)
    {
        playableDirector.Play(timelineAsset);
    }

 

    //void PlayCutscene(TimelineAsset timelineAsset, CinemachineVirtualCameraBase camera)
    //{
    //    animationBehaviour.animator.StopPlayback();
    //    //animationBehaviour.animator.Update(0);

    //    camera.Priority = 100;
    //    currentCamera = camera;
    //    playerState.SetState(PlayerState.State.none);
    //    playableDirector.Play(timelineAsset);
    //    CutsceneStarted?.Invoke(timelineAsset);
    //} 

    public void StopCutscene()
    {
        TimelineAsset timelineAsset = playableDirector.playableAsset as TimelineAsset;
        CutsceneEnded?.Invoke(timelineAsset);

        if (playableDirector.state == PlayState.Playing)
        {
            playableDirector.Stop();
        }

        //easeBlendCamera.Priority = 0;
        //currentCamera.Priority = 0;
        //currentCamera = null;

        playerState.SetState(PlayerState.State.ground);
        animationBehaviour.animator.Rebind();
        //animationBehaviour.animator.StartPlayback();
        //animationBehaviour.animator.Update(0);
    }
    void OnPlayableStopped(PlayableDirector playableDirector)
    {
        if (playableDirector == this.playableDirector)
        {
            StopCutscene();
        }
    }


    //void BlendOverrideCut()
    //{
    //    var customBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Styles.Cut, 0);

    //    // Apply this blend when switching from cameraA to cameraB
    //    cinemachineBrain.DefaultBlend = customBlend;

    //    StartCoroutine(RestoreDefaultBlendCoroutine());
    //}

    //IEnumerator RestoreDefaultBlendCoroutine()
    //{
    //    yield return new WaitForSeconds(1f);

    //    cinemachineBrain.DefaultBlend = defaultBlend;
    //}



}
