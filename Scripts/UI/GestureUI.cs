using UnityEngine;

public class GestureUI : MonoBehaviour, IControllerSwitch, ITutorialPrompt
{
    public void UpdateOnControllerSwitch()
    {
        if (!IsActive())
        {
            currentSprite = controlSchemeUIManager.currentSprites.gesture;
        }
    }

    public bool IsActive() => currentSprite == controlSchemeUIManager.empty || currentSprite == null;

    [HideInInspector] public Sprite currentSprite;

    ControlSchemeUIManager controlSchemeUIManager;
    GestureBehaviour gestureBehaviour;
    CameraCheck cameraCheck; //should perhaps still include

    private void Awake()
    {
        controlSchemeUIManager = FindAnyObjectByType<ControlSchemeUIManager>();
        gestureBehaviour = FindAnyObjectByType<GestureBehaviour>();
        cameraCheck = FindAnyObjectByType<CameraCheck>();
    }

    private void OnEnable()
    {
        gestureBehaviour.GesturablesChange += OnGesturablesChange;
    }

    private void OnDisable()
    {
        gestureBehaviour.GesturablesChange -= OnGesturablesChange;
    }

    void OnGesturablesChange(bool inRange)
    {
        if (inRange)
        {
            currentSprite = controlSchemeUIManager.currentSprites.gesture;
        }
        else
        {
            ClearSprite();
        }
    }

    void ClearSprite()
    {
        currentSprite = controlSchemeUIManager.empty;
        Debug.Log("Sprite cleared");
    }
}
