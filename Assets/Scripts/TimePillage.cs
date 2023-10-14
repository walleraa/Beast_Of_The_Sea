using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePillage : MonoBehaviour
{
    public float pillage_counter = 5f;
    public bool debug = false;

    private float time_start = 0f;
    private float frames_per_second = 0f;
    private int pirates_pillaging = 0;
    private AudioSource audio_source;

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
            if (exists && debug)
            {
                Debug.Log("End Pillaging");
                exists = false;
                Debug.Log(time_start);
            }

            // Disable the renderer and collider
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (debug)
            Debug.Log("Start pillaging");

        pirates_pillaging += 1;
        UpdateTime();
    }

    // Keep checking if there's a pirate there
    private void OnCollisionStay(Collision collision)
    {
        UpdateTime();
    }

    // Sometimes the pirates slide off the collider
    private void OnCollisionExit(Collision collision)
    {
        if (debug)
            Debug.Log("Pause pillaging");

        pirates_pillaging -= 1;
    }

    private void UpdateTime()
    {
        time_start += 1 / (frames_per_second * pirates_pillaging);

        if (debug)
            Debug.Log(time_start);
    }
}
