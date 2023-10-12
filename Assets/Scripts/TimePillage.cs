using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePillage : MonoBehaviour
{
    public float pillage_counter = 5f;

    private float time_start = 0f;
    private float frames_per_second = 0f;
    private int pirates_pillaging = 0;

    private bool exists = true;

    private void Start()
    {
        frames_per_second = 1 / Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // The pirates have spent enough time pillaging
        if (time_start >= pillage_counter)
        {
            // For debug purposes only
            if (exists)
            {
                Debug.Log("End Pillaging");
                exists = false;
            }

            // Disable the renderer and collider
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Start pillaging");
        pirates_pillaging += 1;
        time_start += 1 / (frames_per_second * pirates_pillaging);
    }

    // Keep checking if there's a pirate there
    private void OnCollisionStay(Collision collision)
    {
        //being_pillaged = true;
        time_start += 1 / (frames_per_second * pirates_pillaging);
    }

    // Sometimes the pirates slide off the collider
    private void OnCollisionExit(Collision collision)
    {
        pirates_pillaging -= 1;
    }
}
