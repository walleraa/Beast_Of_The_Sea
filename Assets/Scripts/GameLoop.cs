using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public string current_scene = "Level_Template";
    public int level_gold = 2;
    public int level_pirates = 1;

    Subscription<PirateSunkEvent> pirate_sunk_event_subscription;
    Subscription<GoldPillagedEvent> gold_pillaged_event_subscription;
    bool can_restart = false;

    void Start()
    {
        pirate_sunk_event_subscription = EventBus.Subscribe<PirateSunkEvent>(_OnPirateSunkEvent);
        gold_pillaged_event_subscription = EventBus.Subscribe<GoldPillagedEvent>(_OnGoldPillagedEvent);
    }

    void _OnPirateSunkEvent(PirateSunkEvent e)
    {
        if (level_pirates - e.new_pirate == 0)
            can_restart = true;
    }

    void _OnGoldPillagedEvent(GoldPillagedEvent e)
    {
        if (level_gold - e.new_gold == 0)
            can_restart = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) && can_restart)
        {
            SceneManager.LoadScene(current_scene);
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(pirate_sunk_event_subscription);
        EventBus.Unsubscribe(gold_pillaged_event_subscription);
    }
}
