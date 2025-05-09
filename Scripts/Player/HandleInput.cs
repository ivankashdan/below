using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleInput : MonoBehaviour
{
    public static HandleInput Instance { get; private set; }

    Vector2 rawInput = Vector2.zero;
    Vector3 handledInput = Vector3.zero;
    bool handlingInput = true;

    SpeedChange speedChange;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        speedChange = FindAnyObjectByType<SpeedChange>();
    }

    private void Update()
    {
        handledInput = HandleControllerInput();
    }

    public Vector3 GetHandledInput() => handledInput;
    public void IsHandlingInput(bool input) => handlingInput = input;
    public void SetRawInput(Vector2 input)
    {
        if (GameState.Instance.IsPlayerInControl())
        {
            rawInput = input;
        }
        else
        {
            rawInput = Vector2.zero;
        }
    }
    public Vector3 HandleControllerInput()
    {
        Vector3 handledInput = Vector3.zero;

        if (handlingInput)
        {
            if (rawInput.x != 0 || rawInput.y != 0)
            {
                Vector2 input = new Vector2(rawInput.x, rawInput.y);
                Vector3 right = Camera.main.transform.right;
                Vector3 forward = Vector3.Cross(right, Vector3.up);

                handledInput = (right * input.x) + (forward * input.y);//

                float currentSpeed = speedChange.GetCurrentSpeed();
                handledInput *= currentSpeed;
                handledInput = Vector3.ClampMagnitude(handledInput, currentSpeed);
            }
        }
        return handledInput;
    }
}
