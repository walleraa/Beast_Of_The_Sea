using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowUp : MonoBehaviour
{
    public float descend_speed = 10f;
    public float down_timer = 3.5f;

    private Rigidbody rb;
    private bool is_down = false;
    private ArrowKeyMovement arrow_key_movement_script;
    private bool controls_locked = false;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        arrow_key_movement_script = GetComponent<ArrowKeyMovement>();
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
        arrow_key_movement_script.SetControlLock(controls_locked);

        // Set rotation and velocity to go down
        rb.velocity = direction * descend_speed;
        transform.rotation = Quaternion.LookRotation(direction);

        yield return new WaitForSeconds(down_timer);

        // Set rotation and velocity to stablize
        direction = transform.rotation * Vector3.forward + transform.rotation * Vector3.up;
        rb.velocity = direction.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        // Unlock controls
        controls_locked = false;
        arrow_key_movement_script.SetControlLock(controls_locked);

        is_down = true;
        arrow_key_movement_script.SetIsDown(is_down);
    }

    IEnumerator RiseUp()
    {
        Debug.Log("Go Up");
        Vector3 direction = transform.rotation * Vector3.forward + transform.rotation * Vector3.up;

        // Lock conrols
        controls_locked = true;
        arrow_key_movement_script.SetControlLock(controls_locked);

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
        arrow_key_movement_script.SetControlLock(controls_locked);

        is_down = false;
        arrow_key_movement_script.SetIsDown(is_down);
    }
}
