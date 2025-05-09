using UnityEngine;

public class SenseComposition : MonoBehaviour
{

    public float composerY = 0f;
    public float modTopY = 0f;
    public float modBottomY = 0f;

    ScreenYBehaviour screenY;

    ShellSense sense;

    private void Awake()
    {
        screenY = GetComponent<ScreenYBehaviour>();

        sense = FindAnyObjectByType<ShellSense>();
    }

    private void OnEnable()
    {
        sense.SenseActivated += OnSenseActivated;
        sense.SenseDeactivated += OnSenseDeactivated;
    }

    private void OnDisable()
    {
        sense.SenseActivated -= OnSenseActivated;
        sense.SenseDeactivated += OnSenseDeactivated;
    }

    void OnSenseActivated()
    {
        screenY.StartScreenYTransition(composerY, modTopY, modBottomY);
        //screenY.StartScreenYTransition(composerY);
    }

    void OnSenseDeactivated()
    {
        screenY.StartScreenYTransitionToBase();
        //screenY.StartScreenYTransitionToBase();
    }


}
