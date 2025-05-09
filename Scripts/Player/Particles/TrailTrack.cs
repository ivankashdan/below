using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TrailTrack : MonoBehaviour
{
    public Anatomy.Part target;
    public Vector3 positionOffset;

    Anatomy anatomy;

    private void OnEnable()
    {
        anatomy = FindAnyObjectByType<Anatomy>();
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            if (anatomy != null)
            {
                if (anatomy.dictionary.TryGetValue(target, out Transform targetTransform))
                {
                    transform.position = targetTransform.position + transform.rotation * positionOffset;
                    transform.rotation = targetTransform.rotation;
                }
            }
            else
            {
                Debug.Log("anatomy not initialised");
            }
        }
    }


}
