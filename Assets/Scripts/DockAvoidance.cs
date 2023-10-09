using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockAvoidance : MonoBehaviour
{
    private float collision_limit = 15f;
    private float timer = 0f;
    private bool stuck = false;
    private float speed = 20f;

    private void Update()
    {
        if (timer >= collision_limit && stuck) {
            Debug.Log("Stuck");
            transform.Translate((Vector3.forward + Vector3.right) * speed * Time.deltaTime);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        // If colliding with dock
        if (collision.gameObject.layer == 10)
        {
            timer += 1;
            stuck = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Unstuck");
        timer = 0f;
        stuck = false;
    }
}
