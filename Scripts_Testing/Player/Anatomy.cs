using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Anatomy : MonoBehaviour
{
    public Transform head;
    public Transform shellTarget;
    public Transform root;

    public Transform legBackL, legBackR, legFrontL, legFrontR;

    public Dictionary<Part, Transform> dictionary;

    private void OnEnable()
    {
        dictionary = new Dictionary<Part, Transform>()
        {
            {Part.root, root},
            {Part.shell, shellTarget},
            {Part.head, head},
            {Part.legBackL, legBackL},
            {Part.legBackR, legBackR},
            {Part.legFrontL, legFrontL},
            {Part.legFrontR, legFrontR}
        };
    }

    public Transform[] GetLegTips()
    {
        return new Transform[] { legBackL, legBackR, legFrontL, legFrontR };
    }

    public Transform GetShellTarget()
    {
        return shellTarget;
    }

    public enum Part
    {
        root, shell, head, legBackL, legBackR, legFrontL, legFrontR
    }

   

}
