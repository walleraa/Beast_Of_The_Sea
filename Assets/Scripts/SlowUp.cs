using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowUp : MonoBehaviour
{
    public float descend_speed = 10f;
    public float down_timer = 3.5f;
    public GameObject[] cannons;

    private Rigidbody rb;
    private bool is_down = false;
    private ArrowKeyMovement arrow_key_movement_script;
    private Bounce bounce_script;
    private bool controls_locked = false;
    private RigidbodyConstraints constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrow_key_movement_script = GetComponent<ArrowKeyMovement>();
        bounce_script = GetComponent<Bounce>();
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

        // Set rotation and velocity to go up
        rb.velocity = direction * descend_speed;
        transform.rotation = Quaternion.LookRotation(direction);

        yield return new WaitForSeconds(down_timer);

        // Set rotation and velocity to stabilize
        direction = transform.rotation * Vector3.forward + transform.rotation * Vector3.down;
        rb.velocity = direction.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        // Unlock controls
        controls_locked = false;
        arrow_key_movement_script.enabled = !controls_locked;
        rb.constraints = constraints | RigidbodyConstraints.FreezePositionY;
        bounce_script.SetIsAscending(false);
        GetComponent<RisingTide>().enabled = true;

        // Unlock cannons after rising up=
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
