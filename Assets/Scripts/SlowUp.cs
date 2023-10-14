using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowUp : MonoBehaviour
{
    public float descend_speed = 10f;
    public float down_timer = 3.5f;
    public GameObject[] cannons;
    public float camera_init_angle = 18.85f;
    public float camera_down_angle = 0f;
    public AudioSource audio_source;
    public AudioClip bell_toll_clip;

    private Rigidbody rb;
    private bool is_down = false;
    private ArrowKeyMovement arrow_key_movement_script;
    private Bounce bounce_script;
    private bool controls_locked = false;
    private RigidbodyConstraints constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    private Transform main_camera_transform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrow_key_movement_script = GetComponent<ArrowKeyMovement>();
        bounce_script = GetComponent<Bounce>();
        main_camera_transform = gameObject.transform.GetChild(0);
    }

    private void Update()
    {
        if (!controls_locked)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (is_down)
                {
                    StartCoroutine(RiseUp());
                }
                else
                {
                    StartCoroutine(GoDown());
                }
            }
        }
    }

    IEnumerator GoDown()
    {
        Debug.Log("Go Down");
        Vector3 direction = transform.rotation * Vector3.forward + transform.rotation * Vector3.down;

        // Lock conrols
        controls_locked = true;
        arrow_key_movement_script.enabled = !controls_locked;
        rb.constraints = constraints;
        bounce_script.SetIsDescending(true);

        // Disable the rising tide
        GetComponent<RisingTide>().enabled = false;

        // Lock cannons to prevent them firing underwater
        for (int i = 0; i < cannons.Length; ++i)
        {
            cannons[i].GetComponent<OpenFire>().SetCannonLock(true);
        }

        // Set rotation and velocity to go down
        rb.velocity = direction * descend_speed;
        transform.rotation = Quaternion.LookRotation(direction);
        Debug.Log(rb.velocity);

        // Change audio
        audio_source.volume = .5f;

        yield return new WaitForSeconds(down_timer);

        // Set rotation and velocity to stablize
        direction = transform.rotation * Vector3.forward + transform.rotation * Vector3.up;
        rb.velocity = direction.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        // Unlock controls
        controls_locked = false;
        arrow_key_movement_script.enabled = !controls_locked;
        rb.constraints = constraints | RigidbodyConstraints.FreezePositionY;
        bounce_script.SetIsDescending(false);

        // Angle the camera up
        main_camera_transform.rotation = Quaternion.Euler(camera_down_angle, transform.eulerAngles.y, 0f);

        is_down = true;
        arrow_key_movement_script.SetIsDown(is_down);
    }

    IEnumerator RiseUp()
    {
        Debug.Log("Go Up");
        Vector3 direction = transform.rotation * Vector3.forward + transform.rotation * Vector3.up;

        // Lock conrols
        controls_locked = true;
        arrow_key_movement_script.enabled = !controls_locked;
        rb.constraints = constraints;
        bounce_script.SetIsAscending(true);

        // Angle the camera back down
        main_camera_transform.rotation = Quaternion.Euler(camera_init_angle, transform.eulerAngles.y, 0f);

        // Set rotation and velocity to go up
        rb.velocity = direction * descend_speed;
        transform.rotation = Quaternion.LookRotation(direction);

        yield return new WaitForSeconds(down_timer);

        // Change audio
        audio_source.volume = 1f;

        // Set rotation and velocity to stabilize
        direction = transform.rotation * Vector3.forward + transform.rotation * Vector3.down;
        rb.velocity = direction.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        // Unlock controls
        controls_locked = false;
        arrow_key_movement_script.enabled = !controls_locked;
        rb.constraints = constraints | RigidbodyConstraints.FreezePositionY;
        bounce_script.SetIsAscending(false);

        // Enable the rising tide
        GetComponent<RisingTide>().enabled = true;

        // Unlock cannons after rising up
        for (int i = 0; i < cannons.Length; ++i)
        {
            cannons[i].GetComponent<OpenFire>().SetCannonLock(false);
        }

        is_down = false;
        arrow_key_movement_script.SetIsDown(is_down);
    }

    // Using control lock instead of disabling component to avoid resetting is_down
    public void SetControlsLocked(bool param)
    {
        controls_locked = param;
    }
}
