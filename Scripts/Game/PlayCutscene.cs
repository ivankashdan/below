using UnityEngine;
using UnityEngine.Timeline;

public class PlayCutscene : MonoBehaviour
{
    public TimelineAsset playable;
    public bool triggerOnCollision = true;
    //bool keepControl = false;
    //public bool cutBlend;

    CutsceneManager cutsceneManager;

    bool played;
    

    void Start()
    {
        cutsceneManager = FindAnyObjectByType<CutsceneManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerOnCollision)
        {
            if (other.CompareTag("Player"))
            {
                Play();
            }
        }
    }

    public void Play()
    {
        if (!played)
        {
            played = true;

            //if (cutBlend)
            //{
            //    cutsceneManager.PlayCutsceneWithCutBlend(playable);
            //}
            //else
            //{
            //if (keepControl)
            cutsceneManager.PlayCutscene(playable);
            //cutsceneManager.PlayAnimation(playable);
            //}
            //else
            //{
            //    cutsceneManager.PlayCutscene(playable);
            //}

            //}
        }
    }

}
