using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillage : MonoBehaviour
{
    public float pillage_counter = 5f;

    private float time_start = 0f;
    private bool pillaging = false;
    private EnemyMovement pirate;

    private void Start()
    {
        pirate = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        // Finished pillaging
        if (pillaging && Time.time - time_start > pillage_counter)
        {
            pillaging = false;
            pirate.SetPillaging(pillaging);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Collided with gold
        if (collision.gameObject.layer == 8)
        {
            pillaging = true;
            time_start = Time.time;
            pirate.SetPillaging(pillaging);
        }
    }
}
