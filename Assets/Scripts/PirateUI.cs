using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PirateUI : MonoBehaviour
{
    public int pirates = 1;

    Subscription<PirateSunkEvent> pirate_sunk_event_subscription;

    void Start()
    {
        pirate_sunk_event_subscription = EventBus.Subscribe<PirateSunkEvent>(_OnPirateSunkEvent);
    }

    void _OnPirateSunkEvent(PirateSunkEvent e)
    {
        int gold_count = pirates - e.new_pirate;
        GetComponent<Text>().text = "Pirates: " + gold_count;
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(pirate_sunk_event_subscription);
    }
}