using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This keeps information selected from the Title Screen, thanks to this the Loader System nows wich level must build. 
/// </summary>
public class LoaderInfo
{
    public SceneConfiguration sceneConfiguration
    {
        get; set;
    }
    public int nextLevel
    {
        get; set;
    }
}