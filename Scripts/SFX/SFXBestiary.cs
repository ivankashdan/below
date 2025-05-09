using UnityEngine;

public class SFXBestiary : MusicTrack
{
    MusicTrack storedTrack;

    private void OnEnable()
    {
        MenuManager.MenuOpened += OnMenuOpened;
        MenuManager.MenuClosed += OnMenuClosed;

    }

    private void OnDisable()
    {
        MenuManager.MenuOpened -= OnMenuOpened;
        MenuManager.MenuClosed -= OnMenuClosed;
    }

    void OnMenuOpened(MenuManager.Menu menu)
    {
        if (menu == MenuManager.Menu.Bestiary)
        {
            storedTrack = audioCrossfade.currentTrack;
            PlayTrack();
        }
      
    }

    void OnMenuClosed(MenuManager.Menu menu)
    {
        if (menu == MenuManager.Menu.Bestiary)
        {
            audioCrossfade.CrossfadeToOnce(storedTrack);
        }
    }
}
