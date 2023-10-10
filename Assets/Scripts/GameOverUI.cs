using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public int level_gold = 2;
    public int level_pirates = 1;
    public int current_level_num = 1;

    Subscription<PirateSunkEvent> pirate_sunk_event_subscription;
    Subscription<GoldPillagedEvent> gold_pillaged_event_subscription;
    private bool game_over = false;

    void Start()
    {
        pirate_sunk_event_subscription = EventBus.Subscribe<PirateSunkEvent>(_OnPirateSunkEvent);
        gold_pillaged_event_subscription = EventBus.Subscribe<GoldPillagedEvent>(_OnGoldPillagedEvent);
    }

    void _OnPirateSunkEvent(PirateSunkEvent e)
    {
        if (level_pirates - e.new_pirate == 0)
        {
            Debug.Log("Victory");
            if (!game_over)
            {
                if (current_level_num == 6)
                    GetComponent<TMPro.TextMeshProUGUI>().text = "You Win! To Davy Jones's Locker!";
                else
                    GetComponent<TMPro.TextMeshProUGUI>().text = "You Win!\nPress '1' to advance to level " + (current_level_num + 1) + "\nPress '0' to go to menu";
            }

            game_over = true;
        }
    }

    void _OnGoldPillagedEvent(GoldPillagedEvent e)
    {
        if (level_gold - e.new_gold == 0)
        {
            Debug.Log("Defeat");
            if (!game_over)
                GetComponent<TMPro.TextMeshProUGUI>().text = "You Lose! Gold Pillaged!\nPress '1' to restart level " + (current_level_num + 1) + "\nPress '0' to go to menu"; ;

            game_over = true;
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(pirate_sunk_event_subscription);
        EventBus.Unsubscribe(gold_pillaged_event_subscription);
    }
}
