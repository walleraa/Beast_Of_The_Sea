using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingTide : MonoBehaviour
{
    public float y_position = 4.25f;
    public float rise_time = 1f;

    private void Update()
    {
        // if below y_position
        if (transform.position.y < y_position)
        {
            // Calculate speed needed to rise in given time
            float speed = (y_position - transform.position.y) / rise_time;

            // Calculation from ChatGPT
            // Rise
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
