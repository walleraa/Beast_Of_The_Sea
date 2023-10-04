using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounce_speed = 5f;
    public float descend_speed = -25f;
    public float ascend_speed = 25f;

    private Rigidbody rb;
    private bool is_descending = false;
    private bool is_ascending = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 back = transform.rotation * Vector3.back;

        // Prevent bug where player gets stuck and doesn't bounce
        if (back.x < 0 && back.x > -1)
            back.x -= 1;
        else if (back.x > 0 && back.x < 1)
            back.x += 1;
        if (back.z < 0 && back.z > -1)
            back.z -= 1;
        else if (back.z > 0 && back.z < 1)
            back.z += 1;

        // Set dedicated y velocity to prevent bounce interefering with down and rise
        if (is_descending)
            back.y = descend_speed/bounce_speed;
        else if (is_ascending)
            back.y = ascend_speed/bounce_speed;

        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 8)
        {
            // Fine I'll do an actual bounce
            //back.x *= -1;
            //back.z *= -1;

            rb.velocity = back * bounce_speed;
        }
    }

    public void SetIsDescending(bool param)
    {
        is_descending = param;
    }
    public void SetIsAscending(bool param)
    {
        is_ascending = param;
    }
}
