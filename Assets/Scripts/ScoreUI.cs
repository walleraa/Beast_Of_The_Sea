using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Subscription<ScoreEvent> score_event_subscription;

    void Start()
    {
        score_event_subscription = EventBus.Subscribe<ScoreEvent>(_OnScoreUpdated);
    }

    void _OnScoreUpdated(ScoreEvent e)
    {
        GetComponent<Text>().text = "Gold: " + e.new_score + "\n" + "Pirates: ";
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(score_event_subscription);
    }
}
