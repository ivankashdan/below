using UnityEngine;

public class BestiaryPhytoplankton : MonoBehaviour
{
    public BestiaryScriptableObject reference;

    bool triggered;

    private void OnEnable()
    {
        MenuManager.MenuClosed += OnMenuClosed;
    }

    private void OnDisable()
    {
        MenuManager.MenuClosed -= OnMenuClosed;
    }


    void OnMenuClosed(MenuManager.Menu menu)
    {
        if (!triggered)
        {
            if (menu == MenuManager.Menu.Memory)
            {
                BestiaryManager.AddEntry(reference);
                triggered = true;
            }
        }
            
       
    }

}
