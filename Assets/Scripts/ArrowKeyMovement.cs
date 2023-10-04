using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeyMovement : MonoBehaviour
{
    public float acceleration = 1f;
    public float top_speed = 40f;
    public float deceleration = 1f;
    public float turn_speed = 30f;

    public float water_acceleration = 4f;
    public float water_speed = 70f;
    public float water_deceleration = 4f;
    public float water_turn_speed = 60f;

    private Rigidbody rb;
    private float current_speed;
    private bool controls_locked = false;
    private bool is_down = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        current_speed = rb.velocity.magnitude;
    }

    private void Update()
    {
        if (!controls_locked)
        {
            float horizontal_input = Input.GetAxis("Horizontal");
            float vertical_input = Input.GetAxis("Vertical");
            float rotation_amount = horizontal_input * turn_speed * Time.deltaTime;
            float water_rotation_amount = horizontal_input * water_turn_speed * Time.deltaTime;

            // Move the ship forward when holding forward
            if (vertical_input > 0)
            {
                if (!is_down)
                {
                    // Don't surpass top speed
                    if (current_speed < top_speed)
                    {
                        rb.AddRelativeForce(Vector3.forward * acceleration);
                    }
                }
                else
                {
                    // Don't surpass top water speed
                    if (current_speed < water_speed)
                    {
                        rb.AddRelativeForce(Vector3.forward * water_acceleration);
                    }
                }
            }

            // Add deceleration (water resistance?)
            if (rb.velocity.magnitude > 0f)
            {
                if (!is_down)
                    rb.velocity -= rb.velocity * deceleration * Time.deltaTime;
                else
                    rb.velocity -= rb.velocity * water_deceleration * Time.deltaTime;
            }

            // Only turn the ship when moving
            if (rb.velocity.magnitude > 0f)
            {
                rotation_amount *= rb.velocity.magnitude / top_speed;
                water_rotation_amount *= rb.velocity.magnitude / water_speed;

                // Turn the boat based on horizontal input and current speed
                // Calculations from ChatGPT
                if (!is_down)
                {
                    rb.rotation *= Quaternion.Euler(0f, rotation_amount, 0f);
                    rb.velocity = Quaternion.Euler(0f, rotation_amount, 0f) * rb.velocity;
                }
                else
                {
                    rb.rotation *= Quaternion.Euler(0f, water_rotation_amount, 0f);
                    rb.velocity = Quaternion.Euler(0f, water_rotation_amount, 0f) * rb.velocity;
                }
            }
        }
    }

    public void SetControlLock(bool controls)
    {
        controls_locked = controls;
    }

    public void SetIsDown(bool down)
    {
        is_down = down;
    }
}
