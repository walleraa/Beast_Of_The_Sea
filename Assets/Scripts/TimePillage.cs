using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePillage : MonoBehaviour
{
    public float pillage_counter = 5f;

    private float time_start = 0f;
    private bool being_pillaged = false;

    private void Update()
    {
        // Disable the gold once it's been fully pillaged
        if (Time.time - time_start > pillage_counter && being_pillaged)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
        }
        // Reset knowledge on a pirate being there so if they disappear the pillaging stops
        else if (being_pillaged)
        {
            being_pillaged = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Prevent the counter from restarting when another pirate arrives
        if (!being_pillaged && collision.gameObject.layer == 7)
        {
            being_pillaged = true;
            time_start = Time.time;
        }
        else if (collision.gameObject.layer == 7)
        {
            being_pillaged = false;
        }
    }

    // Keep checking if there's a pirate there
    private void OnCollisionStay(Collision collision)
    {
        being_pillaged = true;
    }
}
