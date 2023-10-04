using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float bounce_speed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 back = transform.rotation * Vector3.back;
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 8)
        { 
            rb.velocity = back * bounce_speed;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 back = transform.rotation * Vector3.back;
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 8)
        {
            rb.velocity = back * bounce_speed;
        }
    }
}
