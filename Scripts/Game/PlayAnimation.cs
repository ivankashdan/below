using UnityEngine;
using UnityEngine.Timeline;

public class PlayAnimation : MonoBehaviour
{
    public TimelineAsset playable;
    public bool triggerOnCollision = true;

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

            cutsceneManager.PlayAnimation(playable);
        }
    }
}
