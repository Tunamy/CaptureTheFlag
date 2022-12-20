using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CocheControl : MonoBehaviour
{
    // La velocidad máxima del coche
    public float maxSpeed = 10.0f;

    // La aceleración del coche
    public float acceleration = 5.0f;

    // La fuerza de frenado del coche
    public float brakePower = 5.0f;

    // El ángulo máximo de giro del coche
    public float maxSteeringAngle = 45.0f;

    // La referencia al componente Rigidbody2D del coche
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // Acelera el coche hacia adelante cuando se presiona la flecha hacia arriba
        if (Input.GetKey(KeyCode.UpArrow))
        {
            float currentSpeed = rb.velocity.magnitude;
            if (currentSpeed < maxSpeed)
            {
                rb.AddForce(transform.up * acceleration);
            }
        }

        // Frena el coche cuando se suelta la flecha hacia arriba
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {

                rb.AddForce(-transform.up * brakePower);
        }

        // Gira el coche hacia la izquierda cuando se presiona la flecha hacia la izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.MoveRotation(rb.rotation - maxSteeringAngle);
        }

        // Gira el coche hacia la derecha cuando se presiona la flecha hacia la derecha
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.MoveRotation(rb.rotation + maxSteeringAngle);
        }
    }

}





