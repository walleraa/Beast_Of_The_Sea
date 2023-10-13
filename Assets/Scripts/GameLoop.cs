using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public string current_level = "Level Template";
    public string next_level = "Level 1";
    public int level_gold = 2;
    public int level_pirates = 1;
    public AudioClip victory_music_clip;

    private Subscription<PirateSunkEvent> pirate_sunk_event_subscription;
    private Subscription<GoldPillagedEvent> gold_pillaged_event_subscription;
    private bool can_restart = false;
    private bool victory = false;
    private AudioSource audio_source;

    void Start()
    {
        pirate_sunk_event_subscription = EventBus.Subscribe<PirateSunkEvent>(_OnPirateSunkEvent);
        gold_pillaged_event_subscription = EventBus.Subscribe<GoldPillagedEvent>(_OnGoldPillagedEvent);
        audio_source = GetComponent<AudioSource>();
        if (!audio_source.isPlaying)
            audio_source.Play();
    }

    void _OnPirateSunkEvent(PirateSunkEvent e)
    {
        if (level_pirates - e.new_pirate == 0 && !can_restart)
        {
            victory = true;

            // Switch to victory music
            if (audio_source.isPlaying)
            {
                audio_source.Pause();
                audio_source.clip = victory_music_clip;
                audio_source.Play();

                // Don't want to loop the victory music
                audio_source.loop = false;
            }
        }
    }

    void _OnGoldPillagedEvent(GoldPillagedEvent e)
    {
        if (level_gold - e.new_gold == 0)
            can_restart = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && can_restart)
        {
            SceneManager.LoadScene(current_level);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && victory)
        {
            SceneManager.LoadScene(next_level);
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(pirate_sunk_event_subscription);
        EventBus.Unsubscribe(gold_pillaged_event_subscription);
    }
}