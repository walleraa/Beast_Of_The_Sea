using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePillage : MonoBehaviour
{
    public float pillage_counter = 5f;

    private int pirates_pillaging = 0;
    private float time_start = 0f;

    private void Update()
    {
        //Disable the gold once it's been fully pillaged
        if (Time.time - time_start > pillage_counter && pirates_pillaging > 0)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<MeshCollider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Prevent the counter from restarting when another pirate arrives
        if (pirates_pillaging == 0 && collision.gameObject.layer == 7)
        {
            ++pirates_pillaging;
            time_start = Time.time;
        }
        else if (collision.gameObject.layer == 7)
        {
            ++pirates_pillaging;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            --pirates_pillaging;
            Debug.Log("Done");
            // If all pirates have been sunk, reset the pillage timer
            // Should do this on its own using pirates_pillaging

            //if (pirates_pillaging == 0)
            //{
            //    time_start = 0f;
            //}
        }
    }
}
