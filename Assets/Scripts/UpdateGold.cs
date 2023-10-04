using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGold : MonoBehaviour
{
    public static int gold_taken = 0;

    private bool pillaged = false;

    void Update()
    {
        if (!GetComponent<MeshRenderer>().enabled && !pillaged)
        {
            ++gold_taken;
            pillaged = true;
            EventBus.Publish<GoldPillagedEvent>(new GoldPillagedEvent(gold_taken));
            //Debug.Log(gold);
        }
    }
}

public class GoldPillagedEvent
{
    public int new_gold = 0;
    public GoldPillagedEvent(int _new_gold) { new_gold = _new_gold; }

    public override string ToString()
    {
        return "new_gold: " + new_gold;
    }
}