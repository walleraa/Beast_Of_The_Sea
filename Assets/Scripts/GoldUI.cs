using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    public int gold = 2;
    public int pirates = 1;

    Subscription<GoldPillagedEvent> gold_pillaged_event_subscription;

    void Start()
    {
        gold_pillaged_event_subscription = EventBus.Subscribe<GoldPillagedEvent>(_OnGoldPillagedEvent);
    }

    void _OnGoldPillagedEvent(GoldPillagedEvent e)
    {
        int gold_count = gold - e.new_gold;
        GetComponent<Text>().text = "Gold: " + gold_count;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gold_pillaged_event_subscription);
    }
}
