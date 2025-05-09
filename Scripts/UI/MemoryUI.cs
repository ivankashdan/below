using UnityEngine;
using UnityEngine.UI;

public class MemoryUI : MonoBehaviour, IControllerSwitch, ITutorialPrompt
{
    public void UpdateOnControllerSwitch()
    {
        //if (!IsActive())
        //{
        spriteFirst = controlSchemeUIManager.currentSprites.interact;
        spriteSecond = controlSchemeUIManager.currentSprites.read;
        //}
    }

    public string textFirst = "Exit";
    public string textSecond = "Read";


    public bool IsActive() => active;
    //=> spriteSecond == controlSchemeUIManager.empty || spriteSecond == null;
    bool active;

    [HideInInspector] public Sprite spriteFirst;
    [HideInInspector] public Sprite spriteSecond;


    MemoryMenu memoryMenu;
    ControlSchemeUIManager controlSchemeUIManager;
    private void Awake()
    {
        memoryMenu = FindAnyObjectByType<MemoryMenu>();
        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();
    }

    private void Start()
    {
        UpdateOnControllerSwitch();
    }

    private void OnEnable()
    {
        memoryMenu.MemoryViewed += OnMemoryViewed;
        memoryMenu.MemoryClosed += OnMemoryClosed;
    }

    private void OnDisable()
    {
        memoryMenu.MemoryViewed -= OnMemoryViewed;
        memoryMenu.MemoryClosed -= OnMemoryClosed;
    }

    void OnMemoryViewed()
    {
        active = true;
        //spriteSecond = controlSchemeUIManager.currentSprites.read;
    }

    void OnMemoryClosed()
    {
        active = false;
        //spriteSecond = controlSchemeUIManager.empty;
    }

}
