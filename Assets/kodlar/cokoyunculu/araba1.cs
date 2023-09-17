using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class araba1 : MonoBehaviour
{

    [Header("araba ayarları")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;


    //locak variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;
    float velocityVsUp = 0;

    public GameObject bir;
    public GameObject iki;

    Rigidbody2D carRigidbody2D;
    

    private void Awake() {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(bir.activeSelf){driftFactor = 0;maxSpeed = 0; accelerationFactor = 0; turnFactor = 0;}
        if(iki.activeSelf){driftFactor = 0;maxSpeed = 0; accelerationFactor = 0; turnFactor = 0;}
    }

    private void FixedUpdate() 
    {
        if(GameManager.instance.GetGameStates()== GameStates.countdown){return;}

        ApplyEngineForce();   
        KillOrthogonelVelocity();
        ApplySteering(); 

    }

    void ApplyEngineForce(){

        velocityVsUp = Vector2.Dot(transform.up,carRigidbody2D.velocity);
        if(velocityVsUp > maxSpeed && accelerationInput > 0){return;}

        if(velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0){return;}

        if(carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0){return;}

        if(accelerationInput == 0){carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f,Time.deltaTime * 3);}
        else{carRigidbody2D.drag = 0;}

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        carRigidbody2D.AddForce(engineForceVector,ForceMode2D.Force);
    }

    void ApplySteering(){

        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonelVelocity(){
        Vector2 forwartVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity,transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity,transform.right);
    
        carRigidbody2D.velocity = forwartVelocity + rightVelocity * driftFactor;
    }

    public float GetLateralVelocity()
    {
        return Vector2.Dot(transform.right,carRigidbody2D.velocity);
    }

    public bool IsTireScreeching(out float lateralVelocity,out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if(accelerationInput < 0 && velocityVsUp >0)
        {
            isBraking = true;
            return true;
        }

        if(Mathf.Abs(GetLateralVelocity()) > 4.0f)
        {
            return true;
        }

        return false;

    }

    public void SetInputVector(Vector2 inputVector){
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    
    }

    

}
