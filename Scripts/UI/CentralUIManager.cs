using TMPro;
using UnityEngine;

public class CentralUIManager : MonoBehaviour, ITutorialPrompt
{

    public GameObject centralObject;
    public TMP_Text centralText;

    bool active;
    public bool IsActive() => active;

    private void Start()
    {
        SetActive(false);
    }

    public void SetActive(bool value)
    {
        centralObject.SetActive(value);
        active = value;
        if (value ==  false)
        {
            FormatCentralText(FontStyles.Normal);
        }
    }

    public void SetCentralText(string text, Color color)
    {
        centralText.text = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}";
    }

    public void SetCentralText(string text)
    {
        //centralText.richText = true;
        centralText.text = text;
    }

    public void FormatCentralText(FontStyles fontStyle)
    {
        centralText.fontStyle = fontStyle;
    }

}
