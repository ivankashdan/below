using UnityEngine;
using UnityEngine.UI;

public class StageDebug : MonoBehaviour
{
    public Text text;

    ScaleControl scaleControl;
    SpeedChange speedChange;

    private void Awake()
    {
        scaleControl = FindAnyObjectByType<ScaleControl>();
        speedChange = FindAnyObjectByType<SpeedChange>();
    }

    private void Update()
    {
        text.text = 
            //"Stage: " + scaleControl.GetStage() + "\n" + 
                    "GFXScale: " + scaleControl.GetStageGFXScale() + "\n" +
                    "Speed: " + speedChange.walkMultiplier;

    }
}
