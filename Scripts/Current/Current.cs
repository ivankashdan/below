using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(ParticleSystem))]
[ExecuteInEditMode]
public abstract class Current : MonoBehaviour
{
    [Range(0f, 200f)]
    public float waterSpeed;
    public float height;
    public float radius;
    public float particlesRadiusOffset;
    [Space(10)]
    public bool cameraTurn = true;
    [Space(10)]
    public bool overrideAutoParticles;
    public ParticleSystem.MinMaxCurve manualSpeed;
    public ParticleSystem.MinMaxCurve manualLifetime;
    public ParticleSystem.MinMaxCurve manualSize;
    public ParticleSystem.MinMaxCurve manualEmission;

    bool inCurrent;


    protected Vector3 waterDirection;
    public Vector3 GetWaterDirection() => waterDirection;

    protected Transform playerObject;
    protected ParticleSystem particles;
    protected PlayerState playerState;
    protected CapsuleCollider capsuleCollider;
    protected Transform gfx;

    CameraBehaviourManager newCameraBehaviour;
    CannonState cannonState;
    Coroutine entranceCoroutine;

    protected virtual void Start()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        playerObject = GameObject.FindWithTag("Player").transform;
        cannonState = FindAnyObjectByType<CannonState>();
        newCameraBehaviour = FindAnyObjectByType<CameraBehaviourManager>();
        particles = GetComponent<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        if (transform.childCount != 0) gfx = transform.GetChild(0);
    }

    protected virtual void Update()
    {
        InitializeComponents();
        UpdateGFX();

        if (!overrideAutoParticles)
        {
            AutoParticlesUpdate();
        }
        else
        {
            ManualParticlesUpdate();
        }

        UpdateParticleRadius();

        UpdateColliderSettings();
        DisplayWaterDirection();
        CheckCurrentStatus();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player") && !cannonState.IsBreakingFromCurrent())
        {
            AddCurrent();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            cannonState.SetBreakingFromCurrent(false);
        }
    }



    void InitializeComponents()
    {
        if (capsuleCollider == null) capsuleCollider = GetComponent<CapsuleCollider>();
        if (particles == null) particles = GetComponent<ParticleSystem>();
        if (playerState == null) playerState = FindAnyObjectByType<PlayerState>();
        if (gfx == null && transform.childCount != 0) gfx = transform.GetChild(0);
    }

    void UpdateGFX()
    {
        if (gfx != null)
        {
            float pipeWidth = radius * 2.4f;
            gfx.localScale = new Vector3(pipeWidth, pipeWidth, pipeWidth);
            gfx.localPosition = new Vector3(0, radius * -2.946f, radius * 1.12f);
        }
    }

    void AutoParticlesUpdate()
    {
        float animationSpeed = waterSpeed / 2;
        float capsuleTip = radius;
        float tipPercentile = capsuleTip / height;

        var main = particles.main;
        var speedCurve = new ParticleSystem.MinMaxCurve((animationSpeed / 2), animationSpeed);
        main.startSpeed = speedCurve;

        var lifetime = particles.lifetimeByEmitterSpeed;
        lifetime.enabled = true;
        lifetime.range = new Vector2(speedCurve.constantMin, speedCurve.constantMax);

        float curveMultiplier = (height / animationSpeed) / 5f;
        float tipOffset = curveMultiplier * tipPercentile;
        lifetime.curveMultiplier = curveMultiplier - tipOffset;

        var size = particles.sizeBySpeed;
        size.enabled = true;
        size.range = new Vector2(speedCurve.constantMin, speedCurve.constantMax);
    }

    void ManualParticlesUpdate()
    {
        var main = particles.main;

        var size = particles.sizeBySpeed;
        size.enabled = false;
        main.startSize = manualSize;
        main.startSpeed = manualSpeed;

        var lifetime = particles.lifetimeByEmitterSpeed;
        lifetime.enabled = false;
        main.startLifetime = manualLifetime;

        var emission = particles.emission;
        emission.rateOverTime = manualEmission;
    }

    void UpdateParticleRadius()
    {
        var shape = particles.shape;
        shape.radius = radius + particlesRadiusOffset;
    }


    void UpdateColliderSettings()
    {
        float capsuleTip = radius;
        capsuleCollider.radius = radius;
        capsuleCollider.height = height;
        capsuleCollider.center = new Vector3(0, (height / 2) - capsuleTip, 0);
    }

    void DisplayWaterDirection()
    {
        waterDirection = transform.localRotation * Vector3.up;
        Debug.DrawLine(transform.position, transform.position + waterDirection * 10, Color.green);
    }

    void CheckCurrentStatus()
    {
        if (inCurrent && !IsInsideCollider(playerObject.position) && entranceCoroutine == null)
        {
            RemoveCurrent();
        }
    }

    void AddCurrent()
    {
        cannonState.AddCurrent(this);
        entranceCoroutine = StartCoroutine(EntranceCoroutine());
        inCurrent = true;
    }

    void RemoveCurrent()
    {
        cannonState.RemoveCurrent(this);
        inCurrent = false;

        //if (cameraTurn) newCameraBehaviour.TempXRecenter(); //ADD THIS BACK IN??
    }


    bool IsInsideCollider(Vector3 point)
    {
        Vector3 closestPoint = capsuleCollider.ClosestPoint(point);
        return (closestPoint - point).sqrMagnitude < Mathf.Epsilon;
    }


    IEnumerator EntranceCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        entranceCoroutine = null;
    }

}