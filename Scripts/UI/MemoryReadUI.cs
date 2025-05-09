using UnityEngine;
using UnityEngine.UI;

public class MemoryReadUI : MonoBehaviour
{
    public Image image;

    MemoryMenu memoryMenu;
    ControlSchemeUIManager controlSchemeUIManager;

    private void Awake()
    {
        memoryMenu = FindAnyObjectByType<MemoryMenu>();
        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();

    }

    private void Start()
    {
        ClearSprite();
    }


    private void OnEnable()
    {
        memoryMenu.MemoryRead += OnStartedReading;
        memoryMenu.StoppedReading += OnStoppedReading;
    }

    private void OnDisable()
    {
        memoryMenu.MemoryRead -= OnStartedReading;
        memoryMenu.StoppedReading -= OnStoppedReading;
    }

    void OnStartedReading(Sprite sprite)
    {
        image.sprite = sprite;
    }

    void OnStoppedReading()
    {
        ClearSprite();
    }

    void ClearSprite()
    {
        image.sprite = controlSchemeUIManager.empty;
    }

}
