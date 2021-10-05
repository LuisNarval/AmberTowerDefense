using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : Singleton<WaveSystem>
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            EventBus.Publish("Spawn");
        }
    }


}