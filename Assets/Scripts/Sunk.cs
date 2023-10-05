using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunk : MonoBehaviour
{
    public float sinking_speed = 5f;
    public float sinking_duration = 4f;
    public float sinking_angle = 8f;

    private Rigidbody rb;
    private EnemyMovement enemy_movement_script;
    private BoxCollider box_collider;
    private bool sinking = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemy_movement_script = GetComponent<EnemyMovement>();
        box_collider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (sinking)
        {
            // Heading to the Locker
            rb.velocity = Vector3.down * sinking_speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sunk!");
        sinking = true;

        // Disable pirate movement
        enemy_movement_script.enabled = false;

        // Going down
        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, sinking_angle));
        
        // Disable collider in order to stop any pillaging
        box_collider.enabled = false;

        // Start the process of sinking
        StartCoroutine(Sinking());
    }

    IEnumerator Sinking()
    {
        yield return new WaitForSeconds(sinking_duration);

        Debug.Log("Sent to Davy Jones's Locker");
        Destroy(gameObject);
    }
}
