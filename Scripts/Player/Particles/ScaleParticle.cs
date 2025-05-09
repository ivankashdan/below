using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class ScaleParticle : MonoBehaviour
{
    public float multiplier = 1f;

    ScaleControl scaleControl;

    private void Awake()
    {
        scaleControl = FindAnyObjectByType<ScaleControl>();
    }

    private void OnEnable()
    {
        scaleControl.ScaleUpdated += UpdateScale;
    }

    private void OnDisable()
    {
        scaleControl.ScaleUpdated -= UpdateScale;
    }

    void UpdateScale()
    {
        float playerScale = scaleControl.GetScale;
        Vector3 scaleVector = new Vector3(playerScale, playerScale, playerScale);

        transform.localScale = scaleVector * multiplier;
    }
}
