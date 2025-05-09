using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
    public float rotSpeed = 15.0f;
    [SerializeField] Vector3 momentum = Vector3.zero;
    //public float baseGravity

    Vector3 movement;
    Vector3 rotation;
    float momentumImpactOnRotation = 0.25f;
    bool bypassInputMovement;
    bool bypassInputRotation;
    bool addMomentumToRotation;

    PlayerState playerState;
    HandleInput handleInput;
    ScaleControl scaleControl;
    CharacterController charController;
    Transform playerObject;
    
    private void Start()
    {
        playerState = FindAnyObjectByType<PlayerState>();
        handleInput = FindAnyObjectByType<HandleInput>();
        scaleControl = FindAnyObjectByType<ScaleControl>();
        playerObject = GameObject.FindWithTag("Player").transform;
        charController = FindAnyObjectByType<CharacterController>();
    }


    private void Update()
    {
        if (GameState.Instance.IsPlayerInControl() && playerState.DoesStateReceiveInput())
        {
            if (!bypassInputMovement) SetMovementWithInput();
            if (!bypassInputRotation) SetRotationWithInput();

            AddMomentumToMovement();
            if (addMomentumToRotation) AddMomentumToRotation();

            //ScaleMovementWithGFX();

            ProcessMovement();
            ProcessRotationOnYOnly();//stops body from tipping
        }
    }

    public Vector3 GetMovement() => movement;


    public Vector3 GetMomentum() => momentum;
    public void SetMomentum(Vector3 momentum)
    {
        this.momentum = momentum;
        //Debug.Log("Current momentum = " +  this.momentum);
    }

   

    public bool BypassInputMovement(bool bypass) => bypassInputMovement = bypass;
    public bool BypassInputRotation(bool bypass) => bypassInputRotation = bypass;
    public bool AddMomentumToRotation(bool add) => addMomentumToRotation = add;
    void SetMovementWithInput() => movement = handleInput.GetHandledInput();
    void SetRotationWithInput() => rotation = handleInput.GetHandledInput();
    //void ScaleMomentumWithGFX()
    //{
    //    momentum *= scaleControl.GetStageGFXScale();
    //}
    void AddMomentumToMovement()
    {
        movement += momentum;
    }
    void AddMomentumToRotation()
    {
        rotation += (momentum * momentumImpactOnRotation);
    }

    void ScaleMovementWithGFX()
    {
        movement /= scaleControl.GetStageGFXScale();
    }

    void ProcessMovement()
    {
        movement *= Time.deltaTime;
        charController.Move(movement);
    }

    void ProcessRotationOnYOnly()
    {
        if (rotation != Vector3.zero)
        {
            Quaternion direction = Quaternion.LookRotation(rotation);
            Vector3 directionY = direction.eulerAngles;
            directionY = new Vector3(0, directionY.y, 0);
            direction = Quaternion.Euler(directionY);
            //
            playerObject.rotation = Quaternion.Lerp(playerObject.rotation, direction, rotSpeed * Time.deltaTime);
        }
    }
}
