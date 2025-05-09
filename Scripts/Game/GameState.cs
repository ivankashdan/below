using UnityEngine;
using UnityEngine.Playables;

public class GameState : MonoBehaviour
{

    public static GameState Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    MemoryMenu memoryMenu;
    PlayableDirector playableDirector;

    private void Start()
    {
        memoryMenu = FindAnyObjectByType<MemoryMenu>();

        playableDirector = FindAnyObjectByType<PlayableDirector>();
    }





    //out of date below

    public bool IsPlayerInControl() =>
        !IsCutscenePlaying()
        //&& !IsCardDisplayed()
        && !memoryMenu.IsMemoryOpen()
        && !MenuManager.IsAnyMenuOpen();
     
    public bool IsPlayablePlaying() => playableDirector != null && playableDirector.state == PlayState.Playing;
    public bool IsCutscenePlaying() => playableDirector != null && IsPlayablePlaying() && IsPlayableCutscene();
    public bool IsPlayableCutscene() => playableDirector != null && playableDirector.playableAsset.name.Contains("cutscene");

    //public bool IsCardDisplayed() => cardOpening.IsImageActive() || cardEnd.IsImageActive();



}
