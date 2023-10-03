using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour
{
    public float acceleration = 1f;
    public float top_speed = 40f;
    public float deceleration = 1f;
    public float turn_speed = 30f;

    private Rigidbody rb;
    private float current_speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        current_speed = rb.velocity.magnitude;
    }

    private void Update()
    {
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");
        float rotation_amount = horizontal_input * turn_speed * Time.deltaTime;

        // Move the ship forward when holding forward
        if (vertical_input > 0)
        {
            // Don't surpass top speed
            if (current_speed < top_speed)
            {
                rb.AddRelativeForce(Vector3.forward * acceleration);
            }
        }

        // Slow the ship down when not moving forward
        if (rb.velocity.magnitude > 0f)
        {
            rb.velocity -= rb.velocity * deceleration * Time.deltaTime;
        }

        // Only turn the ship when moving
        if (rb.velocity.magnitude > 0f)
        {
            rotation_amount *= rb.velocity.magnitude / top_speed;

            // Turn the boat based on horizontal input and current speed
            rb.rotation *= Quaternion.Euler(0f, rotation_amount, 0f);
            rb.velocity = Quaternion.Euler(0f, rotation_amount, 0f) * rb.velocity;
        }
    }
}
