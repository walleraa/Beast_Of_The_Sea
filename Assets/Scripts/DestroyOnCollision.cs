using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Delete cannonball if colliding with a border
        if (collision.gameObject.layer == 3)
        {
            Destroy(gameObject);
        }
    }
}
