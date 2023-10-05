using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFire : MonoBehaviour
{
    public GameObject cannonball_prefab;
    public float cooldown = 2f;
    public float cannonball_speed = 50f;
    public float cannonball_life = .5f;

    private bool cannons_locked = false;
    private bool on_cooldown = false;
    private SlowUp slow_up_script;

    private void Start()
    {
        slow_up_script = GetComponentInParent<SlowUp>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X");
            if (!cannons_locked && !on_cooldown)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        Vector3 bearing = transform.rotation * Vector3.forward + new Vector3(0f, 0f, 0f);
        // Instantiate the cannonball
        GameObject cannonball = Instantiate(cannonball_prefab, transform.position + bearing, new Quaternion(0, 0, 0, 0));

        // Shoot the cannonball
        cannonball.GetComponent<Rigidbody>().velocity = bearing * cannonball_speed;

        // Destroy the cannonball after some time
        Destroy(cannonball, cannonball_life);

        // Set cooldown
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        // Prevent from going down while on cooldown
        slow_up_script.SetControlsLocked(true);

        yield return new WaitForSeconds(cooldown);
        on_cooldown = false;

        // Enable going down after cooldown
        slow_up_script.SetControlsLocked(false);
    }

    // Using control lock instead of disabling component to prevent cooldown resets
    public void SetCannonLock(bool param)
    {
        cannons_locked = param;

        // After unlocking cannons, come off cooldown
        if (!cannons_locked)
            StartCoroutine(Cooldown());
        // When locking cannons, set the cooldown on
        else
            on_cooldown = true;
    }
}
