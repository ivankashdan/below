using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class ScaleTrail : MonoBehaviour
{

    public float multiplier = 1f;

    ScaleControl scaleControl;
    TrailRenderer trailRenderer;

    private void Awake()
    {
        scaleControl = FindAnyObjectByType<ScaleControl>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        scaleControl.ScaleUpdated += UpdateWidth;
    }

    private void OnDisable()
    {
        scaleControl.ScaleUpdated -= UpdateWidth;
    }

    void UpdateWidth()
    {
        trailRenderer.widthMultiplier = scaleControl.GetScale * multiplier;
    }


}
