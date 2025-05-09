using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class BubbleIdle : MonoBehaviour
{
    public Vector2 idleSpeedRange = new Vector2(0.4f, 2f);
    public Vector2 walkSpeedRange = new Vector2(10, 20);
    public Vector2 runSpeedRange = new Vector2(20, 40);
    [Space(10)]
    public Vector2 idleDelayRange = new Vector2(5, 10);
    public Vector2 runDelayRange = new Vector2(2.5f, 5);
    [Space(10)]
    public Vector2 durationRange = new Vector2(0.5f, 3);
    [Space(10)]
    public float speedCheckDelay = 0.5f;

    float delay, duration, delayCount, playerSpeed, speedCheckDelayCount;

    Vector3 lastPosition;

    ParticleSystem bubbles;
    SpeedChange speedChange;
    ScaleControl scaleControl;
  
    private void Start()
    {
        bubbles = GetComponent<ParticleSystem>();
        speedChange = FindAnyObjectByType<SpeedChange>();
        scaleControl = FindAnyObjectByType<ScaleControl>();
    }


    private void Update()
    {
        playerSpeed = GetSpeed();

        speedCheckDelayCount += Time.deltaTime;
        if (speedCheckDelayCount > speedCheckDelay) //every half-second
        {
            speedCheckDelayCount = 0;
            AssignSpeed();
        }

        delayCount += Time.deltaTime;
        if (delayCount > delay) //when it turns on
        {
            delayCount = 0;
        
            AssignDelay();
            if (bubbles.isStopped) AssignDuration();

            bubbles.Play();
        }
    }

    void AssignSpeed()
    {
        var main = bubbles.main;
        float minValue = 1f;
        float maxValue = 1f;
        float scaleMultiplier = scaleControl.GetScale;

        if (IsMoving())
        {
            if (speedChange != null && speedChange.IsSpeed(SpeedChange.Speed.run))
            {
                minValue = runSpeedRange.x;
                maxValue = runSpeedRange.y;
            }
            else
            {
                minValue = walkSpeedRange.x;
                maxValue = walkSpeedRange.y;
            }
            minValue *= scaleMultiplier;
            maxValue *= scaleMultiplier;
        }
        else main.startSpeed = new ParticleSystem.MinMaxCurve(minValue, maxValue);
    }


    void AssignDelay()
    {
        if (IsMoving() && speedChange != null && speedChange.IsSpeed(SpeedChange.Speed.run))
        {
            delay = Random.Range(runDelayRange.x, runDelayRange.y);
        }
        else
        {
            delay = Random.Range(idleDelayRange.x, idleDelayRange.y);
        }
    }

    void AssignDuration()
    {
        var main = bubbles.main;
        duration = Random.Range(durationRange.x, durationRange.y);
        main.duration = duration;
    }


    bool IsMoving() => playerSpeed > 1;

    float GetSpeed() 
    {
        float distance = Vector3.Distance(transform.position, lastPosition);
        lastPosition = transform.position;
        return distance / Time.deltaTime;
    }
}
