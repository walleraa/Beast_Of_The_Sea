using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGold : MonoBehaviour
{
    public static int gold = 5;

    void OnTriggerEnter(Collider other)
    {
        gold--;
        EventBus.Publish<GoldPillaged>(new GoldPillaged(gold));
    }
}

public class GoldPillaged
{
    public int new_gold = 0;
    public GoldPillaged(int _new_gold) { new_gold = _new_gold; }

    public override string ToString()
    {
        return "Gold: " + new_gold;
    }
}