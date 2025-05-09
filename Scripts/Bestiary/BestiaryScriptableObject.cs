using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BestiaryScriptableObject", menuName = "ScriptableObjects/BestiaryScriptableObject", order = 1)]
public class BestiaryScriptableObject : ScriptableObject
{
    public string title;
    public Sprite image;
    public Sprite icon;

    [TextArea(5, 5)]
    public string description;
}
