using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;



public class CocheControl : MonoBehaviour
{
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float driftFactor = 0.95f;
    public float friccion = 0.5f;
    public float velocidadMaxima = 20f;


    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    //Components
    Rigidbody2D carRigidbody2D;

    
   
    

    private void Awake()
    {
        
            carRigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    void FixedUpdate()
    {

        if (GetComponent<PhotonView>().IsMine)
        {
            ApplyEngineForce();
            ApplySteering();
            killorthogonalvelocity();

        }

    }

    void ApplyEngineForce() {

        if (carRigidbody2D.velocity.magnitude > velocidadMaxima)
            return;

        if(accelerationInput == 0) //friccion
        {
            carRigidbody2D.AddForce(-carRigidbody2D.velocity * friccion, ForceMode2D.Force);
        }

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput *accelerationFactor;

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);

    }
    void ApplySteering() { //giro

        float velocidadMinimaParaGirar = (carRigidbody2D.velocity.magnitude / 8);
        velocidadMinimaParaGirar = Mathf.Clamp01(velocidadMinimaParaGirar);

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * velocidadMinimaParaGirar;

        //Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void killorthogonalvelocity() 
    {

        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right* Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;

    }

    public void SetInputVector(Vector2 inputVector)  // llega la tecla que pulsamos
    {

        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;

    }



}





