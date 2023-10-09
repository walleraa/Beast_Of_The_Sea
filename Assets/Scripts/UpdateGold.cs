using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateGold : MonoBehaviour
{
    public static int gold_taken = 0;

    private bool pillaged = false;

    private void Start()
    {
        // From ChatGPT
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

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

    // From ChatGPT
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This method will be called when a new scene is loaded.
        Debug.Log("Scene loaded: " + scene.name);
        gold_taken = 0;
        // You can put your scene-specific code here.
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