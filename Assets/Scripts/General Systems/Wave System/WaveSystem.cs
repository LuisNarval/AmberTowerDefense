using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            EventBus.Publish(GameEvent.SPAWN);
        }
    }


}