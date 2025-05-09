using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExecutable : MonoBehaviour
{
    public static GameExecutable Instance { get; private set; }


    MenuManager menuManager;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        menuManager = FindAnyObjectByType<MenuManager>();

        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        //menuManager.CloseMenu();
        //GameMenu.Instance.MainMenu(false);
        //StartCoroutine(RestartCoroutine());
        Time.timeScale = 0.0f;
        Scene currentScene = SceneManager.GetActiveScene();
        int currentSceneIndex = currentScene.buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Time.timeScale = 1.0f;


    }


    public void QuitGame()
    {
        Application.Quit();
    }


    IEnumerator RestartCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        StopAllCoroutines();
     
    }



}
