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
    private AudioSource audio_source;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemy_movement_script = GetComponent<EnemyMovement>();
        box_collider = GetComponent<BoxCollider>();
        audio_source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (sinking)
        {
            // Heading to the Locker
            rb.velocity = Vector3.zero;
            transform.position = transform.position + Vector3.down * .1f;
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

        // Play an explosion sound to emphasize the sinking
        audio_source.Play();

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
