using NUnit.Framework;
using UnityEngine;

[CreateAssetMenu(fileName = "MemoryScriptableObject", menuName = "ScriptableObjects/MemoryScriptableObject", order = 1)]
public class MemoryScriptableObject : ScriptableObject
{
    [TextArea(10, 20)]
    public string text;

    public float effectRange = 1f;
    //public GameObject image; 
}
