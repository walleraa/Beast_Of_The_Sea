using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject[] gold_list;
    public float speed = 1f;

    private Rigidbody rb;
    private int index = 0;
    private GameObject target_gold;
    private bool defeat = false;
    private bool pillaging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target_gold = gold_list[index];
    }

    void Update()
    {
        // If the current target has been pillaged, move on to the next
        if (target_gold.GetComponent<MeshRenderer>().enabled == false)
        {
            FindNextGold();
        }

        // Move towards the gold unless the game is over or already pillaging
        if (!defeat && !pillaging)
        {
            rb.freezeRotation = false;
            GreedyMovement();
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            rb.freezeRotation = true;
        }
    }

    void FindNextGold()
    {
        Debug.Log("Advance");
        index += 1;

        // Move to the next pile of gold in the level
        if (gold_list.Length > index)
        {
            target_gold = gold_list[index];
        }
        // If there's no more gold, game over
        else
        {
            defeat = true;
        }
    }

    void GreedyMovement()
    {
        // Calculations from ChatGPT
        Transform target_location = target_gold.GetComponent<Transform>();
        Vector3 direction = (target_location.position - transform.position).normalized;
        rb.velocity = direction * speed;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void SetPillaging(bool pillage)
    {
        pillaging = pillage;
    }
}
