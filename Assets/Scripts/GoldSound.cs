using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSound : MonoBehaviour
{
    private AudioSource audio_source;
    private Subscription<GoldPillagedEvent> gold_pillaged_event_subscription;

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
        gold_pillaged_event_subscription = EventBus.Subscribe<GoldPillagedEvent>(_OnGoldPillagedEvent);
    }

    private void _OnGoldPillagedEvent(GoldPillagedEvent e)
    {
        Debug.Log("Clink");
        audio_source.Play();
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gold_pillaged_event_subscription);
    }
}
