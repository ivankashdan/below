using UnityEngine;
using UnityEngine.UI;

public class MainMenuPrompt : PromptUI, IControllerSwitch
{
    public Image selectIcon;
    public Image exitIcon;
  
    public override void UpdateOnControllerSwitch()
    {
        if (MenuManager.Instance.IsMenuOpen(menu))
        {
            selectIcon.sprite = controlSchemeUIManager.currentSprites.select;
            exitIcon.sprite = controlSchemeUIManager.currentSprites.escape;
            
            float smallLargeHeight = 85f;
            promptUIManager.ResizeUI(selectIcon, smallLargeHeight);
            promptUIManager.ResizeUI(exitIcon, smallLargeHeight);
        }
    }

   
}
