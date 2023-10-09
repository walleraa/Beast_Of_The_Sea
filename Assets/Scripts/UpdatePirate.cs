using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdatePirate : MonoBehaviour
{
    public static int pirates_sunk = 0;

    private bool sunk = false;

    private void Start()
    {
        // From ChatGPT
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (!GetComponent<BoxCollider>().enabled && !sunk)
        {
            sunk = true;
            ++pirates_sunk;
            EventBus.Publish<PirateSunkEvent>(new PirateSunkEvent(pirates_sunk));
        }
    }

    // From ChatGPT
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This method will be called when a new scene is loaded.
        Debug.Log("Scene loaded: " + scene.name);
        pirates_sunk = 0;
        // You can put your scene-specific code here.
    }
}

public class PirateSunkEvent
{
    public int new_pirate = 0;
    public PirateSunkEvent(int _new_pirate) { new_pirate = _new_pirate; }

    public override string ToString()
    {
        return "new_pirate: " + new_pirate;
    }
}