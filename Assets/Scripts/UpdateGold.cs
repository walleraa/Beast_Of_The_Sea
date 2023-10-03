using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGold : MonoBehaviour
{
    public static int gold = 5;

    void OnCollisionEnter(Collision collision)
    {
        gold--;
        EventBus.Publish<GoldPillaged>(new GoldPillaged(gold));
        Debug.Log(gold);
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