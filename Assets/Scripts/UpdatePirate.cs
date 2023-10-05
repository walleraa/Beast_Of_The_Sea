using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePirate : MonoBehaviour
{
    public static int pirates_sunk = 0;

    private bool sunk = false;

    void Update()
    {
        if (!GetComponent<BoxCollider>().enabled && !sunk)
        {
            sunk = true;
            ++pirates_sunk;
            EventBus.Publish<PirateSunkEvent>(new PirateSunkEvent(pirates_sunk));
        }
    }
}

public class PirateSunkEvent
{
    public int new_pirate = 0;
    public PirateSunkEvent(int _new_pirate) { new_pirate = _new_pirate; }

    public override string ToString()
    {
        return "new_pirate: " + new_pirate;
    }
}