using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[ExecuteInEditMode]
public class DampedTool : MonoBehaviour
{

    public bool generate;

    private void Update()
    {
        if (!Application.isPlaying)
        {
            if (generate)
            {
                AddDampToChildAttachToParent(transform);
             





                generate = false;
            }
        }

       
    }

    void AddDampToChildAttachToParent(Transform transform)
    {
        if (transform.childCount != 0)
        {
            foreach (Transform t in transform)
            {
                var damp = t.AddComponent<DampedTransform>();
                damp.data.constrainedObject = t;
                damp.data.sourceObject = transform;

                AddDampToChildAttachToParent(t);

            }
        }

      
    }


}
