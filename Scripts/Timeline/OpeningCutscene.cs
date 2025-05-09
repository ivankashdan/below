using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class OpeningCutscene : MonoBehaviour
{

    public TimelineAsset playable;

    CutsceneManager cutsceneManager;

    private void Awake()
    {
        cutsceneManager = GetComponent<CutsceneManager>();
    }

    public void StartCutscene()
    {
        cutsceneManager.PlayCutscene(playable);
    }


  

}
