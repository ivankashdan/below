using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedChange : MonoBehaviour
{
    public float walkMultiplier = 1f;

    public float runMultiplier = 2;
    public float glideMultiplier = 1.5f;
    public float hopMultiplier = 3;
    public float energyMultiplier = 10;

    public float speedChangeAcceleration = 5f;
    public float speedChangeDeceleration = 3f;
    [Space(10)]
    public float currentSpeed;
    public float baseSpeed = 1f;
    public float defaultBaseSpeed;
    public float walkSpeed;
    public float runSpeed;
    float glideSpeed;
    float energySpeed;
    float hopSpeed;

    Dictionary<Speed, float> speedValuePairs;

    Dictionary<Acceleration, float> accelerationValuePairs;
    public enum Speed
    {
        walk,
        run, 
        glide, 
        hop
    };

    public enum Acceleration
    {
        accelerate,
        decelerate
    }


    Coroutine speedChangeCoroutine;

    PlayerState playerState;
    InputManager input;


    private void Awake()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        input = FindAnyObjectByType<InputManager>();

        speedValuePairs = new Dictionary<Speed, float>()
        {
            {Speed.walk, walkSpeed},
            {Speed.run, runSpeed},
            {Speed.glide, glideSpeed},
            {Speed.hop, hopSpeed},
        };

        accelerationValuePairs = new Dictionary<Acceleration, float>()
        {
            {Acceleration.accelerate, speedChangeAcceleration},
            {Acceleration.decelerate, speedChangeDeceleration},
        };
    }

    private void Start()
    {
        defaultBaseSpeed = baseSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            walkMultiplier--;
            ResetSpeedsFromBase();
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            walkMultiplier++;
            ResetSpeedsFromBase();
        }

        if (!IsChangingSpeed() && GameState.Instance.IsPlayerInControl())
        {
            if (playerState.IsInState(PlayerState.State.ground))
            {
                if (InputManager.controls.Gameplay.Sprint.IsPressed())
                {
                    if (!IsSpeed(Speed.run))
                    {
                        SetSpeed(Speed.run, Acceleration.accelerate);
                    }
                }
                else
                {
                    if (!IsSpeed(Speed.walk))
                    {
                        SetSpeed(Speed.walk, Acceleration.decelerate);
                    }
                }

            }
            else if (playerState.IsInState(PlayerState.State.gliding))
            {
                if (!IsSpeed(Speed.glide))
                {
                    SetSpeed(Speed.glide, Acceleration.accelerate);
                }
            }
            else
            {
                if (!IsSpeed(Speed.walk))
                {
                    SetSpeed(Speed.walk, Acceleration.decelerate);
                }
            }
        }
    }

    public float GetCurrentSpeed() => currentSpeed;
    public float GetSpeed(Speed speed) => speedValuePairs.GetValueOrDefault(speed);

    public bool IsSpeed(Speed speed) => currentSpeed == speedValuePairs.GetValueOrDefault(speed);
    
    bool IsChangingSpeed() => speedChangeCoroutine != null;
    public void SetBaseSpeed(float speed) => ResetSpeedsWithNewBase(speed);
    public void ModifyBaseSpeed(float multipler) => ResetSpeedsWithNewBase(baseSpeed * multipler);


    public void SetSpeed(Speed speed, Acceleration acceleration) => 
        SetSpeed(speedValuePairs.GetValueOrDefault(speed), 
        accelerationValuePairs.GetValueOrDefault(acceleration));

    public void ResetSpeedsWithNewBase(float newBaseSpeed)
    {
        baseSpeed = newBaseSpeed;
        ResetSpeedsFromBase();

        //Debug.Log("Base speed: " + this.baseSpeed.ToString());
    }
    public void ResetSpeedsFromBase()
    {
        walkSpeed = baseSpeed * walkMultiplier;
        runSpeed = walkSpeed * runMultiplier;
        energySpeed = runSpeed * 2;
        glideSpeed = walkSpeed * glideMultiplier;
        hopSpeed = walkSpeed * hopMultiplier;

        currentSpeed = walkSpeed;

        speedValuePairs[Speed.walk] = walkSpeed;
        speedValuePairs[Speed.run] = runSpeed;
        speedValuePairs[Speed.glide] = glideSpeed;
        speedValuePairs[Speed.hop] = hopSpeed;

        //Debug.Log("Base speed: " + this.baseSpeed.ToString());
    }

    void SetSpeed(float speed, float acceleration)
    {
        //Debug.Log("speed changed: " + speed.ToString());

        if (speedChangeCoroutine != null)
            StopCoroutine(speedChangeCoroutine);

        speedChangeCoroutine = StartCoroutine(ChangeSpeedCoroutine(speed, acceleration));
 
    }

    IEnumerator ChangeSpeedCoroutine(float targetSpeed, float rate)
    {
        while (!Mathf.Approximately(currentSpeed, targetSpeed))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, rate * Time.deltaTime);
            yield return null;
        }
        currentSpeed = targetSpeed;
        speedChangeCoroutine = null;
    }






}
